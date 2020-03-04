using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface ILanguageStylesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        LanguageStylesPost GetPost(Int64 ixLanguageStyle);        
		LanguageStyles Get(Int64 ixLanguageStyle);
        IQueryable<LanguageStyles> Index();
        IQueryable<LanguageStyles> IndexDb();
        List<string> VerifyLanguageStyleDeleteOK(Int64 ixLanguageStyle, string sLanguageStyle);
        void RegisterCreate(LanguageStylesPost languagestylesPost);
        void RegisterEdit(LanguageStylesPost languagestylesPost);
        void RegisterDelete(LanguageStylesPost languagestylesPost);
        void Rollback();
        void Commit();
    }
}
  

