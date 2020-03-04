using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;
using BotSpiel.DataAccess.Repositories;
using BotSpiel.DataAccess.Utilities;

namespace BotSpiel.Services.Utilities
{
    public class Picking
    {
        private readonly IPickBatchesService _pickbatchesService;
        private readonly IOutboundOrderLinesInventoryAllocationService _outboundorderlinesinventoryallocationService;
        private readonly IOutboundOrdersService _outboundordersService;
        private readonly IMoveQueuesService _movequeuesService;
        private readonly IOutboundOrderLinesService _outboundorderlinesService;
        private readonly CommonLookUps _commonLookUps;
        private readonly IInventoryUnitsService _inventoryunitsService;
        private readonly IInventoryLocationsService _inventorylocationsService;
        public Picking(
              IOutboundOrderLinesInventoryAllocationService outboundorderlinesinventoryallocationService
            , IOutboundOrdersService outboundordersService
            , IPickBatchesService pickbatchesService
            , IMoveQueuesService movequeuesService
            , IOutboundOrderLinesService outboundorderlinesService
            , CommonLookUps commonLookUps
            , IInventoryUnitsService inventoryunitsService
            , IInventoryLocationsService inventorylocationsService
            )
        {
            _outboundorderlinesinventoryallocationService = outboundorderlinesinventoryallocationService;
            _outboundordersService = outboundordersService;
            _pickbatchesService = pickbatchesService;
            _movequeuesService = movequeuesService;
            _outboundorderlinesService = outboundorderlinesService;
            _commonLookUps = commonLookUps;
            _inventoryunitsService = inventoryunitsService;
            _inventorylocationsService = inventorylocationsService;
        }

