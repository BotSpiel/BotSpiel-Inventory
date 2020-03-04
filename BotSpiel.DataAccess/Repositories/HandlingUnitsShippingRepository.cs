using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class HandlingUnitsShippingRepository : IHandlingUnitsShippingRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly HandlingUnitsShippingDB _context;
  
        public HandlingUnitsShippingRepository(HandlingUnitsShippingDB context)
        {
            _context = context;
  
        }

        public HandlingUnitsShippingPost GetPost(Int64 ixHandlingUnitShipping) => _context.HandlingUnitsShippingPost.AsNoTracking().Where(x => x.ixHandlingUnitShipping == ixHandlingUnitShipping).First();
         
		public HandlingUnitsShipping Get(Int64 ixHandlingUnitShipping)
        {
            HandlingUnitsShipping handlingunitsshipping = _context.HandlingUnitsShipping.AsNoTracking().Where(x => x.ixHandlingUnitShipping == ixHandlingUnitShipping).First();
            handlingunitsshipping.HandlingUnits = _context.HandlingUnits.Find(handlingunitsshipping.ixHandlingUnit);
            handlingunitsshipping.Statuses = _context.Statuses.Find(handlingunitsshipping.ixStatus);

            return handlingunitsshipping;
        }

        public IQueryable<HandlingUnitsShipping> Index()
        {
            var handlingunitsshipping = _context.HandlingUnitsShipping.Include(a => a.HandlingUnits).Include(a => a.Statuses).AsNoTracking(); 
            return handlingunitsshipping;
        }

        public IQueryable<HandlingUnitsShipping> IndexDb()
        {
            var handlingunitsshipping = _context.HandlingUnitsShipping.Include(a => a.HandlingUnits).Include(a => a.Statuses).AsNoTracking(); 
            return handlingunitsshipping;
        }
       public IQueryable<HandlingUnits> selectHandlingUnits()
        {
            List<HandlingUnits> handlingunits = new List<HandlingUnits>();
            _context.HandlingUnits.Include(a => a.HandlingUnitsFKDiffParentHandlingUnit).Include(a => a.HandlingUnitTypes).Include(a => a.MaterialHandlingUnitConfigurations).Include(a => a.MaterialsFKDiffPackingMaterial).Include(a => a.Statuses).Include(a => a.UnitsOfMeasurementFKDiffHeightUnit).Include(a => a.UnitsOfMeasurementFKDiffLengthUnit).Include(a => a.UnitsOfMeasurementFKDiffWeightUnit).Include(a => a.UnitsOfMeasurementFKDiffWidthUnit).AsNoTracking()
                .ToList()
                .ForEach(x => handlingunits.Add(x));
            return handlingunits.AsQueryable();
        }
        public IQueryable<Statuses> selectStatuses()
        {
            List<Statuses> statuses = new List<Statuses>();
            _context.Statuses.AsNoTracking()
                .ToList()
                .ForEach(x => statuses.Add(x));
            return statuses.AsQueryable();
        }
       public IQueryable<HandlingUnits> HandlingUnitsDb()
        {
            List<HandlingUnits> handlingunits = new List<HandlingUnits>();
            _context.HandlingUnits.Include(a => a.HandlingUnitsFKDiffParentHandlingUnit).Include(a => a.HandlingUnitTypes).Include(a => a.MaterialHandlingUnitConfigurations).Include(a => a.MaterialsFKDiffPackingMaterial).Include(a => a.Statuses).Include(a => a.UnitsOfMeasurementFKDiffHeightUnit).Include(a => a.UnitsOfMeasurementFKDiffLengthUnit).Include(a => a.UnitsOfMeasurementFKDiffWeightUnit).Include(a => a.UnitsOfMeasurementFKDiffWidthUnit).AsNoTracking()
                .ToList()
                .ForEach(x => handlingunits.Add(x));
            return handlingunits.AsQueryable();
        }
        public IQueryable<Statuses> StatusesDb()
        {
            List<Statuses> statuses = new List<Statuses>();
            _context.Statuses.AsNoTracking()
                .ToList()
                .ForEach(x => statuses.Add(x));
            return statuses.AsQueryable();
        }
        public bool VerifyHandlingUnitShippingUnique(Int64 ixHandlingUnitShipping, string sHandlingUnitShipping)
        {
            if (_context.HandlingUnitsShipping.AsNoTracking().Where(x => x.sHandlingUnitShipping == sHandlingUnitShipping).Any() && ixHandlingUnitShipping == 0L) return false;
            else if (_context.HandlingUnitsShipping.AsNoTracking().Where(x => x.sHandlingUnitShipping == sHandlingUnitShipping && x.ixHandlingUnitShipping != ixHandlingUnitShipping).Any() && ixHandlingUnitShipping != 0L) return false;
            else return true;
        }

        public List<string> VerifyHandlingUnitShippingDeleteOK(Int64 ixHandlingUnitShipping, string sHandlingUnitShipping)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(HandlingUnitsShippingPost handlingunitsshippingPost)
		{
            _context.HandlingUnitsShippingPost.Add(handlingunitsshippingPost); 
        }

        public void RegisterEdit(HandlingUnitsShippingPost handlingunitsshippingPost)
        {
            _context.Entry(handlingunitsshippingPost).State = EntityState.Modified;
        }

        public void RegisterDelete(HandlingUnitsShippingPost handlingunitsshippingPost)
        {
            _context.HandlingUnitsShippingPost.Remove(handlingunitsshippingPost);
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
  

