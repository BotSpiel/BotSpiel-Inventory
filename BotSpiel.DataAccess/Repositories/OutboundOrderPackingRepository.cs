using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class OutboundOrderPackingRepository : IOutboundOrderPackingRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly OutboundOrderPackingDB _context;
  
        public OutboundOrderPackingRepository(OutboundOrderPackingDB context)
        {
            _context = context;
  
        }

        public OutboundOrderPackingPost GetPost(Int64 ixOutboundOrderPack) => _context.OutboundOrderPackingPost.AsNoTracking().Where(x => x.ixOutboundOrderPack == ixOutboundOrderPack).First();
         
		public OutboundOrderPacking Get(Int64 ixOutboundOrderPack)
        {
            OutboundOrderPacking outboundorderpacking = _context.OutboundOrderPacking.AsNoTracking().Where(x => x.ixOutboundOrderPack == ixOutboundOrderPack).First();
            outboundorderpacking.HandlingUnits = _context.HandlingUnits.Find(outboundorderpacking.ixHandlingUnit);
            outboundorderpacking.OutboundOrderLines = _context.OutboundOrderLines.Find(outboundorderpacking.ixOutboundOrderLine);
            outboundorderpacking.Statuses = _context.Statuses.Find(outboundorderpacking.ixStatus);

            return outboundorderpacking;
        }

        public IQueryable<OutboundOrderPacking> Index()
        {
            var outboundorderpacking = _context.OutboundOrderPacking.Include(a => a.OutboundOrderLines).Include(a => a.HandlingUnits).Include(a => a.Statuses).AsNoTracking(); 
            return outboundorderpacking;
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
            _context.OutboundOrderLines.Include(a => a.Materials).Include(a => a.Statuses).AsNoTracking()
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
        public bool VerifyOutboundOrderPackUnique(Int64 ixOutboundOrderPack, string sOutboundOrderPack)
        {
            if (_context.OutboundOrderPacking.AsNoTracking().Where(x => x.sOutboundOrderPack == sOutboundOrderPack).Any() && ixOutboundOrderPack == 0L) return false;
            else if (_context.OutboundOrderPacking.AsNoTracking().Where(x => x.sOutboundOrderPack == sOutboundOrderPack && x.ixOutboundOrderPack != ixOutboundOrderPack).Any() && ixOutboundOrderPack != 0L) return false;
            else return true;
        }

        public List<string> VerifyOutboundOrderPackDeleteOK(Int64 ixOutboundOrderPack, string sOutboundOrderPack)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(OutboundOrderPackingPost outboundorderpackingPost)
		{
            _context.OutboundOrderPackingPost.Add(outboundorderpackingPost); 
        }

        public void RegisterEdit(OutboundOrderPackingPost outboundorderpackingPost)
        {
            _context.Entry(outboundorderpackingPost).State = EntityState.Modified;
        }

        public void RegisterDelete(OutboundOrderPackingPost outboundorderpackingPost)
        {
            _context.OutboundOrderPackingPost.Remove(outboundorderpackingPost);
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
  

