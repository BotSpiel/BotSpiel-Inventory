using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface ICountrySubDivisionsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        CountrySubDivisionsPost GetPost(Int64 ixCountrySubDivision);        
		CountrySubDivisions Get(Int64 ixCountrySubDivision);
        IQueryable<CountrySubDivisions> Index();
       IQueryable<Countries> selectCountries();
        bool VerifyCountrySubDivisionUnique(Int64 ixCountrySubDivision, string sCountrySubDivision);
        List<string> VerifyCountrySubDivisionDeleteOK(Int64 ixCountrySubDivision, string sCountrySubDivision);
        void RegisterCreate(CountrySubDivisionsPost countrysubdivisionsPost);
        void RegisterEdit(CountrySubDivisionsPost countrysubdivisionsPost);
        void RegisterDelete(CountrySubDivisionsPost countrysubdivisionsPost);
        void Rollback();
        void Commit();
    }
}
  

