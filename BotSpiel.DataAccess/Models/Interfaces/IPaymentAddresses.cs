using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IPaymentAddresses
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixPaymentAddress { get; set; }
		Int64 ixPaymentAddressEdit { get; set; }
		String sPaymentAddress { get; set; }
		String sStreetAndNumberOrPostOfficeBoxOne { get; set; }
		String sStreetAndNumberOrPostOfficeBoxTwo { get; set; }
		String sCityOrSuburb { get; set; }
		String sCountrySubDivisionCode { get; set; }
		String sZipOrPostCode { get; set; }
		String sCountryCode { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
    }
}
  

