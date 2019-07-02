using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface ILocationFunctionsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixLocationFunction { get; set; }
		String sLocationFunction { get; set; }
		String sLocationFunctionCode { get; set; }
		String UserName { get; set; }
    }
}
  

