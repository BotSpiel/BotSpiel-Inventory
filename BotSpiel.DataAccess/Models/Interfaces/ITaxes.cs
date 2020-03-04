using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface ITaxes
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixTax { get; set; }
		Int64 ixTaxEdit { get; set; }
		String sTax { get; set; }
		Int64 ixCountry { get; set; }
		Int64 ixCountrySubDivision { get; set; }
		Decimal nRate { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		Countries Countries { get; set; }
		CountrySubDivisions CountrySubDivisions { get; set; }
    }
}
  

