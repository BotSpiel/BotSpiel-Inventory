using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface ICountries
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixCountry { get; set; }
		Int64 ixCountryEdit { get; set; }
		String sCountry { get; set; }
		Int64 ixPlanetSubRegion { get; set; }
		String sCountryCode { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		PlanetSubRegions PlanetSubRegions { get; set; }
    }
}
  

