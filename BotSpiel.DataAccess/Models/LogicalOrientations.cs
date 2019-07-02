using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class LogicalOrientations : ILogicalOrientations
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Logical Orientation ID")]
		public virtual Int64 ixLogicalOrientation { get; set; }
		[Display(Name = "Logical Orientation ID")]
		public virtual Int64 ixLogicalOrientationEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Logical Orientation")]
		public virtual String sLogicalOrientation { get; set; }
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
  

