using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class OutboundOrderLinesPost : IOutboundOrderLinesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Outbound Order Line ID")]
		public virtual Int64 ixOutboundOrderLine { get; set; }
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
		public virtual String UserName { get; set; }
    }
}
  

