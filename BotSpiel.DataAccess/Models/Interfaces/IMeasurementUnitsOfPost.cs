using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IMeasurementUnitsOfPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixMeasurementUnitOf { get; set; }
		String sMeasurementUnitOf { get; set; }
		String UserName { get; set; }
    }
}
  

