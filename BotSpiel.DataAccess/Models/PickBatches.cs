using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class PickBatches : IPickBatches
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public PickBatches()
        {
		PickBatchTypes _PickBatchTypes = new PickBatchTypes();
		PickBatchTypes = _PickBatchTypes;
		Statuses _Statuses = new Statuses();
		Statuses = _Statuses;

        }
		[Display(Name = "Pick Batch ID")]
		public virtual Int64 ixPickBatch { get; set; }
		[Display(Name = "Pick Batch ID")]
		public virtual Int64 ixPickBatchEdit { get; set; }
		[Display(Name = "Pick Batch")]
		public virtual String sPickBatch { get; set; }
		[Required]
		[Display(Name = "Pick Batch Type ID")]
		public virtual Int64 ixPickBatchType { get; set; }
		[Required]
		[Display(Name = "Multi Resource")]
		public virtual Boolean bMultiResource { get; set; }
		[Required]
		[Display(Name = "Start By")]
		public virtual DateTime dtStartBy { get; set; }
		[Required]
		[Display(Name = "Complete By")]
		public virtual DateTime dtCompleteBy { get; set; }
		[Required]
		[Display(Name = "Status ID")]
		public virtual Int64 ixStatus { get; set; }
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
		[ForeignKey("ixPickBatchType")]
		public virtual PickBatchTypes PickBatchTypes { get; set; }
		[ForeignKey("ixStatus")]
		public virtual Statuses Statuses { get; set; }
    }
}
  

