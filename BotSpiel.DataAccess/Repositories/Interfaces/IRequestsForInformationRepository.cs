using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IRequestsForInformationRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        RequestsForInformationPost GetPost(Int64 ixRequestForInformation);        
		RequestsForInformation Get(Int64 ixRequestForInformation);
        IQueryable<RequestsForInformation> Index();
        IQueryable<RequestsForInformation> IndexDb();
       IQueryable<Languages> selectLanguages();
        IQueryable<Topics> selectTopics();
        IQueryable<LanguageStyles> selectLanguageStyles();
        IQueryable<ResponseTypes> selectResponseTypes();
       IQueryable<Languages> LanguagesDb();
        IQueryable<Topics> TopicsDb();
        IQueryable<LanguageStyles> LanguageStylesDb();
        IQueryable<ResponseTypes> ResponseTypesDb();
        List<string> VerifyRequestForInformationDeleteOK(Int64 ixRequestForInformation, string sRequestForInformation);
        void RegisterCreate(RequestsForInformationPost requestsforinformationPost);
        void RegisterEdit(RequestsForInformationPost requestsforinformationPost);
        void RegisterDelete(RequestsForInformationPost requestsforinformationPost);
        void Rollback();
        void Commit();
    }
}
  

