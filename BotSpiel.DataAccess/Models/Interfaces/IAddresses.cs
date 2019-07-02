using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IAddresses
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixAddress { get; set; }
		Int64 ixAddressEdit { get; set; }
		String sAddress { get; set; }
		String sStreetAndNumberOrPostOfficeBoxOne { get; set; }
		String sStreetAndNumberOrPostOfficeBoxTwo { get; set; }
		String sStreetAndNumberOrPostOfficeBoxThree { get; set; }
		String sCityOrSuburb { get; set; }
		String sZipOrPostCode { get; set; }
		Int64 ixStateOrProvince { get; set; }
		Int64 ixCountry { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		CountrySubDivisions CountrySubDivisionsFKDiffStateOrProvince { get; set; }
		Countries Countries { get; set; }
    }
}
  

