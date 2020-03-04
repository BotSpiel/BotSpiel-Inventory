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
    public class PutAway
    {
        private readonly ILocationFunctionsService _locationfunctionsService;
        private readonly IInventoryUnitsService _inventoryunitsService;
        private readonly IInventoryLocationsSlottingService _inventorylocationsslottingService;
        private readonly VolumeAndWeight _volumeAndWeight;
        private readonly IInventoryLocationsService _inventorylocationsService;
        public PutAway(ILocationFunctionsService locationfunctionsService, IInventoryUnitsService inventoryunitsService, IInventoryLocationsSlottingService inventorylocationsslottingService, VolumeAndWeight volumeAndWeight, IInventoryLocationsService inventorylocationsService)
        {
            _locationfunctionsService = locationfunctionsService;
            _inventoryunitsService = inventoryunitsService;
            _inventorylocationsslottingService = inventorylocationsslottingService;
            _volumeAndWeight = volumeAndWeight;
            _inventorylocationsService = inventorylocationsService;
        }

        public Int64 getPutAwaySuggestion(Int64 ixHandlingUnit, Int64 ixCompany, Int64 ixFacility, Int64 ixInventoryLocationUser)
        {
            var inventoryLocationUserPost = _inventorylocationsService.GetPost(ixInventoryLocationUser);
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
                x.sLocationFunctionCode == "RV" ||
                x.sLocationFunctionCode == "LD" ||
                x.sLocationFunctionCode == "FP"
                ).Select(x => x.ixLocationFunction).ToList(); 

            //We implement Proximity in bay to slotted location THEN Location type THEN Closest + Defaults
            var inventoryUnitsOnHandlingUnit = _inventoryunitsService.IndexDb().Where(x => x.ixHandlingUnit == ixHandlingUnit).ToList();
            var materials = inventoryUnitsOnHandlingUnit.GroupBy(x => x.ixMaterial)
                            .Select(y =>
                            new
                            {
                                ixMaterial = y.Key,
                                nTotalBaseUnits = y.Sum(u => u.nBaseUnitQuantity)
                            }
                            ).ToList();
            var ixMaterialDominant = materials.OrderByDescending(x => x.nTotalBaseUnits).Select(x => x.ixMaterial).FirstOrDefault();
            var slottedLocations = _inventorylocationsslottingService.IndexDb().Where(x => x.ixMaterial == ixMaterialDominant && x.InventoryLocations.ixFacility == ixFacility).ToList();

            if (slottedLocations.Count() > 0) //We have slotted locations
            {
                var locationsslotted = _inventorylocationsService.IndexDbPost().Where(x => slottedLocations.Select(y => y.ixInventoryLocation).Contains(x.ixInventoryLocation)).ToList();
                //var slottedlocationsPost = locationsslotted.Select(l => _inventorylocationsService.GetPost(l.ixInventoryLocation)).ToList();

                putAwaySuggestions = locationsslotted.Where(x => _volumeAndWeight.handlingUnitWillFitLocation(ixHandlingUnit, inventoryUnitsOnHandlingUnit, x)).ToList();

                if (putAwaySuggestions.Count() > 0)
                {
                    //We now apply the defaults - we look for the location with the most inventory
                    var inventoryInSlottedLocations = _inventoryunitsService.IndexDbPost().Where(x => x.ixFacility == ixFacility && x.ixCompany == ixCompany && putAwaySuggestions.Select(s => s.ixInventoryLocation).Contains(x.ixInventoryLocation)).ToList();
                    if (inventoryInSlottedLocations.Count() > 0)
                    {
                        var ixInventoryLocationSuggestion = inventoryInSlottedLocations.GroupBy(x => x.ixInventoryLocation)
                            .Select(y =>
                            new
                            {
                                ixInventoryLocation = y.Key,
                                nTotalBaseUnits = y.Sum(u => u.nBaseUnitQuantity)
                            }
                            )
                            .OrderByDescending(z => z.nTotalBaseUnits)
                            .Select(s => s.ixInventoryLocation).FirstOrDefault();
                        return ixInventoryLocationSuggestion;
                    }
                    else
                    {
                        return putAwaySuggestions.Select(x => x.ixInventoryLocation).FirstOrDefault();
                    }

                }
                //else //Now we look for locations in the slotted locations bays closest to the slotted location (To be implemented)
                //{

                //}
            }

            //Now we attempt to consolidate the inventory
            var existingInventoryLocations = _inventoryunitsService.IndexDb().Where(x => x.ixFacility == ixFacility && x.ixCompany == ixCompany && x.ixMaterial == ixMaterialDominant && allowedLocationFunctions.Contains(x.InventoryLocations.ixLocationFunction)).ToList();
            var existingInventoryLocationsPost = existingInventoryLocations.Select(l => _inventorylocationsService.GetPost(l.ixInventoryLocation)).Distinct().ToList();
            putAwaySuggestions = existingInventoryLocationsPost.Where(x => _volumeAndWeight.handlingUnitWillFitLocation(ixHandlingUnit, inventoryUnitsOnHandlingUnit, x)).ToList();

            if (putAwaySuggestions.Count() > 0) //We have locations with existing inventory that fits.
            {
                //We now apply the defaults - we look for the location with the most inventory
                var existinginventoryInLocations = _inventoryunitsService.IndexDb().Where(x => putAwaySuggestions.Select(s => s.ixInventoryLocation).Contains(x.ixInventoryLocation)).ToList();
                if (existinginventoryInLocations.Count() > 0)
                {
                    var ixInventoryLocationSuggestion = existinginventoryInLocations.GroupBy(x => x.ixInventoryLocation)
                        .Select(y =>
                        new
                        {
                            ixInventoryLocation = y.Key,
                            nTotalBaseUnits = y.Sum(u => u.nBaseUnitQuantity)
                        }
                        )
                        .OrderByDescending(z => z.nTotalBaseUnits)
                        .Select(s => s.ixInventoryLocation).FirstOrDefault();
                    return ixInventoryLocationSuggestion;
                }
                else
                {
                    return putAwaySuggestions.Select(x => x.ixInventoryLocation).FirstOrDefault();
                }
            }

            //Now we look for empty locations by function (LD and RV) and proximity to the user (To be refined for performance)
            var userLocation = _inventorylocationsService.GetPost(ixInventoryLocationUser);

            var ldLocactionsInFacility = _inventorylocationsService.IndexDb().Where(x => x.ixFacility == ixFacility && x.LocationFunctions.sLocationFunctionCode == "LD").ToList();
            var ldLocactionsInFacilityWithInventory = _inventoryunitsService.IndexDb().Where(x => x.ixFacility == ixFacility && x.ixCompany == ixCompany && x.InventoryLocations.LocationFunctions.sLocationFunctionCode == "LD").ToList();
            var ldLocactionsInFacilityEmpty = ldLocactionsInFacility.Select(x => x.ixInventoryLocation).Except(ldLocactionsInFacilityWithInventory.Select(x => x.ixInventoryLocation)).ToList();
            var ldLocactionsInFacilityEmptyPost = ldLocactionsInFacilityEmpty.Select(l => _inventorylocationsService.GetPost(l)).Distinct().ToList();
            putAwaySuggestions = ldLocactionsInFacilityEmptyPost.Where(x => _volumeAndWeight.handlingUnitWillFitLocation(ixHandlingUnit, inventoryUnitsOnHandlingUnit, x)).ToList();

            if (putAwaySuggestions.Count() > 0) //We have LD empty locations that fits.
            {
                var ixInventoryLocationSuggestion = putAwaySuggestions.Select(x => new { x.ixInventoryLocation, nProximity = Math.Abs(x.nSequence - inventoryLocationUserPost.nSequence) }).OrderBy(x => x.nProximity).Select(x => x.ixInventoryLocation).FirstOrDefault();
                return ixInventoryLocationSuggestion;
            }

            var rvLocactionsInFacility = _inventorylocationsService.IndexDb().Where(x => x.ixFacility == ixFacility && x.LocationFunctions.sLocationFunctionCode == "RV").ToList();
            var rvLocactionsInFacilityWithInventory = _inventoryunitsService.IndexDb().Where(x => x.ixFacility == ixFacility && x.ixCompany == ixCompany && x.InventoryLocations.LocationFunctions.sLocationFunctionCode == "RV").ToList();
            var rvLocactionsInFacilityEmpty = rvLocactionsInFacility.Select(x => x.ixInventoryLocation).Except(ldLocactionsInFacilityWithInventory.Select(x => x.ixInventoryLocation)).ToList();
            var rvLocactionsInFacilityEmptyPost = rvLocactionsInFacilityEmpty.Select(l => _inventorylocationsService.GetPost(l)).Distinct().ToList();
            putAwaySuggestions = rvLocactionsInFacilityEmptyPost.Where(x => _volumeAndWeight.handlingUnitWillFitLocation(ixHandlingUnit, inventoryUnitsOnHandlingUnit, x)).ToList();

            if (putAwaySuggestions.Count() > 0) //We have RV empty locations that fits.
            {
                var ixInventoryLocationSuggestion = putAwaySuggestions.Select(x => new { x.ixInventoryLocation, nProximity = Math.Abs(x.nSequence - inventoryLocationUserPost.nSequence) }).OrderBy(x => x.nProximity).Select(x => x.ixInventoryLocation).FirstOrDefault();
                return ixInventoryLocationSuggestion;
            }

            return 0;

        }

    }
}
