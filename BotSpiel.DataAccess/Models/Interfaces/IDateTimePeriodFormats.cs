using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IDateTimePeriodFormats
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixDateTimePeriodFormat { get; set; }
		Int64 ixDateTimePeriodFormatEdit { get; set; }
		String sDateTimePeriodFormat { get; set; }
		String sDateTimePeriodFormatCode { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
    }
}
  

