using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface ITaxesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        TaxesPost GetPost(Int64 ixTax);        
		Taxes Get(Int64 ixTax);
        IQueryable<Taxes> Index();
        IQueryable<Taxes> IndexDb();
       IQueryable<Countries> selectCountries();
        IQueryable<CountrySubDivisions> selectCountrySubDivisions();
       IQueryable<Countries> CountriesDb();
        IQueryable<CountrySubDivisions> CountrySubDivisionsDb();
        bool VerifyTaxUnique(Int64 ixTax, string sTax);
        List<string> VerifyTaxDeleteOK(Int64 ixTax, string sTax);

        Task<Int64> Create(TaxesPost taxesPost);
        Task Edit(TaxesPost taxesPost);
        Task Delete(TaxesPost taxesPost);
    }
}
  

