using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class InboundOrderLinesPost : IInboundOrderLinesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Inbound Order Line ID")]
		public virtual Int64 ixInboundOrderLine { get; set; }
		[Display(Name = "Inbound Order Line")]
		public virtual String sInboundOrderLine { get; set; }
		[Required]
		[Display(Name = "Inbound Order ID")]
		public virtual Int64 ixInboundOrder { get; set; }
		[StringLength(300)]
		[Required]
		[Display(Name = "Order Line Reference")]
		public virtual String sOrderLineReference { get; set; }
		[Required]
		[Display(Name = "Material ID")]
		public virtual Int64 ixMaterial { get; set; }
		[Display(Name = "Material Handling Unit Configuration ID")]
		public virtual Int64? ixMaterialHandlingUnitConfiguration { get; set; }
		[Display(Name = "Handling Unit Type ID")]
		public virtual Int64? ixHandlingUnitType { get; set; }
		[Display(Name = "Handling Unit Quantity")]
		public virtual Double? nHandlingUnitQuantity { get; set; }
		[Required]
		[Display(Name = "Base Unit Quantity Expected")]
		public virtual Double nBaseUnitQuantityExpected { get; set; }
		[Required]
		[Display(Name = "Base Unit Quantity Received")]
		public virtual Double nBaseUnitQuantityReceived { get; set; }
		[StringLength(100)]
		[Display(Name = "Batch Number")]
		public virtual String sBatchNumber { get; set; }
		[StringLength(100)]
		[Display(Name = "Serial Number")]
		public virtual String sSerialNumber { get; set; }
		[Required]
		[Display(Name = "Status ID")]
		public virtual Int64 ixStatus { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

