using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IPlanetsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixPlanet { get; set; }
		String sPlanet { get; set; }
		Int64 ixPlanetarySystem { get; set; }
		String UserName { get; set; }
    }
}
  

