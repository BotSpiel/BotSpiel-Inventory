using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IRequestsForActionRepository
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
        void RegisterCreate(RequestsForActionPost requestsforactionPost);
        void RegisterEdit(RequestsForActionPost requestsforactionPost);
        void RegisterDelete(RequestsForActionPost requestsforactionPost);
        void Rollback();
        void Commit();
    }
}
  

