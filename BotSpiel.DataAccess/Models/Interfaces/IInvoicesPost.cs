using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IInvoicesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixInvoice { get; set; }
		String sInvoice { get; set; }
		Int64 ixPurchase { get; set; }
		String UserName { get; set; }
    }
}
  

