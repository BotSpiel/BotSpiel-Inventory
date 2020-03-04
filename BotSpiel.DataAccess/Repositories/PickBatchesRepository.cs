using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class PickBatchesRepository : IPickBatchesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly PickBatchesDB _context;
       private readonly MoveQueuesDB _contextMoveQueues;
        private readonly OutboundOrdersDB _contextOutboundOrders;
        private readonly PickBatchPickingDB _contextPickBatchPicking;
  
        public PickBatchesRepository(PickBatchesDB context, MoveQueuesDB contextMoveQueues, OutboundOrdersDB contextOutboundOrders, PickBatchPickingDB contextPickBatchPicking)
        {
            _context = context;
           _contextMoveQueues = contextMoveQueues;
            _contextOutboundOrders = contextOutboundOrders;
            _contextPickBatchPicking = contextPickBatchPicking;
  
        }

        public PickBatchesPost GetPost(Int64 ixPickBatch) => _context.PickBatchesPost.AsNoTracking().Where(x => x.ixPickBatch == ixPickBatch).First();
         
		public PickBatches Get(Int64 ixPickBatch)
        {
            PickBatches pickbatches = _context.PickBatches.AsNoTracking().Where(x => x.ixPickBatch == ixPickBatch).First();
            pickbatches.PickBatchTypes = _context.PickBatchTypes.Find(pickbatches.ixPickBatchType);
            pickbatches.Statuses = _context.Statuses.Find(pickbatches.ixStatus);

            return pickbatches;
        }

        public IQueryable<PickBatches> Index()
        {
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //var pickbatches = _context.PickBatches.Include(a => a.PickBatchTypes).Include(a => a.Statuses).AsNoTracking();
            //Replaced Code Block End
            var pickbatches = _context.PickBatches.OrderByDescending(a => a.ixPickBatch).Include(a => a.PickBatchTypes).Include(a => a.Statuses).AsNoTracking();
            //Custom Code End
            return pickbatches;
        }

        public IQueryable<PickBatches> IndexDb()
        {
            var pickbatches = _context.PickBatches.Include(a => a.PickBatchTypes).Include(a => a.Statuses).AsNoTracking(); 
            return pickbatches;
        }

        //Custom Code Start | Added Code Block 
        public IQueryable<PickBatchesPost> IndexDbPost()
        {
            var pickbatches = _context.PickBatchesPost.AsNoTracking();
            return pickbatches;
        }
        //Custom Code End

        public IQueryable<PickBatchTypes> selectPickBatchTypes()
        {
            List<PickBatchTypes> pickbatchtypes = new List<PickBatchTypes>();
            _context.PickBatchTypes.AsNoTracking()
                .ToList()
                .ForEach(x => pickbatchtypes.Add(x));
            return pickbatchtypes.AsQueryable();
        }
        public IQueryable<Statuses> selectStatuses()
        {
            List<Statuses> statuses = new List<Statuses>();

            //Custom Code Start | Added Code Block 
            List<string> allowedStatuses = new List<string>() { "Inactive", "Active", "Started", "Complete" };
            //Custom Code End

            _context.Statuses.AsNoTracking()
                .ToList()
                //Custom Code Start | Replaced Code Block
                //Replaced Code Block Start
                //.ForEach(x => statuses.Add(x));
                //Replaced Code Block End
                .ForEach(x =>
                {
                    if (allowedStatuses.Contains(x.sStatus))
                    {
                        statuses.Add(x);
                    }
                }
                );
                //Custom Code End
            return statuses.AsQueryable();
        }
       public IQueryable<PickBatchTypes> PickBatchTypesDb()
        {
            List<PickBatchTypes> pickbatchtypes = new List<PickBatchTypes>();
            _context.PickBatchTypes.AsNoTracking()
                .ToList()
                .ForEach(x => pickbatchtypes.Add(x));
            return pickbatchtypes.AsQueryable();
        }
        public IQueryable<Statuses> StatusesDb()
        {
            List<Statuses> statuses = new List<Statuses>();
            _context.Statuses.AsNoTracking()
                .ToList()
                .ForEach(x => statuses.Add(x));
            return statuses.AsQueryable();
        }
        public bool VerifyPickBatchUnique(Int64 ixPickBatch, string sPickBatch)
        {
            if (_context.PickBatches.AsNoTracking().Where(x => x.sPickBatch == sPickBatch).Any() && ixPickBatch == 0L) return false;
            else if (_context.PickBatches.AsNoTracking().Where(x => x.sPickBatch == sPickBatch && x.ixPickBatch != ixPickBatch).Any() && ixPickBatch != 0L) return false;
            else return true;
        }

        public List<string> VerifyPickBatchDeleteOK(Int64 ixPickBatch, string sPickBatch)
        {
            List<string> existInEntities = new List<string>();
           if (_contextMoveQueues.MoveQueues.AsNoTracking().Where(x => x.ixPickBatch == ixPickBatch).Any()) existInEntities.Add("MoveQueues");
            if (_contextOutboundOrders.OutboundOrders.AsNoTracking().Where(x => x.ixPickBatch == ixPickBatch).Any()) existInEntities.Add("OutboundOrders");
 
            return existInEntities;
        }


        public void RegisterCreate(PickBatchesPost pickbatchesPost)
		{
            _context.PickBatchesPost.Add(pickbatchesPost); 
        }

        public void RegisterEdit(PickBatchesPost pickbatchesPost)
        {
            _context.Entry(pickbatchesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(PickBatchesPost pickbatchesPost)
        {
            _context.PickBatchesPost.Remove(pickbatchesPost);
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
  

