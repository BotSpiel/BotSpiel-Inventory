using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class PlanetRegionsPost : IPlanetRegionsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Planet Region ID")]
		public virtual Int64 ixPlanetRegion { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyPlanetRegion", controller: "PlanetRegions", AdditionalFields = nameof(ixPlanetRegion))]
		[Display(Name = "Planet Region")]
		public virtual String sPlanetRegion { get; set; }
		[Required]
		[Display(Name = "Planet ID")]
		public virtual Int64 ixPlanet { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

