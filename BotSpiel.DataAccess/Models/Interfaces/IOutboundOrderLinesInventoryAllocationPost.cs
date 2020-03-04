using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IOutboundOrderLinesInventoryAllocationPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixOutboundOrderLineInventoryAllocation { get; set; }
		String sOutboundOrderLineInventoryAllocation { get; set; }
		Int64 ixOutboundOrderLine { get; set; }
		Double nBaseUnitQuantityAllocated { get; set; }
		Double nBaseUnitQuantityPicked { get; set; }
		Int64 ixStatus { get; set; }
		String UserName { get; set; }
    }
}
  

