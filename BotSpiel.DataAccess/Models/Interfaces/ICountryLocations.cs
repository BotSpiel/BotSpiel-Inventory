using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface ICountryLocations
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixCountryLocation { get; set; }
		Int64 ixCountryLocationEdit { get; set; }
		String sCountryLocation { get; set; }
		Int64 ixCountrySubDivision { get; set; }
		String sLocationCode { get; set; }
		String sNameWithoutDiacritics { get; set; }
		String sLatitude { get; set; }
		String sLongitude { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		CountrySubDivisions CountrySubDivisions { get; set; }
    }
}
  

