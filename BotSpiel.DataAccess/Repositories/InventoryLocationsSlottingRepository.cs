using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class InventoryLocationsSlottingRepository : IInventoryLocationsSlottingRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly InventoryLocationsSlottingDB _context;
  
        public InventoryLocationsSlottingRepository(InventoryLocationsSlottingDB context)
        {
            _context = context;
  
        }

        public InventoryLocationsSlottingPost GetPost(Int64 ixInventoryLocationSlotting) => _context.InventoryLocationsSlottingPost.AsNoTracking().Where(x => x.ixInventoryLocationSlotting == ixInventoryLocationSlotting).First();
         
		public InventoryLocationsSlotting Get(Int64 ixInventoryLocationSlotting)
        {
            InventoryLocationsSlotting inventorylocationsslotting = _context.InventoryLocationsSlotting.AsNoTracking().Where(x => x.ixInventoryLocationSlotting == ixInventoryLocationSlotting).First();
            inventorylocationsslotting.InventoryLocations = _context.InventoryLocations.Find(inventorylocationsslotting.ixInventoryLocation);
            inventorylocationsslotting.Materials = _context.Materials.Find(inventorylocationsslotting.ixMaterial);

            return inventorylocationsslotting;
        }

        public IQueryable<InventoryLocationsSlotting> Index()
        {
            var inventorylocationsslotting = _context.InventoryLocationsSlotting.Include(a => a.InventoryLocations).Include(a => a.Materials).AsNoTracking(); 
            return inventorylocationsslotting;
        }
       public IQueryable<InventoryLocations> selectInventoryLocations()
        {
            List<InventoryLocations> inventorylocations = new List<InventoryLocations>();
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //_context.InventoryLocations.Include(a => a.Companies).Include(a => a.FacilityAisleFaces).Include(a => a.FacilityFloors).Include(a => a.FacilityWorkAreas).Include(a => a.FacilityZones).Include(a => a.InventoryLocationSizes).Include(a => a.LocationFunctions).Include(a => a.UnitsOfMeasurementFKDiffXOffsetUnit).Include(a => a.UnitsOfMeasurementFKDiffYOffsetUnit).Include(a => a.UnitsOfMeasurementFKDiffZOffsetUnit).AsNoTracking()
            //Replaced Code Block End
            var locationsAlreadySlotted = _context.InventoryLocationsSlottingPost.Select(x => x.ixInventoryLocation);
            _context.InventoryLocations.Where(a => !locationsAlreadySlotted.Contains(a.ixInventoryLocation)).Include(a => a.Companies).Include(a => a.FacilityAisleFaces).Include(a => a.FacilityFloors).Include(a => a.FacilityWorkAreas).Include(a => a.FacilityZones).Include(a => a.InventoryLocationSizes).Include(a => a.LocationFunctions).Include(a => a.UnitsOfMeasurementFKDiffXOffsetUnit).Include(a => a.UnitsOfMeasurementFKDiffYOffsetUnit).Include(a => a.UnitsOfMeasurementFKDiffZOffsetUnit).Where(x => x.LocationFunctions.sLocationFunctionCode == "FP").AsNoTracking()
            //Custom Code End
                .ToList()
                .ForEach(x => inventorylocations.Add(x));
            return inventorylocations.AsQueryable();
        }
        public IQueryable<Materials> selectMaterials()
        {
            List<Materials> materials = new List<Materials>();
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //_context.Materials.Include(a => a.MaterialTypes).Include(a => a.UnitsOfMeasurementFKDiffBaseUnit).Include(a => a.UnitsOfMeasurementFKDiffDensityUnit).Include(a => a.UnitsOfMeasurementFKDiffShelflifeUnit).AsNoTracking()
            //Replaced Code Block End
            var materialsAlreadySlotted = _context.InventoryLocationsSlottingPost.Select(x => x.ixMaterial);
            _context.Materials.Where(a => !materialsAlreadySlotted.Contains(a.ixMaterial)).Include(a => a.MaterialTypes).Include(a => a.UnitsOfMeasurementFKDiffBaseUnit).Include(a => a.UnitsOfMeasurementFKDiffDensityUnit).Include(a => a.UnitsOfMeasurementFKDiffShelflifeUnit).AsNoTracking()
            //Custom Code End
                .ToList()
                .ForEach(x => materials.Add(x));
            return materials.AsQueryable();
        }
        public bool VerifyInventoryLocationSlottingUnique(Int64 ixInventoryLocationSlotting, string sInventoryLocationSlotting)
        {
            if (_context.InventoryLocationsSlotting.AsNoTracking().Where(x => x.sInventoryLocationSlotting == sInventoryLocationSlotting).Any() && ixInventoryLocationSlotting == 0L) return false;
            else if (_context.InventoryLocationsSlotting.AsNoTracking().Where(x => x.sInventoryLocationSlotting == sInventoryLocationSlotting && x.ixInventoryLocationSlotting != ixInventoryLocationSlotting).Any() && ixInventoryLocationSlotting != 0L) return false;
            else return true;
        }

        public List<string> VerifyInventoryLocationSlottingDeleteOK(Int64 ixInventoryLocationSlotting, string sInventoryLocationSlotting)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(InventoryLocationsSlottingPost inventorylocationsslottingPost)
		{
            _context.InventoryLocationsSlottingPost.Add(inventorylocationsslottingPost); 
        }

        public void RegisterEdit(InventoryLocationsSlottingPost inventorylocationsslottingPost)
        {
            _context.Entry(inventorylocationsslottingPost).State = EntityState.Modified;
        }

        public void RegisterDelete(InventoryLocationsSlottingPost inventorylocationsslottingPost)
        {
            _context.InventoryLocationsSlottingPost.Remove(inventorylocationsslottingPost);
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
  
