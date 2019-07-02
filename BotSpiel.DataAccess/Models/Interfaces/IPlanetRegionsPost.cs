using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IPlanetRegionsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixPlanetRegion { get; set; }
		String sPlanetRegion { get; set; }
		Int64 ixPlanet { get; set; }
		String UserName { get; set; }
    }
}
  

