using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IAccusationsRepository
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
        void RegisterCreate(AccusationsPost accusationsPost);
        void RegisterEdit(AccusationsPost accusationsPost);
        void RegisterDelete(AccusationsPost accusationsPost);
        void Rollback();
        void Commit();
    }
}
  

