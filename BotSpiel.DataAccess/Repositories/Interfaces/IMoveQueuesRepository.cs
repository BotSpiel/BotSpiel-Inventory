using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IMoveQueuesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        MoveQueuesPost GetPost(Int64 ixMoveQueue);        
		MoveQueues Get(Int64 ixMoveQueue);
        IQueryable<MoveQueues> Index();
        IQueryable<MoveQueues> IndexDb();
       IQueryable<Statuses> selectStatuses();
        IQueryable<InventoryUnits> selectInventoryUnits();
        IQueryable<HandlingUnits> selectHandlingUnits();
        IQueryable<InventoryLocations> selectInventoryLocations();
        IQueryable<MoveQueueContexts> selectMoveQueueContexts();
        IQueryable<MoveQueueTypes> selectMoveQueueTypes();
        IQueryable<InboundOrderLines> selectInboundOrderLines();
        IQueryable<OutboundOrderLines> selectOutboundOrderLines();
        IQueryable<PickBatches> selectPickBatches();
       IQueryable<Statuses> StatusesDb();
        IQueryable<InventoryUnits> InventoryUnitsDb();
        IQueryable<HandlingUnits> HandlingUnitsDb();
        IQueryable<InventoryLocations> InventoryLocationsDb();
        IQueryable<MoveQueueContexts> MoveQueueContextsDb();
        IQueryable<MoveQueueTypes> MoveQueueTypesDb();
        IQueryable<InboundOrderLines> InboundOrderLinesDb();
        IQueryable<OutboundOrderLines> OutboundOrderLinesDb();
        IQueryable<PickBatches> PickBatchesDb();
       List<KeyValuePair<Int64?, string>> selectInventoryUnitsNullable();
        List<KeyValuePair<Int64?, string>> selectHandlingUnitsNullable();
        List<KeyValuePair<Int64?, string>> selectInventoryLocationsNullable();
        List<KeyValuePair<Int64?, string>> selectInboundOrderLinesNullable();
        List<KeyValuePair<Int64?, string>> selectOutboundOrderLinesNullable();
        List<KeyValuePair<Int64?, string>> selectPickBatchesNullable();
        bool VerifyMoveQueueUnique(Int64 ixMoveQueue, string sMoveQueue);
        List<string> VerifyMoveQueueDeleteOK(Int64 ixMoveQueue, string sMoveQueue);
        void RegisterCreate(MoveQueuesPost movequeuesPost);
        void RegisterEdit(MoveQueuesPost movequeuesPost);
        void RegisterDelete(MoveQueuesPost movequeuesPost);
        void Rollback();
        void Commit();
    }
}
  

