using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IRequestForActionSimilesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        RequestForActionSimilesPost GetPost(Int64 ixRequestForActionSimile);        
		RequestForActionSimiles Get(Int64 ixRequestForActionSimile);
        IQueryable<RequestForActionSimiles> Index();
        IQueryable<RequestForActionSimiles> IndexDb();
       IQueryable<RequestsForAction> selectRequestsForAction();
       IQueryable<RequestsForAction> RequestsForActionDb();
        List<string> VerifyRequestForActionSimileDeleteOK(Int64 ixRequestForActionSimile, string sRequestForActionSimile);
        void RegisterCreate(RequestForActionSimilesPost requestforactionsimilesPost);
        void RegisterEdit(RequestForActionSimilesPost requestforactionsimilesPost);
        void RegisterDelete(RequestForActionSimilesPost requestforactionsimilesPost);
        void Rollback();
        void Commit();
    }
}
  

