using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IPurchaseEmailsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixPurchaseEmail { get; set; }
		String sPurchaseEmail { get; set; }
		Int64 ixPurchase { get; set; }
		Int64 ixSendEmail { get; set; }
		String UserName { get; set; }
    }
}
  

