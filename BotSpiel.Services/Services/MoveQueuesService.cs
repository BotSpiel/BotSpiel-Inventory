using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class MoveQueuesService : IMoveQueuesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IMoveQueuesRepository _movequeuesRepository;

        public MoveQueuesService(IMoveQueuesRepository movequeuesRepository)
        {
            _movequeuesRepository = movequeuesRepository;
        }

        public MoveQueuesPost GetPost(Int64 ixMoveQueue) => _movequeuesRepository.GetPost(ixMoveQueue);
        public MoveQueues Get(Int64 ixMoveQueue) => _movequeuesRepository.Get(ixMoveQueue);
        public IQueryable<MoveQueues> Index() => _movequeuesRepository.Index();
       public IQueryable<Statuses> selectStatuses() => _movequeuesRepository.selectStatuses();
        public IQueryable<InventoryUnits> selectInventoryUnits() => _movequeuesRepository.selectInventoryUnits();
        public IQueryable<HandlingUnits> selectHandlingUnits() => _movequeuesRepository.selectHandlingUnits();
        public IQueryable<InventoryLocations> selectInventoryLocations() => _movequeuesRepository.selectInventoryLocations();
        public IQueryable<MoveQueueContexts> selectMoveQueueContexts() => _movequeuesRepository.selectMoveQueueContexts();
        public IQueryable<MoveQueueTypes> selectMoveQueueTypes() => _movequeuesRepository.selectMoveQueueTypes();
        public IQueryable<InboundOrderLines> selectInboundOrderLines() => _movequeuesRepository.selectInboundOrderLines();
        public IQueryable<OutboundOrderLines> selectOutboundOrderLines() => _movequeuesRepository.selectOutboundOrderLines();
        public IQueryable<PickBatches> selectPickBatches() => _movequeuesRepository.selectPickBatches();
       public List<KeyValuePair<Int64?, string>> selectInventoryUnitsNullable() => _movequeuesRepository.selectInventoryUnitsNullable();
        public List<KeyValuePair<Int64?, string>> selectHandlingUnitsNullable() => _movequeuesRepository.selectHandlingUnitsNullable();
        public List<KeyValuePair<Int64?, string>> selectInventoryLocationsNullable() => _movequeuesRepository.selectInventoryLocationsNullable();
        public List<KeyValuePair<Int64?, string>> selectInboundOrderLinesNullable() => _movequeuesRepository.selectInboundOrderLinesNullable();
        public List<KeyValuePair<Int64?, string>> selectOutboundOrderLinesNullable() => _movequeuesRepository.selectOutboundOrderLinesNullable();
        public List<KeyValuePair<Int64?, string>> selectPickBatchesNullable() => _movequeuesRepository.selectPickBatchesNullable();
        public bool VerifyMoveQueueUnique(Int64 ixMoveQueue, string sMoveQueue) => _movequeuesRepository.VerifyMoveQueueUnique(ixMoveQueue, sMoveQueue);
        public List<string> VerifyMoveQueueDeleteOK(Int64 ixMoveQueue, string sMoveQueue) => _movequeuesRepository.VerifyMoveQueueDeleteOK(ixMoveQueue, sMoveQueue);

        public Task<Int64> Create(MoveQueuesPost movequeuesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._movequeuesRepository.RegisterCreate(movequeuesPost);
            try
            {
                this._movequeuesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._movequeuesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(movequeuesPost.ixMoveQueue);

        }
        public Task Edit(MoveQueuesPost movequeuesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._movequeuesRepository.RegisterEdit(movequeuesPost);
            try
            {
                this._movequeuesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._movequeuesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(MoveQueuesPost movequeuesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._movequeuesRepository.RegisterDelete(movequeuesPost);
            try
            {
                this._movequeuesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._movequeuesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

