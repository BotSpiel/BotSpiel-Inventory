using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IUnitsOfMeasurementPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixUnitOfMeasurement { get; set; }
		String sUnitOfMeasurement { get; set; }
		Int64 ixMeasurementUnitOf { get; set; }
		Int64 ixMeasurementSystem { get; set; }
		String sSymbol { get; set; }
		String UserName { get; set; }
    }
}
  

