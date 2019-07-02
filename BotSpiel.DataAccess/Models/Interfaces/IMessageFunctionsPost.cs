using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IMessageFunctionsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixMessageFunction { get; set; }
		String sMessageFunction { get; set; }
		String sMessageFunctionCode { get; set; }
		String UserName { get; set; }
    }
}
  

