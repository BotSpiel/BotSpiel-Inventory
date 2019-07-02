using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class OutboundOrderLines : IOutboundOrderLines
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public OutboundOrderLines()
        {
		Materials _Materials = new Materials();
		Materials = _Materials;
		Statuses _Statuses = new Statuses();
		Statuses = _Statuses;

        }
		[Display(Name = "Outbound Order Line ID")]
		public virtual Int64 ixOutboundOrderLine { get; set; }
		[Display(Name = "Outbound Order Line ID")]
		public virtual Int64 ixOutboundOrderLineEdit { get; set; }
		[Display(Name = "Outbound Order Line")]
		public virtual String sOutboundOrderLine { get; set; }
		[StringLength(300)]
		[Required]
		[Display(Name = "Order Line Reference")]
		public virtual String sOrderLineReference { get; set; }
		[Required]
		[Display(Name = "Material ID")]
		public virtual Int64 ixMaterial { get; set; }
		[StringLength(100)]
		[Display(Name = "Batch Number")]
		public virtual String sBatchNumber { get; set; }
		[StringLength(100)]
		[Display(Name = "Serial Number")]
		public virtual String sSerialNumber { get; set; }
		[Required]
		[Display(Name = "Base Unit Quantity Ordered")]
		public virtual Double nBaseUnitQuantityOrdered { get; set; }
		[Required]
		[Display(Name = "Base Unit Quantity Shipped")]
		public virtual Double nBaseUnitQuantityShipped { get; set; }
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
		[ForeignKey("ixMaterial")]
		public virtual Materials Materials { get; set; }
		[ForeignKey("ixStatus")]
		public virtual Statuses Statuses { get; set; }
    }
}
  

