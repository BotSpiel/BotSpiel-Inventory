using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class InventoryUnitTransactionContexts : IInventoryUnitTransactionContexts
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Inventory Unit Transaction Context ID")]
		public virtual Int64 ixInventoryUnitTransactionContext { get; set; }
		[Display(Name = "Inventory Unit Transaction Context ID")]
		public virtual Int64 ixInventoryUnitTransactionContextEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Inventory Unit Transaction Context")]
		public virtual String sInventoryUnitTransactionContext { get; set; }
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
  

