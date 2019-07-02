using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface ICountrySubDivisions
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixCountrySubDivision { get; set; }
		Int64 ixCountrySubDivisionEdit { get; set; }
		String sCountrySubDivision { get; set; }
		Int64 ixCountry { get; set; }
		String sCountrySubDivisionCode { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		Countries Countries { get; set; }
    }
}
  

