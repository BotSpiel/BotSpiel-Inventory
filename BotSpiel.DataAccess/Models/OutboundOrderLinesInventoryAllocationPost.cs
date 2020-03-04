using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class OutboundOrderLinesInventoryAllocationPost : IOutboundOrderLinesInventoryAllocationPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Outbound Order Line Inventory Allocation ID")]
		public virtual Int64 ixOutboundOrderLineInventoryAllocation { get; set; }
		[Display(Name = "Outbound Order Line Inventory Allocation")]
		public virtual String sOutboundOrderLineInventoryAllocation { get; set; }
		[Required]
		[Display(Name = "Outbound Order Line ID")]
		public virtual Int64 ixOutboundOrderLine { get; set; }
		[Required]
		[Display(Name = "Base Unit Quantity Allocated")]
		public virtual Double nBaseUnitQuantityAllocated { get; set; }
		[Required]
		[Display(Name = "Base Unit Quantity Picked")]
		public virtual Double nBaseUnitQuantityPicked { get; set; }
		[Required]
		[Display(Name = "Status ID")]
		public virtual Int64 ixStatus { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

