using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;
//Custom Code Start | Added Code Block 
using BotSpiel.DataAccess.Utilities;
//Custom Code End

namespace BotSpiel.DataAccess.Repositories
{

    public class OutboundOrderLinesInventoryAllocationRepository : IOutboundOrderLinesInventoryAllocationRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly OutboundOrderLinesInventoryAllocationDB _context;
        //Custom Code Start | Added Code Block 
        private readonly CommonLookUpsRepository _commonLookUpsRepository;
        //Custom Code End

        //Custom Code Start | Replaced Code Block
        //Replaced Code Block Start
        public OutboundOrderLinesInventoryAllocationRepository(OutboundOrderLinesInventoryAllocationDB context)
        //Replaced Code Block End
        //public OutboundOrderLinesInventoryAllocationRepository(OutboundOrderLinesInventoryAllocationDB context, CommonLookUpsRepository commonLookUpsRepository)
        //Custom Code End
        {
            _context = context;
            //_commonLookUpsRepository = commonLookUpsRepository;
        }

        public OutboundOrderLinesInventoryAllocationPost GetPost(Int64 ixOutboundOrderLineInventoryAllocation) => _context.OutboundOrderLinesInventoryAllocationPost.AsNoTracking().Where(x => x.ixOutboundOrderLineInventoryAllocation == ixOutboundOrderLineInventoryAllocation).First();
         
		public OutboundOrderLinesInventoryAllocation Get(Int64 ixOutboundOrderLineInventoryAllocation)
        {
            OutboundOrderLinesInventoryAllocation outboundorderlinesinventoryallocation = _context.OutboundOrderLinesInventoryAllocation.AsNoTracking().Where(x => x.ixOutboundOrderLineInventoryAllocation == ixOutboundOrderLineInventoryAllocation).First();
            outboundorderlinesinventoryallocation.OutboundOrderLines = _context.OutboundOrderLines.Find(outboundorderlinesinventoryallocation.ixOutboundOrderLine);
            outboundorderlinesinventoryallocation.Statuses = _context.Statuses.Find(outboundorderlinesinventoryallocation.ixStatus);

            return outboundorderlinesinventoryallocation;
        }

        public IQueryable<OutboundOrderLinesInventoryAllocation> Index()
        {
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //var outboundorderlinesinventoryallocation = _context.OutboundOrderLinesInventoryAllocation.Include(a => a.OutboundOrderLines).Include(a => a.Statuses).AsNoTracking();
            //Replaced Code Block End
            var outboundorderlinesinventoryallocation = _context.OutboundOrderLinesInventoryAllocation.OrderByDescending(a => a.ixOutboundOrderLineInventoryAllocation).Include(a => a.OutboundOrderLines).Include(a => a.Statuses).Where(a => a.Statuses.sStatus != "Complete").AsNoTracking();
            //Custom Code End
            return outboundorderlinesinventoryallocation;
        }

        public IQueryable<OutboundOrderLinesInventoryAllocation> IndexDb()
        {
            var outboundorderlinesinventoryallocation = _context.OutboundOrderLinesInventoryAllocation.Include(a => a.OutboundOrderLines).Include(a => a.Statuses).AsNoTracking();
            return outboundorderlinesinventoryallocation;
        }

        //Custom Code Start | Added Code Block 
        public IQueryable<OutboundOrderLinesInventoryAllocationPost> IndexDbPost()
        {
            var outboundorderlinesinventoryallocation = _context.OutboundOrderLinesInventoryAllocationPost.AsNoTracking();
            return outboundorderlinesinventoryallocation;
        }
        //Custom Code End

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
        public bool VerifyOutboundOrderLineInventoryAllocationUnique(Int64 ixOutboundOrderLineInventoryAllocation, string sOutboundOrderLineInventoryAllocation)
        {
            if (_context.OutboundOrderLinesInventoryAllocation.AsNoTracking().Where(x => x.sOutboundOrderLineInventoryAllocation == sOutboundOrderLineInventoryAllocation).Any() && ixOutboundOrderLineInventoryAllocation == 0L) return false;
            else if (_context.OutboundOrderLinesInventoryAllocation.AsNoTracking().Where(x => x.sOutboundOrderLineInventoryAllocation == sOutboundOrderLineInventoryAllocation && x.ixOutboundOrderLineInventoryAllocation != ixOutboundOrderLineInventoryAllocation).Any() && ixOutboundOrderLineInventoryAllocation != 0L) return false;
            else return true;
        }

        public List<string> VerifyOutboundOrderLineInventoryAllocationDeleteOK(Int64 ixOutboundOrderLineInventoryAllocation, string sOutboundOrderLineInventoryAllocation)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(OutboundOrderLinesInventoryAllocationPost outboundorderlinesinventoryallocationPost)
		{
            _context.OutboundOrderLinesInventoryAllocationPost.Add(outboundorderlinesinventoryallocationPost); 
        }

        public void RegisterEdit(OutboundOrderLinesInventoryAllocationPost outboundorderlinesinventoryallocationPost)
        {
            _context.Entry(outboundorderlinesinventoryallocationPost).State = EntityState.Modified;
        }

        public void RegisterDelete(OutboundOrderLinesInventoryAllocationPost outboundorderlinesinventoryallocationPost)
        {
            _context.OutboundOrderLinesInventoryAllocationPost.Remove(outboundorderlinesinventoryallocationPost);
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
  

