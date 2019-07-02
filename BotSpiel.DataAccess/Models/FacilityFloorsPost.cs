using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class FacilityFloorsPost : IFacilityFloorsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Facility Floor ID")]
		public virtual Int64 ixFacilityFloor { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyFacilityFloor", controller: "FacilityFloors", AdditionalFields = nameof(ixFacilityFloor))]
		[Display(Name = "Facility Floor")]
		public virtual String sFacilityFloor { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

