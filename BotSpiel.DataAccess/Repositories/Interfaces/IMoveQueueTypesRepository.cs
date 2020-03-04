using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IMoveQueueTypesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        MoveQueueTypesPost GetPost(Int64 ixMoveQueueType);        
		MoveQueueTypes Get(Int64 ixMoveQueueType);
        IQueryable<MoveQueueTypes> Index();
        IQueryable<MoveQueueTypes> IndexDb();
        bool VerifyMoveQueueTypeUnique(Int64 ixMoveQueueType, string sMoveQueueType);
        List<string> VerifyMoveQueueTypeDeleteOK(Int64 ixMoveQueueType, string sMoveQueueType);
        void RegisterCreate(MoveQueueTypesPost movequeuetypesPost);
        void RegisterEdit(MoveQueueTypesPost movequeuetypesPost);
        void RegisterDelete(MoveQueueTypesPost movequeuetypesPost);
        void Rollback();
        void Commit();
    }
}
  

