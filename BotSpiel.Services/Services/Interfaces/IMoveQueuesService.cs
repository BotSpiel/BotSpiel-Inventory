using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IMoveQueuesService
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
       IQueryable<Statuses> selectStatuses();
        IQueryable<InventoryUnits> selectInventoryUnits();
        IQueryable<HandlingUnits> selectHandlingUnits();
        IQueryable<InventoryLocations> selectInventoryLocations();
        IQueryable<MoveQueueContexts> selectMoveQueueContexts();
        IQueryable<MoveQueueTypes> selectMoveQueueTypes();
        IQueryable<InboundOrderLines> selectInboundOrderLines();
        IQueryable<OutboundOrderLines> selectOutboundOrderLines();
        IQueryable<PickBatches> selectPickBatches();
       List<KeyValuePair<Int64?, string>> selectInventoryUnitsNullable();
        List<KeyValuePair<Int64?, string>> selectHandlingUnitsNullable();
        List<KeyValuePair<Int64?, string>> selectInventoryLocationsNullable();
        List<KeyValuePair<Int64?, string>> selectInboundOrderLinesNullable();
        List<KeyValuePair<Int64?, string>> selectOutboundOrderLinesNullable();
        List<KeyValuePair<Int64?, string>> selectPickBatchesNullable();
        bool VerifyMoveQueueUnique(Int64 ixMoveQueue, string sMoveQueue);
        List<string> VerifyMoveQueueDeleteOK(Int64 ixMoveQueue, string sMoveQueue);

        Task<Int64> Create(MoveQueuesPost movequeuesPost);
        Task Edit(MoveQueuesPost movequeuesPost);
        Task Delete(MoveQueuesPost movequeuesPost);
    }
}
  

