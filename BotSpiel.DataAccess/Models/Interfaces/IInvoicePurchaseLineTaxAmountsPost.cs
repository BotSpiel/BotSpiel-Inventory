using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IInvoicePurchaseLineTaxAmountsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixInvoicePurchaseLineTaxAmount { get; set; }
		String sInvoicePurchaseLineTaxAmount { get; set; }
		Int64 ixInvoicePurchaseLineAmount { get; set; }
		Int64 ixTax { get; set; }
		Decimal mAmount { get; set; }
		Int64 ixCurrency { get; set; }
		String UserName { get; set; }
    }
}
  

