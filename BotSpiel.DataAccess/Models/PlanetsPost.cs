using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class PlanetsPost : IPlanetsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Planet ID")]
		public virtual Int64 ixPlanet { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyPlanet", controller: "Planets", AdditionalFields = nameof(ixPlanet))]
		[Display(Name = "Planet")]
		public virtual String sPlanet { get; set; }
		[Required]
		[Display(Name = "Planetary System ID")]
		public virtual Int64 ixPlanetarySystem { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

