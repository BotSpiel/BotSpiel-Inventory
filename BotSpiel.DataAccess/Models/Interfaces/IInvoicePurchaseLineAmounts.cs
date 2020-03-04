using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IInvoicePurchaseLineAmounts
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixInvoicePurchaseLineAmount { get; set; }
		Int64 ixInvoicePurchaseLineAmountEdit { get; set; }
		String sInvoicePurchaseLineAmount { get; set; }
		Int64 ixInvoice { get; set; }
		Int64 ixPurchaseLine { get; set; }
		Decimal mAmount { get; set; }
		Int64 ixCurrency { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		Invoices Invoices { get; set; }
		PurchaseLines PurchaseLines { get; set; }
		Currencies Currencies { get; set; }
    }
}
  

