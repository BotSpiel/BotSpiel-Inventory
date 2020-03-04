using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class InvoicePurchaseLineAmountsPost : IInvoicePurchaseLineAmountsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Invoice Purchase Line Amount ID")]
		public virtual Int64 ixInvoicePurchaseLineAmount { get; set; }
		[Display(Name = "Invoice Purchase Line Amount")]
		public virtual String sInvoicePurchaseLineAmount { get; set; }
		[Required]
		[Display(Name = "Invoice ID")]
		public virtual Int64 ixInvoice { get; set; }
		[Required]
		[Display(Name = "Purchase Line ID")]
		public virtual Int64 ixPurchaseLine { get; set; }
		[Required]
		[Display(Name = "Amount")]
		public virtual Decimal mAmount { get; set; }
		[Required]
		[Display(Name = "Currency ID")]
		public virtual Int64 ixCurrency { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

