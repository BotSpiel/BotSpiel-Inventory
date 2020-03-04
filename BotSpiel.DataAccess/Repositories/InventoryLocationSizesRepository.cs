using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class InventoryLocationSizesRepository : IInventoryLocationSizesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly InventoryLocationSizesDB _context;
       private readonly FacilityAisleFacesDB _contextFacilityAisleFaces;
        private readonly InventoryLocationsDB _contextInventoryLocations;
  
        public InventoryLocationSizesRepository(InventoryLocationSizesDB context, FacilityAisleFacesDB contextFacilityAisleFaces, InventoryLocationsDB contextInventoryLocations)
        {
            _context = context;
           _contextFacilityAisleFaces = contextFacilityAisleFaces;
            _contextInventoryLocations = contextInventoryLocations;
  
        }

        public InventoryLocationSizesPost GetPost(Int64 ixInventoryLocationSize) => _context.InventoryLocationSizesPost.AsNoTracking().Where(x => x.ixInventoryLocationSize == ixInventoryLocationSize).First();
         
		public InventoryLocationSizes Get(Int64 ixInventoryLocationSize)
        {
            InventoryLocationSizes inventorylocationsizes = _context.InventoryLocationSizes.AsNoTracking().Where(x => x.ixInventoryLocationSize == ixInventoryLocationSize).First();
            inventorylocationsizes.UnitsOfMeasurementFKDiffHeightUnit = _context.UnitsOfMeasurement.Find(inventorylocationsizes.ixHeightUnit);
            inventorylocationsizes.UnitsOfMeasurementFKDiffLengthUnit = _context.UnitsOfMeasurement.Find(inventorylocationsizes.ixLengthUnit);
            inventorylocationsizes.UnitsOfMeasurementFKDiffUsableVolumeUnit = _context.UnitsOfMeasurement.Find(inventorylocationsizes.ixUsableVolumeUnit);
            inventorylocationsizes.UnitsOfMeasurementFKDiffWidthUnit = _context.UnitsOfMeasurement.Find(inventorylocationsizes.ixWidthUnit);

            return inventorylocationsizes;
        }

        public IQueryable<InventoryLocationSizes> Index()
        {
            var inventorylocationsizes = _context.InventoryLocationSizes.Include(a => a.UnitsOfMeasurementFKDiffLengthUnit).Include(a => a.UnitsOfMeasurementFKDiffWidthUnit).Include(a => a.UnitsOfMeasurementFKDiffHeightUnit).Include(a => a.UnitsOfMeasurementFKDiffUsableVolumeUnit).AsNoTracking(); 
            return inventorylocationsizes;
        }

        public IQueryable<InventoryLocationSizes> IndexDb()
        {
            var inventorylocationsizes = _context.InventoryLocationSizes.Include(a => a.UnitsOfMeasurementFKDiffLengthUnit).Include(a => a.UnitsOfMeasurementFKDiffWidthUnit).Include(a => a.UnitsOfMeasurementFKDiffHeightUnit).Include(a => a.UnitsOfMeasurementFKDiffUsableVolumeUnit).AsNoTracking(); 
            return inventorylocationsizes;
        }
       public IQueryable<UnitsOfMeasurement> selectUnitsOfMeasurement()
        {
            List<UnitsOfMeasurement> unitsofmeasurement = new List<UnitsOfMeasurement>();
            _context.UnitsOfMeasurement.Include(a => a.MeasurementSystems).Include(a => a.MeasurementUnitsOf).AsNoTracking()
                .ToList()
                .ForEach(x => unitsofmeasurement.Add(x));
            return unitsofmeasurement.AsQueryable();
        }
       public IQueryable<UnitsOfMeasurement> UnitsOfMeasurementDb()
        {
            List<UnitsOfMeasurement> unitsofmeasurement = new List<UnitsOfMeasurement>();
            _context.UnitsOfMeasurement.Include(a => a.MeasurementSystems).Include(a => a.MeasurementUnitsOf).AsNoTracking()
                .ToList()
                .ForEach(x => unitsofmeasurement.Add(x));
            return unitsofmeasurement.AsQueryable();
        }
        public bool VerifyInventoryLocationSizeUnique(Int64 ixInventoryLocationSize, string sInventoryLocationSize)
        {
            if (_context.InventoryLocationSizes.AsNoTracking().Where(x => x.sInventoryLocationSize == sInventoryLocationSize).Any() && ixInventoryLocationSize == 0L) return false;
            else if (_context.InventoryLocationSizes.AsNoTracking().Where(x => x.sInventoryLocationSize == sInventoryLocationSize && x.ixInventoryLocationSize != ixInventoryLocationSize).Any() && ixInventoryLocationSize != 0L) return false;
            else return true;
        }

        public List<string> VerifyInventoryLocationSizeDeleteOK(Int64 ixInventoryLocationSize, string sInventoryLocationSize)
        {
            List<string> existInEntities = new List<string>();
           if (_contextFacilityAisleFaces.FacilityAisleFaces.AsNoTracking().Where(x => x.ixDefaultInventoryLocationSize == ixInventoryLocationSize).Any()) existInEntities.Add("FacilityAisleFaces");
            if (_contextInventoryLocations.InventoryLocations.AsNoTracking().Where(x => x.ixInventoryLocationSize == ixInventoryLocationSize).Any()) existInEntities.Add("InventoryLocations");

            return existInEntities;
        }


        public void RegisterCreate(InventoryLocationSizesPost inventorylocationsizesPost)
		{
            _context.InventoryLocationSizesPost.Add(inventorylocationsizesPost); 
        }

        public void RegisterEdit(InventoryLocationSizesPost inventorylocationsizesPost)
        {
            _context.Entry(inventorylocationsizesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(InventoryLocationSizesPost inventorylocationsizesPost)
        {
            _context.InventoryLocationSizesPost.Remove(inventorylocationsizesPost);
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
  

