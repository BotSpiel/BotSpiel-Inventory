using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class FacilityAisleFacesRepository : IFacilityAisleFacesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly FacilityAisleFacesDB _context;
       private readonly FacilityAisleFacesDB _contextFacilityAisleFaces;
        private readonly InventoryLocationsDB _contextInventoryLocations;
  
        public FacilityAisleFacesRepository(FacilityAisleFacesDB context, FacilityAisleFacesDB contextFacilityAisleFaces, InventoryLocationsDB contextInventoryLocations)
        {
            _context = context;
           _contextFacilityAisleFaces = contextFacilityAisleFaces;
            _contextInventoryLocations = contextInventoryLocations;
  
        }

        public FacilityAisleFacesPost GetPost(Int64 ixFacilityAisleFace) => _context.FacilityAisleFacesPost.AsNoTracking().Where(x => x.ixFacilityAisleFace == ixFacilityAisleFace).First();
         
		public FacilityAisleFaces Get(Int64 ixFacilityAisleFace)
        {
            FacilityAisleFaces facilityaislefaces = _context.FacilityAisleFaces.AsNoTracking().Where(x => x.ixFacilityAisleFace == ixFacilityAisleFace).First();
            facilityaislefaces.AisleFaceStorageTypes = _context.AisleFaceStorageTypes.Find(facilityaislefaces.ixAisleFaceStorageType);
            facilityaislefaces.BaySequenceTypes = _context.BaySequenceTypes.Find(facilityaislefaces.ixBaySequenceType);
            facilityaislefaces.Facilities = _context.Facilities.Find(facilityaislefaces.ixFacility);
            if (facilityaislefaces.ixPairedAisleFace != null)
        {
            facilityaislefaces.FacilityAisleFacesFKDiffPairedAisleFace = _context.FacilityAisleFaces.Find(facilityaislefaces.ixPairedAisleFace);
        }
            facilityaislefaces.FacilityFloors = _context.FacilityFloors.Find(facilityaislefaces.ixFacilityFloor);
            if (facilityaislefaces.ixDefaultFacilityZone != null)
        {
            facilityaislefaces.FacilityZonesFKDiffDefaultFacilityZone = _context.FacilityZones.Find(facilityaislefaces.ixDefaultFacilityZone);
        }
            facilityaislefaces.InventoryLocationSizesFKDiffDefaultInventoryLocationSize = _context.InventoryLocationSizes.Find(facilityaislefaces.ixDefaultInventoryLocationSize);
            if (facilityaislefaces.ixDefaultLocationFunction != null)
        {
            facilityaislefaces.LocationFunctionsFKDiffDefaultLocationFunction = _context.LocationFunctions.Find(facilityaislefaces.ixDefaultLocationFunction);
        }
            facilityaislefaces.LogicalOrientations = _context.LogicalOrientations.Find(facilityaislefaces.ixLogicalOrientation);
            if (facilityaislefaces.ixXOffsetUnit != null)
        {
            facilityaislefaces.UnitsOfMeasurementFKDiffXOffsetUnit = _context.UnitsOfMeasurement.Find(facilityaislefaces.ixXOffsetUnit);
        }
            if (facilityaislefaces.ixYOffsetUnit != null)
        {
            facilityaislefaces.UnitsOfMeasurementFKDiffYOffsetUnit = _context.UnitsOfMeasurement.Find(facilityaislefaces.ixYOffsetUnit);
        }

            return facilityaislefaces;
        }

        public IQueryable<FacilityAisleFaces> Index()
        {
            var facilityaislefaces = _context.FacilityAisleFaces.Include(a => a.FacilityFloors).Include(a => a.BaySequenceTypes).Include(a => a.LogicalOrientations).Include(a => a.AisleFaceStorageTypes).Include(a => a.InventoryLocationSizesFKDiffDefaultInventoryLocationSize).Include(a => a.Facilities).AsNoTracking(); 
            return facilityaislefaces;
        }

        public IQueryable<FacilityAisleFaces> IndexDb()
        {
            var facilityaislefaces = _context.FacilityAisleFaces.Include(a => a.FacilityFloors).Include(a => a.BaySequenceTypes).Include(a => a.LogicalOrientations).Include(a => a.AisleFaceStorageTypes).Include(a => a.InventoryLocationSizesFKDiffDefaultInventoryLocationSize).Include(a => a.Facilities).AsNoTracking(); 
            return facilityaislefaces;
        }
       public IQueryable<AisleFaceStorageTypes> selectAisleFaceStorageTypes()
        {
            List<AisleFaceStorageTypes> aislefacestoragetypes = new List<AisleFaceStorageTypes>();
            _context.AisleFaceStorageTypes.AsNoTracking()
                .ToList()
                .ForEach(x => aislefacestoragetypes.Add(x));
            return aislefacestoragetypes.AsQueryable();
        }
        public IQueryable<BaySequenceTypes> selectBaySequenceTypes()
        {
            List<BaySequenceTypes> baysequencetypes = new List<BaySequenceTypes>();
            _context.BaySequenceTypes.AsNoTracking()
                .ToList()
                .ForEach(x => baysequencetypes.Add(x));
            return baysequencetypes.AsQueryable();
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
            _context.FacilityAisleFaces.Include(a => a.AisleFaceStorageTypes).Include(a => a.BaySequenceTypes).Include(a => a.Facilities).Include(a => a.FacilityAisleFacesFKDiffPairedAisleFace).Include(a => a.FacilityFloors).Include(a => a.FacilityZonesFKDiffDefaultFacilityZone).Include(a => a.InventoryLocationSizesFKDiffDefaultInventoryLocationSize).Include(a => a.LocationFunctionsFKDiffDefaultLocationFunction).Include(a => a.LogicalOrientations).Include(a => a.UnitsOfMeasurementFKDiffXOffsetUnit).Include(a => a.UnitsOfMeasurementFKDiffYOffsetUnit).AsNoTracking()
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
            _context.LocationFunctions.AsNoTracking()
                .ToList()
                .ForEach(x => locationfunctions.Add(x));
            return locationfunctions.AsQueryable();
        }
        public IQueryable<LogicalOrientations> selectLogicalOrientations()
        {
            List<LogicalOrientations> logicalorientations = new List<LogicalOrientations>();
            _context.LogicalOrientations.AsNoTracking()
                .ToList()
                .ForEach(x => logicalorientations.Add(x));
            return logicalorientations.AsQueryable();
        }
        public IQueryable<UnitsOfMeasurement> selectUnitsOfMeasurement()
        {
            List<UnitsOfMeasurement> unitsofmeasurement = new List<UnitsOfMeasurement>();
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //_context.UnitsOfMeasurement.Include(a => a.MeasurementSystems).Include(a => a.MeasurementUnitsOf).AsNoTracking()
            //Replaced Code Block End
            _context.UnitsOfMeasurement.Include(a => a.MeasurementSystems).Include(a => a.MeasurementUnitsOf).AsNoTracking().Where(x => x.MeasurementUnitsOf.sMeasurementUnitOf == "Length").OrderBy(x => x.MeasurementSystems.ixMeasurementSystem).ThenBy(x => x.sUnitOfMeasurement)
            //Custom Code End                
                .ToList()
                .ForEach(x => unitsofmeasurement.Add(x));
            return unitsofmeasurement.AsQueryable();
        }
       public IQueryable<AisleFaceStorageTypes> AisleFaceStorageTypesDb()
        {
            List<AisleFaceStorageTypes> aislefacestoragetypes = new List<AisleFaceStorageTypes>();
            _context.AisleFaceStorageTypes.AsNoTracking()
                .ToList()
                .ForEach(x => aislefacestoragetypes.Add(x));
            return aislefacestoragetypes.AsQueryable();
        }
        public IQueryable<BaySequenceTypes> BaySequenceTypesDb()
        {
            List<BaySequenceTypes> baysequencetypes = new List<BaySequenceTypes>();
            _context.BaySequenceTypes.AsNoTracking()
                .ToList()
                .ForEach(x => baysequencetypes.Add(x));
            return baysequencetypes.AsQueryable();
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
            _context.FacilityAisleFaces.Include(a => a.AisleFaceStorageTypes).Include(a => a.BaySequenceTypes).Include(a => a.Facilities).Include(a => a.FacilityAisleFacesFKDiffPairedAisleFace).Include(a => a.FacilityFloors).Include(a => a.FacilityZonesFKDiffDefaultFacilityZone).Include(a => a.InventoryLocationSizesFKDiffDefaultInventoryLocationSize).Include(a => a.LocationFunctionsFKDiffDefaultLocationFunction).Include(a => a.LogicalOrientations).Include(a => a.UnitsOfMeasurementFKDiffXOffsetUnit).Include(a => a.UnitsOfMeasurementFKDiffYOffsetUnit).AsNoTracking()
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
        public IQueryable<LogicalOrientations> LogicalOrientationsDb()
        {
            List<LogicalOrientations> logicalorientations = new List<LogicalOrientations>();
            _context.LogicalOrientations.AsNoTracking()
                .ToList()
                .ForEach(x => logicalorientations.Add(x));
            return logicalorientations.AsQueryable();
        }
        public IQueryable<UnitsOfMeasurement> UnitsOfMeasurementDb()
        {
            List<UnitsOfMeasurement> unitsofmeasurement = new List<UnitsOfMeasurement>();
            _context.UnitsOfMeasurement.Include(a => a.MeasurementSystems).Include(a => a.MeasurementUnitsOf).AsNoTracking()
                .ToList()
                .ForEach(x => unitsofmeasurement.Add(x));
            return unitsofmeasurement.AsQueryable();
        }
       public List<KeyValuePair<Int64?, string>> selectFacilityAisleFacesNullable()
        {
            List<KeyValuePair<Int64?, string>> facilityaislefacesNullable = new List<KeyValuePair<Int64?, string>>();
            facilityaislefacesNullable.Add(new KeyValuePair<Int64?, string>(null, "None"));
            _context.FacilityAisleFaces
                .OrderBy(k => k.sFacilityAisleFace)
                .ToList()
                .ForEach(k => facilityaislefacesNullable.Add(new KeyValuePair<Int64?, string>(k.ixFacilityAisleFace, k.sFacilityAisleFace)));
            return facilityaislefacesNullable;
        }
        public List<KeyValuePair<Int64?, string>> selectFacilityZonesNullable()
        {
            List<KeyValuePair<Int64?, string>> facilityzonesNullable = new List<KeyValuePair<Int64?, string>>();
            facilityzonesNullable.Add(new KeyValuePair<Int64?, string>(null, "None"));
            _context.FacilityZones
                .OrderBy(k => k.sFacilityZone)
                .ToList()
                .ForEach(k => facilityzonesNullable.Add(new KeyValuePair<Int64?, string>(k.ixFacilityZone, k.sFacilityZone)));
            return facilityzonesNullable;
        }
        public List<KeyValuePair<Int64?, string>> selectLocationFunctionsNullable()
        {
            List<KeyValuePair<Int64?, string>> locationfunctionsNullable = new List<KeyValuePair<Int64?, string>>();
            locationfunctionsNullable.Add(new KeyValuePair<Int64?, string>(null, "None"));
            //Custom Code Start | Added Code Block 
            List<string> locationTypes = new List<string>(new string[] { "RC", "RV", "LD", "FP", "CN", "SH", "ST", "TR", "PE" });
            //Custom Code End

            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //_context.LocationFunctions
            //Replaced Code Block End
            _context.LocationFunctions.Join(locationTypes, lf => lf.sLocationFunctionCode, lt => lt.ToString(), (lf, lt) => lf).AsNoTracking()
                //Custom Code End
                .OrderBy(k => k.sLocationFunction)
                .ToList()
                .ForEach(k => locationfunctionsNullable.Add(new KeyValuePair<Int64?, string>(k.ixLocationFunction, k.sLocationFunction)));
            return locationfunctionsNullable;
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
        public bool VerifyFacilityAisleFaceUnique(Int64 ixFacilityAisleFace, string sFacilityAisleFace)
        {
            if (_context.FacilityAisleFaces.AsNoTracking().Where(x => x.sFacilityAisleFace == sFacilityAisleFace).Any() && ixFacilityAisleFace == 0L) return false;
            else if (_context.FacilityAisleFaces.AsNoTracking().Where(x => x.sFacilityAisleFace == sFacilityAisleFace && x.ixFacilityAisleFace != ixFacilityAisleFace).Any() && ixFacilityAisleFace != 0L) return false;
            else return true;
        }

        public List<string> VerifyFacilityAisleFaceDeleteOK(Int64 ixFacilityAisleFace, string sFacilityAisleFace)
        {
            List<string> existInEntities = new List<string>();
           if (_contextFacilityAisleFaces.FacilityAisleFaces.AsNoTracking().Where(x => x.ixPairedAisleFace == ixFacilityAisleFace).Any()) existInEntities.Add("FacilityAisleFaces");
            if (_contextInventoryLocations.InventoryLocations.AsNoTracking().Where(x => x.ixFacilityAisleFace == ixFacilityAisleFace).Any()) existInEntities.Add("InventoryLocations");

            return existInEntities;
        }


        public void RegisterCreate(FacilityAisleFacesPost facilityaislefacesPost)
		{
            _context.FacilityAisleFacesPost.Add(facilityaislefacesPost); 
        }

        public void RegisterEdit(FacilityAisleFacesPost facilityaislefacesPost)
        {
            _context.Entry(facilityaislefacesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(FacilityAisleFacesPost facilityaislefacesPost)
        {
            _context.FacilityAisleFacesPost.Remove(facilityaislefacesPost);
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
  

