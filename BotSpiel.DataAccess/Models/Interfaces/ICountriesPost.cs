using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface ICountriesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixCountry { get; set; }
		String sCountry { get; set; }
		Int64 ixPlanetSubRegion { get; set; }
		String sCountryCode { get; set; }
		String UserName { get; set; }
    }
}
  

