using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IPurchaseTextMessagesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixPurchaseTextMessage { get; set; }
		String sPurchaseTextMessage { get; set; }
		Int64 ixPurchase { get; set; }
		Int64 ixSendTextMessage { get; set; }
		String UserName { get; set; }
    }
}
  

