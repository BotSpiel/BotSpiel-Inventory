using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IPaymentAddressesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixPaymentAddress { get; set; }
		String sPaymentAddress { get; set; }
		String sStreetAndNumberOrPostOfficeBoxOne { get; set; }
		String sStreetAndNumberOrPostOfficeBoxTwo { get; set; }
		String sCityOrSuburb { get; set; }
		String sCountrySubDivisionCode { get; set; }
		String sZipOrPostCode { get; set; }
		String sCountryCode { get; set; }
		String UserName { get; set; }
    }
}
  

