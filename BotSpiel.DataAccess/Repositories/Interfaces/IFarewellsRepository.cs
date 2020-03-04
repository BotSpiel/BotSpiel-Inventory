using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IFarewellsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        FarewellsPost GetPost(Int64 ixFarewell);        
		Farewells Get(Int64 ixFarewell);
        IQueryable<Farewells> Index();
        IQueryable<Farewells> IndexDb();
       IQueryable<Languages> selectLanguages();
        IQueryable<LanguageStyles> selectLanguageStyles();
        IQueryable<ResponseTypes> selectResponseTypes();
       IQueryable<Languages> LanguagesDb();
        IQueryable<LanguageStyles> LanguageStylesDb();
        IQueryable<ResponseTypes> ResponseTypesDb();
        List<string> VerifyFarewellDeleteOK(Int64 ixFarewell, string sFarewell);
        void RegisterCreate(FarewellsPost farewellsPost);
        void RegisterEdit(FarewellsPost farewellsPost);
        void RegisterDelete(FarewellsPost farewellsPost);
        void Rollback();
        void Commit();
    }
}
  

