using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IUnitsOfMeasurement
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixUnitOfMeasurement { get; set; }
		Int64 ixUnitOfMeasurementEdit { get; set; }
		String sUnitOfMeasurement { get; set; }
		Int64 ixMeasurementUnitOf { get; set; }
		Int64 ixMeasurementSystem { get; set; }
		String sSymbol { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		MeasurementUnitsOf MeasurementUnitsOf { get; set; }
		MeasurementSystems MeasurementSystems { get; set; }
    }
}
  

