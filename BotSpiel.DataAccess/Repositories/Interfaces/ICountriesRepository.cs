using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface ICountriesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        CountriesPost GetPost(Int64 ixCountry);        
		Countries Get(Int64 ixCountry);
        IQueryable<Countries> Index();
        IQueryable<Countries> IndexDb();
       IQueryable<PlanetSubRegions> selectPlanetSubRegions();
       IQueryable<PlanetSubRegions> PlanetSubRegionsDb();
        bool VerifyCountryUnique(Int64 ixCountry, string sCountry);
        List<string> VerifyCountryDeleteOK(Int64 ixCountry, string sCountry);
        void RegisterCreate(CountriesPost countriesPost);
        void RegisterEdit(CountriesPost countriesPost);
        void RegisterDelete(CountriesPost countriesPost);
        void Rollback();
        void Commit();
    }
}
  

