using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IMoveQueueContextsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        MoveQueueContextsPost GetPost(Int64 ixMoveQueueContext);        
		MoveQueueContexts Get(Int64 ixMoveQueueContext);
        IQueryable<MoveQueueContexts> Index();
        bool VerifyMoveQueueContextUnique(Int64 ixMoveQueueContext, string sMoveQueueContext);
        List<string> VerifyMoveQueueContextDeleteOK(Int64 ixMoveQueueContext, string sMoveQueueContext);

        Task<Int64> Create(MoveQueueContextsPost movequeuecontextsPost);
        Task Edit(MoveQueueContextsPost movequeuecontextsPost);
        Task Delete(MoveQueueContextsPost movequeuecontextsPost);
    }
}
  

