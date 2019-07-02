using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class MoveQueues : IMoveQueues
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public MoveQueues()
        {
		MoveQueueTypes _MoveQueueTypes = new MoveQueueTypes();
		MoveQueueTypes = _MoveQueueTypes;
		MoveQueueContexts _MoveQueueContexts = new MoveQueueContexts();
		MoveQueueContexts = _MoveQueueContexts;
		InventoryUnits _InventoryUnitsFKDiffSourceInventoryUnit = new InventoryUnits();
		InventoryUnitsFKDiffSourceInventoryUnit = _InventoryUnitsFKDiffSourceInventoryUnit;
		InventoryUnits _InventoryUnitsFKDiffTargetInventoryUnit = new InventoryUnits();
		InventoryUnitsFKDiffTargetInventoryUnit = _InventoryUnitsFKDiffTargetInventoryUnit;
		InventoryLocations _InventoryLocationsFKDiffSourceInventoryLocation = new InventoryLocations();
		InventoryLocationsFKDiffSourceInventoryLocation = _InventoryLocationsFKDiffSourceInventoryLocation;
		InventoryLocations _InventoryLocationsFKDiffTargetInventoryLocation = new InventoryLocations();
		InventoryLocationsFKDiffTargetInventoryLocation = _InventoryLocationsFKDiffTargetInventoryLocation;
		HandlingUnits _HandlingUnitsFKDiffSourceHandlingUnit = new HandlingUnits();
		HandlingUnitsFKDiffSourceHandlingUnit = _HandlingUnitsFKDiffSourceHandlingUnit;
		HandlingUnits _HandlingUnitsFKDiffTargetHandlingUnit = new HandlingUnits();
		HandlingUnitsFKDiffTargetHandlingUnit = _HandlingUnitsFKDiffTargetHandlingUnit;
		InboundOrderLines _InboundOrderLines = new InboundOrderLines();
		InboundOrderLines = _InboundOrderLines;
		OutboundOrderLines _OutboundOrderLines = new OutboundOrderLines();
		OutboundOrderLines = _OutboundOrderLines;
		PickBatches _PickBatches = new PickBatches();
		PickBatches = _PickBatches;
		Statuses _Statuses = new Statuses();
		Statuses = _Statuses;

        }
		[Display(Name = "Move Queue ID")]
		public virtual Int64 ixMoveQueue { get; set; }
		[Display(Name = "Move Queue ID")]
		public virtual Int64 ixMoveQueueEdit { get; set; }
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
		[ForeignKey("ixMoveQueueType")]
		public virtual MoveQueueTypes MoveQueueTypes { get; set; }
		[ForeignKey("ixMoveQueueContext")]
		public virtual MoveQueueContexts MoveQueueContexts { get; set; }
		[ForeignKey("ixSourceInventoryUnit")]
		public virtual InventoryUnits InventoryUnitsFKDiffSourceInventoryUnit { get; set; }
		[ForeignKey("ixTargetInventoryUnit")]
		public virtual InventoryUnits InventoryUnitsFKDiffTargetInventoryUnit { get; set; }
		[ForeignKey("ixSourceInventoryLocation")]
		public virtual InventoryLocations InventoryLocationsFKDiffSourceInventoryLocation { get; set; }
		[ForeignKey("ixTargetInventoryLocation")]
		public virtual InventoryLocations InventoryLocationsFKDiffTargetInventoryLocation { get; set; }
		[ForeignKey("ixSourceHandlingUnit")]
		public virtual HandlingUnits HandlingUnitsFKDiffSourceHandlingUnit { get; set; }
		[ForeignKey("ixTargetHandlingUnit")]
		public virtual HandlingUnits HandlingUnitsFKDiffTargetHandlingUnit { get; set; }
		[ForeignKey("ixInboundOrderLine")]
		public virtual InboundOrderLines InboundOrderLines { get; set; }
		[ForeignKey("ixOutboundOrderLine")]
		public virtual OutboundOrderLines OutboundOrderLines { get; set; }
		[ForeignKey("ixPickBatch")]
		public virtual PickBatches PickBatches { get; set; }
		[ForeignKey("ixStatus")]
		public virtual Statuses Statuses { get; set; }
    }
}
  

