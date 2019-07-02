using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class GalaxiesPost : IGalaxiesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Galaxy ID")]
		public virtual Int64 ixGalaxy { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyGalaxy", controller: "Galaxies", AdditionalFields = nameof(ixGalaxy))]
		[Display(Name = "Galaxy")]
		public virtual String sGalaxy { get; set; }
		[Required]
		[Display(Name = "Universe ID")]
		public virtual Int64 ixUniverse { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

