using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IPlanetarySystems
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixPlanetarySystem { get; set; }
		Int64 ixPlanetarySystemEdit { get; set; }
		String sPlanetarySystem { get; set; }
		Int64 ixGalaxy { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		Galaxies Galaxies { get; set; }
    }
}
  

