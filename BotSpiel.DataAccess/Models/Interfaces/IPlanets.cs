using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IPlanets
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixPlanet { get; set; }
		Int64 ixPlanetEdit { get; set; }
		String sPlanet { get; set; }
		Int64 ixPlanetarySystem { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		PlanetarySystems PlanetarySystems { get; set; }
    }
}
  

