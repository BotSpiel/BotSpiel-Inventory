using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IPlanetarySystemsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixPlanetarySystem { get; set; }
		String sPlanetarySystem { get; set; }
		Int64 ixGalaxy { get; set; }
		String UserName { get; set; }
    }
}
  

