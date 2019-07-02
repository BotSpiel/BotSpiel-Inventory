using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class MoveQueuesPost : IMoveQueuesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Move Queue ID")]
		public virtual Int64 ixMoveQueue { get; set; }
		[Display(Name = "Move Queue")]
		public virtual String sMoveQueue { get; set; }
		[Required]
		[Display(Name = "Move Queue Type ID")]
		public virtual Int64 ixMoveQueueType { get; set; }
		[Required]
		[Display(Name = "Move Queue Context ID")]
		public virtual Int64 ixMoveQueueContext { get; set; }
		[Display(Name = "Source Inventory Unit ID")]
		public virtual Int64? ixSourceInventoryUnit { get; set; }
		[Display(Name = "Target Inventory Unit ID")]
		public virtual Int64? ixTargetInventoryUnit { get; set; }
		[Display(Name = "Source Inventory Location ID")]
		public virtual Int64? ixSourceInventoryLocation { get; set; }
		[Display(Name = "Target Inventory Location ID")]
		public virtual Int64? ixTargetInventoryLocation { get; set; }
		[Display(Name = "Source Handling Unit ID")]
		public virtual Int64? ixSourceHandlingUnit { get; set; }
		[Display(Name = "Target Handling Unit ID")]
		public virtual Int64? ixTargetHandlingUnit { get; set; }
		[StringLength(300)]
		[Display(Name = "Preferred Resource")]
		public virtual String sPreferredResource { get; set; }
		[Required]
		[Display(Name = "Base Unit Quantity")]
		public virtual Double nBaseUnitQuantity { get; set; }
		[Display(Name = "Start By")]
		public virtual DateTime? dtStartBy { get; set; }
		[Display(Name = "Complete By")]
		public virtual DateTime? dtCompleteBy { get; set; }
		[Display(Name = "Started At")]
		public virtual DateTime? dtStartedAt { get; set; }
		[Display(Name = "Completed At")]
		public virtual DateTime? dtCompletedAt { get; set; }
		[Display(Name = "Inbound Order Line ID")]
		public virtual Int64? ixInboundOrderLine { get; set; }
		[Display(Name = "Outbound Order Line ID")]
		public virtual Int64? ixOutboundOrderLine { get; set; }
		[Display(Name = "Pick Batch ID")]
		public virtual Int64? ixPickBatch { get; set; }
		[Required]
		[Display(Name = "Status ID")]
		public virtual Int64 ixStatus { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

