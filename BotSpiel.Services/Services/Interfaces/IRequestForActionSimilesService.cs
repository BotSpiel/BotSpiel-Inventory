using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IRequestForActionSimilesService
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

        Task<Int64> Create(RequestForActionSimilesPost requestforactionsimilesPost);
        Task Edit(RequestForActionSimilesPost requestforactionsimilesPost);
        Task Delete(RequestForActionSimilesPost requestforactionsimilesPost);
    }
}
  

