using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class PickBatchTypes : IPickBatchTypes
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Pick Batch Type ID")]
		public virtual Int64 ixPickBatchType { get; set; }
		[Display(Name = "Pick Batch Type ID")]
		public virtual Int64 ixPickBatchTypeEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Pick Batch Type")]
		public virtual String sPickBatchType { get; set; }
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
  

