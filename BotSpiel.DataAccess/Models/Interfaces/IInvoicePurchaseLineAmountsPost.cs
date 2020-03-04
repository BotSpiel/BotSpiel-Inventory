using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IInvoicePurchaseLineAmountsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixInvoicePurchaseLineAmount { get; set; }
		String sInvoicePurchaseLineAmount { get; set; }
		Int64 ixInvoice { get; set; }
		Int64 ixPurchaseLine { get; set; }
		Decimal mAmount { get; set; }
		Int64 ixCurrency { get; set; }
		String UserName { get; set; }
    }
}
  

