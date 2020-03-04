using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface ICountryLocationsRepository
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
        void RegisterCreate(CountryLocationsPost countrylocationsPost);
        void RegisterEdit(CountryLocationsPost countrylocationsPost);
        void RegisterDelete(CountryLocationsPost countrylocationsPost);
        void Rollback();
        void Commit();
    }
}
  

