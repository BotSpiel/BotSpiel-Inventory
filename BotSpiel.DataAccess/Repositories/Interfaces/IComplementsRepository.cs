using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IComplementsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        ComplementsPost GetPost(Int64 ixComplement);        
		Complements Get(Int64 ixComplement);
        IQueryable<Complements> Index();
        IQueryable<Complements> IndexDb();
       IQueryable<Languages> selectLanguages();
        IQueryable<LanguageStyles> selectLanguageStyles();
        IQueryable<ResponseTypes> selectResponseTypes();
       IQueryable<Languages> LanguagesDb();
        IQueryable<LanguageStyles> LanguageStylesDb();
        IQueryable<ResponseTypes> ResponseTypesDb();
        List<string> VerifyComplementDeleteOK(Int64 ixComplement, string sComplement);
        void RegisterCreate(ComplementsPost complementsPost);
        void RegisterEdit(ComplementsPost complementsPost);
        void RegisterDelete(ComplementsPost complementsPost);
        void Rollback();
        void Commit();
    }
}
  

