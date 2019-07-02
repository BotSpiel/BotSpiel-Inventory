using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IPlanetSubRegionsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixPlanetSubRegion { get; set; }
		String sPlanetSubRegion { get; set; }
		Int64 ixPlanetRegion { get; set; }
		String UserName { get; set; }
    }
}
  

