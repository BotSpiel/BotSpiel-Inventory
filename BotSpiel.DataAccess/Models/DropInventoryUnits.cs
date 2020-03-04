using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class DropInventoryUnits : IDropInventoryUnits
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Drop Inventory Unit ID")]
		public virtual Int64 ixDropInventoryUnit { get; set; }
		[Display(Name = "Drop Inventory Unit ID")]
		public virtual Int64 ixDropInventoryUnitEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Drop Inventory Unit")]
		public virtual String sDropInventoryUnit { get; set; }
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
  

