using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface ILanguageStylesService
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

        Task<Int64> Create(LanguageStylesPost languagestylesPost);
        Task Edit(LanguageStylesPost languagestylesPost);
        Task Delete(LanguageStylesPost languagestylesPost);
    }
}
  

