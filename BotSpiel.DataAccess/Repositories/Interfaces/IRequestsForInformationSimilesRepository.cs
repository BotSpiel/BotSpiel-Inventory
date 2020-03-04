using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IRequestsForInformationSimilesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        RequestsForInformationSimilesPost GetPost(Int64 ixRequestsForInformationSimile);        
		RequestsForInformationSimiles Get(Int64 ixRequestsForInformationSimile);
        IQueryable<RequestsForInformationSimiles> Index();
        IQueryable<RequestsForInformationSimiles> IndexDb();
       IQueryable<RequestsForInformation> selectRequestsForInformation();
       IQueryable<RequestsForInformation> RequestsForInformationDb();
        List<string> VerifyRequestsForInformationSimileDeleteOK(Int64 ixRequestsForInformationSimile, string sRequestsForInformationSimile);
        void RegisterCreate(RequestsForInformationSimilesPost requestsforinformationsimilesPost);
        void RegisterEdit(RequestsForInformationSimilesPost requestsforinformationsimilesPost);
        void RegisterDelete(RequestsForInformationSimilesPost requestsforinformationsimilesPost);
        void Rollback();
        void Commit();
    }
}
  

