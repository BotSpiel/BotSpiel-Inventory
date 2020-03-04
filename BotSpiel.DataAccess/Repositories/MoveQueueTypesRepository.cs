using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class MoveQueueTypesRepository : IMoveQueueTypesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly MoveQueueTypesDB _context;
       private readonly MoveQueuesDB _contextMoveQueues;
  
        public MoveQueueTypesRepository(MoveQueueTypesDB context, MoveQueuesDB contextMoveQueues)
        {
            _context = context;
           _contextMoveQueues = contextMoveQueues;
  
        }

        public MoveQueueTypesPost GetPost(Int64 ixMoveQueueType) => _context.MoveQueueTypesPost.AsNoTracking().Where(x => x.ixMoveQueueType == ixMoveQueueType).First();
         
		public MoveQueueTypes Get(Int64 ixMoveQueueType)
        {
            MoveQueueTypes movequeuetypes = _context.MoveQueueTypes.AsNoTracking().Where(x => x.ixMoveQueueType == ixMoveQueueType).First();
            return movequeuetypes;
        }

        public IQueryable<MoveQueueTypes> Index()
        {
            var movequeuetypes = _context.MoveQueueTypes.AsNoTracking(); 
            return movequeuetypes;
        }

        public IQueryable<MoveQueueTypes> IndexDb()
        {
            var movequeuetypes = _context.MoveQueueTypes.AsNoTracking(); 
            return movequeuetypes;
        }
        public bool VerifyMoveQueueTypeUnique(Int64 ixMoveQueueType, string sMoveQueueType)
        {
            if (_context.MoveQueueTypes.AsNoTracking().Where(x => x.sMoveQueueType == sMoveQueueType).Any() && ixMoveQueueType == 0L) return false;
            else if (_context.MoveQueueTypes.AsNoTracking().Where(x => x.sMoveQueueType == sMoveQueueType && x.ixMoveQueueType != ixMoveQueueType).Any() && ixMoveQueueType != 0L) return false;
            else return true;
        }

        public List<string> VerifyMoveQueueTypeDeleteOK(Int64 ixMoveQueueType, string sMoveQueueType)
        {
            List<string> existInEntities = new List<string>();
           if (_contextMoveQueues.MoveQueues.AsNoTracking().Where(x => x.ixMoveQueueType == ixMoveQueueType).Any()) existInEntities.Add("MoveQueues");

            return existInEntities;
        }


        public void RegisterCreate(MoveQueueTypesPost movequeuetypesPost)
		{
            _context.MoveQueueTypesPost.Add(movequeuetypesPost); 
        }

        public void RegisterEdit(MoveQueueTypesPost movequeuetypesPost)
        {
            _context.Entry(movequeuetypesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(MoveQueueTypesPost movequeuetypesPost)
        {
            _context.MoveQueueTypesPost.Remove(movequeuetypesPost);
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
  

