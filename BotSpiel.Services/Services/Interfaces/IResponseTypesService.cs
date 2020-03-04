using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IResponseTypesService
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

        Task<Int64> Create(ResponseTypesPost responsetypesPost);
        Task Edit(ResponseTypesPost responsetypesPost);
        Task Delete(ResponseTypesPost responsetypesPost);
    }
}
  

