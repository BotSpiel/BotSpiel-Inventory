using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IDateTimePeriodFunctions
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixDateTimePeriodFunction { get; set; }
		Int64 ixDateTimePeriodFunctionEdit { get; set; }
		String sDateTimePeriodFunction { get; set; }
		String sDateTimePeriodFunctionCode { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
    }
}
  

