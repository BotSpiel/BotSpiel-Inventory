using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IMessageFunctionsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        MessageFunctionsPost GetPost(Int64 ixMessageFunction);        
		MessageFunctions Get(Int64 ixMessageFunction);
        IQueryable<MessageFunctions> Index();
        bool VerifyMessageFunctionUnique(Int64 ixMessageFunction, string sMessageFunction);
        List<string> VerifyMessageFunctionDeleteOK(Int64 ixMessageFunction, string sMessageFunction);
        void RegisterCreate(MessageFunctionsPost messagefunctionsPost);
        void RegisterEdit(MessageFunctionsPost messagefunctionsPost);
        void RegisterDelete(MessageFunctionsPost messagefunctionsPost);
        void Rollback();
        void Commit();
    }
}
  

