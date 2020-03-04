using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IMessageFunctionsService
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
        IQueryable<MessageFunctions> IndexDb();
        bool VerifyMessageFunctionUnique(Int64 ixMessageFunction, string sMessageFunction);
        List<string> VerifyMessageFunctionDeleteOK(Int64 ixMessageFunction, string sMessageFunction);

        Task<Int64> Create(MessageFunctionsPost messagefunctionsPost);
        Task Edit(MessageFunctionsPost messagefunctionsPost);
        Task Delete(MessageFunctionsPost messagefunctionsPost);
    }
}
  

