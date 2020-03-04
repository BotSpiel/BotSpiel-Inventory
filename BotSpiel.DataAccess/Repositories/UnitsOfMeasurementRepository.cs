using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class UnitsOfMeasurementRepository : IUnitsOfMeasurementRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly UnitsOfMeasurementDB _context;
       private readonly FacilityAisleFacesDB _contextFacilityAisleFaces;
        private readonly HandlingUnitsDB _contextHandlingUnits;
        private readonly InventoryLocationsDB _contextInventoryLocations;
        private readonly InventoryLocationSizesDB _contextInventoryLocationSizes;
        private readonly MaterialHandlingUnitConfigurationsDB _contextMaterialHandlingUnitConfigurations;
        private readonly MaterialsDB _contextMaterials;
        private readonly UnitOfMeasurementConversionsDB _contextUnitOfMeasurementConversions;
  
        public UnitsOfMeasurementRepository(UnitsOfMeasurementDB context, FacilityAisleFacesDB contextFacilityAisleFaces, HandlingUnitsDB contextHandlingUnits, InventoryLocationsDB contextInventoryLocations, InventoryLocationSizesDB contextInventoryLocationSizes, MaterialHandlingUnitConfigurationsDB contextMaterialHandlingUnitConfigurations, MaterialsDB contextMaterials, UnitOfMeasurementConversionsDB contextUnitOfMeasurementConversions)
        {
            _context = context;
           _contextFacilityAisleFaces = contextFacilityAisleFaces;
            _contextHandlingUnits = contextHandlingUnits;
            _contextInventoryLocations = contextInventoryLocations;
            _contextInventoryLocationSizes = contextInventoryLocationSizes;
            _contextMaterialHandlingUnitConfigurations = contextMaterialHandlingUnitConfigurations;
            _contextMaterials = contextMaterials;
            _contextUnitOfMeasurementConversions = contextUnitOfMeasurementConversions;
  
        }

        public UnitsOfMeasurementPost GetPost(Int64 ixUnitOfMeasurement) => _context.UnitsOfMeasurementPost.AsNoTracking().Where(x => x.ixUnitOfMeasurement == ixUnitOfMeasurement).First();
         
		public UnitsOfMeasurement Get(Int64 ixUnitOfMeasurement)
        {
            UnitsOfMeasurement unitsofmeasurement = _context.UnitsOfMeasurement.AsNoTracking().Where(x => x.ixUnitOfMeasurement == ixUnitOfMeasurement).First();
            unitsofmeasurement.MeasurementSystems = _context.MeasurementSystems.Find(unitsofmeasurement.ixMeasurementSystem);
            unitsofmeasurement.MeasurementUnitsOf = _context.MeasurementUnitsOf.Find(unitsofmeasurement.ixMeasurementUnitOf);

            return unitsofmeasurement;
        }

        public IQueryable<UnitsOfMeasurement> Index()
        {
            var unitsofmeasurement = _context.UnitsOfMeasurement.Include(a => a.MeasurementUnitsOf).Include(a => a.MeasurementSystems).AsNoTracking(); 
            return unitsofmeasurement;
        }

        public IQueryable<UnitsOfMeasurement> IndexDb()
        {
            var unitsofmeasurement = _context.UnitsOfMeasurement.Include(a => a.MeasurementUnitsOf).Include(a => a.MeasurementSystems).AsNoTracking(); 
            return unitsofmeasurement;
        }
       public IQueryable<MeasurementSystems> selectMeasurementSystems()
        {
            List<MeasurementSystems> measurementsystems = new List<MeasurementSystems>();
            _context.MeasurementSystems.AsNoTracking()
                .ToList()
                .ForEach(x => measurementsystems.Add(x));
            return measurementsystems.AsQueryable();
        }
        public IQueryable<MeasurementUnitsOf> selectMeasurementUnitsOf()
        {
            List<MeasurementUnitsOf> measurementunitsof = new List<MeasurementUnitsOf>();
            _context.MeasurementUnitsOf.AsNoTracking()
                .ToList()
                .ForEach(x => measurementunitsof.Add(x));
            return measurementunitsof.AsQueryable();
        }
       public IQueryable<MeasurementSystems> MeasurementSystemsDb()
        {
            List<MeasurementSystems> measurementsystems = new List<MeasurementSystems>();
            _context.MeasurementSystems.AsNoTracking()
                .ToList()
                .ForEach(x => measurementsystems.Add(x));
            return measurementsystems.AsQueryable();
        }
        public IQueryable<MeasurementUnitsOf> MeasurementUnitsOfDb()
        {
            List<MeasurementUnitsOf> measurementunitsof = new List<MeasurementUnitsOf>();
            _context.MeasurementUnitsOf.AsNoTracking()
                .ToList()
                .ForEach(x => measurementunitsof.Add(x));
            return measurementunitsof.AsQueryable();
        }
        public bool VerifyUnitOfMeasurementUnique(Int64 ixUnitOfMeasurement, string sUnitOfMeasurement)
        {
            if (_context.UnitsOfMeasurement.AsNoTracking().Where(x => x.sUnitOfMeasurement == sUnitOfMeasurement).Any() && ixUnitOfMeasurement == 0L) return false;
            else if (_context.UnitsOfMeasurement.AsNoTracking().Where(x => x.sUnitOfMeasurement == sUnitOfMeasurement && x.ixUnitOfMeasurement != ixUnitOfMeasurement).Any() && ixUnitOfMeasurement != 0L) return false;
            else return true;
        }

        public List<string> VerifyUnitOfMeasurementDeleteOK(Int64 ixUnitOfMeasurement, string sUnitOfMeasurement)
        {
            List<string> existInEntities = new List<string>();
           if (_contextFacilityAisleFaces.FacilityAisleFaces.AsNoTracking().Where(x => x.ixXOffsetUnit == ixUnitOfMeasurement).Any()) existInEntities.Add("FacilityAisleFaces");
            if (_contextFacilityAisleFaces.FacilityAisleFaces.AsNoTracking().Where(x => x.ixYOffsetUnit == ixUnitOfMeasurement).Any()) existInEntities.Add("FacilityAisleFaces");
            if (_contextHandlingUnits.HandlingUnits.AsNoTracking().Where(x => x.ixHeightUnit == ixUnitOfMeasurement).Any()) existInEntities.Add("HandlingUnits");
            if (_contextHandlingUnits.HandlingUnits.AsNoTracking().Where(x => x.ixLengthUnit == ixUnitOfMeasurement).Any()) existInEntities.Add("HandlingUnits");
            if (_contextHandlingUnits.HandlingUnits.AsNoTracking().Where(x => x.ixWeightUnit == ixUnitOfMeasurement).Any()) existInEntities.Add("HandlingUnits");
            if (_contextHandlingUnits.HandlingUnits.AsNoTracking().Where(x => x.ixWidthUnit == ixUnitOfMeasurement).Any()) existInEntities.Add("HandlingUnits");
            if (_contextInventoryLocations.InventoryLocations.AsNoTracking().Where(x => x.ixXOffsetUnit == ixUnitOfMeasurement).Any()) existInEntities.Add("InventoryLocations");
            if (_contextInventoryLocations.InventoryLocations.AsNoTracking().Where(x => x.ixYOffsetUnit == ixUnitOfMeasurement).Any()) existInEntities.Add("InventoryLocations");
            if (_contextInventoryLocations.InventoryLocations.AsNoTracking().Where(x => x.ixZOffsetUnit == ixUnitOfMeasurement).Any()) existInEntities.Add("InventoryLocations");
            if (_contextInventoryLocationSizes.InventoryLocationSizes.AsNoTracking().Where(x => x.ixHeightUnit == ixUnitOfMeasurement).Any()) existInEntities.Add("InventoryLocationSizes");
            if (_contextInventoryLocationSizes.InventoryLocationSizes.AsNoTracking().Where(x => x.ixLengthUnit == ixUnitOfMeasurement).Any()) existInEntities.Add("InventoryLocationSizes");
            if (_contextInventoryLocationSizes.InventoryLocationSizes.AsNoTracking().Where(x => x.ixUsableVolumeUnit == ixUnitOfMeasurement).Any()) existInEntities.Add("InventoryLocationSizes");
            if (_contextInventoryLocationSizes.InventoryLocationSizes.AsNoTracking().Where(x => x.ixWidthUnit == ixUnitOfMeasurement).Any()) existInEntities.Add("InventoryLocationSizes");
            if (_contextMaterialHandlingUnitConfigurations.MaterialHandlingUnitConfigurations.AsNoTracking().Where(x => x.ixHeightUnit == ixUnitOfMeasurement).Any()) existInEntities.Add("MaterialHandlingUnitConfigurations");
            if (_contextMaterialHandlingUnitConfigurations.MaterialHandlingUnitConfigurations.AsNoTracking().Where(x => x.ixLengthUnit == ixUnitOfMeasurement).Any()) existInEntities.Add("MaterialHandlingUnitConfigurations");
            if (_contextMaterialHandlingUnitConfigurations.MaterialHandlingUnitConfigurations.AsNoTracking().Where(x => x.ixWidthUnit == ixUnitOfMeasurement).Any()) existInEntities.Add("MaterialHandlingUnitConfigurations");
            if (_contextMaterials.Materials.AsNoTracking().Where(x => x.ixBaseUnit == ixUnitOfMeasurement).Any()) existInEntities.Add("Materials");
            if (_contextMaterials.Materials.AsNoTracking().Where(x => x.ixDensityUnit == ixUnitOfMeasurement).Any()) existInEntities.Add("Materials");
            if (_contextMaterials.Materials.AsNoTracking().Where(x => x.ixHeightUnit == ixUnitOfMeasurement).Any()) existInEntities.Add("Materials");
            if (_contextMaterials.Materials.AsNoTracking().Where(x => x.ixLengthUnit == ixUnitOfMeasurement).Any()) existInEntities.Add("Materials");
            if (_contextMaterials.Materials.AsNoTracking().Where(x => x.ixShelflifeUnit == ixUnitOfMeasurement).Any()) existInEntities.Add("Materials");
            if (_contextMaterials.Materials.AsNoTracking().Where(x => x.ixWeightUnit == ixUnitOfMeasurement).Any()) existInEntities.Add("Materials");
            if (_contextMaterials.Materials.AsNoTracking().Where(x => x.ixWidthUnit == ixUnitOfMeasurement).Any()) existInEntities.Add("Materials");
            if (_contextUnitOfMeasurementConversions.UnitOfMeasurementConversions.AsNoTracking().Where(x => x.ixUnitOfMeasurementFrom == ixUnitOfMeasurement).Any()) existInEntities.Add("UnitOfMeasurementConversions");
            if (_contextUnitOfMeasurementConversions.UnitOfMeasurementConversions.AsNoTracking().Where(x => x.ixUnitOfMeasurementTo == ixUnitOfMeasurement).Any()) existInEntities.Add("UnitOfMeasurementConversions");

            return existInEntities;
        }


        public void RegisterCreate(UnitsOfMeasurementPost unitsofmeasurementPost)
		{
            _context.UnitsOfMeasurementPost.Add(unitsofmeasurementPost); 
        }

        public void RegisterEdit(UnitsOfMeasurementPost unitsofmeasurementPost)
        {
            _context.Entry(unitsofmeasurementPost).State = EntityState.Modified;
        }

        public void RegisterDelete(UnitsOfMeasurementPost unitsofmeasurementPost)
        {
            _context.UnitsOfMeasurementPost.Remove(unitsofmeasurementPost);
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
  

