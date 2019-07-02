using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IMoveQueueTypesService
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
        bool VerifyMoveQueueTypeUnique(Int64 ixMoveQueueType, string sMoveQueueType);
        List<string> VerifyMoveQueueTypeDeleteOK(Int64 ixMoveQueueType, string sMoveQueueType);

        Task<Int64> Create(MoveQueueTypesPost movequeuetypesPost);
        Task Edit(MoveQueueTypesPost movequeuetypesPost);
        Task Delete(MoveQueueTypesPost movequeuetypesPost);
    }
}
  

