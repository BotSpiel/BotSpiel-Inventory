using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IMaterialHandlingUnitConfigurations
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixMaterialHandlingUnitConfiguration { get; set; }
		Int64 ixMaterialHandlingUnitConfigurationEdit { get; set; }
		String sMaterialHandlingUnitConfiguration { get; set; }
		Int64 ixMaterial { get; set; }
		Int32 nNestingLevel { get; set; }
		Int64 ixHandlingUnitType { get; set; }
		Double nQuantity { get; set; }
		Double? nLength { get; set; }
		Int64? ixLengthUnit { get; set; }
		Double? nWidth { get; set; }
		Int64? ixWidthUnit { get; set; }
		Double? nHeight { get; set; }
		Int64? ixHeightUnit { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		Materials Materials { get; set; }
		HandlingUnitTypes HandlingUnitTypes { get; set; }
		UnitsOfMeasurement UnitsOfMeasurementFKDiffLengthUnit { get; set; }
		UnitsOfMeasurement UnitsOfMeasurementFKDiffWidthUnit { get; set; }
		UnitsOfMeasurement UnitsOfMeasurementFKDiffHeightUnit { get; set; }
    }
}
  

