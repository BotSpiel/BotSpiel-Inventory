using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;
//Custom Code Start | Added Code Block
using BotSpiel.DataAccess.Utilities;
//Custom Code End

namespace BotSpiel.DataAccess.Repositories
{

    public class InventoryLocationsRepository : IInventoryLocationsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly InventoryLocationsDB _context;
       private readonly InventoryLocationsSlottingDB _contextInventoryLocationsSlotting;
        private readonly InventoryUnitsDB _contextInventoryUnits;
        private readonly InventoryUnitTransactionsDB _contextInventoryUnitTransactions;
        private readonly MoveQueuesDB _contextMoveQueues;
        private readonly OutboundCarrierManifestsDB _contextOutboundCarrierManifests;
        private readonly ReceivingDB _contextReceiving;
        //Custom Code Start | Added Code Block
        private readonly CommonlyUsedSelects _commonlyUsedSelects;
        //Custom Code End

        //Custom Code Start | Replaced Code Block
        //Replaced Code Block Start
        //public InventoryLocationsRepository(InventoryLocationsDB context, InventoryLocationsSlottingDB contextInventoryLocationsSlotting, InventoryUnitsDB contextInventoryUnits, InventoryUnitTransactionsDB contextInventoryUnitTransactions, MoveQueuesDB contextMoveQueues, OutboundCarrierManifestsDB contextOutboundCarrierManifests, ReceivingDB contextReceiving)
        //Replaced Code Block End
        public InventoryLocationsRepository(InventoryLocationsDB context, InventoryLocationsSlottingDB contextInventoryLocationsSlotting, InventoryUnitsDB contextInventoryUnits, InventoryUnitTransactionsDB contextInventoryUnitTransactions, MoveQueuesDB contextMoveQueues, OutboundCarrierManifestsDB contextOutboundCarrierManifests, ReceivingDB contextReceiving, CommonlyUsedSelects commonlyUsedSelects)
        //Custom Code End
        {
            _context = context;
           _contextInventoryLocationsSlotting = contextInventoryLocationsSlotting;
            _contextInventoryUnits = contextInventoryUnits;
            _contextInventoryUnitTransactions = contextInventoryUnitTransactions;
            _contextMoveQueues = contextMoveQueues;
            _contextOutboundCarrierManifests = contextOutboundCarrierManifests;
            _contextReceiving = contextReceiving;
            //Custom Code Start | Added Code Block
            _commonlyUsedSelects = commonlyUsedSelects;
            //Custom Code End
        }

        public InventoryLocationsPost GetPost(Int64 ixInventoryLocation) => _context.InventoryLocationsPost.AsNoTracking().Where(x => x.ixInventoryLocation == ixInventoryLocation).First();
         
		public InventoryLocations Get(Int64 ixInventoryLocation)
        {
            InventoryLocations inventorylocations = _context.InventoryLocations.AsNoTracking().Where(x => x.ixInventoryLocation == ixInventoryLocation).First();
            if (inventorylocations.ixCompany != null)
        {
            inventorylocations.Companies = _context.Companies.Find(inventorylocations.ixCompany);
        }
            inventorylocations.Facilities = _context.Facilities.Find(inventorylocations.ixFacility);
            inventorylocations.FacilityAisleFaces = _context.FacilityAisleFaces.Find(inventorylocations.ixFacilityAisleFace);
            inventorylocations.FacilityFloors = _context.FacilityFloors.Find(inventorylocations.ixFacilityFloor);
            inventorylocations.FacilityWorkAreas = _context.FacilityWorkAreas.Find(inventorylocations.ixFacilityWorkArea);
            inventorylocations.FacilityZones = _context.FacilityZones.Find(inventorylocations.ixFacilityZone);
            if (inventorylocations.ixInventoryLocationSize != null)
        {
            inventorylocations.InventoryLocationSizes = _context.InventoryLocationSizes.Find(inventorylocations.ixInventoryLocationSize);
        }
            inventorylocations.LocationFunctions = _context.LocationFunctions.Find(inventorylocations.ixLocationFunction);
            if (inventorylocations.ixXOffsetUnit != null)
        {
            inventorylocations.UnitsOfMeasurementFKDiffXOffsetUnit = _context.UnitsOfMeasurement.Find(inventorylocations.ixXOffsetUnit);
        }
            if (inventorylocations.ixYOffsetUnit != null)
        {
            inventorylocations.UnitsOfMeasurementFKDiffYOffsetUnit = _context.UnitsOfMeasurement.Find(inventorylocations.ixYOffsetUnit);
        }
            if (inventorylocations.ixZOffsetUnit != null)
        {
            inventorylocations.UnitsOfMeasurementFKDiffZOffsetUnit = _context.UnitsOfMeasurement.Find(inventorylocations.ixZOffsetUnit);
        }

            return inventorylocations;
        }

