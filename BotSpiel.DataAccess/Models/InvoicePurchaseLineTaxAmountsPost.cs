using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class InvoicePurchaseLineTaxAmountsPost : IInvoicePurchaseLineTaxAmountsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Invoice Purchase Line Tax Amount ID")]
		public virtual Int64 ixInvoicePurchaseLineTaxAmount { get; set; }
		[Display(Name = "Invoice Purchase Line Tax Amount")]
		public virtual String sInvoicePurchaseLineTaxAmount { get; set; }
		[Required]
		[Display(Name = "Invoice Purchase Line Amount ID")]
		public virtual Int64 ixInvoicePurchaseLineAmount { get; set; }
		[Required]
		[Display(Name = "Tax ID")]
		public virtual Int64 ixTax { get; set; }
		[Required]
		[Display(Name = "Amount")]
		public virtual Decimal mAmount { get; set; }
		[Required]
		[Display(Name = "Currency ID")]
		public virtual Int64 ixCurrency { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

