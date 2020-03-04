using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;
using BotSpiel.DataAccess.Repositories;

namespace BotSpiel.Services.Utilities
{
    public class Inventory
    {
        private readonly IInventoryUnitsRepository _inventoryunitsRepository;
        private readonly IOutboundOrderLinesInventoryAllocationRepository _outboundorderlinesinventoryallocationRepository;
        private readonly IOutboundOrderLinesRepository _outboundorderlinesRepository;
        private readonly IStatusesRepository _statusesRepository;
        private readonly IInventoryStatesRepository _inventorystatesRepository;
        private readonly IPickBatchesRepository _pickbatchesRepository;
        private readonly IOutboundOrdersRepository _outboundordersRepository;
        private readonly IOutboundOrderLinesService _outboundorderlinesService;
        private readonly IOutboundOrderLinesInventoryAllocationService _outboundorderlinesinventoryallocationService;

        public Inventory(IInventoryUnitsRepository inventoryunitsRepository
            , IOutboundOrderLinesInventoryAllocationRepository outboundorderlinesinventoryallocationRepository
            , IOutboundOrderLinesRepository outboundorderlinesRepository
            , IStatusesRepository statusesRepository
            , IInventoryStatesRepository inventorystatesRepository
            , IPickBatchesRepository pickbatchesRepository
            , IOutboundOrdersRepository outboundordersRepository
            , IOutboundOrderLinesService outboundorderlinesService
            , IOutboundOrderLinesInventoryAllocationService outboundorderlinesinventoryallocationService
            )
        {
            _inventoryunitsRepository = inventoryunitsRepository;
            _outboundorderlinesinventoryallocationRepository = outboundorderlinesinventoryallocationRepository;
            _outboundorderlinesRepository = outboundorderlinesRepository;
            _statusesRepository = statusesRepository;
            _inventorystatesRepository = inventorystatesRepository;
            _pickbatchesRepository = pickbatchesRepository;
            _outboundordersRepository = outboundordersRepository;
            _outboundorderlinesService = outboundorderlinesService;
            _outboundorderlinesinventoryallocationService = outboundorderlinesinventoryallocationService;
        }

        public void allocateBatch(Int64 ixPickBatch, String UserName)
        {
            //We check that the batch is not already allocated and activated - if so we do nothing
            var pickBatch = _pickbatchesRepository.Get(ixPickBatch);
            if (pickBatch.Statuses.sStatus == "Inactive")
            {
                //We get the orders associated with the batch
                var ordersInBatch = _outboundordersRepository.IndexDb().Where(x => x.ixPickBatch == ixPickBatch && x.Statuses.sStatus == "Active").Select(x => new { x.ixOutboundOrder, x.ixFacility, x.ixCompany });
                ordersInBatch.ToList().ForEach(x =>
                {
                    var linesInOrders = _outboundorderlinesRepository.IndexDb().Where(l => l.ixOutboundOrder == x.ixOutboundOrder && l.Statuses.sStatus == "Active").Select(l => new { l.ixOutboundOrderLine, l.ixMaterial, l.nBaseUnitQuantityOrdered });
                    linesInOrders
                    .ToList().ForEach(l =>
                        {
                            var qtyAvailableToAllocate = getQtyAvailable(x.ixFacility, x.ixCompany, l.ixMaterial) - getQtyAllocated(x.ixFacility, x.ixCompany, l.ixMaterial);
                            if (qtyAvailableToAllocate > 0)
                            {
                                OutboundOrderLinesInventoryAllocationPost outboundOrderLineInventoryAllocation = new OutboundOrderLinesInventoryAllocationPost();
                                outboundOrderLineInventoryAllocation.ixOutboundOrderLine = l.ixOutboundOrderLine;
                                var orderLine = _outboundorderlinesRepository.GetPost(l.ixOutboundOrderLine);
                                if (qtyAvailableToAllocate >= l.nBaseUnitQuantityOrdered)
                                {
                                    outboundOrderLineInventoryAllocation.nBaseUnitQuantityAllocated = l.nBaseUnitQuantityOrdered;
                                    orderLine.ixStatus = _statusesRepository.IndexDb().Where(s => s.sStatus == "Allocated").Select(s => s.ixStatus).FirstOrDefault();
                                    orderLine.UserName = UserName;
                                    _outboundorderlinesService.Edit(orderLine);
                                }
                                else
                                {
                                    outboundOrderLineInventoryAllocation.nBaseUnitQuantityAllocated = qtyAvailableToAllocate;
                                    orderLine.ixStatus = _statusesRepository.IndexDb().Where(s => s.sStatus == "Partially Allocated").Select(s => s.ixStatus).FirstOrDefault();
                                    orderLine.UserName = UserName;
                                    _outboundorderlinesService.Edit(orderLine);
                                }
                                outboundOrderLineInventoryAllocation.ixStatus = _statusesRepository.IndexDb().Where(s => s.sStatus == "Active").Select(s => s.ixStatus).FirstOrDefault();
                                outboundOrderLineInventoryAllocation.UserName = UserName;
                                _outboundorderlinesinventoryallocationService.Create(outboundOrderLineInventoryAllocation);
                            }
                        }
                        );
                }
                );

            }
        }

