using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class InvoicePurchaseLineTaxAmounts : IInvoicePurchaseLineTaxAmounts
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public InvoicePurchaseLineTaxAmounts()
        {
		InvoicePurchaseLineAmounts _InvoicePurchaseLineAmounts = new InvoicePurchaseLineAmounts();
		InvoicePurchaseLineAmounts = _InvoicePurchaseLineAmounts;
		Taxes _Taxes = new Taxes();
		Taxes = _Taxes;
		Currencies _Currencies = new Currencies();
		Currencies = _Currencies;

        }
		[Display(Name = "Invoice Purchase Line Tax Amount ID")]
		public virtual Int64 ixInvoicePurchaseLineTaxAmount { get; set; }
		[Display(Name = "Invoice Purchase Line Tax Amount ID")]
		public virtual Int64 ixInvoicePurchaseLineTaxAmountEdit { get; set; }
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
		[ForeignKey("ixInvoicePurchaseLineAmount")]
		public virtual InvoicePurchaseLineAmounts InvoicePurchaseLineAmounts { get; set; }
		[ForeignKey("ixTax")]
		public virtual Taxes Taxes { get; set; }
		[ForeignKey("ixCurrency")]
		public virtual Currencies Currencies { get; set; }
    }
}
  

