using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IPlanetSubRegions
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixPlanetSubRegion { get; set; }
		Int64 ixPlanetSubRegionEdit { get; set; }
		String sPlanetSubRegion { get; set; }
		Int64 ixPlanetRegion { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		PlanetRegions PlanetRegions { get; set; }
    }
}
  

