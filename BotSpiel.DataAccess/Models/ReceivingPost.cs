using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class ReceivingPost : IReceivingPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Receipt ID")]
		public virtual Int64 ixReceipt { get; set; }
		[Display(Name = "Receipt")]
		public virtual String sReceipt { get; set; }
		[Required]
		[Display(Name = "Inventory Location ID")]
		public virtual Int64 ixInventoryLocation { get; set; }
		[Required]
		[Display(Name = "Inbound Order ID")]
		public virtual Int64 ixInboundOrder { get; set; }
		[Required]
		[Display(Name = "Handling Unit ID")]
		public virtual Int64 ixHandlingUnit { get; set; }
		[Required]
		[Display(Name = "Material ID")]
		public virtual Int64 ixMaterial { get; set; }
		[Display(Name = "Material Handling Unit Configuration ID")]
		public virtual Int64? ixMaterialHandlingUnitConfiguration { get; set; }
		[Display(Name = "Handling Unit Type ID")]
		public virtual Int64? ixHandlingUnitType { get; set; }
		[Display(Name = "Handling Unit Quantity")]
		public virtual Double? nHandlingUnitQuantity { get; set; }
		[StringLength(100)]
		[Display(Name = "Batch Number")]
		public virtual String sBatchNumber { get; set; }
		[StringLength(100)]
		[Display(Name = "Serial Number")]
		public virtual String sSerialNumber { get; set; }
		[Required]
		[Display(Name = "Base Unit Quantity Received")]
		public virtual Double nBaseUnitQuantityReceived { get; set; }
		[Required]
		[Display(Name = "Status ID")]
		public virtual Int64 ixStatus { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

