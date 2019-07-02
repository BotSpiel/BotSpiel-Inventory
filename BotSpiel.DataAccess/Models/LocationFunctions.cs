using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class LocationFunctions : ILocationFunctions
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Location Function ID")]
		public virtual Int64 ixLocationFunction { get; set; }
		[Display(Name = "Location Function ID")]
		public virtual Int64 ixLocationFunctionEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Location Function")]
		public virtual String sLocationFunction { get; set; }
		[StringLength(30)]
		[Required]
		[Display(Name = "Location Function Code")]
		public virtual String sLocationFunctionCode { get; set; }
		[Required]
		[Display(Name = "Created At")]
		public virtual DateTime dtCreatedAt { get; set; }
		[Required]
		[Display(Name = "Changed At")]
		public virtual DateTime dtChangedAt { get; set; }
		[StringLength(256)]
		[Required]
		[Display(Name = "Created By")]
		public virtual String sCreatedBy { get; set; }
		[StringLength(256)]
		[Required]
		[Display(Name = "Changed By")]
		public virtual String sChangedBy { get; set; }
    }
}
  

