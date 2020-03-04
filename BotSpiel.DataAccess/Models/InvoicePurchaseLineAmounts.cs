using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class InvoicePurchaseLineAmounts : IInvoicePurchaseLineAmounts
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public InvoicePurchaseLineAmounts()
        {
		Invoices _Invoices = new Invoices();
		Invoices = _Invoices;
		PurchaseLines _PurchaseLines = new PurchaseLines();
		PurchaseLines = _PurchaseLines;
		Currencies _Currencies = new Currencies();
		Currencies = _Currencies;

        }
		[Display(Name = "Invoice Purchase Line Amount ID")]
		public virtual Int64 ixInvoicePurchaseLineAmount { get; set; }
		[Display(Name = "Invoice Purchase Line Amount ID")]
		public virtual Int64 ixInvoicePurchaseLineAmountEdit { get; set; }
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
		[ForeignKey("ixInvoice")]
		public virtual Invoices Invoices { get; set; }
		[ForeignKey("ixPurchaseLine")]
		public virtual PurchaseLines PurchaseLines { get; set; }
		[ForeignKey("ixCurrency")]
		public virtual Currencies Currencies { get; set; }
    }
}
  

