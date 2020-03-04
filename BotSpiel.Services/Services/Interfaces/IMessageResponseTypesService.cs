using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IMessageResponseTypesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        MessageResponseTypesPost GetPost(Int64 ixMessageResponseType);        
		MessageResponseTypes Get(Int64 ixMessageResponseType);
        IQueryable<MessageResponseTypes> Index();
        IQueryable<MessageResponseTypes> IndexDb();
        bool VerifyMessageResponseTypeUnique(Int64 ixMessageResponseType, string sMessageResponseType);
        List<string> VerifyMessageResponseTypeDeleteOK(Int64 ixMessageResponseType, string sMessageResponseType);

        Task<Int64> Create(MessageResponseTypesPost messageresponsetypesPost);
        Task Edit(MessageResponseTypesPost messageresponsetypesPost);
        Task Delete(MessageResponseTypesPost messageresponsetypesPost);
    }
}
  

