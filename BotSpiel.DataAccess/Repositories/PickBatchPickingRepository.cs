using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class PickBatchPickingRepository : IPickBatchPickingRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly PickBatchPickingDB _context;
  
        public PickBatchPickingRepository(PickBatchPickingDB context)
        {
            _context = context;
  
        }

        public PickBatchPickingPost GetPost(Int64 ixPickBatchPick) => _context.PickBatchPickingPost.AsNoTracking().Where(x => x.ixPickBatchPick == ixPickBatchPick).First();
         
		public PickBatchPicking Get(Int64 ixPickBatchPick)
        {
            PickBatchPicking pickbatchpicking = _context.PickBatchPicking.AsNoTracking().Where(x => x.ixPickBatchPick == ixPickBatchPick).First();
            pickbatchpicking.HandlingUnits = _context.HandlingUnits.Find(pickbatchpicking.ixHandlingUnit);
            pickbatchpicking.InventoryUnits = _context.InventoryUnits.Find(pickbatchpicking.ixInventoryUnit);
            pickbatchpicking.PickBatches = _context.PickBatches.Find(pickbatchpicking.ixPickBatch);

            return pickbatchpicking;
        }

        public IQueryable<PickBatchPicking> Index()
        {
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //var pickbatchpicking = _context.PickBatchPicking.Include(a => a.InventoryUnits).Include(a => a.PickBatches).Include(a => a.HandlingUnits).AsNoTracking();
            //Replaced Code Block End
            var pickbatchpicking = _context.PickBatchPicking.OrderByDescending(a => a.ixPickBatchPick).Include(a => a.InventoryUnits).Include(a => a.PickBatches).Include(a => a.HandlingUnits).AsNoTracking();
            //Custom Code End
            return pickbatchpicking;
        }

        public IQueryable<PickBatchPicking> IndexDb()
        {
            var pickbatchpicking = _context.PickBatchPicking.Include(a => a.InventoryUnits).Include(a => a.PickBatches).Include(a => a.HandlingUnits).AsNoTracking(); 
            return pickbatchpicking;
        }
       public IQueryable<HandlingUnits> selectHandlingUnits()
        {
            List<HandlingUnits> handlingunits = new List<HandlingUnits>();
            _context.HandlingUnits.Include(a => a.HandlingUnitsFKDiffParentHandlingUnit).Include(a => a.HandlingUnitTypes).Include(a => a.MaterialHandlingUnitConfigurations).Include(a => a.MaterialsFKDiffPackingMaterial).Include(a => a.Statuses).Include(a => a.UnitsOfMeasurementFKDiffHeightUnit).Include(a => a.UnitsOfMeasurementFKDiffLengthUnit).Include(a => a.UnitsOfMeasurementFKDiffWeightUnit).Include(a => a.UnitsOfMeasurementFKDiffWidthUnit).AsNoTracking()
                .ToList()
                .ForEach(x => handlingunits.Add(x));
            return handlingunits.AsQueryable();
        }
        public IQueryable<InventoryUnits> selectInventoryUnits()
        {
            List<InventoryUnits> inventoryunits = new List<InventoryUnits>();
            _context.InventoryUnits.Include(a => a.Companies).Include(a => a.Facilities).Include(a => a.HandlingUnits).Include(a => a.InventoryLocations).Include(a => a.InventoryStates).Include(a => a.Materials).Include(a => a.Statuses).AsNoTracking()
                .ToList()
                .ForEach(x => inventoryunits.Add(x));
            return inventoryunits.AsQueryable();
        }
        public IQueryable<PickBatches> selectPickBatches()
        {
            List<PickBatches> pickbatches = new List<PickBatches>();
            _context.PickBatches.Include(a => a.PickBatchTypes).Include(a => a.Statuses).AsNoTracking()
                .ToList()
                .ForEach(x => pickbatches.Add(x));
            return pickbatches.AsQueryable();
        }
       public IQueryable<HandlingUnits> HandlingUnitsDb()
        {
            List<HandlingUnits> handlingunits = new List<HandlingUnits>();
            _context.HandlingUnits.Include(a => a.HandlingUnitsFKDiffParentHandlingUnit).Include(a => a.HandlingUnitTypes).Include(a => a.MaterialHandlingUnitConfigurations).Include(a => a.MaterialsFKDiffPackingMaterial).Include(a => a.Statuses).Include(a => a.UnitsOfMeasurementFKDiffHeightUnit).Include(a => a.UnitsOfMeasurementFKDiffLengthUnit).Include(a => a.UnitsOfMeasurementFKDiffWeightUnit).Include(a => a.UnitsOfMeasurementFKDiffWidthUnit).AsNoTracking()
                .ToList()
                .ForEach(x => handlingunits.Add(x));
            return handlingunits.AsQueryable();
        }
        public IQueryable<InventoryUnits> InventoryUnitsDb()
        {
            List<InventoryUnits> inventoryunits = new List<InventoryUnits>();
            _context.InventoryUnits.Include(a => a.Companies).Include(a => a.Facilities).Include(a => a.HandlingUnits).Include(a => a.InventoryLocations).Include(a => a.InventoryStates).Include(a => a.Materials).Include(a => a.Statuses).AsNoTracking()
                .ToList()
                .ForEach(x => inventoryunits.Add(x));
            return inventoryunits.AsQueryable();
        }
        public IQueryable<PickBatches> PickBatchesDb()
        {
            List<PickBatches> pickbatches = new List<PickBatches>();
            _context.PickBatches.Include(a => a.PickBatchTypes).Include(a => a.Statuses).AsNoTracking()
                .ToList()
                .ForEach(x => pickbatches.Add(x));
            return pickbatches.AsQueryable();
        }
        public List<string> VerifyPickBatchPickDeleteOK(Int64 ixPickBatchPick, string sPickBatchPick)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(PickBatchPickingPost pickbatchpickingPost)
		{
            _context.PickBatchPickingPost.Add(pickbatchpickingPost); 
        }

        public void RegisterEdit(PickBatchPickingPost pickbatchpickingPost)
        {
            _context.Entry(pickbatchpickingPost).State = EntityState.Modified;
        }

        public void RegisterDelete(PickBatchPickingPost pickbatchpickingPost)
        {
            _context.PickBatchPickingPost.Remove(pickbatchpickingPost);
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
  

