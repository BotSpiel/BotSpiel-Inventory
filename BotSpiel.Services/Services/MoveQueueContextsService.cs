using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class MoveQueueContextsService : IMoveQueueContextsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IMoveQueueContextsRepository _movequeuecontextsRepository;

        public MoveQueueContextsService(IMoveQueueContextsRepository movequeuecontextsRepository)
        {
            _movequeuecontextsRepository = movequeuecontextsRepository;
        }

        public MoveQueueContextsPost GetPost(Int64 ixMoveQueueContext) => _movequeuecontextsRepository.GetPost(ixMoveQueueContext);
        public MoveQueueContexts Get(Int64 ixMoveQueueContext) => _movequeuecontextsRepository.Get(ixMoveQueueContext);
        public IQueryable<MoveQueueContexts> Index() => _movequeuecontextsRepository.Index();
        public IQueryable<MoveQueueContexts> IndexDb() => _movequeuecontextsRepository.IndexDb();
        public bool VerifyMoveQueueContextUnique(Int64 ixMoveQueueContext, string sMoveQueueContext) => _movequeuecontextsRepository.VerifyMoveQueueContextUnique(ixMoveQueueContext, sMoveQueueContext);
        public List<string> VerifyMoveQueueContextDeleteOK(Int64 ixMoveQueueContext, string sMoveQueueContext) => _movequeuecontextsRepository.VerifyMoveQueueContextDeleteOK(ixMoveQueueContext, sMoveQueueContext);

        public Task<Int64> Create(MoveQueueContextsPost movequeuecontextsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._movequeuecontextsRepository.RegisterCreate(movequeuecontextsPost);
            try
            {
                this._movequeuecontextsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._movequeuecontextsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(movequeuecontextsPost.ixMoveQueueContext);

        }
        public Task Edit(MoveQueueContextsPost movequeuecontextsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._movequeuecontextsRepository.RegisterEdit(movequeuecontextsPost);
            try
            {
                this._movequeuecontextsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._movequeuecontextsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(MoveQueueContextsPost movequeuecontextsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._movequeuecontextsRepository.RegisterDelete(movequeuecontextsPost);
            try
            {
                this._movequeuecontextsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._movequeuecontextsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

