using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface ICountriesService
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

        Task<Int64> Create(CountriesPost countriesPost);
        Task Edit(CountriesPost countriesPost);
        Task Delete(CountriesPost countriesPost);
    }
}
  

