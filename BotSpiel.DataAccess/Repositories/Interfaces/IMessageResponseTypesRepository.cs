using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IMessageResponseTypesRepository
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
        void RegisterCreate(MessageResponseTypesPost messageresponsetypesPost);
        void RegisterEdit(MessageResponseTypesPost messageresponsetypesPost);
        void RegisterDelete(MessageResponseTypesPost messageresponsetypesPost);
        void Rollback();
        void Commit();
    }
}
  

