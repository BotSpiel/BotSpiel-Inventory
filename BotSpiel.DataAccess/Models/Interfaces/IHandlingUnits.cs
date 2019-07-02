using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IHandlingUnits
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixHandlingUnit { get; set; }
		Int64 ixHandlingUnitEdit { get; set; }
		String sHandlingUnit { get; set; }
		Int64 ixHandlingUnitType { get; set; }
		Int64? ixParentHandlingUnit { get; set; }
		Int64? ixPackingMaterial { get; set; }
		Int64? ixMaterialHandlingUnitConfiguration { get; set; }
		Double? nLength { get; set; }
		Int64? ixLengthUnit { get; set; }
		Double? nWidth { get; set; }
		Int64? ixWidthUnit { get; set; }
		Double? nHeight { get; set; }
		Int64? ixHeightUnit { get; set; }
		Double? nWeight { get; set; }
		Int64? ixWeightUnit { get; set; }
		Int64? ixStatus { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		HandlingUnitTypes HandlingUnitTypes { get; set; }
		HandlingUnits HandlingUnitsFKDiffParentHandlingUnit { get; set; }
		Materials MaterialsFKDiffPackingMaterial { get; set; }
		MaterialHandlingUnitConfigurations MaterialHandlingUnitConfigurations { get; set; }
		UnitsOfMeasurement UnitsOfMeasurementFKDiffLengthUnit { get; set; }
		UnitsOfMeasurement UnitsOfMeasurementFKDiffWidthUnit { get; set; }
		UnitsOfMeasurement UnitsOfMeasurementFKDiffHeightUnit { get; set; }
		UnitsOfMeasurement UnitsOfMeasurementFKDiffWeightUnit { get; set; }
		Statuses Statuses { get; set; }
    }
}
  

