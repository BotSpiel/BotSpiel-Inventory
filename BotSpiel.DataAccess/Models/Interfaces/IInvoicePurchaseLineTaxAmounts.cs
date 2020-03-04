using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IInvoicePurchaseLineTaxAmounts
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixInvoicePurchaseLineTaxAmount { get; set; }
		Int64 ixInvoicePurchaseLineTaxAmountEdit { get; set; }
		String sInvoicePurchaseLineTaxAmount { get; set; }
		Int64 ixInvoicePurchaseLineAmount { get; set; }
		Int64 ixTax { get; set; }
		Decimal mAmount { get; set; }
		Int64 ixCurrency { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		InvoicePurchaseLineAmounts InvoicePurchaseLineAmounts { get; set; }
		Taxes Taxes { get; set; }
		Currencies Currencies { get; set; }
    }
}
  