        public Tuple<Int64, double> getPickSuggestion(Int64 ixPickBatch, BotUserData _botUserData)
        {
            Tuple<Int64, double> pickSuggestion = new Tuple<long, double>(0, 0);
            var pickBatch = _pickbatchesService.Get(ixPickBatch);

            if (pickBatch.PickBatchTypes.sPickBatchType.Contains("Discrete order picking")) //Order based picking
            {
                //We pick order by order
                var ordersInBatch = _outboundordersService.IndexDb().Where(x => x.ixPickBatch == ixPickBatch).OrderBy(x => x.ixOutboundOrder).Select(x => new { ixFacility = x.ixFacility, ixCompany = x.ixCompany, ixOutboundOrder = x.ixOutboundOrder }).ToList();

                var orderLinesInBatch = _outboundorderlinesService.IndexDb().Select(x => new { ixOutboundOrder = x.ixOutboundOrder, ixOutboundOrderLine = x.ixOutboundOrderLine, ixMaterial = x.ixMaterial })
                                        //.Where(x => ordersInBatch.Contains(x.ixOutboundOrder)).OrderBy(x => x.ixOutboundOrder).ThenBy(x => x.ixOutboundOrderLine)
                                        .Join(ordersInBatch, ol => ol.ixOutboundOrder, ob => ob.ixOutboundOrder, (ol, ob) => new { Ob = ob, Ol = ol })
                                        .Select(lib => new { ixFacility = lib.Ob.ixFacility, ixCompany = lib.Ob.ixCompany, ixOutboundOrder = lib.Ol.ixOutboundOrder, ixOutboundOrderLine = lib.Ol.ixOutboundOrderLine, ixMaterial = lib.Ol.ixMaterial })
                                        .ToList();

                ordersInBatch.ForEach(o =>
                {
                    var orderLinesInOrder = orderLinesInBatch.Where(x => x.ixOutboundOrder == o.ixOutboundOrder).Select(x => x.ixOutboundOrderLine).ToList();

                    var openLinesToPick = _outboundorderlinesinventoryallocationService.IndexDb()
                                .Where(x => orderLinesInOrder.Contains(x.ixOutboundOrderLine) && x.nBaseUnitQuantityAllocated > x.nBaseUnitQuantityPicked)
                                .Join(orderLinesInBatch, op => op.ixOutboundOrderLine, ol => ol.ixOutboundOrderLine, (op, ol) => new { Op = op, Ol = ol })
                                .Select(x => new { ixOutboundOrder = x.Ol.ixOutboundOrder, ixOutboundOrderLine = x.Ol.ixOutboundOrderLine, ixMaterial = x.Ol.ixMaterial, nBaseUnitQuantityOpen = x.Op.nBaseUnitQuantityAllocated - x.Op.nBaseUnitQuantityPicked }).Distinct().ToList();

                    var inventoryUnitCandidates = _inventoryunitsService.IndexDb().Where(x =>
                                                                x.ixFacility == o.ixFacility &&
                                                                x.ixCompany == o.ixCompany &&
                                                                _commonLookUps.getPickAndPlaceLocationFunctions().Select(lf => lf.ixLocationFunction).Contains(x.InventoryLocations.ixLocationFunction) &&
                                                                _commonLookUps.getAvailableInventoryStates().Select(s => s.ixInventoryState).Contains(x.ixInventoryState)
                                                                )
                                                    .Join(openLinesToPick, iu => iu.ixMaterial, ol => ol.ixMaterial, (iu, ol) => new { Iu = iu, Ol = ol })
                                                    .Where(x => (x.Iu.nBaseUnitQuantity - x.Iu.nBaseUnitQuantityQueued) >= 0)
                                                    .ToList();

                    pickSuggestion = inventoryUnitCandidates.Select(x =>
                                            new
                                            {
                                                ixInventoryUnit = x.Iu.ixInventoryUnit,
                                                bQtyMatch = x.Ol.nBaseUnitQuantityOpen == x.Iu.nBaseUnitQuantity - x.Iu.nBaseUnitQuantityQueued ? true : false,
                                                bIsSingleIuHU = _inventoryunitsService.IndexDbPost().Where(iu => iu.ixHandlingUnit == x.Iu.ixHandlingUnit).Count() == 1 ? true : false,
                                                nLocationTypePreference =
                                                (
                                                    x.Iu.InventoryLocations.ixLocationFunction == _commonLookUps.getLocationFunctions().Where(f => f.sLocationFunctionCode == "FP").Select(f => f.ixLocationFunction).FirstOrDefault() ? 1 :
                                                    x.Iu.InventoryLocations.ixLocationFunction == _commonLookUps.getLocationFunctions().Where(f => f.sLocationFunctionCode == "LD").Select(f => f.ixLocationFunction).FirstOrDefault() ? 2 :
                                                    x.Iu.InventoryLocations.ixLocationFunction == _commonLookUps.getLocationFunctions().Where(f => f.sLocationFunctionCode == "LD").Select(f => f.ixLocationFunction).FirstOrDefault() ? 3 : 1000
                                                ),
                                                nDistance = Math.Abs(x.Iu.InventoryLocations.nSequence - _inventorylocationsService.GetPost(_botUserData.ixInventoryLocation).nSequence),
                                                nPickQtyDiff = (x.Iu.nBaseUnitQuantity - x.Iu.nBaseUnitQuantityQueued) - x.Ol.nBaseUnitQuantityOpen,
                                                nPickQty = (x.Iu.nBaseUnitQuantity - x.Iu.nBaseUnitQuantityQueued) - x.Ol.nBaseUnitQuantityOpen >= 0 ? x.Ol.nBaseUnitQuantityOpen : (x.Iu.nBaseUnitQuantity - x.Iu.nBaseUnitQuantityQueued)
                                            }
                                            )
                                            .OrderByDescending(i => i.bQtyMatch)
                                            .ThenByDescending(i => i.bIsSingleIuHU)
                                            .ThenBy(i => i.nLocationTypePreference)
                                            .ThenByDescending(i => i.nPickQtyDiff)
                                            .ThenBy(i => i.nDistance)
                                            .Select(ps => Tuple.Create(ps.ixInventoryUnit, ps.nPickQty)).FirstOrDefault();

                    if (pickSuggestion.Item1 > 0 && pickSuggestion.Item2 > 0)
                    {
                        return;
                    }

                }
                );
            }
            else
            {
                //We implement this later
            }

            return pickSuggestion;
        }

        public bool isHandlingUnitPick(InventoryUnits inventoryUnit, double nPickQty)
        {
            if (_inventoryunitsService.IndexDbPost().Where(x => x.ixHandlingUnit == inventoryUnit.ixHandlingUnit).Count() == 1  && inventoryUnit.nBaseUnitQuantity == nPickQty)
                return true;
            else
                return false;
        }

        public bool isCompleteInventoryUnitPick(InventoryUnits inventoryUnit, double nPickQty)
        {
            if (inventoryUnit.nBaseUnitQuantity == nPickQty)
                return true;
            else
                return false;
        }

