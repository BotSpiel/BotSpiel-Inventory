using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class OutboundOrderLinePackingRepository : IOutboundOrderLinePackingRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly OutboundOrderLinePackingDB _context;
  
        public OutboundOrderLinePackingRepository(OutboundOrderLinePackingDB context)
        {
            _context = context;
  
        }

        public OutboundOrderLinePackingPost GetPost(Int64 ixOutboundOrderLinePack) => _context.OutboundOrderLinePackingPost.AsNoTracking().Where(x => x.ixOutboundOrderLinePack == ixOutboundOrderLinePack).First();
         
		public OutboundOrderLinePacking Get(Int64 ixOutboundOrderLinePack)
        {
            OutboundOrderLinePacking outboundorderlinepacking = _context.OutboundOrderLinePacking.AsNoTracking().Where(x => x.ixOutboundOrderLinePack == ixOutboundOrderLinePack).First();
            outboundorderlinepacking.HandlingUnits = _context.HandlingUnits.Find(outboundorderlinepacking.ixHandlingUnit);
            outboundorderlinepacking.OutboundOrderLines = _context.OutboundOrderLines.Find(outboundorderlinepacking.ixOutboundOrderLine);
            outboundorderlinepacking.Statuses = _context.Statuses.Find(outboundorderlinepacking.ixStatus);

            return outboundorderlinepacking;
        }

        public IQueryable<OutboundOrderLinePacking> Index()
        {
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //var outboundorderlinepacking = _context.OutboundOrderLinePacking.Include(a => a.OutboundOrderLines).Include(a => a.HandlingUnits).Include(a => a.Statuses).AsNoTracking();
            //Replaced Code Block End
            var outboundorderlinepacking = _context.OutboundOrderLinePacking.OrderByDescending(a => a.ixOutboundOrderLinePack).Include(a => a.OutboundOrderLines).Include(a => a.HandlingUnits).Include(a => a.Statuses).AsNoTracking();
            //Custom Code End
            return outboundorderlinepacking;
        }

        public IQueryable<OutboundOrderLinePacking> IndexDb()
        {
            var outboundorderlinepacking = _context.OutboundOrderLinePacking.Include(a => a.OutboundOrderLines).Include(a => a.HandlingUnits).Include(a => a.Statuses).AsNoTracking(); 
            return outboundorderlinepacking;
        }
       public IQueryable<HandlingUnits> selectHandlingUnits()
        {
            List<HandlingUnits> handlingunits = new List<HandlingUnits>();
            _context.HandlingUnits.Include(a => a.HandlingUnitsFKDiffParentHandlingUnit).Include(a => a.HandlingUnitTypes).Include(a => a.MaterialHandlingUnitConfigurations).Include(a => a.MaterialsFKDiffPackingMaterial).Include(a => a.Statuses).Include(a => a.UnitsOfMeasurementFKDiffHeightUnit).Include(a => a.UnitsOfMeasurementFKDiffLengthUnit).Include(a => a.UnitsOfMeasurementFKDiffWeightUnit).Include(a => a.UnitsOfMeasurementFKDiffWidthUnit).AsNoTracking()
                .ToList()
                .ForEach(x => handlingunits.Add(x));
            return handlingunits.AsQueryable();
        }
        public IQueryable<OutboundOrderLines> selectOutboundOrderLines()
        {
            List<OutboundOrderLines> outboundorderlines = new List<OutboundOrderLines>();
            _context.OutboundOrderLines.Include(a => a.Materials).Include(a => a.OutboundOrders).Include(a => a.Statuses).AsNoTracking()
                .ToList()
                .ForEach(x => outboundorderlines.Add(x));
            return outboundorderlines.AsQueryable();
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
        public IQueryable<OutboundOrderLines> OutboundOrderLinesDb()
        {
            List<OutboundOrderLines> outboundorderlines = new List<OutboundOrderLines>();
            _context.OutboundOrderLines.Include(a => a.Materials).Include(a => a.OutboundOrders).Include(a => a.Statuses).AsNoTracking()
                .ToList()
                .ForEach(x => outboundorderlines.Add(x));
            return outboundorderlines.AsQueryable();
        }
        public IQueryable<Statuses> StatusesDb()
        {
            List<Statuses> statuses = new List<Statuses>();
            _context.Statuses.AsNoTracking()
                .ToList()
                .ForEach(x => statuses.Add(x));
            return statuses.AsQueryable();
        }
        public bool VerifyOutboundOrderLinePackUnique(Int64 ixOutboundOrderLinePack, string sOutboundOrderLinePack)
        {
            if (_context.OutboundOrderLinePacking.AsNoTracking().Where(x => x.sOutboundOrderLinePack == sOutboundOrderLinePack).Any() && ixOutboundOrderLinePack == 0L) return false;
            else if (_context.OutboundOrderLinePacking.AsNoTracking().Where(x => x.sOutboundOrderLinePack == sOutboundOrderLinePack && x.ixOutboundOrderLinePack != ixOutboundOrderLinePack).Any() && ixOutboundOrderLinePack != 0L) return false;
            else return true;
        }

        public List<string> VerifyOutboundOrderLinePackDeleteOK(Int64 ixOutboundOrderLinePack, string sOutboundOrderLinePack)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(OutboundOrderLinePackingPost outboundorderlinepackingPost)
		{
            _context.OutboundOrderLinePackingPost.Add(outboundorderlinepackingPost); 
        }

        public void RegisterEdit(OutboundOrderLinePackingPost outboundorderlinepackingPost)
        {
            _context.Entry(outboundorderlinepackingPost).State = EntityState.Modified;
        }

        public void RegisterDelete(OutboundOrderLinePackingPost outboundorderlinepackingPost)
        {
            _context.OutboundOrderLinePackingPost.Remove(outboundorderlinepackingPost);
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
  

