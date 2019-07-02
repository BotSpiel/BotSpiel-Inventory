using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface ILanguagesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        LanguagesPost GetPost(Int64 ixLanguage);        
		Languages Get(Int64 ixLanguage);
        IQueryable<Languages> Index();
        bool VerifyLanguageUnique(Int64 ixLanguage, string sLanguage);
        List<string> VerifyLanguageDeleteOK(Int64 ixLanguage, string sLanguage);
        void RegisterCreate(LanguagesPost languagesPost);
        void RegisterEdit(LanguagesPost languagesPost);
        void RegisterDelete(LanguagesPost languagesPost);
        void Rollback();
        void Commit();
    }
}
  

