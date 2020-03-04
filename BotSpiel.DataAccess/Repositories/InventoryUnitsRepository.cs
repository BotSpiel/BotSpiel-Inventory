using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class InventoryUnitsRepository : IInventoryUnitsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly InventoryUnitsDB _context;
       private readonly InventoryUnitTransactionsDB _contextInventoryUnitTransactions;
        private readonly MoveQueuesDB _contextMoveQueues;
        private readonly PickBatchPickingDB _contextPickBatchPicking;
  
        public InventoryUnitsRepository(InventoryUnitsDB context, InventoryUnitTransactionsDB contextInventoryUnitTransactions, MoveQueuesDB contextMoveQueues, PickBatchPickingDB contextPickBatchPicking)
        {
            _context = context;
           _contextInventoryUnitTransactions = contextInventoryUnitTransactions;
            _contextMoveQueues = contextMoveQueues;
            _contextPickBatchPicking = contextPickBatchPicking;
  
        }

        public InventoryUnitsPost GetPost(Int64 ixInventoryUnit) => _context.InventoryUnitsPost.AsNoTracking().Where(x => x.ixInventoryUnit == ixInventoryUnit).First();
         
		public InventoryUnits Get(Int64 ixInventoryUnit)
        {
            InventoryUnits inventoryunits = _context.InventoryUnits.AsNoTracking().Where(x => x.ixInventoryUnit == ixInventoryUnit).First();
            inventoryunits.Companies = _context.Companies.Find(inventoryunits.ixCompany);
            inventoryunits.Facilities = _context.Facilities.Find(inventoryunits.ixFacility);
            if (inventoryunits.ixHandlingUnit != null)
        {
            inventoryunits.HandlingUnits = _context.HandlingUnits.Find(inventoryunits.ixHandlingUnit);
        }
            inventoryunits.InventoryLocations = _context.InventoryLocations.Find(inventoryunits.ixInventoryLocation);
            inventoryunits.InventoryStates = _context.InventoryStates.Find(inventoryunits.ixInventoryState);
            inventoryunits.Materials = _context.Materials.Find(inventoryunits.ixMaterial);
            inventoryunits.Statuses = _context.Statuses.Find(inventoryunits.ixStatus);

            return inventoryunits;
        }

        public IQueryable<InventoryUnits> Index()
        {
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //var inventoryunits = _context.InventoryUnits.Include(a => a.Facilities).Include(a => a.Companies).Include(a => a.Materials).Include(a => a.InventoryStates).Include(a => a.InventoryLocations).Include(a => a.Statuses).AsNoTracking();
            //Replaced Code Block End
            var inventoryunits = _context.InventoryUnits.Where(a => a.nBaseUnitQuantity > 0).Include(a => a.Facilities).Include(a => a.Companies).Include(a => a.Materials).Include(a => a.InventoryStates).Include(a => a.InventoryLocations).Include(a => a.Statuses).Include(a => a.HandlingUnits).AsNoTracking();
            //Custom Code End
            return inventoryunits;
        }

        public IQueryable<InventoryUnits> IndexDb()
        {
            var inventoryunits = _context.InventoryUnits.Include(a => a.Facilities).Include(a => a.Companies).Include(a => a.Materials).Include(a => a.InventoryStates).Include(a => a.InventoryLocations).Include(a => a.Statuses).AsNoTracking(); 
            return inventoryunits;
        }

        //Custom Code Start | Added Code Block 
        public IQueryable<InventoryUnitsPost> IndexDbPost()
        {
            var inventoryunits = _context.InventoryUnitsPost.AsNoTracking();
            return inventoryunits;
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
        public IQueryable<HandlingUnits> selectHandlingUnits()
        {
            List<HandlingUnits> handlingunits = new List<HandlingUnits>();
            _context.HandlingUnits.Include(a => a.HandlingUnitsFKDiffParentHandlingUnit).Include(a => a.HandlingUnitTypes).Include(a => a.MaterialHandlingUnitConfigurations).Include(a => a.MaterialsFKDiffPackingMaterial).Include(a => a.Statuses).Include(a => a.UnitsOfMeasurementFKDiffHeightUnit).Include(a => a.UnitsOfMeasurementFKDiffLengthUnit).Include(a => a.UnitsOfMeasurementFKDiffWeightUnit).Include(a => a.UnitsOfMeasurementFKDiffWidthUnit).AsNoTracking()
                .ToList()
                .ForEach(x => handlingunits.Add(x));
            return handlingunits.AsQueryable();
        }
        public IQueryable<InventoryLocations> selectInventoryLocations()
        {
            List<InventoryLocations> inventorylocations = new List<InventoryLocations>();
            _context.InventoryLocations.Include(a => a.Companies).Include(a => a.FacilityAisleFaces).Include(a => a.FacilityFloors).Include(a => a.FacilityWorkAreas).Include(a => a.FacilityZones).Include(a => a.InventoryLocationSizes).Include(a => a.LocationFunctions).Include(a => a.UnitsOfMeasurementFKDiffXOffsetUnit).Include(a => a.UnitsOfMeasurementFKDiffYOffsetUnit).Include(a => a.UnitsOfMeasurementFKDiffZOffsetUnit).AsNoTracking()
                .ToList()
                .ForEach(x => inventorylocations.Add(x));
            return inventorylocations.AsQueryable();
        }
        public IQueryable<InventoryStates> selectInventoryStates()
        {
            List<InventoryStates> inventorystates = new List<InventoryStates>();
            _context.InventoryStates.AsNoTracking()
                .ToList()
                .ForEach(x => inventorystates.Add(x));
            return inventorystates.AsQueryable();
        }
        public IQueryable<Materials> selectMaterials()
        {
            List<Materials> materials = new List<Materials>();
            _context.Materials.Include(a => a.MaterialTypes).Include(a => a.UnitsOfMeasurementFKDiffBaseUnit).Include(a => a.UnitsOfMeasurementFKDiffDensityUnit).Include(a => a.UnitsOfMeasurementFKDiffHeightUnit).Include(a => a.UnitsOfMeasurementFKDiffLengthUnit).Include(a => a.UnitsOfMeasurementFKDiffShelflifeUnit).Include(a => a.UnitsOfMeasurementFKDiffWeightUnit).Include(a => a.UnitsOfMeasurementFKDiffWidthUnit).AsNoTracking()
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
        public IQueryable<HandlingUnits> HandlingUnitsDb()
        {
            List<HandlingUnits> handlingunits = new List<HandlingUnits>();
            _context.HandlingUnits.Include(a => a.HandlingUnitsFKDiffParentHandlingUnit).Include(a => a.HandlingUnitTypes).Include(a => a.MaterialHandlingUnitConfigurations).Include(a => a.MaterialsFKDiffPackingMaterial).Include(a => a.Statuses).Include(a => a.UnitsOfMeasurementFKDiffHeightUnit).Include(a => a.UnitsOfMeasurementFKDiffLengthUnit).Include(a => a.UnitsOfMeasurementFKDiffWeightUnit).Include(a => a.UnitsOfMeasurementFKDiffWidthUnit).AsNoTracking()
                .ToList()
                .ForEach(x => handlingunits.Add(x));
            return handlingunits.AsQueryable();
        }
        public IQueryable<InventoryLocations> InventoryLocationsDb()
        {
            List<InventoryLocations> inventorylocations = new List<InventoryLocations>();
            _context.InventoryLocations.Include(a => a.Companies).Include(a => a.FacilityAisleFaces).Include(a => a.FacilityFloors).Include(a => a.FacilityWorkAreas).Include(a => a.FacilityZones).Include(a => a.InventoryLocationSizes).Include(a => a.LocationFunctions).Include(a => a.UnitsOfMeasurementFKDiffXOffsetUnit).Include(a => a.UnitsOfMeasurementFKDiffYOffsetUnit).Include(a => a.UnitsOfMeasurementFKDiffZOffsetUnit).AsNoTracking()
                .ToList()
                .ForEach(x => inventorylocations.Add(x));
            return inventorylocations.AsQueryable();
        }
        public IQueryable<InventoryStates> InventoryStatesDb()
        {
            List<InventoryStates> inventorystates = new List<InventoryStates>();
            _context.InventoryStates.AsNoTracking()
                .ToList()
                .ForEach(x => inventorystates.Add(x));
            return inventorystates.AsQueryable();
        }
        public IQueryable<Materials> MaterialsDb()
        {
            List<Materials> materials = new List<Materials>();
            _context.Materials.Include(a => a.MaterialTypes).Include(a => a.UnitsOfMeasurementFKDiffBaseUnit).Include(a => a.UnitsOfMeasurementFKDiffDensityUnit).Include(a => a.UnitsOfMeasurementFKDiffHeightUnit).Include(a => a.UnitsOfMeasurementFKDiffLengthUnit).Include(a => a.UnitsOfMeasurementFKDiffShelflifeUnit).Include(a => a.UnitsOfMeasurementFKDiffWeightUnit).Include(a => a.UnitsOfMeasurementFKDiffWidthUnit).AsNoTracking()
                .ToList()
                .ForEach(x => materials.Add(x));
            return materials.AsQueryable();
        }
        public IQueryable<Statuses> StatusesDb()
        {
            List<Statuses> statuses = new List<Statuses>();
            _context.Statuses.AsNoTracking()
                .ToList()
                .ForEach(x => statuses.Add(x));
            return statuses.AsQueryable();
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
        public bool VerifyInventoryUnitUnique(Int64 ixInventoryUnit, string sInventoryUnit)
        {
            if (_context.InventoryUnits.AsNoTracking().Where(x => x.sInventoryUnit == sInventoryUnit).Any() && ixInventoryUnit == 0L) return false;
            else if (_context.InventoryUnits.AsNoTracking().Where(x => x.sInventoryUnit == sInventoryUnit && x.ixInventoryUnit != ixInventoryUnit).Any() && ixInventoryUnit != 0L) return false;
            else return true;
        }

        public List<string> VerifyInventoryUnitDeleteOK(Int64 ixInventoryUnit, string sInventoryUnit)
        {
            List<string> existInEntities = new List<string>();
           if (_contextInventoryUnitTransactions.InventoryUnitTransactions.AsNoTracking().Where(x => x.ixInventoryUnit == ixInventoryUnit).Any()) existInEntities.Add("InventoryUnitTransactions");
            if (_contextMoveQueues.MoveQueues.AsNoTracking().Where(x => x.ixSourceInventoryUnit == ixInventoryUnit).Any()) existInEntities.Add("MoveQueues");
            if (_contextMoveQueues.MoveQueues.AsNoTracking().Where(x => x.ixTargetInventoryUnit == ixInventoryUnit).Any()) existInEntities.Add("MoveQueues");
            if (_contextPickBatchPicking.PickBatchPicking.AsNoTracking().Where(x => x.ixInventoryUnit == ixInventoryUnit).Any()) existInEntities.Add("PickBatchPicking");

            return existInEntities;
        }


        public void RegisterCreate(InventoryUnitsPost inventoryunitsPost)
		{
            _context.InventoryUnitsPost.Add(inventoryunitsPost); 
        }

        public void RegisterEdit(InventoryUnitsPost inventoryunitsPost)
        {
            _context.Entry(inventoryunitsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(InventoryUnitsPost inventoryunitsPost)
        {
            _context.InventoryUnitsPost.Remove(inventoryunitsPost);
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
  

