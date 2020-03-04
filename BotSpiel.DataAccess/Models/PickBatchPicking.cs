using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class PickBatchPicking : IPickBatchPicking
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public PickBatchPicking()
        {
		PickBatches _PickBatches = new PickBatches();
		PickBatches = _PickBatches;
		InventoryUnits _InventoryUnits = new InventoryUnits();
		InventoryUnits = _InventoryUnits;
		HandlingUnits _HandlingUnits = new HandlingUnits();
		HandlingUnits = _HandlingUnits;

        }
		[Display(Name = "Pick Batch Pick ID")]
		public virtual Int64 ixPickBatchPick { get; set; }
		[Display(Name = "Pick Batch Pick ID")]
		public virtual Int64 ixPickBatchPickEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Pick Batch Pick")]
		public virtual String sPickBatchPick { get; set; }
		[Required]
		[Display(Name = "Pick Batch ID")]
		public virtual Int64 ixPickBatch { get; set; }
		[Required]
		[Display(Name = "Inventory Unit ID")]
		public virtual Int64 ixInventoryUnit { get; set; }
		[Required]
		[Display(Name = "Base Unit Quantity Picked")]
		public virtual Double nBaseUnitQuantityPicked { get; set; }
		[StringLength(300)]
		[Required]
		[Display(Name = "Pack To Handling Unit")]
		public virtual String sPackToHandlingUnit { get; set; }
		[Required]
		[Display(Name = "Handling Unit ID")]
		public virtual Int64 ixHandlingUnit { get; set; }
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
		[ForeignKey("ixPickBatch")]
		public virtual PickBatches PickBatches { get; set; }
		[ForeignKey("ixInventoryUnit")]
		public virtual InventoryUnits InventoryUnits { get; set; }
		[ForeignKey("ixHandlingUnit")]
		public virtual HandlingUnits HandlingUnits { get; set; }
    }
}
  

