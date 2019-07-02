using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IMoveQueueContextsRepository
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
        void RegisterCreate(MoveQueueContextsPost movequeuecontextsPost);
        void RegisterEdit(MoveQueueContextsPost movequeuecontextsPost);
        void RegisterDelete(MoveQueueContextsPost movequeuecontextsPost);
        void Rollback();
        void Commit();
    }
}
  

