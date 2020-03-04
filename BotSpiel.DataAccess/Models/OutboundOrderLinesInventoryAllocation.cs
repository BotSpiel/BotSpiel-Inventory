using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class OutboundOrderLinesInventoryAllocation : IOutboundOrderLinesInventoryAllocation
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public OutboundOrderLinesInventoryAllocation()
        {
		OutboundOrderLines _OutboundOrderLines = new OutboundOrderLines();
		OutboundOrderLines = _OutboundOrderLines;
		Statuses _Statuses = new Statuses();
		Statuses = _Statuses;

        }
		[Display(Name = "Outbound Order Line Inventory Allocation ID")]
		public virtual Int64 ixOutboundOrderLineInventoryAllocation { get; set; }
		[Display(Name = "Outbound Order Line Inventory Allocation ID")]
		public virtual Int64 ixOutboundOrderLineInventoryAllocationEdit { get; set; }
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
		[ForeignKey("ixOutboundOrderLine")]
		public virtual OutboundOrderLines OutboundOrderLines { get; set; }
		[ForeignKey("ixStatus")]
		public virtual Statuses Statuses { get; set; }
    }
}
  

