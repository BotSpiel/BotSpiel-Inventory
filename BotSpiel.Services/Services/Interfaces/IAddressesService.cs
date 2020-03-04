using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IAddressesService
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

        Task<Int64> Create(AddressesPost addressesPost);
        Task Edit(AddressesPost addressesPost);
        Task Delete(AddressesPost addressesPost);
        //Custom Code Start | Added Code Block 
        List<KeyValuePair<Int64?, string>> selectEmptyCountrySubDivisionsDropdown();
        //Custom Code End
    }
}
  

