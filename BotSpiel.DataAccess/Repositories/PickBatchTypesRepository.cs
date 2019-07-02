using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class PickBatchTypesRepository : IPickBatchTypesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly PickBatchTypesDB _context;
       private readonly PickBatchesDB _contextPickBatches;
  
        public PickBatchTypesRepository(PickBatchTypesDB context, PickBatchesDB contextPickBatches)
        {
            _context = context;
           _contextPickBatches = contextPickBatches;
  
        }

        public PickBatchTypesPost GetPost(Int64 ixPickBatchType) => _context.PickBatchTypesPost.AsNoTracking().Where(x => x.ixPickBatchType == ixPickBatchType).First();
         
		public PickBatchTypes Get(Int64 ixPickBatchType)
        {
            PickBatchTypes pickbatchtypes = _context.PickBatchTypes.AsNoTracking().Where(x => x.ixPickBatchType == ixPickBatchType).First();
            return pickbatchtypes;
        }

        public IQueryable<PickBatchTypes> Index()
        {
            var pickbatchtypes = _context.PickBatchTypes.AsNoTracking(); 
            return pickbatchtypes;
        }
        public bool VerifyPickBatchTypeUnique(Int64 ixPickBatchType, string sPickBatchType)
        {
            if (_context.PickBatchTypes.AsNoTracking().Where(x => x.sPickBatchType == sPickBatchType).Any() && ixPickBatchType == 0L) return false;
            else if (_context.PickBatchTypes.AsNoTracking().Where(x => x.sPickBatchType == sPickBatchType && x.ixPickBatchType != ixPickBatchType).Any() && ixPickBatchType != 0L) return false;
            else return true;
        }

        public List<string> VerifyPickBatchTypeDeleteOK(Int64 ixPickBatchType, string sPickBatchType)
        {
            List<string> existInEntities = new List<string>();
           if (_contextPickBatches.PickBatches.AsNoTracking().Where(x => x.ixPickBatchType == ixPickBatchType).Any()) existInEntities.Add("PickBatches");

            return existInEntities;
        }


        public void RegisterCreate(PickBatchTypesPost pickbatchtypesPost)
		{
            _context.PickBatchTypesPost.Add(pickbatchtypesPost); 
        }

        public void RegisterEdit(PickBatchTypesPost pickbatchtypesPost)
        {
            _context.Entry(pickbatchtypesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(PickBatchTypesPost pickbatchtypesPost)
        {
            _context.PickBatchTypesPost.Remove(pickbatchtypesPost);
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
  

