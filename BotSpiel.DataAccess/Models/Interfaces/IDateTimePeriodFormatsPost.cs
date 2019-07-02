using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IDateTimePeriodFormatsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixDateTimePeriodFormat { get; set; }
		String sDateTimePeriodFormat { get; set; }
		String sDateTimePeriodFormatCode { get; set; }
		String UserName { get; set; }
    }
}
  

