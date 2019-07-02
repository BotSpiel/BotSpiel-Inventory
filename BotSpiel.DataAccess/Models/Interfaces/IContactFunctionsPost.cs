using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IContactFunctionsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixContactFunction { get; set; }
		String sContactFunction { get; set; }
		String sContactFunctionCode { get; set; }
		String UserName { get; set; }
    }
}
  

