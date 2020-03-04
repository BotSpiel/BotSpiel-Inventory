using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IAddressesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        AddressesPost GetPost(Int64 ixAddress);        
		Addresses Get(Int64 ixAddress);
        IQueryable<Addresses> Index();
        IQueryable<Addresses> IndexDb();
       IQueryable<Countries> selectCountries();
        IQueryable<CountrySubDivisions> selectCountrySubDivisions();
       IQueryable<Countries> CountriesDb();
        IQueryable<CountrySubDivisions> CountrySubDivisionsDb();
        bool VerifyAddressUnique(Int64 ixAddress, string sAddress);
        List<string> VerifyAddressDeleteOK(Int64 ixAddress, string sAddress);
        void RegisterCreate(AddressesPost addressesPost);
        void RegisterEdit(AddressesPost addressesPost);
        void RegisterDelete(AddressesPost addressesPost);
        void Rollback();
        void Commit();
        //Custom Code Start | Added Code Block 
        List<KeyValuePair<Int64?, string>> selectEmptyCountrySubDivisionsDropdown();
        //Custom Code End
    }
}
  

