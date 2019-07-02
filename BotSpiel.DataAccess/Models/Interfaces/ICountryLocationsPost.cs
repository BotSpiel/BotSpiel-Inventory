using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface ICountryLocationsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixCountryLocation { get; set; }
		String sCountryLocation { get; set; }
		Int64 ixCountrySubDivision { get; set; }
		String sLocationCode { get; set; }
		String sNameWithoutDiacritics { get; set; }
		String sLatitude { get; set; }
		String sLongitude { get; set; }
		String UserName { get; set; }
    }
}
  

