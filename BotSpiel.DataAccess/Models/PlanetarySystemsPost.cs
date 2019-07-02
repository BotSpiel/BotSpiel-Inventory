using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class PlanetarySystemsPost : IPlanetarySystemsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Planetary System ID")]
		public virtual Int64 ixPlanetarySystem { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyPlanetarySystem", controller: "PlanetarySystems", AdditionalFields = nameof(ixPlanetarySystem))]
		[Display(Name = "Planetary System")]
		public virtual String sPlanetarySystem { get; set; }
		[Required]
		[Display(Name = "Galaxy ID")]
		public virtual Int64 ixGalaxy { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