        public IQueryable<InventoryLocations> Index()
        {
            var inventorylocations = _context.InventoryLocations.Include(a => a.LocationFunctions).Include(a => a.FacilityFloors).Include(a => a.FacilityZones).Include(a => a.FacilityWorkAreas).Include(a => a.FacilityAisleFaces).Include(a => a.Facilities).AsNoTracking(); 
            return inventorylocations;
        }

        public IQueryable<InventoryLocations> IndexDb()
        {
            var inventorylocations = _context.InventoryLocations.Include(a => a.LocationFunctions).Include(a => a.FacilityFloors).Include(a => a.FacilityZones).Include(a => a.FacilityWorkAreas).Include(a => a.FacilityAisleFaces).Include(a => a.Facilities).AsNoTracking(); 
            return inventorylocations;
        }
        //Custom Code Start | Added Code Block 
        public IQueryable<InventoryLocationsPost> IndexDbPost()
        {
            var inventorylocations = _context.InventoryLocationsPost.AsNoTracking();
            return inventorylocations;
        }
        //Custom Code End

        public IQueryable<Companies> selectCompanies()
        {
            List<Companies> companies = new List<Companies>();
            _context.Companies.AsNoTracking()
                .ToList()
                .ForEach(x => companies.Add(x));
            return companies.AsQueryable();
        }
        public IQueryable<Facilities> selectFacilities()
        {
            List<Facilities> facilities = new List<Facilities>();
            _context.Facilities.Include(a => a.Addresses).AsNoTracking()
                .ToList()
                .ForEach(x => facilities.Add(x));
            return facilities.AsQueryable();
        }
        public IQueryable<FacilityAisleFaces> selectFacilityAisleFaces()
        {
            List<FacilityAisleFaces> facilityaislefaces = new List<FacilityAisleFaces>();
            _context.FacilityAisleFaces.Include(a => a.AisleFaceStorageTypes).Include(a => a.BaySequenceTypes).Include(a => a.FacilityAisleFacesFKDiffPairedAisleFace).Include(a => a.FacilityFloors).Include(a => a.FacilityZonesFKDiffDefaultFacilityZone).Include(a => a.InventoryLocationSizesFKDiffDefaultInventoryLocationSize).Include(a => a.LocationFunctionsFKDiffDefaultLocationFunction).Include(a => a.LogicalOrientations).Include(a => a.UnitsOfMeasurementFKDiffXOffsetUnit).Include(a => a.UnitsOfMeasurementFKDiffYOffsetUnit).AsNoTracking()
                .ToList()
                .ForEach(x => facilityaislefaces.Add(x));
            return facilityaislefaces.AsQueryable();
        }
        public IQueryable<FacilityFloors> selectFacilityFloors()
        {
            List<FacilityFloors> facilityfloors = new List<FacilityFloors>();
            _context.FacilityFloors.AsNoTracking()
                .ToList()
                .ForEach(x => facilityfloors.Add(x));
            return facilityfloors.AsQueryable();
        }
        public IQueryable<FacilityWorkAreas> selectFacilityWorkAreas()
        {
            List<FacilityWorkAreas> facilityworkareas = new List<FacilityWorkAreas>();
            _context.FacilityWorkAreas.AsNoTracking()
                .ToList()
                .ForEach(x => facilityworkareas.Add(x));
            return facilityworkareas.AsQueryable();
        }
        public IQueryable<FacilityZones> selectFacilityZones()
        {
            List<FacilityZones> facilityzones = new List<FacilityZones>();
            _context.FacilityZones.AsNoTracking()
                .ToList()
                .ForEach(x => facilityzones.Add(x));
            return facilityzones.AsQueryable();
        }
        public IQueryable<InventoryLocationSizes> selectInventoryLocationSizes()
        {
            List<InventoryLocationSizes> inventorylocationsizes = new List<InventoryLocationSizes>();
            _context.InventoryLocationSizes.Include(a => a.UnitsOfMeasurementFKDiffHeightUnit).Include(a => a.UnitsOfMeasurementFKDiffLengthUnit).Include(a => a.UnitsOfMeasurementFKDiffUsableVolumeUnit).Include(a => a.UnitsOfMeasurementFKDiffWidthUnit).AsNoTracking()
                .ToList()
                .ForEach(x => inventorylocationsizes.Add(x));
            return inventorylocationsizes.AsQueryable();
        }
        public IQueryable<LocationFunctions> selectLocationFunctions()
        {
            List<LocationFunctions> locationfunctions = new List<LocationFunctions>();
            //Custom Code Start | Added Code Block 
            List<string> locationTypes = new List<string>(new string[] { "RC", "RV", "LD", "FP", "CN", "SH", "ST", "TR", "PE" });
            //Custom Code End
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //_context.LocationFunctions.AsNoTracking()
            //Replaced Code Block End
            _context.LocationFunctions.Join(locationTypes, lf => lf.sLocationFunctionCode, lt => lt.ToString(), (lf, lt) => lf).AsNoTracking()            
            //Custom Code End
                .ToList()
                .ForEach(x => locationfunctions.Add(x));
            return locationfunctions.AsQueryable();
        }
        public IQueryable<UnitsOfMeasurement> selectUnitsOfMeasurement()
        {

            //Custom Code End
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //List<UnitsOfMeasurement> unitsofmeasurement = new List<UnitsOfMeasurement>();
            //_context.UnitsOfMeasurement.Include(a => a.MeasurementSystems).Include(a => a.MeasurementUnitsOf).AsNoTracking()
            //    .ToList()
            //    .ForEach(x => unitsofmeasurement.Add(x));
            //return unitsofmeasurement.AsQueryable();
            //Replaced Code Block End
            return _commonlyUsedSelects.selectUnitsOfMeasurementLength();
            //Custom Code End
        }
       public IQueryable<Companies> CompaniesDb()
        {
            List<Companies> companies = new List<Companies>();
            _context.Companies.AsNoTracking()
                .ToList()
                .ForEach(x => companies.Add(x));
            return companies.AsQueryable();
        }
        public IQueryable<Facilities> FacilitiesDb()
        {
            List<Facilities> facilities = new List<Facilities>();
            _context.Facilities.Include(a => a.Addresses).AsNoTracking()
                .ToList()
                .ForEach(x => facilities.Add(x));
            return facilities.AsQueryable();
        }
        public IQueryable<FacilityAisleFaces> FacilityAisleFacesDb()
        {
            List<FacilityAisleFaces> facilityaislefaces = new List<FacilityAisleFaces>();
            _context.FacilityAisleFaces.Include(a => a.AisleFaceStorageTypes).Include(a => a.BaySequenceTypes).Include(a => a.FacilityAisleFacesFKDiffPairedAisleFace).Include(a => a.FacilityFloors).Include(a => a.FacilityZonesFKDiffDefaultFacilityZone).Include(a => a.InventoryLocationSizesFKDiffDefaultInventoryLocationSize).Include(a => a.LocationFunctionsFKDiffDefaultLocationFunction).Include(a => a.LogicalOrientations).Include(a => a.UnitsOfMeasurementFKDiffXOffsetUnit).Include(a => a.UnitsOfMeasurementFKDiffYOffsetUnit).AsNoTracking()
                .ToList()
                .ForEach(x => facilityaislefaces.Add(x));
            return facilityaislefaces.AsQueryable();
        }
        public IQueryable<FacilityFloors> FacilityFloorsDb()
        {
            List<FacilityFloors> facilityfloors = new List<FacilityFloors>();
            _context.FacilityFloors.AsNoTracking()
                .ToList()
                .ForEach(x => facilityfloors.Add(x));
            return facilityfloors.AsQueryable();
        }
        public IQueryable<FacilityWorkAreas> FacilityWorkAreasDb()
        {
            List<FacilityWorkAreas> facilityworkareas = new List<FacilityWorkAreas>();
            _context.FacilityWorkAreas.AsNoTracking()
                .ToList()
                .ForEach(x => facilityworkareas.Add(x));
            return facilityworkareas.AsQueryable();
        }
        public IQueryable<FacilityZones> FacilityZonesDb()
        {
            List<FacilityZones> facilityzones = new List<FacilityZones>();
            _context.FacilityZones.AsNoTracking()
                .ToList()
                .ForEach(x => facilityzones.Add(x));
            return facilityzones.AsQueryable();
        }
        public IQueryable<InventoryLocationSizes> InventoryLocationSizesDb()
        {
            List<InventoryLocationSizes> inventorylocationsizes = new List<InventoryLocationSizes>();
            _context.InventoryLocationSizes.Include(a => a.UnitsOfMeasurementFKDiffHeightUnit).Include(a => a.UnitsOfMeasurementFKDiffLengthUnit).Include(a => a.UnitsOfMeasurementFKDiffUsableVolumeUnit).Include(a => a.UnitsOfMeasurementFKDiffWidthUnit).AsNoTracking()
                .ToList()
                .ForEach(x => inventorylocationsizes.Add(x));
            return inventorylocationsizes.AsQueryable();
        }
        public IQueryable<LocationFunctions> LocationFunctionsDb()
        {
            List<LocationFunctions> locationfunctions = new List<LocationFunctions>();
            _context.LocationFunctions.AsNoTracking()
                .ToList()
                .ForEach(x => locationfunctions.Add(x));
            return locationfunctions.AsQueryable();
        }
        public IQueryable<UnitsOfMeasurement> UnitsOfMeasurementDb()
        {
            List<UnitsOfMeasurement> unitsofmeasurement = new List<UnitsOfMeasurement>();
            _context.UnitsOfMeasurement.Include(a => a.MeasurementSystems).Include(a => a.MeasurementUnitsOf).AsNoTracking()
                .ToList()
                .ForEach(x => unitsofmeasurement.Add(x));
            return unitsofmeasurement.AsQueryable();
        }
       public List<KeyValuePair<Int64?, string>> selectCompaniesNullable()
        {
            List<KeyValuePair<Int64?, string>> companiesNullable = new List<KeyValuePair<Int64?, string>>();
            companiesNullable.Add(new KeyValuePair<Int64?, string>(null, "None"));
            _context.Companies
                .OrderBy(k => k.sCompany)
                .ToList()
                .ForEach(k => companiesNullable.Add(new KeyValuePair<Int64?, string>(k.ixCompany, k.sCompany)));
            return companiesNullable;
        }
        public List<KeyValuePair<Int64?, string>> selectInventoryLocationSizesNullable()
        {
            List<KeyValuePair<Int64?, string>> inventorylocationsizesNullable = new List<KeyValuePair<Int64?, string>>();
            inventorylocationsizesNullable.Add(new KeyValuePair<Int64?, string>(null, "None"));
            _context.InventoryLocationSizes
                .OrderBy(k => k.sInventoryLocationSize)
                .ToList()
                .ForEach(k => inventorylocationsizesNullable.Add(new KeyValuePair<Int64?, string>(k.ixInventoryLocationSize, k.sInventoryLocationSize)));
            return inventorylocationsizesNullable;
        }
        public List<KeyValuePair<Int64?, string>> selectUnitsOfMeasurementNullable()
        {
            List<KeyValuePair<Int64?, string>> unitsofmeasurementNullable = new List<KeyValuePair<Int64?, string>>();
            unitsofmeasurementNullable.Add(new KeyValuePair<Int64?, string>(null, "None"));
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //_context.UnitsOfMeasurement
            //Replaced Code Block End
            _context.UnitsOfMeasurement.Include(a => a.MeasurementUnitsOf).AsNoTracking().Where(x => x.MeasurementUnitsOf.sMeasurementUnitOf == "Length").OrderBy(x => x.MeasurementSystems.ixMeasurementSystem).ThenBy(x => x.sUnitOfMeasurement)
            //Custom Code End
                .OrderBy(k => k.sUnitOfMeasurement)
                .ToList()
                .ForEach(k => unitsofmeasurementNullable.Add(new KeyValuePair<Int64?, string>(k.ixUnitOfMeasurement, k.sUnitOfMeasurement)));
            return unitsofmeasurementNullable;
        }
        public bool VerifyInventoryLocationUnique(Int64 ixInventoryLocation, string sInventoryLocation)
        {
            if (_context.InventoryLocations.AsNoTracking().Where(x => x.sInventoryLocation == sInventoryLocation).Any() && ixInventoryLocation == 0L) return false;
            else if (_context.InventoryLocations.AsNoTracking().Where(x => x.sInventoryLocation == sInventoryLocation && x.ixInventoryLocation != ixInventoryLocation).Any() && ixInventoryLocation != 0L) return false;
            else return true;
        }

