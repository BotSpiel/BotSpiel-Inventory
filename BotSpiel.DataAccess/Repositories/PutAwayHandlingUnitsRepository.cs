using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class PutAwayHandlingUnitsRepository : IPutAwayHandlingUnitsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly PutAwayHandlingUnitsDB _context;
  
        public PutAwayHandlingUnitsRepository(PutAwayHandlingUnitsDB context)
        {
            _context = context;
  
        }

        public PutAwayHandlingUnitsPost GetPost(Int64 ixPutAwayHandlingUnit) => _context.PutAwayHandlingUnitsPost.AsNoTracking().Where(x => x.ixPutAwayHandlingUnit == ixPutAwayHandlingUnit).First();
         
		public PutAwayHandlingUnits Get(Int64 ixPutAwayHandlingUnit)
        {
            PutAwayHandlingUnits putawayhandlingunits = _context.PutAwayHandlingUnits.AsNoTracking().Where(x => x.ixPutAwayHandlingUnit == ixPutAwayHandlingUnit).First();
            putawayhandlingunits.HandlingUnits = _context.HandlingUnits.Find(putawayhandlingunits.ixHandlingUnit);
            putawayhandlingunits.InventoryLocations = _context.InventoryLocations.Find(putawayhandlingunits.ixInventoryLocation);

            return putawayhandlingunits;
        }

        public IQueryable<PutAwayHandlingUnits> Index()
        {
            var putawayhandlingunits = _context.PutAwayHandlingUnits.Include(a => a.HandlingUnits).Include(a => a.InventoryLocations).AsNoTracking(); 
            return putawayhandlingunits;
        }

        public IQueryable<PutAwayHandlingUnits> IndexDb()
        {
            var putawayhandlingunits = _context.PutAwayHandlingUnits.Include(a => a.HandlingUnits).Include(a => a.InventoryLocations).AsNoTracking(); 
            return putawayhandlingunits;
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
            _context.InventoryLocations.Include(a => a.Companies).Include(a => a.Facilities).Include(a => a.FacilityAisleFaces).Include(a => a.FacilityFloors).Include(a => a.FacilityWorkAreas).Include(a => a.FacilityZones).Include(a => a.InventoryLocationSizes).Include(a => a.LocationFunctions).Include(a => a.UnitsOfMeasurementFKDiffXOffsetUnit).Include(a => a.UnitsOfMeasurementFKDiffYOffsetUnit).Include(a => a.UnitsOfMeasurementFKDiffZOffsetUnit).AsNoTracking()
                .ToList()
                .ForEach(x => inventorylocations.Add(x));
            return inventorylocations.AsQueryable();
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
            _context.InventoryLocations.Include(a => a.Companies).Include(a => a.Facilities).Include(a => a.FacilityAisleFaces).Include(a => a.FacilityFloors).Include(a => a.FacilityWorkAreas).Include(a => a.FacilityZones).Include(a => a.InventoryLocationSizes).Include(a => a.LocationFunctions).Include(a => a.UnitsOfMeasurementFKDiffXOffsetUnit).Include(a => a.UnitsOfMeasurementFKDiffYOffsetUnit).Include(a => a.UnitsOfMeasurementFKDiffZOffsetUnit).AsNoTracking()
                .ToList()
                .ForEach(x => inventorylocations.Add(x));
            return inventorylocations.AsQueryable();
        }
        public List<string> VerifyPutAwayHandlingUnitDeleteOK(Int64 ixPutAwayHandlingUnit, string sPutAwayHandlingUnit)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(PutAwayHandlingUnitsPost putawayhandlingunitsPost)
		{
            _context.PutAwayHandlingUnitsPost.Add(putawayhandlingunitsPost); 
        }

        public void RegisterEdit(PutAwayHandlingUnitsPost putawayhandlingunitsPost)
        {
            _context.Entry(putawayhandlingunitsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(PutAwayHandlingUnitsPost putawayhandlingunitsPost)
        {
            _context.PutAwayHandlingUnitsPost.Remove(putawayhandlingunitsPost);
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
  

