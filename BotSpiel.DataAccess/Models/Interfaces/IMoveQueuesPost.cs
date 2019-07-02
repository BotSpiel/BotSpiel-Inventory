using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IMoveQueuesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixMoveQueue { get; set; }
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
		String UserName { get; set; }
    }
}
  

