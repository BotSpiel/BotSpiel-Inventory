using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class OutboundOrderLinePackingPost : IOutboundOrderLinePackingPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Outbound Order Line Pack ID")]
		public virtual Int64 ixOutboundOrderLinePack { get; set; }
		[Display(Name = "Outbound Order Line Pack")]
		public virtual String sOutboundOrderLinePack { get; set; }
		[Required]
		[Display(Name = "Outbound Order Line ID")]
		public virtual Int64 ixOutboundOrderLine { get; set; }
		[Required]
		[Display(Name = "Handling Unit ID")]
		public virtual Int64 ixHandlingUnit { get; set; }
		[Required]
		[Display(Name = "Base Unit Quantity Packed")]
		public virtual Double nBaseUnitQuantityPacked { get; set; }
		[Required]
		[Display(Name = "Status ID")]
		public virtual Int64 ixStatus { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

