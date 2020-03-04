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
    public class Shipping
    {

        private readonly ILocationFunctionsService _locationfunctionsService;
        private readonly IInventoryUnitsService _inventoryunitsService;
        private readonly IInventoryLocationsSlottingService _inventorylocationsslottingService;
        private readonly VolumeAndWeight _volumeAndWeight;
        private readonly IInventoryLocationsService _inventorylocationsService;
        private readonly CommonLookUps _commonLookUps;
        private readonly IOutboundCarrierManifestsService _outboundcarriermanifestsService;
        private readonly IOutboundOrdersRepository _outboundordersRepository;
        //private readonly IOutboundCarrierManifestPickupsService _outboundcarriermanifestpickupsService;
        private readonly IOutboundShipmentsService _outboundshipmentsService;
        private readonly IOutboundOrderLinesService _outboundorderlinesService;
        private readonly IOutboundOrderLinePackingService _outboundorderlinepackingService;
        private readonly IOutboundOrderLinesInventoryAllocationService _outboundorderlinesinventoryallocationService;
        private readonly IHandlingUnitsService _handlingunitsService;

        public Shipping(ILocationFunctionsService locationfunctionsService
            , IInventoryUnitsService inventoryunitsService
            , IInventoryLocationsSlottingService inventorylocationsslottingService
            , VolumeAndWeight volumeAndWeight
            , IInventoryLocationsService inventorylocationsService
            , CommonLookUps commonLookUps
            , IOutboundCarrierManifestsService outboundcarriermanifestsService
            , IOutboundOrdersRepository outboundordersRepository
            //, IOutboundCarrierManifestPickupsService outboundcarriermanifestpickupsService
            , IOutboundShipmentsService outboundshipmentsService
            , IOutboundOrderLinesService outboundorderlinesService
            , IOutboundOrderLinePackingService outboundorderlinepackingService
            , IOutboundOrderLinesInventoryAllocationService outboundorderlinesinventoryallocationService
            , IHandlingUnitsService handlingunitsService
            )
        {
            _locationfunctionsService = locationfunctionsService;
            _inventoryunitsService = inventoryunitsService;
            _inventorylocationsslottingService = inventorylocationsslottingService;
            _volumeAndWeight = volumeAndWeight;
            _inventorylocationsService = inventorylocationsService;
            _commonLookUps = commonLookUps;
            _outboundcarriermanifestsService = outboundcarriermanifestsService;
            _outboundordersRepository = outboundordersRepository;
            //_outboundcarriermanifestpickupsService = outboundcarriermanifestpickupsService;
            _outboundshipmentsService = outboundshipmentsService;
            _outboundorderlinesService = outboundorderlinesService;
            _outboundorderlinepackingService = outboundorderlinepackingService;
            _outboundorderlinesinventoryallocationService = outboundorderlinesinventoryallocationService;
            _handlingunitsService = handlingunitsService;
        }

        public Int64 getTrailerDoorSuggestion(Int64 ixFacility)
        {
            List<InventoryLocationsPost> putAwaySuggestions = new List<InventoryLocationsPost>();
            //Receiving RC
            //Reserve Storage RV
            //Let Down Storage    LD
            //Forward Pick Storage    FP
            //Consolidation   CN
            //Shipping    SH
            //Staging ST
            //Trailer Doors TR
            //Person PE
            var allowedLocationFunctions = _locationfunctionsService.IndexDb().Where(x =>
                x.sLocationFunctionCode == "CN" ||
                x.sLocationFunctionCode == "SH" ||
                x.sLocationFunctionCode == "ST" ||
                x.sLocationFunctionCode == "TR"
                ).Select(x => x.ixLocationFunction).ToList();

            //We implemenent a basic strategy we see if we a trailer door not currently assigned to a carrier manifest - if not we get the TR with the lowest sequence number

            var allocatedTrailerDoors = _outboundcarriermanifestsService.IndexDb().Where(x => x.ixStatus == _commonLookUps.getStatuses().Where(s => s.sStatus == "Active").Select(s => s.ixStatus).FirstOrDefault()).Select(x => x.ixPickupInventoryLocation).Distinct().ToList();

            if (_inventorylocationsService.IndexDb().Where(x =>
                        x.ixFacility == ixFacility &&
                        x.LocationFunctions.sLocationFunctionCode == "TR" &&
                        !allocatedTrailerDoors.Contains(x.ixInventoryLocation)
                        ).Any()
                )
            {
                return _inventorylocationsService.IndexDb().Where(x =>
                                        x.ixFacility == ixFacility &&
                                        x.LocationFunctions.sLocationFunctionCode == "TR" &&
                                        !allocatedTrailerDoors.Contains(x.ixInventoryLocation)
                                        ).Select(x => x.ixInventoryLocation).FirstOrDefault();

            }
            else
            {
                return _inventorylocationsService.IndexDb().Where(x =>
                                        x.ixFacility == ixFacility &&
                                        x.LocationFunctions.sLocationFunctionCode == "TR"
                                        ).Select(x => x.ixInventoryLocation).FirstOrDefault();
            }

        }

        public Int64 getDropLocationForPickBatch(Int64 ixPickBatch)
        {
            return _outboundordersRepository.IndexDb().Where(x => x.ixPickBatch == ixPickBatch).Select(x => x.OutboundShipments.OutboundCarrierManifests.ixPickupInventoryLocation).Distinct().FirstOrDefault() ?? 0;
        }


        public void shipInventoryForManifest(Int64 ixOutboundCarrierManifest, string UserName)
        {
            var ixStatusActive = _commonLookUps.getStatuses().Where(s => s.sStatus == "Active").Select(s => s.ixStatus).FirstOrDefault();
            var ixStatusInactive = _commonLookUps.getStatuses().Where(s => s.sStatus == "Inactive").Select(s => s.ixStatus).FirstOrDefault();
            var ixStatusComplete = _commonLookUps.getStatuses().Where(s => s.sStatus == "Complete").Select(s => s.ixStatus).FirstOrDefault();
            var ixInventoryUnitTransactionContext = _commonLookUps.getInventoryUnitTransactionContext().Where(c => c.sInventoryUnitTransactionContext == "Handling Units Shipping").Select(c => c.ixInventoryUnitTransactionContext).FirstOrDefault();

            var outboundshipments = _outboundshipmentsService.IndexDb().Where(sh => sh.ixOutboundCarrierManifest == ixOutboundCarrierManifest && sh.ixStatus == ixStatusActive)
            .Select(sh => sh.ixOutboundShipment).ToList();

            var outboundorders = _outboundordersRepository.IndexDb().Where(o => o.ixStatus == ixStatusActive)
            .Join(outboundshipments, o => o.ixOutboundShipment, sh => sh, (o, sh) => new { O = o, Sh = sh })
            .Select(x => x.O.ixOutboundOrder).ToList();

            var outboundorderlines = _outboundorderlinesService.IndexDb()
                .Join(outboundorders, ol => ol.ixOutboundOrder, o => o, (ol, o) => new { Ol = ol, O = o })
                .Select(x => x.Ol.ixOutboundOrderLine).ToList();

            var outboundorderlinesinventoryallocation = _outboundorderlinesinventoryallocationService.IndexDb()
                .Join(outboundorderlines, ola => ola.ixOutboundOrderLine, ol => ol, (ola, ol) => new { Ola = ola, Ol = ol })
                .Where(x => x.Ola.nBaseUnitQuantityPicked == x.Ola.nBaseUnitQuantityAllocated).Select(x => x.Ola.ixOutboundOrderLineInventoryAllocation).ToList();

            var outboundorderlinepacking = _outboundorderlinepackingService.IndexDb().Where(x => x.ixStatus == ixStatusActive)
                .Join(outboundorderlines, p => p.ixOutboundOrderLine, ol => ol, (p, ol) => new { P = p, Ol = ol })
                .Select(x => x.P.ixOutboundOrderLinePack).ToList();

            var outboundorderlinesshipped = _outboundorderlinepackingService.IndexDb().Where(x => x.ixStatus == ixStatusActive)
                .Join(outboundorderlines, p => p.ixOutboundOrderLine, ol => ol, (p, ol) => new { P = p, Ol = ol })
                .Select(x => new { ixOutboundOrderLine = x.P.ixOutboundOrderLine, nBaseUnitQuantityPacked = x.P.nBaseUnitQuantityPacked }).ToList();

            var handlingunitsToShip = _outboundorderlinepackingService.IndexDb().Where(x => x.ixStatus == ixStatusActive)
                .Join(outboundorderlines, p => p.ixOutboundOrderLine, ol => ol, (p, ol) => new { P = p, Ol = ol })
                .Select(x => x.P.ixHandlingUnit).Distinct().ToList();

            var inventoryunitsToShip = _inventoryunitsService.IndexDbPost().Where(x => x.nBaseUnitQuantity > 0)
                .Join(handlingunitsToShip, iu => iu.ixHandlingUnit, hu => hu, (iu, hu) => new { Iu = iu, Hu = hu })
                .Select(x => x.Iu.ixInventoryUnit).Distinct().ToList();

            inventoryunitsToShip.ForEach(x =>
                {
                    var inventoryunit = _inventoryunitsService.GetPost(x);
                    inventoryunit.nBaseUnitQuantity = 0;
                    inventoryunit.ixStatus = ixStatusInactive;
                    inventoryunit.UserName = UserName;
                    _inventoryunitsService.Edit(inventoryunit, ixInventoryUnitTransactionContext);
                }
                );

            handlingunitsToShip.ForEach(x =>
            {
                var handlingunit = _handlingunitsService.GetPost(x);
                handlingunit.ixStatus = ixStatusInactive;
                handlingunit.UserName = UserName;
                _handlingunitsService.Edit(handlingunit);
            }
            );

            //Now we set the statuses to complete
            var outboundcarriermanifest = _outboundcarriermanifestsService.GetPost(ixOutboundCarrierManifest);
            outboundcarriermanifest.ixStatus = ixStatusComplete;
            outboundcarriermanifest.UserName = UserName;
            _outboundcarriermanifestsService.Edit(outboundcarriermanifest);

            outboundshipments.ForEach(x =>
            {
                var outboundshipment = _outboundshipmentsService.GetPost(x);
                outboundshipment.ixStatus = ixStatusComplete;
                outboundshipment.UserName = UserName;
                _outboundshipmentsService.Edit(outboundshipment);
            }
            );

            outboundorders.ForEach(x =>
            {
                var outboundorder = _outboundordersRepository.GetPost(x);
                outboundorder.ixStatus = ixStatusComplete;
                outboundorder.UserName = UserName;
                _outboundordersRepository.RegisterEdit(outboundorder);
                _outboundordersRepository.Commit();
            }
            );

            outboundorderlines
                .Join(outboundorderlinesshipped, ol => ol, ols => ols.ixOutboundOrderLine, (ol, ols) => new {OL= ol, Ols = ols })
                .Select(o => new { ixOutboundOrderLine = o.Ols.ixOutboundOrderLine, nBaseUnitQuantityPacked = o.Ols.nBaseUnitQuantityPacked } ).ToList()
                .ForEach(x =>
                {
                    var outboundorderline = _outboundorderlinesService.GetPost(x.ixOutboundOrderLine);
                    outboundorderline.nBaseUnitQuantityShipped += x.nBaseUnitQuantityPacked;
                    outboundorderline.ixStatus = ixStatusComplete;
                    outboundorderline.UserName = UserName;
                    _outboundorderlinesService.Edit(outboundorderline);
                }
                );

            outboundorderlinesinventoryallocation.ForEach(x =>
            {
                var outboundorderlineinventoryallocation = _outboundorderlinesinventoryallocationService.GetPost(x);
                outboundorderlineinventoryallocation.ixStatus = ixStatusComplete;
                outboundorderlineinventoryallocation.UserName = UserName;
                _outboundorderlinesinventoryallocationService.Edit(outboundorderlineinventoryallocation);
            }
            );

            outboundorderlinepacking.ForEach(x =>
                {
                    var outboundorderlinepack = _outboundorderlinepackingService.GetPost(x);
                    outboundorderlinepack.ixStatus = ixStatusComplete;
                    outboundorderlinepack.UserName = UserName;
                    _outboundorderlinepackingService.Edit(outboundorderlinepack);
                }
                );

        }

    }
}
