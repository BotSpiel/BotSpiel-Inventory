using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;
//Custom Code Start | Added Code Block 
using BotSpiel.Services.Utilities;
using BotSpiel.DataAccess.Utilities;
//Custom Code End

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
        //Custom Code Start | Added Code Block 
        private readonly CommonLookUps _commonLookUps;
        private readonly IInventoryLocationsService _inventorylocationsService;
        private readonly IInventoryUnitsService _inventoryunitsService;
        private readonly VolumeAndWeight _volumeAndWeight;
        //Custom Code End
        //Custom Code Start | Replaced Code Block
        //Replaced Code Block Start
        //public MoveQueuesService(IMoveQueuesRepository movequeuesRepository)
        //Replaced Code Block End
        public MoveQueuesService(IMoveQueuesRepository movequeuesRepository, CommonLookUps commonLookUps, IInventoryLocationsService inventorylocationsService, IInventoryUnitsService inventoryunitsService, VolumeAndWeight volumeAndWeight)
        //Custom Code End
        {
            _movequeuesRepository = movequeuesRepository;
            //Custom Code Start | Added Code Block 
            _commonLookUps = commonLookUps;
            _inventorylocationsService = inventorylocationsService;
            _inventoryunitsService = inventoryunitsService;
            _volumeAndWeight = volumeAndWeight;
            //Custom Code End
        }

        public MoveQueuesPost GetPost(Int64 ixMoveQueue) => _movequeuesRepository.GetPost(ixMoveQueue);
        public MoveQueues Get(Int64 ixMoveQueue) => _movequeuesRepository.Get(ixMoveQueue);
        public IQueryable<MoveQueues> Index() => _movequeuesRepository.Index();
        public IQueryable<MoveQueues> IndexDb() => _movequeuesRepository.IndexDb();
       public IQueryable<Statuses> selectStatuses() => _movequeuesRepository.selectStatuses();
        public IQueryable<InventoryUnits> selectInventoryUnits() => _movequeuesRepository.selectInventoryUnits();
        public IQueryable<HandlingUnits> selectHandlingUnits() => _movequeuesRepository.selectHandlingUnits();
        public IQueryable<InventoryLocations> selectInventoryLocations() => _movequeuesRepository.selectInventoryLocations();
        public IQueryable<MoveQueueContexts> selectMoveQueueContexts() => _movequeuesRepository.selectMoveQueueContexts();
        public IQueryable<MoveQueueTypes> selectMoveQueueTypes() => _movequeuesRepository.selectMoveQueueTypes();
        public IQueryable<InboundOrderLines> selectInboundOrderLines() => _movequeuesRepository.selectInboundOrderLines();
        public IQueryable<OutboundOrderLines> selectOutboundOrderLines() => _movequeuesRepository.selectOutboundOrderLines();
        public IQueryable<PickBatches> selectPickBatches() => _movequeuesRepository.selectPickBatches();
       public IQueryable<Statuses> StatusesDb() => _movequeuesRepository.StatusesDb();
        public IQueryable<InventoryUnits> InventoryUnitsDb() => _movequeuesRepository.InventoryUnitsDb();
        public IQueryable<HandlingUnits> HandlingUnitsDb() => _movequeuesRepository.HandlingUnitsDb();
        public IQueryable<InventoryLocations> InventoryLocationsDb() => _movequeuesRepository.InventoryLocationsDb();
        public IQueryable<MoveQueueContexts> MoveQueueContextsDb() => _movequeuesRepository.MoveQueueContextsDb();
        public IQueryable<MoveQueueTypes> MoveQueueTypesDb() => _movequeuesRepository.MoveQueueTypesDb();
        public IQueryable<InboundOrderLines> InboundOrderLinesDb() => _movequeuesRepository.InboundOrderLinesDb();
        public IQueryable<OutboundOrderLines> OutboundOrderLinesDb() => _movequeuesRepository.OutboundOrderLinesDb();
        public IQueryable<PickBatches> PickBatchesDb() => _movequeuesRepository.PickBatchesDb();
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

            //Custom Code Start | Added Code Block 
            //We update the queued utilization if necesary
            if (movequeuesPost.ixMoveQueueType == _commonLookUps.getMoveQueueTypes().Where(x => x.sMoveQueueType == "Consolidated Pickup - Consolidated Drop").Select(x => x.ixMoveQueueType).FirstOrDefault()
                //&& movequeuesPost.ixMoveQueueContext == _commonLookUps.getMoveQueueContexts().Where(x => x.sMoveQueueContext == "Putaway").Select(x => x.ixMoveQueueContext).FirstOrDefault()
                && 
                movequeuesPost.ixSourceInventoryLocation != movequeuesPost.ixTargetInventoryLocation
                )
            {
                var targetInventoryLocation = _inventorylocationsService.GetPost(movequeuesPost.ixTargetInventoryLocation ?? 0);
                var inventoryUnitsOnHandlingUnit = _inventoryunitsService.IndexDb().Where(x => x.ixHandlingUnit == movequeuesPost.ixTargetHandlingUnit).ToList();
                targetInventoryLocation.nQueuedUtilisationPercent = _volumeAndWeight.getNewLocationQueuedUtilisationPercent(movequeuesPost.ixTargetHandlingUnit ?? 0, inventoryUnitsOnHandlingUnit, targetInventoryLocation, true);
                targetInventoryLocation.UserName = movequeuesPost.UserName;
                _inventorylocationsService.Edit(targetInventoryLocation);
            }

            if (movequeuesPost.ixMoveQueueType == _commonLookUps.getMoveQueueTypes().Where(x => x.sMoveQueueType == "Unit Pickup - Consolidated Drop").Select(x => x.ixMoveQueueType).FirstOrDefault()
                //&& movequeuesPost.ixMoveQueueContext == _commonLookUps.getMoveQueueContexts().Where(x => x.sMoveQueueContext == "Picking").Select(x => x.ixMoveQueueContext).FirstOrDefault()
                &&
                movequeuesPost.ixSourceInventoryLocation != movequeuesPost.ixTargetInventoryLocation
                )
            {
                var targetInventoryLocation = _inventorylocationsService.GetPost(movequeuesPost.ixTargetInventoryLocation ?? 0);
                var targetInventoryUnit = _inventoryunitsService.GetPost(movequeuesPost.ixTargetInventoryUnit ?? 0);
                targetInventoryLocation.nQueuedUtilisationPercent = _volumeAndWeight.getNewLocationQueuedUtilisationPercent(targetInventoryUnit, targetInventoryLocation, true);
                targetInventoryLocation.UserName = movequeuesPost.UserName;
                _inventorylocationsService.Edit(targetInventoryLocation);
            }



            //Custom Code End

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
            //Custom Code Start | Added Code Block 
            if (movequeuesPost.ixMoveQueueType == _commonLookUps.getMoveQueueTypes().Where(x => x.sMoveQueueType == "Consolidated Pickup - Consolidated Drop").Select(x => x.ixMoveQueueType).FirstOrDefault()
                //&& movequeuesPost.ixMoveQueueContext == _commonLookUps.getMoveQueueContexts().Where(x => x.sMoveQueueContext == "Putaway").Select(x => x.ixMoveQueueContext).FirstOrDefault()
                && movequeuesPost.ixSourceInventoryLocation != movequeuesPost.ixTargetInventoryLocation
                )
            {
                _inventoryunitsService.IndexDbPost().Where(x => x.ixHandlingUnit == movequeuesPost.ixTargetHandlingUnit).ToList().ForEach(iu =>
                    {
                        iu.ixInventoryLocation = movequeuesPost.ixTargetInventoryLocation ?? 0;
                        iu.UserName = movequeuesPost.UserName;
                        _inventoryunitsService.Edit(iu, _commonLookUps.getInventoryUnitTransactionContext().Where(x => x.sInventoryUnitTransactionContext == "Move Queue Execution").Select(x => x.ixInventoryUnitTransactionContext).FirstOrDefault());
                    }
                    );
                var targetInventoryLocation = _inventorylocationsService.GetPost(movequeuesPost.ixTargetInventoryLocation ?? 0);
                var inventoryUnitsOnHandlingUnit = _inventoryunitsService.IndexDb().Where(x => x.ixHandlingUnit == movequeuesPost.ixTargetHandlingUnit).ToList();
                targetInventoryLocation.nQueuedUtilisationPercent = _volumeAndWeight.getNewLocationQueuedUtilisationPercent(movequeuesPost.ixTargetHandlingUnit ?? 0, inventoryUnitsOnHandlingUnit, targetInventoryLocation, false);
                targetInventoryLocation.UserName = movequeuesPost.UserName;
                _inventorylocationsService.Edit(targetInventoryLocation);

            }

            if (movequeuesPost.ixMoveQueueType == _commonLookUps.getMoveQueueTypes().Where(x => x.sMoveQueueType == "Unit Pickup - Consolidated Drop").Select(x => x.ixMoveQueueType).FirstOrDefault()
                //&& movequeuesPost.ixMoveQueueContext == _commonLookUps.getMoveQueueContexts().Where(x => x.sMoveQueueContext == "Putaway").Select(x => x.ixMoveQueueContext).FirstOrDefault()
                && movequeuesPost.ixSourceInventoryLocation != movequeuesPost.ixTargetInventoryLocation
                )
            {
                var targetInventoryUnit = _inventoryunitsService.GetPost(movequeuesPost.ixTargetInventoryUnit ?? 0);

                targetInventoryUnit.ixInventoryLocation = movequeuesPost.ixTargetInventoryLocation ?? 0;
                targetInventoryUnit.UserName = movequeuesPost.UserName;
                _inventoryunitsService.Edit(targetInventoryUnit, _commonLookUps.getInventoryUnitTransactionContext().Where(x => x.sInventoryUnitTransactionContext == "Move Queue Execution").Select(x => x.ixInventoryUnitTransactionContext).FirstOrDefault());

                var targetInventoryLocation = _inventorylocationsService.GetPost(movequeuesPost.ixTargetInventoryLocation ?? 0);
                targetInventoryLocation.nQueuedUtilisationPercent = _volumeAndWeight.getNewLocationQueuedUtilisationPercent(targetInventoryUnit, targetInventoryLocation, false);
                targetInventoryLocation.UserName = movequeuesPost.UserName;
                _inventorylocationsService.Edit(targetInventoryLocation);

            }


            //Custom Code End

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
  

