using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IPurchaseEmails
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixPurchaseEmail { get; set; }
		Int64 ixPurchaseEmailEdit { get; set; }
		String sPurchaseEmail { get; set; }
		Int64 ixPurchase { get; set; }
		Int64 ixSendEmail { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		Purchases Purchases { get; set; }
		SendEmails SendEmails { get; set; }
    }
}
  

