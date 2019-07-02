using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IPlanetRegions
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixPlanetRegion { get; set; }
		Int64 ixPlanetRegionEdit { get; set; }
		String sPlanetRegion { get; set; }
		Int64 ixPlanet { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		Planets Planets { get; set; }
    }
}
  

