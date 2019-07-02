using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class HandlingUnitTypes : IHandlingUnitTypes
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Handling Unit Type ID")]
		public virtual Int64 ixHandlingUnitType { get; set; }
		[Display(Name = "Handling Unit Type ID")]
		public virtual Int64 ixHandlingUnitTypeEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Handling Unit Type")]
		public virtual String sHandlingUnitType { get; set; }
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
  

