using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface ICountrySubDivisionsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixCountrySubDivision { get; set; }
		String sCountrySubDivision { get; set; }
		Int64 ixCountry { get; set; }
		String sCountrySubDivisionCode { get; set; }
		String UserName { get; set; }
    }
}
  

