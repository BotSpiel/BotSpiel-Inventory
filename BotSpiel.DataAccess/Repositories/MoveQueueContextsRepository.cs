using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class MoveQueueContextsRepository : IMoveQueueContextsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly MoveQueueContextsDB _context;
       private readonly MoveQueuesDB _contextMoveQueues;
  
        public MoveQueueContextsRepository(MoveQueueContextsDB context, MoveQueuesDB contextMoveQueues)
        {
            _context = context;
           _contextMoveQueues = contextMoveQueues;
  
        }

        public MoveQueueContextsPost GetPost(Int64 ixMoveQueueContext) => _context.MoveQueueContextsPost.AsNoTracking().Where(x => x.ixMoveQueueContext == ixMoveQueueContext).First();
         
		public MoveQueueContexts Get(Int64 ixMoveQueueContext)
        {
            MoveQueueContexts movequeuecontexts = _context.MoveQueueContexts.AsNoTracking().Where(x => x.ixMoveQueueContext == ixMoveQueueContext).First();
            return movequeuecontexts;
        }

        public IQueryable<MoveQueueContexts> Index()
        {
            var movequeuecontexts = _context.MoveQueueContexts.AsNoTracking(); 
            return movequeuecontexts;
        }

        public IQueryable<MoveQueueContexts> IndexDb()
        {
            var movequeuecontexts = _context.MoveQueueContexts.AsNoTracking(); 
            return movequeuecontexts;
        }
        public bool VerifyMoveQueueContextUnique(Int64 ixMoveQueueContext, string sMoveQueueContext)
        {
            if (_context.MoveQueueContexts.AsNoTracking().Where(x => x.sMoveQueueContext == sMoveQueueContext).Any() && ixMoveQueueContext == 0L) return false;
            else if (_context.MoveQueueContexts.AsNoTracking().Where(x => x.sMoveQueueContext == sMoveQueueContext && x.ixMoveQueueContext != ixMoveQueueContext).Any() && ixMoveQueueContext != 0L) return false;
            else return true;
        }

        public List<string> VerifyMoveQueueContextDeleteOK(Int64 ixMoveQueueContext, string sMoveQueueContext)
        {
            List<string> existInEntities = new List<string>();
           if (_contextMoveQueues.MoveQueues.AsNoTracking().Where(x => x.ixMoveQueueContext == ixMoveQueueContext).Any()) existInEntities.Add("MoveQueues");

            return existInEntities;
        }


        public void RegisterCreate(MoveQueueContextsPost movequeuecontextsPost)
		{
            _context.MoveQueueContextsPost.Add(movequeuecontextsPost); 
        }

        public void RegisterEdit(MoveQueueContextsPost movequeuecontextsPost)
        {
            _context.Entry(movequeuecontextsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(MoveQueueContextsPost movequeuecontextsPost)
        {
            _context.MoveQueueContextsPost.Remove(movequeuecontextsPost);
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
  

