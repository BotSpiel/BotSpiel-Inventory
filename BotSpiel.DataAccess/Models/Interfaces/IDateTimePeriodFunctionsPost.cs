using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IDateTimePeriodFunctionsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixDateTimePeriodFunction { get; set; }
		String sDateTimePeriodFunction { get; set; }
		String sDateTimePeriodFunctionCode { get; set; }
		String UserName { get; set; }
    }
}
  

