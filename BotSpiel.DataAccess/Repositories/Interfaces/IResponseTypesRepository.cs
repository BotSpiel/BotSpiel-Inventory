using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IResponseTypesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        ResponseTypesPost GetPost(Int64 ixResponseType);        
		ResponseTypes Get(Int64 ixResponseType);
        IQueryable<ResponseTypes> Index();
        IQueryable<ResponseTypes> IndexDb();
        List<string> VerifyResponseTypeDeleteOK(Int64 ixResponseType, string sResponseType);
        void RegisterCreate(ResponseTypesPost responsetypesPost);
        void RegisterEdit(ResponseTypesPost responsetypesPost);
        void RegisterDelete(ResponseTypesPost responsetypesPost);
        void Rollback();
        void Commit();
    }
}
  

