using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface ICountrySubDivisionsService
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

        Task<Int64> Create(CountrySubDivisionsPost countrysubdivisionsPost);
        Task Edit(CountrySubDivisionsPost countrysubdivisionsPost);
        Task Delete(CountrySubDivisionsPost countrysubdivisionsPost);
    }
}
  

