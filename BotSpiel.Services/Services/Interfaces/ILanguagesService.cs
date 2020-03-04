using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface ILanguagesService
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
        IQueryable<Languages> IndexDb();
        bool VerifyLanguageUnique(Int64 ixLanguage, string sLanguage);
        List<string> VerifyLanguageDeleteOK(Int64 ixLanguage, string sLanguage);

        Task<Int64> Create(LanguagesPost languagesPost);
        Task Edit(LanguagesPost languagesPost);
        Task Delete(LanguagesPost languagesPost);
    }
}
  

