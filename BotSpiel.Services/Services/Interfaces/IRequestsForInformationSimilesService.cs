using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IRequestsForInformationSimilesService
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

        Task<Int64> Create(RequestsForInformationSimilesPost requestsforinformationsimilesPost);
        Task Edit(RequestsForInformationSimilesPost requestsforinformationsimilesPost);
        Task Delete(RequestsForInformationSimilesPost requestsforinformationsimilesPost);
    }
}
  

