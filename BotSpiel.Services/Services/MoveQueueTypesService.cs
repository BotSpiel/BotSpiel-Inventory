using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class MoveQueueTypesService : IMoveQueueTypesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IMoveQueueTypesRepository _movequeuetypesRepository;

        public MoveQueueTypesService(IMoveQueueTypesRepository movequeuetypesRepository)
        {
            _movequeuetypesRepository = movequeuetypesRepository;
        }

        public MoveQueueTypesPost GetPost(Int64 ixMoveQueueType) => _movequeuetypesRepository.GetPost(ixMoveQueueType);
        public MoveQueueTypes Get(Int64 ixMoveQueueType) => _movequeuetypesRepository.Get(ixMoveQueueType);
        public IQueryable<MoveQueueTypes> Index() => _movequeuetypesRepository.Index();
        public IQueryable<MoveQueueTypes> IndexDb() => _movequeuetypesRepository.IndexDb();
        public bool VerifyMoveQueueTypeUnique(Int64 ixMoveQueueType, string sMoveQueueType) => _movequeuetypesRepository.VerifyMoveQueueTypeUnique(ixMoveQueueType, sMoveQueueType);
        public List<string> VerifyMoveQueueTypeDeleteOK(Int64 ixMoveQueueType, string sMoveQueueType) => _movequeuetypesRepository.VerifyMoveQueueTypeDeleteOK(ixMoveQueueType, sMoveQueueType);

        public Task<Int64> Create(MoveQueueTypesPost movequeuetypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._movequeuetypesRepository.RegisterCreate(movequeuetypesPost);
            try
            {
                this._movequeuetypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._movequeuetypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(movequeuetypesPost.ixMoveQueueType);

        }
        public Task Edit(MoveQueueTypesPost movequeuetypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._movequeuetypesRepository.RegisterEdit(movequeuetypesPost);
            try
            {
                this._movequeuetypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._movequeuetypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(MoveQueueTypesPost movequeuetypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._movequeuetypesRepository.RegisterDelete(movequeuetypesPost);
            try
            {
                this._movequeuetypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._movequeuetypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

