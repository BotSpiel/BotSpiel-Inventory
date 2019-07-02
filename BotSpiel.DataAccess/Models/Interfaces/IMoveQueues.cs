using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IMoveQueues
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixMoveQueue { get; set; }
		Int64 ixMoveQueueEdit { get; set; }
		String sMoveQueue { get; set; }
		Int64 ixMoveQueueType { get; set; }
		Int64 ixMoveQueueContext { get; set; }
		Int64? ixSourceInventoryUnit { get; set; }
		Int64? ixTargetInventoryUnit { get; set; }
		Int64? ixSourceInventoryLocation { get; set; }
		Int64? ixTargetInventoryLocation { get; set; }
		Int64? ixSourceHandlingUnit { get; set; }
		Int64? ixTargetHandlingUnit { get; set; }
		String sPreferredResource { get; set; }
		Double nBaseUnitQuantity { get; set; }
		DateTime? dtStartBy { get; set; }
		DateTime? dtCompleteBy { get; set; }
		DateTime? dtStartedAt { get; set; }
		DateTime? dtCompletedAt { get; set; }
		Int64? ixInboundOrderLine { get; set; }
		Int64? ixOutboundOrderLine { get; set; }
		Int64? ixPickBatch { get; set; }
		Int64 ixStatus { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		MoveQueueTypes MoveQueueTypes { get; set; }
		MoveQueueContexts MoveQueueContexts { get; set; }
		InventoryUnits InventoryUnitsFKDiffSourceInventoryUnit { get; set; }
		InventoryUnits InventoryUnitsFKDiffTargetInventoryUnit { get; set; }
		InventoryLocations InventoryLocationsFKDiffSourceInventoryLocation { get; set; }
		InventoryLocations InventoryLocationsFKDiffTargetInventoryLocation { get; set; }
		HandlingUnits HandlingUnitsFKDiffSourceHandlingUnit { get; set; }
		HandlingUnits HandlingUnitsFKDiffTargetHandlingUnit { get; set; }
		InboundOrderLines InboundOrderLines { get; set; }
		OutboundOrderLines OutboundOrderLines { get; set; }
		PickBatches PickBatches { get; set; }
		Statuses Statuses { get; set; }
    }
}
  

