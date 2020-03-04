using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IAccusationsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        AccusationsPost GetPost(Int64 ixAccusation);        
		Accusations Get(Int64 ixAccusation);
        IQueryable<Accusations> Index();
        IQueryable<Accusations> IndexDb();
       IQueryable<Languages> selectLanguages();
        IQueryable<LanguageStyles> selectLanguageStyles();
        IQueryable<ResponseTypes> selectResponseTypes();
       IQueryable<Languages> LanguagesDb();
        IQueryable<LanguageStyles> LanguageStylesDb();
        IQueryable<ResponseTypes> ResponseTypesDb();
        List<string> VerifyAccusationDeleteOK(Int64 ixAccusation, string sAccusation);

        Task<Int64> Create(AccusationsPost accusationsPost);
        Task Edit(AccusationsPost accusationsPost);
        Task Delete(AccusationsPost accusationsPost);
    }
}
  

