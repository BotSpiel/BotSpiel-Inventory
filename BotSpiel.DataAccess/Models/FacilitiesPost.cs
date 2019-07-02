using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class FacilitiesPost : IFacilitiesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Facility ID")]
		public virtual Int64 ixFacility { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyFacility", controller: "Facilities", AdditionalFields = nameof(ixFacility))]
		[Display(Name = "Facility")]
		public virtual String sFacility { get; set; }
		[Required]
		[Display(Name = "Address ID")]
		public virtual Int64 ixAddress { get; set; }
		[StringLength(30)]
		[Display(Name = "Latitude")]
		public virtual String sLatitude { get; set; }
		[StringLength(30)]
		[Display(Name = "Longitude")]
		public virtual String sLongitude { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

