using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IUnitOfMeasurementConversions
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixUnitOfMeasurementConversion { get; set; }
		Int64 ixUnitOfMeasurementConversionEdit { get; set; }
		String sUnitOfMeasurementConversion { get; set; }
		Int64 ixUnitOfMeasurementFrom { get; set; }
		Int64 ixUnitOfMeasurementTo { get; set; }
		Double nMultiplier { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		UnitsOfMeasurement UnitsOfMeasurementFKDiffUnitOfMeasurementFrom { get; set; }
		UnitsOfMeasurement UnitsOfMeasurementFKDiffUnitOfMeasurementTo { get; set; }
    }
}
  

