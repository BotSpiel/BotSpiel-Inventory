using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class FacilityZonesPost : IFacilityZonesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Facility Zone ID")]
		public virtual Int64 ixFacilityZone { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyFacilityZone", controller: "FacilityZones", AdditionalFields = nameof(ixFacilityZone))]
		[Display(Name = "Facility Zone")]
		public virtual String sFacilityZone { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

