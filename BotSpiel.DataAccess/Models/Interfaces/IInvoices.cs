using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IInvoices
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixInvoice { get; set; }
		Int64 ixInvoiceEdit { get; set; }
		String sInvoice { get; set; }
		Int64 ixPurchase { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		Purchases Purchases { get; set; }
    }
}
  

