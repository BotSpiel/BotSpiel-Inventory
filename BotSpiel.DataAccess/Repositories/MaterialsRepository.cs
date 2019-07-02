using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class MaterialsRepository : IMaterialsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly MaterialsDB _context;
       private readonly HandlingUnitsDB _contextHandlingUnits;
        private readonly InboundOrderLinesDB _contextInboundOrderLines;
        private readonly InventoryLocationsSlottingDB _contextInventoryLocationsSlotting;
        private readonly InventoryUnitsDB _contextInventoryUnits;
        private readonly InventoryUnitTransactionsDB _contextInventoryUnitTransactions;
        private readonly MaterialHandlingUnitConfigurationsDB _contextMaterialHandlingUnitConfigurations;
        private readonly OutboundOrderLinesDB _contextOutboundOrderLines;
        private readonly ReceivingDB _contextReceiving;
  
        public MaterialsRepository(MaterialsDB context, HandlingUnitsDB contextHandlingUnits, InboundOrderLinesDB contextInboundOrderLines, InventoryLocationsSlottingDB contextInventoryLocationsSlotting, InventoryUnitsDB contextInventoryUnits, InventoryUnitTransactionsDB contextInventoryUnitTransactions, MaterialHandlingUnitConfigurationsDB contextMaterialHandlingUnitConfigurations, OutboundOrderLinesDB contextOutboundOrderLines, ReceivingDB contextReceiving)
        {
            _context = context;
           _contextHandlingUnits = contextHandlingUnits;
            _contextInboundOrderLines = contextInboundOrderLines;
            _contextInventoryLocationsSlotting = contextInventoryLocationsSlotting;
            _contextInventoryUnits = contextInventoryUnits;
            _contextInventoryUnitTransactions = contextInventoryUnitTransactions;
            _contextMaterialHandlingUnitConfigurations = contextMaterialHandlingUnitConfigurations;
            _contextOutboundOrderLines = contextOutboundOrderLines;
            _contextReceiving = contextReceiving;
  
        }

        public MaterialsPost GetPost(Int64 ixMaterial) => _context.MaterialsPost.AsNoTracking().Where(x => x.ixMaterial == ixMaterial).First();
         
		public Materials Get(Int64 ixMaterial)
        {
            Materials materials = _context.Materials.AsNoTracking().Where(x => x.ixMaterial == ixMaterial).First();
            materials.MaterialTypes = _context.MaterialTypes.Find(materials.ixMaterialType);
            materials.UnitsOfMeasurementFKDiffBaseUnit = _context.UnitsOfMeasurement.Find(materials.ixBaseUnit);
            if (materials.ixDensityUnit != null)
        {
            materials.UnitsOfMeasurementFKDiffDensityUnit = _context.UnitsOfMeasurement.Find(materials.ixDensityUnit);
        }
            if (materials.ixHeightUnit != null)
        {
            materials.UnitsOfMeasurementFKDiffHeightUnit = _context.UnitsOfMeasurement.Find(materials.ixHeightUnit);
        }
            if (materials.ixLengthUnit != null)
        {
            materials.UnitsOfMeasurementFKDiffLengthUnit = _context.UnitsOfMeasurement.Find(materials.ixLengthUnit);
        }
            if (materials.ixShelflifeUnit != null)
        {
            materials.UnitsOfMeasurementFKDiffShelflifeUnit = _context.UnitsOfMeasurement.Find(materials.ixShelflifeUnit);
        }
            if (materials.ixWeightUnit != null)
        {
            materials.UnitsOfMeasurementFKDiffWeightUnit = _context.UnitsOfMeasurement.Find(materials.ixWeightUnit);
        }
            if (materials.ixWidthUnit != null)
        {
            materials.UnitsOfMeasurementFKDiffWidthUnit = _context.UnitsOfMeasurement.Find(materials.ixWidthUnit);
        }

            return materials;
        }

        public IQueryable<Materials> Index()
        {
            var materials = _context.Materials.Include(a => a.MaterialTypes).Include(a => a.UnitsOfMeasurementFKDiffBaseUnit).AsNoTracking(); 
            return materials;
        }
       public IQueryable<MaterialTypes> selectMaterialTypes()
        {
            List<MaterialTypes> materialtypes = new List<MaterialTypes>();
            _context.MaterialTypes.AsNoTracking()
                .ToList()
                .ForEach(x => materialtypes.Add(x));
            return materialtypes.AsQueryable();
        }
        public IQueryable<UnitsOfMeasurement> selectUnitsOfMeasurement()
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
        public bool VerifyMaterialUnique(Int64 ixMaterial, string sMaterial)
        {
            if (_context.Materials.AsNoTracking().Where(x => x.sMaterial == sMaterial).Any() && ixMaterial == 0L) return false;
            else if (_context.Materials.AsNoTracking().Where(x => x.sMaterial == sMaterial && x.ixMaterial != ixMaterial).Any() && ixMaterial != 0L) return false;
            else return true;
        }

        public List<string> VerifyMaterialDeleteOK(Int64 ixMaterial, string sMaterial)
        {
            List<string> existInEntities = new List<string>();
           if (_contextHandlingUnits.HandlingUnits.AsNoTracking().Where(x => x.ixPackingMaterial == ixMaterial).Any()) existInEntities.Add("HandlingUnits");
            if (_contextInboundOrderLines.InboundOrderLines.AsNoTracking().Where(x => x.ixMaterial == ixMaterial).Any()) existInEntities.Add("InboundOrderLines");
            if (_contextInventoryLocationsSlotting.InventoryLocationsSlotting.AsNoTracking().Where(x => x.ixMaterial == ixMaterial).Any()) existInEntities.Add("InventoryLocationsSlotting");
            if (_contextInventoryUnits.InventoryUnits.AsNoTracking().Where(x => x.ixMaterial == ixMaterial).Any()) existInEntities.Add("InventoryUnits");
            if (_contextInventoryUnitTransactions.InventoryUnitTransactions.AsNoTracking().Where(x => x.ixMaterialAfter == ixMaterial).Any()) existInEntities.Add("InventoryUnitTransactions");
            if (_contextInventoryUnitTransactions.InventoryUnitTransactions.AsNoTracking().Where(x => x.ixMaterialBefore == ixMaterial).Any()) existInEntities.Add("InventoryUnitTransactions");
            if (_contextMaterialHandlingUnitConfigurations.MaterialHandlingUnitConfigurations.AsNoTracking().Where(x => x.ixMaterial == ixMaterial).Any()) existInEntities.Add("MaterialHandlingUnitConfigurations");
            if (_contextOutboundOrderLines.OutboundOrderLines.AsNoTracking().Where(x => x.ixMaterial == ixMaterial).Any()) existInEntities.Add("OutboundOrderLines");
            if (_contextReceiving.Receiving.AsNoTracking().Where(x => x.ixMaterial == ixMaterial).Any()) existInEntities.Add("Receiving");

            return existInEntities;
        }


        public void RegisterCreate(MaterialsPost materialsPost)
		{
            _context.MaterialsPost.Add(materialsPost); 
        }

        public void RegisterEdit(MaterialsPost materialsPost)
        {
            _context.Entry(materialsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(MaterialsPost materialsPost)
        {
            _context.MaterialsPost.Remove(materialsPost);
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
  

