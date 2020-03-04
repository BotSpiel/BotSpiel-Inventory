using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IOutboundOrderLinesInventoryAllocation
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixOutboundOrderLineInventoryAllocation { get; set; }
		Int64 ixOutboundOrderLineInventoryAllocationEdit { get; set; }
		String sOutboundOrderLineInventoryAllocation { get; set; }
		Int64 ixOutboundOrderLine { get; set; }
		Double nBaseUnitQuantityAllocated { get; set; }
		Double nBaseUnitQuantityPicked { get; set; }
		Int64 ixStatus { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		OutboundOrderLines OutboundOrderLines { get; set; }
		Statuses Statuses { get; set; }
    }
}
  

