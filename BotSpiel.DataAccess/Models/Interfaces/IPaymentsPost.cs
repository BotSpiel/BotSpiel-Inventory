using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IPaymentsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixPayment { get; set; }
		String sPayment { get; set; }
		Int64 ixInvoice { get; set; }
		String UserName { get; set; }
    }
}
  

