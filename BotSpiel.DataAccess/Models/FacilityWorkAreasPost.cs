using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class FacilityWorkAreasPost : IFacilityWorkAreasPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Facility Work Area ID")]
		public virtual Int64 ixFacilityWorkArea { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyFacilityWorkArea", controller: "FacilityWorkAreas", AdditionalFields = nameof(ixFacilityWorkArea))]
		[Display(Name = "Facility Work Area")]
		public virtual String sFacilityWorkArea { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

