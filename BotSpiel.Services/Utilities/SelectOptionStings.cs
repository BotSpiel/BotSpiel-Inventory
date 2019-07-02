using System;
using System.Collections.Generic;
using System.Text;

namespace BotSpiel.Services
{
    public class SelectOptionStings
    {
        private readonly IAddressesService _addressesService;
        public SelectOptionStings(IAddressesService addressesService)
        {
            _addressesService = addressesService;
        }

        public string getAddressesOptionSting(long ixAddress)
        {
            var address = _addressesService.Get(ixAddress);

            return address.sStreetAndNumberOrPostOfficeBoxOne + " " + address.sStreetAndNumberOrPostOfficeBoxTwo + " " + address.sStreetAndNumberOrPostOfficeBoxThree + " " + address.sCityOrSuburb + " " + address.sZipOrPostCode + " " + address.CountrySubDivisionsFKDiffStateOrProvince.sCountrySubDivision + " " + address.Countries.sCountry;
        }


    }
}
