using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IPurchaseTextMessages
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixPurchaseTextMessage { get; set; }
		Int64 ixPurchaseTextMessageEdit { get; set; }
		String sPurchaseTextMessage { get; set; }
		Int64 ixPurchase { get; set; }
		Int64 ixSendTextMessage { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		Purchases Purchases { get; set; }
		SendTextMessages SendTextMessages { get; set; }
    }
}
  