        public List<string> VerifyInventoryLocationDeleteOK(Int64 ixInventoryLocation, string sInventoryLocation)
        {
            List<string> existInEntities = new List<string>();
           if (_contextInventoryLocationsSlotting.InventoryLocationsSlotting.AsNoTracking().Where(x => x.ixInventoryLocation == ixInventoryLocation).Any()) existInEntities.Add("InventoryLocationsSlotting");
            if (_contextInventoryUnits.InventoryUnits.AsNoTracking().Where(x => x.ixInventoryLocation == ixInventoryLocation).Any()) existInEntities.Add("InventoryUnits");
            if (_contextInventoryUnitTransactions.InventoryUnitTransactions.AsNoTracking().Where(x => x.ixInventoryLocationAfter == ixInventoryLocation).Any()) existInEntities.Add("InventoryUnitTransactions");
            if (_contextInventoryUnitTransactions.InventoryUnitTransactions.AsNoTracking().Where(x => x.ixInventoryLocationBefore == ixInventoryLocation).Any()) existInEntities.Add("InventoryUnitTransactions");
            if (_contextMoveQueues.MoveQueues.AsNoTracking().Where(x => x.ixSourceInventoryLocation == ixInventoryLocation).Any()) existInEntities.Add("MoveQueues");
            if (_contextMoveQueues.MoveQueues.AsNoTracking().Where(x => x.ixTargetInventoryLocation == ixInventoryLocation).Any()) existInEntities.Add("MoveQueues");
            if (_contextOutboundCarrierManifests.OutboundCarrierManifests.AsNoTracking().Where(x => x.ixPickupInventoryLocation == ixInventoryLocation).Any()) existInEntities.Add("OutboundCarrierManifests");
            if (_contextReceiving.Receiving.AsNoTracking().Where(x => x.ixInventoryLocation == ixInventoryLocation).Any()) existInEntities.Add("Receiving");

            return existInEntities;
        }


        public void RegisterCreate(InventoryLocationsPost inventorylocationsPost)
		{
            _context.InventoryLocationsPost.Add(inventorylocationsPost); 
        }

        public void RegisterEdit(InventoryLocationsPost inventorylocationsPost)
        {
            _context.Entry(inventorylocationsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(InventoryLocationsPost inventorylocationsPost)
        {
            _context.InventoryLocationsPost.Remove(inventorylocationsPost);
        }

        public void Rollback()
        {
            _context.Rollback();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

    }
}
  

