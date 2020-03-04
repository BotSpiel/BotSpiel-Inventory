using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface ICountryLocationsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        CountryLocationsPost GetPost(Int64 ixCountryLocation);        
		CountryLocations Get(Int64 ixCountryLocation);
        IQueryable<CountryLocations> Index();
        IQueryable<CountryLocations> IndexDb();
       IQueryable<CountrySubDivisions> selectCountrySubDivisions();
       IQueryable<CountrySubDivisions> CountrySubDivisionsDb();
        bool VerifyCountryLocationUnique(Int64 ixCountryLocation, string sCountryLocation);
        List<string> VerifyCountryLocationDeleteOK(Int64 ixCountryLocation, string sCountryLocation);

        Task<Int64> Create(CountryLocationsPost countrylocationsPost);
        Task Edit(CountryLocationsPost countrylocationsPost);
        Task Delete(CountryLocationsPost countrylocationsPost);
    }
}
  

