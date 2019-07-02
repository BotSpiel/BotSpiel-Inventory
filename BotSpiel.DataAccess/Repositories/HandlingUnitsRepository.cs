using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class HandlingUnitsRepository : IHandlingUnitsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly HandlingUnitsDB _context;
       private readonly HandlingUnitsDB _contextHandlingUnits;
        private readonly HandlingUnitsShippingDB _contextHandlingUnitsShipping;
        private readonly InventoryUnitsDB _contextInventoryUnits;
        private readonly InventoryUnitTransactionsDB _contextInventoryUnitTransactions;
        private readonly MoveQueuesDB _contextMoveQueues;
        private readonly OutboundOrderPackingDB _contextOutboundOrderPacking;
        private readonly ReceivingDB _contextReceiving;
  
        public HandlingUnitsRepository(HandlingUnitsDB context, HandlingUnitsDB contextHandlingUnits, HandlingUnitsShippingDB contextHandlingUnitsShipping, InventoryUnitsDB contextInventoryUnits, InventoryUnitTransactionsDB contextInventoryUnitTransactions, MoveQueuesDB contextMoveQueues, OutboundOrderPackingDB contextOutboundOrderPacking, ReceivingDB contextReceiving)
        {
            _context = context;
           _contextHandlingUnits = contextHandlingUnits;
            _contextHandlingUnitsShipping = contextHandlingUnitsShipping;
            _contextInventoryUnits = contextInventoryUnits;
            _contextInventoryUnitTransactions = contextInventoryUnitTransactions;
            _contextMoveQueues = contextMoveQueues;
            _contextOutboundOrderPacking = contextOutboundOrderPacking;
            _contextReceiving = contextReceiving;
  
        }

        public HandlingUnitsPost GetPost(Int64 ixHandlingUnit) => _context.HandlingUnitsPost.AsNoTracking().Where(x => x.ixHandlingUnit == ixHandlingUnit).First();
         
		public HandlingUnits Get(Int64 ixHandlingUnit)
        {
            HandlingUnits handlingunits = _context.HandlingUnits.AsNoTracking().Where(x => x.ixHandlingUnit == ixHandlingUnit).First();
            if (handlingunits.ixParentHandlingUnit != null)
        {
            handlingunits.HandlingUnitsFKDiffParentHandlingUnit = _context.HandlingUnits.Find(handlingunits.ixParentHandlingUnit);
        }
            handlingunits.HandlingUnitTypes = _context.HandlingUnitTypes.Find(handlingunits.ixHandlingUnitType);
            if (handlingunits.ixMaterialHandlingUnitConfiguration != null)
        {
            handlingunits.MaterialHandlingUnitConfigurations = _context.MaterialHandlingUnitConfigurations.Find(handlingunits.ixMaterialHandlingUnitConfiguration);
        }
            if (handlingunits.ixPackingMaterial != null)
        {
            handlingunits.MaterialsFKDiffPackingMaterial = _context.Materials.Find(handlingunits.ixPackingMaterial);
        }
            if (handlingunits.ixStatus != null)
        {
            handlingunits.Statuses = _context.Statuses.Find(handlingunits.ixStatus);
        }
            if (handlingunits.ixHeightUnit != null)
        {
            handlingunits.UnitsOfMeasurementFKDiffHeightUnit = _context.UnitsOfMeasurement.Find(handlingunits.ixHeightUnit);
        }
            if (handlingunits.ixLengthUnit != null)
        {
            handlingunits.UnitsOfMeasurementFKDiffLengthUnit = _context.UnitsOfMeasurement.Find(handlingunits.ixLengthUnit);
        }
            if (handlingunits.ixWeightUnit != null)
        {
            handlingunits.UnitsOfMeasurementFKDiffWeightUnit = _context.UnitsOfMeasurement.Find(handlingunits.ixWeightUnit);
        }
            if (handlingunits.ixWidthUnit != null)
        {
            handlingunits.UnitsOfMeasurementFKDiffWidthUnit = _context.UnitsOfMeasurement.Find(handlingunits.ixWidthUnit);
        }

            return handlingunits;
        }

        public IQueryable<HandlingUnits> Index()
        {
            var handlingunits = _context.HandlingUnits.Include(a => a.HandlingUnitTypes).AsNoTracking(); 
            return handlingunits;
        }
       public IQueryable<HandlingUnits> selectHandlingUnits()
        {
            List<HandlingUnits> handlingunits = new List<HandlingUnits>();
            _context.HandlingUnits.Include(a => a.HandlingUnitsFKDiffParentHandlingUnit).Include(a => a.HandlingUnitTypes).Include(a => a.MaterialHandlingUnitConfigurations).Include(a => a.MaterialsFKDiffPackingMaterial).Include(a => a.Statuses).Include(a => a.UnitsOfMeasurementFKDiffHeightUnit).Include(a => a.UnitsOfMeasurementFKDiffLengthUnit).Include(a => a.UnitsOfMeasurementFKDiffWeightUnit).Include(a => a.UnitsOfMeasurementFKDiffWidthUnit).AsNoTracking()
                .ToList()
                .ForEach(x => handlingunits.Add(x));
            return handlingunits.AsQueryable();
        }
        public IQueryable<HandlingUnitTypes> selectHandlingUnitTypes()
        {
            List<HandlingUnitTypes> handlingunittypes = new List<HandlingUnitTypes>();
            _context.HandlingUnitTypes.AsNoTracking()
                .ToList()
                .ForEach(x => handlingunittypes.Add(x));
            return handlingunittypes.AsQueryable();
        }
        public IQueryable<MaterialHandlingUnitConfigurations> selectMaterialHandlingUnitConfigurations()
        {
            List<MaterialHandlingUnitConfigurations> materialhandlingunitconfigurations = new List<MaterialHandlingUnitConfigurations>();
            _context.MaterialHandlingUnitConfigurations.Include(a => a.HandlingUnitTypes).Include(a => a.Materials).Include(a => a.UnitsOfMeasurementFKDiffHeightUnit).Include(a => a.UnitsOfMeasurementFKDiffLengthUnit).Include(a => a.UnitsOfMeasurementFKDiffWidthUnit).AsNoTracking()
                .ToList()
                .ForEach(x => materialhandlingunitconfigurations.Add(x));
            return materialhandlingunitconfigurations.AsQueryable();
        }
        public IQueryable<Materials> selectMaterials()
        {
            List<Materials> materials = new List<Materials>();
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //_context.Materials.Include(a => a.MaterialTypes).Include(a => a.UnitsOfMeasurementFKDiffBaseUnit).Include(a => a.UnitsOfMeasurementFKDiffDensityUnit).Include(a => a.UnitsOfMeasurementFKDiffHeightUnit).Include(a => a.UnitsOfMeasurementFKDiffLengthUnit).Include(a => a.UnitsOfMeasurementFKDiffShelflifeUnit).Include(a => a.UnitsOfMeasurementFKDiffWeightUnit).Include(a => a.UnitsOfMeasurementFKDiffWidthUnit).AsNoTracking()
            //Replaced Code Block End
            _context.Materials.Include(a => a.MaterialTypes).Include(a => a.UnitsOfMeasurementFKDiffBaseUnit).Include(a => a.UnitsOfMeasurementFKDiffDensityUnit).Include(a => a.UnitsOfMeasurementFKDiffHeightUnit).Include(a => a.UnitsOfMeasurementFKDiffLengthUnit).Include(a => a.UnitsOfMeasurementFKDiffShelflifeUnit).Include(a => a.UnitsOfMeasurementFKDiffWeightUnit).Include(a => a.UnitsOfMeasurementFKDiffWidthUnit).Where(a => a.MaterialTypes.sMaterialType == "Packing Materials").AsNoTracking()
            //Custom Code End
                .ToList()
                .ForEach(x => materials.Add(x));
            return materials.AsQueryable();
        }
        public IQueryable<Statuses> selectStatuses()
        {
            List<Statuses> statuses = new List<Statuses>();
            _context.Statuses.AsNoTracking()
                .ToList()
                .ForEach(x => statuses.Add(x));
            return statuses.AsQueryable();
        }
        public IQueryable<UnitsOfMeasurement> selectUnitsOfMeasurement()
        {
            List<UnitsOfMeasurement> unitsofmeasurement = new List<UnitsOfMeasurement>();
            _context.UnitsOfMeasurement.Include(a => a.MeasurementSystems).Include(a => a.MeasurementUnitsOf).AsNoTracking()
                .ToList()
                .ForEach(x => unitsofmeasurement.Add(x));
            return unitsofmeasurement.AsQueryable();
        }
       public List<KeyValuePair<Int64?, string>> selectHandlingUnitsNullable()
        {
            List<KeyValuePair<Int64?, string>> handlingunitsNullable = new List<KeyValuePair<Int64?, string>>();
            handlingunitsNullable.Add(new KeyValuePair<Int64?, string>(null, "None"));
            _context.HandlingUnits
                .OrderBy(k => k.sHandlingUnit)
                .ToList()
                .ForEach(k => handlingunitsNullable.Add(new KeyValuePair<Int64?, string>(k.ixHandlingUnit, k.sHandlingUnit)));
            return handlingunitsNullable;
        }
        public List<KeyValuePair<Int64?, string>> selectMaterialHandlingUnitConfigurationsNullable()
        {
            List<KeyValuePair<Int64?, string>> materialhandlingunitconfigurationsNullable = new List<KeyValuePair<Int64?, string>>();
            materialhandlingunitconfigurationsNullable.Add(new KeyValuePair<Int64?, string>(null, "None"));
            _context.MaterialHandlingUnitConfigurations
                .OrderBy(k => k.sMaterialHandlingUnitConfiguration)
                .ToList()
                .ForEach(k => materialhandlingunitconfigurationsNullable.Add(new KeyValuePair<Int64?, string>(k.ixMaterialHandlingUnitConfiguration, k.sMaterialHandlingUnitConfiguration)));
            return materialhandlingunitconfigurationsNullable;
        }
        public List<KeyValuePair<Int64?, string>> selectMaterialsNullable()
        {
            List<KeyValuePair<Int64?, string>> materialsNullable = new List<KeyValuePair<Int64?, string>>();
            materialsNullable.Add(new KeyValuePair<Int64?, string>(null, "None"));
            _context.Materials
                .OrderBy(k => k.sMaterial)
                .ToList()
                .ForEach(k => materialsNullable.Add(new KeyValuePair<Int64?, string>(k.ixMaterial, k.sMaterial)));
            return materialsNullable;
        }
        public List<KeyValuePair<Int64?, string>> selectStatusesNullable()
        {
            List<KeyValuePair<Int64?, string>> statusesNullable = new List<KeyValuePair<Int64?, string>>();
            statusesNullable.Add(new KeyValuePair<Int64?, string>(null, "None"));
            _context.Statuses
                .OrderBy(k => k.sStatus)
                .ToList()
                .ForEach(k => statusesNullable.Add(new KeyValuePair<Int64?, string>(k.ixStatus, k.sStatus)));
            return statusesNullable;
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
        public bool VerifyHandlingUnitUnique(Int64 ixHandlingUnit, string sHandlingUnit)
        {
            if (_context.HandlingUnits.AsNoTracking().Where(x => x.sHandlingUnit == sHandlingUnit).Any() && ixHandlingUnit == 0L) return false;
            else if (_context.HandlingUnits.AsNoTracking().Where(x => x.sHandlingUnit == sHandlingUnit && x.ixHandlingUnit != ixHandlingUnit).Any() && ixHandlingUnit != 0L) return false;
            else return true;
        }

        public List<string> VerifyHandlingUnitDeleteOK(Int64 ixHandlingUnit, string sHandlingUnit)
        {
            List<string> existInEntities = new List<string>();
           if (_contextHandlingUnits.HandlingUnits.AsNoTracking().Where(x => x.ixParentHandlingUnit == ixHandlingUnit).Any()) existInEntities.Add("HandlingUnits");
            if (_contextHandlingUnitsShipping.HandlingUnitsShipping.AsNoTracking().Where(x => x.ixHandlingUnit == ixHandlingUnit).Any()) existInEntities.Add("HandlingUnitsShipping");
            if (_contextInventoryUnits.InventoryUnits.AsNoTracking().Where(x => x.ixHandlingUnit == ixHandlingUnit).Any()) existInEntities.Add("InventoryUnits");
            if (_contextInventoryUnitTransactions.InventoryUnitTransactions.AsNoTracking().Where(x => x.ixHandlingUnitAfter == ixHandlingUnit).Any()) existInEntities.Add("InventoryUnitTransactions");
            if (_contextInventoryUnitTransactions.InventoryUnitTransactions.AsNoTracking().Where(x => x.ixHandlingUnitBefore == ixHandlingUnit).Any()) existInEntities.Add("InventoryUnitTransactions");
            if (_contextMoveQueues.MoveQueues.AsNoTracking().Where(x => x.ixSourceHandlingUnit == ixHandlingUnit).Any()) existInEntities.Add("MoveQueues");
            if (_contextMoveQueues.MoveQueues.AsNoTracking().Where(x => x.ixTargetHandlingUnit == ixHandlingUnit).Any()) existInEntities.Add("MoveQueues");
            if (_contextOutboundOrderPacking.OutboundOrderPacking.AsNoTracking().Where(x => x.ixHandlingUnit == ixHandlingUnit).Any()) existInEntities.Add("OutboundOrderPacking");
            if (_contextReceiving.Receiving.AsNoTracking().Where(x => x.ixHandlingUnit == ixHandlingUnit).Any()) existInEntities.Add("Receiving");

            return existInEntities;
        }


        public void RegisterCreate(HandlingUnitsPost handlingunitsPost)
		{
            _context.HandlingUnitsPost.Add(handlingunitsPost); 
        }

        public void RegisterEdit(HandlingUnitsPost handlingunitsPost)
        {
            _context.Entry(handlingunitsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(HandlingUnitsPost handlingunitsPost)
        {
            _context.HandlingUnitsPost.Remove(handlingunitsPost);
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
  

