using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IFarewellsService
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

        Task<Int64> Create(FarewellsPost farewellsPost);
        Task Edit(FarewellsPost farewellsPost);
        Task Delete(FarewellsPost farewellsPost);
    }
}
  