        public double getQtyAllocated(Int64 ixFacility, Int64 ixCompany, Int64 ixMaterial)
        {
            double qtyAllocated = 0;
            var ordersInFacilityCompany = _outboundorderlinesRepository.OutboundOrdersDb().Where(x =>
                x.ixFacility == ixFacility &&
                x.ixCompany == ixCompany &&
                x.ixStatus == _statusesRepository.IndexDb().Where(s => s.sStatus == "Active").Select(s => s.ixStatus).FirstOrDefault()
                ).Select(x => new { ixOutboundOrder = x.ixOutboundOrder });

            if (_outboundorderlinesRepository.IndexDb().Where(x => x.ixMaterial == ixMaterial && ((x.ixStatus == _statusesRepository.IndexDb().Where(s => s.sStatus == "Allocated").Select(s => s.ixStatus).FirstOrDefault()) || (x.ixStatus == _statusesRepository.IndexDb().Where(s => s.sStatus == "Partially Allocated").Select(s => s.ixStatus).FirstOrDefault()))).Select(x => new { x.ixOutboundOrder, x.ixOutboundOrderLine, x.ixMaterial })
                .Join(ordersInFacilityCompany, ol => ol.ixOutboundOrder, o => o.ixOutboundOrder, (ol, o) => new { Ol = ol, O = o })
                .Select(q => q.Ol.ixOutboundOrderLine).Any()
                )
            {

                var orderLines = _outboundorderlinesRepository.IndexDb().Where(x => x.ixMaterial == ixMaterial && ((x.ixStatus == _statusesRepository.IndexDb().Where(s => s.sStatus == "Allocated").Select(s => s.ixStatus).FirstOrDefault()) || (x.ixStatus == _statusesRepository.IndexDb().Where(s => s.sStatus == "Partially Allocated").Select(s => s.ixStatus).FirstOrDefault()))).Select(x => new { x.ixOutboundOrder, x.ixOutboundOrderLine, x.ixMaterial })
                .Join(ordersInFacilityCompany, ol => ol.ixOutboundOrder, o => o.ixOutboundOrder, (ol, o) => new { Ol = ol, O = o })
                .Select(q => new { ixOutboundOrderLine = q.Ol.ixOutboundOrderLine });

                if (
                    _outboundorderlinesinventoryallocationRepository.IndexDb().Where(a => a.ixStatus == _statusesRepository.IndexDb().Where(s => s.sStatus == "Active").Select(s => s.ixStatus).FirstOrDefault())
                    .Select(a => a.nBaseUnitQuantityAllocated).Any()
                    )
                {
                    qtyAllocated = _outboundorderlinesinventoryallocationRepository.IndexDb().Where(a => a.ixStatus == _statusesRepository.IndexDb().Where(s => s.sStatus == "Active").Select(s => s.ixStatus).FirstOrDefault())
                        .Join(orderLines, al => al.ixOutboundOrderLine, ol => ol.ixOutboundOrderLine, (al,ol) => new {Al = al, Ol = ol })
                        .Select(ol => ol.Al.nBaseUnitQuantityAllocated).Sum();

                    return qtyAllocated;
                }
                else
                {
                    return 0.0;
                }
            }
            else
            {
                return 0.0;
            }


        }

        public double getQtyAvailable(Int64 ixFacility, Int64 ixCompany, Int64 ixMaterial)
        {
            var availableStatuses = _inventorystatesRepository.IndexDb().Where(x => !x.sInventoryState.Contains("Unavailable")).Select(x => x.ixInventoryState);

            var qtyAvailable = _inventoryunitsRepository.IndexDb().Where(x =>
                x.ixFacility == ixFacility
                && x.ixCompany == ixCompany
                && x.ixMaterial == ixMaterial
                && availableStatuses.Contains(x.ixInventoryState)
                && x.ixStatus == _statusesRepository.IndexDb().Where(s => s.sStatus == "Active").Select(s => s.ixStatus).FirstOrDefault()
            ).Select(i => i.nBaseUnitQuantity).Sum();

            return qtyAvailable;
        }

        public double getQtyUnavailable(Int64 ixFacility, Int64 ixCompany, Int64 ixMaterial)
        {
            var availableStatuses = _inventorystatesRepository.IndexDb().Where(x => x.sInventoryState.Contains("Unavailable")).Select(x => x.ixInventoryState);

            var qtyUnavailable = _inventoryunitsRepository.IndexDb().Where(x =>
                x.ixFacility == ixFacility
                && x.ixCompany == ixCompany
                && x.ixMaterial == ixMaterial
                && availableStatuses.Contains(x.ixInventoryState)
                && x.ixStatus == _statusesRepository.IndexDb().Where(s => s.sStatus == "Active").Select(s => s.ixStatus).FirstOrDefault()
            ).Select(i => i.nBaseUnitQuantity).Sum();

            return qtyUnavailable;
        }

    }
}
