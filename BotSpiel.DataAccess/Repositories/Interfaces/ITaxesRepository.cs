using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface ITaxesRepository
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
        void RegisterCreate(TaxesPost taxesPost);
        void RegisterEdit(TaxesPost taxesPost);
        void RegisterDelete(TaxesPost taxesPost);
        void Rollback();
        void Commit();
    }
}
  