        public List<Int64> getOrderLinesInBatchForMaterial(Int64 ixPickBatch, Int64 ixMaterial)
        {
            var ordersInBatch = _outboundordersService.IndexDb().Where(x => x.ixPickBatch == ixPickBatch).OrderBy(x => x.ixOutboundOrder).Select(x => new { ixFacility = x.ixFacility, ixCompany = x.ixCompany, ixOutboundOrder = x.ixOutboundOrder }).ToList();

            var orderLinesInBatch = _outboundorderlinesService.IndexDb().Where(x => x.ixMaterial == ixMaterial).Select(x => new { ixOutboundOrder = x.ixOutboundOrder, ixOutboundOrderLine = x.ixOutboundOrderLine, ixMaterial = x.ixMaterial })
                                    .Join(ordersInBatch, ol => ol.ixOutboundOrder, ob => ob.ixOutboundOrder, (ol, ob) => new { Ob = ob, Ol = ol })
                                    .Select(lib => lib.Ol.ixOutboundOrderLine).Distinct()
                                    .ToList();

            return orderLinesInBatch;
        }

        public List<Int64> getOrderLinesInBatch(Int64 ixPickBatch)
        {
            var ordersInBatch = _outboundordersService.IndexDb().Where(x => x.ixPickBatch == ixPickBatch).OrderBy(x => x.ixOutboundOrder).Select(x => new { ixFacility = x.ixFacility, ixCompany = x.ixCompany, ixOutboundOrder = x.ixOutboundOrder }).ToList();

            var orderLinesInBatch = _outboundorderlinesService.IndexDb().Select(x => new { ixOutboundOrder = x.ixOutboundOrder, ixOutboundOrderLine = x.ixOutboundOrderLine, ixMaterial = x.ixMaterial })
                                    .Join(ordersInBatch, ol => ol.ixOutboundOrder, ob => ob.ixOutboundOrder, (ol, ob) => new { Ob = ob, Ol = ol })
                                    .Select(lib => lib.Ol.ixOutboundOrderLine).Distinct()
                                    .ToList();

            return orderLinesInBatch;
        }


        public bool isPickBatchComplete(Int64 ixPickBatch)
        {
            var outboundorderlinesinventoryallocation = _outboundorderlinesinventoryallocationService.IndexDbPost().Where(x => (x.nBaseUnitQuantityAllocated - x.nBaseUnitQuantityPicked) > 0).ToList()
                .Join(getOrderLinesInBatch(ixPickBatch), ol => ol.ixOutboundOrderLine, olb => olb, (ol, olb) => new { Ol = ol, Olb = olb }).ToList();

            if (outboundorderlinesinventoryallocation.Any())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string getOrderAllocationStatus(Int64 ixOutboundOrder)
        {
            if (_outboundorderlinesService.IndexDb().Where(x => x.ixOutboundOrder == ixOutboundOrder).Any())
            {
                if (_outboundorderlinesService.IndexDb().Where(x => x.ixOutboundOrder == ixOutboundOrder && x.Statuses.sStatus.Contains("Active")).Any())
                {
                    return "Not Allocated";
                }
                else
                {
                    if (_outboundorderlinesService.IndexDb().Where(x => x.ixOutboundOrder == ixOutboundOrder && x.Statuses.sStatus.Contains("Partially Allocated")).Any())
                    {
                        return "Partially Allocated";
                    }
                    else
                    {
                        if (_outboundorderlinesService.IndexDb().Where(x => x.ixOutboundOrder == ixOutboundOrder && x.Statuses.sStatus.Contains("Allocated")).Any())
                        {
                            return "Allocated";
                        }
                        else
                        {
                            return "";
                        }
                    }

                }
            }
            else
            {
                return "No Lines";
            }
        }

        public string getPickBatchAllocationStatus(Int64 ixPickBatch)

        {
            List<string> orderAllocationStatuses = new List<string>();

            _outboundordersService.IndexDbPost().Where(x => x.ixPickBatch == ixPickBatch).Select(x => x.ixOutboundOrder).ToList()
                .ForEach(o => 
                {
                    orderAllocationStatuses.Add(getOrderAllocationStatus(o));
                }
                );

            if (orderAllocationStatuses.Where(s => s == "").Any())
            {
                return "";
            }
            else if (orderAllocationStatuses.Contains("Not Allocated"))
            {
                return "Not Allocated";
            }
            else if (orderAllocationStatuses.Contains("Partially Allocated"))
            {
                return "Partially Allocated";
            }
            else 
            {
                return "Allocated";
            }

        }

    }
}
