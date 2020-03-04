using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class MaterialHandlingUnitConfigurationsRepository : IMaterialHandlingUnitConfigurationsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly MaterialHandlingUnitConfigurationsDB _context;
       private readonly HandlingUnitsDB _contextHandlingUnits;
        private readonly InboundOrderLinesDB _contextInboundOrderLines;
        private readonly ReceivingDB _contextReceiving;
  
        public MaterialHandlingUnitConfigurationsRepository(MaterialHandlingUnitConfigurationsDB context, HandlingUnitsDB contextHandlingUnits, InboundOrderLinesDB contextInboundOrderLines, ReceivingDB contextReceiving)
        {
            _context = context;
           _contextHandlingUnits = contextHandlingUnits;
            _contextInboundOrderLines = contextInboundOrderLines;
            _contextReceiving = contextReceiving;
  
        }

        public MaterialHandlingUnitConfigurationsPost GetPost(Int64 ixMaterialHandlingUnitConfiguration) => _context.MaterialHandlingUnitConfigurationsPost.AsNoTracking().Where(x => x.ixMaterialHandlingUnitConfiguration == ixMaterialHandlingUnitConfiguration).First();
         
		public MaterialHandlingUnitConfigurations Get(Int64 ixMaterialHandlingUnitConfiguration)
        {
            MaterialHandlingUnitConfigurations materialhandlingunitconfigurations = _context.MaterialHandlingUnitConfigurations.AsNoTracking().Where(x => x.ixMaterialHandlingUnitConfiguration == ixMaterialHandlingUnitConfiguration).First();
            materialhandlingunitconfigurations.HandlingUnitTypes = _context.HandlingUnitTypes.Find(materialhandlingunitconfigurations.ixHandlingUnitType);
            materialhandlingunitconfigurations.Materials = _context.Materials.Find(materialhandlingunitconfigurations.ixMaterial);
            if (materialhandlingunitconfigurations.ixHeightUnit != null)
        {
            materialhandlingunitconfigurations.UnitsOfMeasurementFKDiffHeightUnit = _context.UnitsOfMeasurement.Find(materialhandlingunitconfigurations.ixHeightUnit);
        }
            if (materialhandlingunitconfigurations.ixLengthUnit != null)
        {
            materialhandlingunitconfigurations.UnitsOfMeasurementFKDiffLengthUnit = _context.UnitsOfMeasurement.Find(materialhandlingunitconfigurations.ixLengthUnit);
        }
            if (materialhandlingunitconfigurations.ixWidthUnit != null)
        {
            materialhandlingunitconfigurations.UnitsOfMeasurementFKDiffWidthUnit = _context.UnitsOfMeasurement.Find(materialhandlingunitconfigurations.ixWidthUnit);
        }

            return materialhandlingunitconfigurations;
        }

        public IQueryable<MaterialHandlingUnitConfigurations> Index()
        {
            var materialhandlingunitconfigurations = _context.MaterialHandlingUnitConfigurations.Include(a => a.Materials).Include(a => a.HandlingUnitTypes).AsNoTracking(); 
            return materialhandlingunitconfigurations;
        }

        public IQueryable<MaterialHandlingUnitConfigurations> IndexDb()
        {
            var materialhandlingunitconfigurations = _context.MaterialHandlingUnitConfigurations.Include(a => a.Materials).Include(a => a.HandlingUnitTypes).AsNoTracking(); 
            return materialhandlingunitconfigurations;
        }
       public IQueryable<HandlingUnitTypes> selectHandlingUnitTypes()
        {
            List<HandlingUnitTypes> handlingunittypes = new List<HandlingUnitTypes>();
            _context.HandlingUnitTypes.AsNoTracking()
                .ToList()
                .ForEach(x => handlingunittypes.Add(x));
            return handlingunittypes.AsQueryable();
        }
        public IQueryable<Materials> selectMaterials()
        {
            List<Materials> materials = new List<Materials>();
            _context.Materials.Include(a => a.MaterialTypes).Include(a => a.UnitsOfMeasurementFKDiffBaseUnit).Include(a => a.UnitsOfMeasurementFKDiffDensityUnit).Include(a => a.UnitsOfMeasurementFKDiffHeightUnit).Include(a => a.UnitsOfMeasurementFKDiffLengthUnit).Include(a => a.UnitsOfMeasurementFKDiffShelflifeUnit).Include(a => a.UnitsOfMeasurementFKDiffWeightUnit).Include(a => a.UnitsOfMeasurementFKDiffWidthUnit).AsNoTracking()
                .ToList()
                .ForEach(x => materials.Add(x));
            return materials.AsQueryable();
        }
        public IQueryable<UnitsOfMeasurement> selectUnitsOfMeasurement()
        {
            List<UnitsOfMeasurement> unitsofmeasurement = new List<UnitsOfMeasurement>();
            _context.UnitsOfMeasurement.Include(a => a.MeasurementSystems).Include(a => a.MeasurementUnitsOf).AsNoTracking()
                .ToList()
                .ForEach(x => unitsofmeasurement.Add(x));
            return unitsofmeasurement.AsQueryable();
        }
       public IQueryable<HandlingUnitTypes> HandlingUnitTypesDb()
        {
            List<HandlingUnitTypes> handlingunittypes = new List<HandlingUnitTypes>();
            _context.HandlingUnitTypes.AsNoTracking()
                .ToList()
                .ForEach(x => handlingunittypes.Add(x));
            return handlingunittypes.AsQueryable();
        }
        public IQueryable<Materials> MaterialsDb()
        {
            List<Materials> materials = new List<Materials>();
            _context.Materials.Include(a => a.MaterialTypes).Include(a => a.UnitsOfMeasurementFKDiffBaseUnit).Include(a => a.UnitsOfMeasurementFKDiffDensityUnit).Include(a => a.UnitsOfMeasurementFKDiffHeightUnit).Include(a => a.UnitsOfMeasurementFKDiffLengthUnit).Include(a => a.UnitsOfMeasurementFKDiffShelflifeUnit).Include(a => a.UnitsOfMeasurementFKDiffWeightUnit).Include(a => a.UnitsOfMeasurementFKDiffWidthUnit).AsNoTracking()
                .ToList()
                .ForEach(x => materials.Add(x));
            return materials.AsQueryable();
        }
        public IQueryable<UnitsOfMeasurement> UnitsOfMeasurementDb()
        {
            List<UnitsOfMeasurement> unitsofmeasurement = new List<UnitsOfMeasurement>();
            _context.UnitsOfMeasurement.Include(a => a.MeasurementSystems).Include(a => a.MeasurementUnitsOf).AsNoTracking()
                .ToList()
                .ForEach(x => unitsofmeasurement.Add(x));
            return unitsofmeasurement.AsQueryable();
        }
       public List<KeyValuePair<Int64?, string>> selectUnitsOfMeasurementNullable()
        {
            List<KeyValuePair<Int64?, string>> unitsofmeasurementNullable = new List<KeyValuePair<Int64?, string>>();
            unitsofmeasurementNullable.Add(new KeyValuePair<Int64?, string>(null, "None"));
            _context.UnitsOfMeasurement
                .OrderBy(k => k.sUnitOfMeasurement)
                .ToList()
                .ForEach(k => unitsofmeasurementNullable.Add(new KeyValuePair<Int64?, string>(k.ixUnitOfMeasurement, k.sUnitOfMeasurement)));
            return unitsofmeasurementNullable;
        }
        public bool VerifyMaterialHandlingUnitConfigurationUnique(Int64 ixMaterialHandlingUnitConfiguration, string sMaterialHandlingUnitConfiguration)
        {
            if (_context.MaterialHandlingUnitConfigurations.AsNoTracking().Where(x => x.sMaterialHandlingUnitConfiguration == sMaterialHandlingUnitConfiguration).Any() && ixMaterialHandlingUnitConfiguration == 0L) return false;
            else if (_context.MaterialHandlingUnitConfigurations.AsNoTracking().Where(x => x.sMaterialHandlingUnitConfiguration == sMaterialHandlingUnitConfiguration && x.ixMaterialHandlingUnitConfiguration != ixMaterialHandlingUnitConfiguration).Any() && ixMaterialHandlingUnitConfiguration != 0L) return false;
            else return true;
        }

        public List<string> VerifyMaterialHandlingUnitConfigurationDeleteOK(Int64 ixMaterialHandlingUnitConfiguration, string sMaterialHandlingUnitConfiguration)
        {
            List<string> existInEntities = new List<string>();
           if (_contextHandlingUnits.HandlingUnits.AsNoTracking().Where(x => x.ixMaterialHandlingUnitConfiguration == ixMaterialHandlingUnitConfiguration).Any()) existInEntities.Add("HandlingUnits");
            if (_contextInboundOrderLines.InboundOrderLines.AsNoTracking().Where(x => x.ixMaterialHandlingUnitConfiguration == ixMaterialHandlingUnitConfiguration).Any()) existInEntities.Add("InboundOrderLines");
            if (_contextReceiving.Receiving.AsNoTracking().Where(x => x.ixMaterialHandlingUnitConfiguration == ixMaterialHandlingUnitConfiguration).Any()) existInEntities.Add("Receiving");

            return existInEntities;
        }


        public void RegisterCreate(MaterialHandlingUnitConfigurationsPost materialhandlingunitconfigurationsPost)
		{
            _context.MaterialHandlingUnitConfigurationsPost.Add(materialhandlingunitconfigurationsPost); 
        }

        public void RegisterEdit(MaterialHandlingUnitConfigurationsPost materialhandlingunitconfigurationsPost)
        {
            _context.Entry(materialhandlingunitconfigurationsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(MaterialHandlingUnitConfigurationsPost materialhandlingunitconfigurationsPost)
        {
            _context.MaterialHandlingUnitConfigurationsPost.Remove(materialhandlingunitconfigurationsPost);
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
  

