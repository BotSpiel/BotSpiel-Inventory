using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IRequestsForActionService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        RequestsForActionPost GetPost(Int64 ixRequestForAction);        
		RequestsForAction Get(Int64 ixRequestForAction);
        IQueryable<RequestsForAction> Index();
        IQueryable<RequestsForAction> IndexDb();
       IQueryable<Languages> selectLanguages();
        IQueryable<LanguageStyles> selectLanguageStyles();
       IQueryable<Languages> LanguagesDb();
        IQueryable<LanguageStyles> LanguageStylesDb();
        List<string> VerifyRequestForActionDeleteOK(Int64 ixRequestForAction, string sRequestForAction);

        Task<Int64> Create(RequestsForActionPost requestsforactionPost);
        Task Edit(RequestsForActionPost requestsforactionPost);
        Task Delete(RequestsForActionPost requestsforactionPost);
    }
}
  

