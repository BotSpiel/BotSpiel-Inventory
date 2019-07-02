using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class PlanetSubRegionsPost : IPlanetSubRegionsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Planet Sub Region ID")]
		public virtual Int64 ixPlanetSubRegion { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyPlanetSubRegion", controller: "PlanetSubRegions", AdditionalFields = nameof(ixPlanetSubRegion))]
		[Display(Name = "Planet Sub Region")]
		public virtual String sPlanetSubRegion { get; set; }
		[Required]
		[Display(Name = "Planet Region ID")]
		public virtual Int64 ixPlanetRegion { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

