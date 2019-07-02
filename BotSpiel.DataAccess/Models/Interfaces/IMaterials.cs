using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IMaterials
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixMaterial { get; set; }
		Int64 ixMaterialEdit { get; set; }
		String sMaterial { get; set; }
		String sDescription { get; set; }
		Int64 ixMaterialType { get; set; }
		Int64 ixBaseUnit { get; set; }
		Boolean bTrackSerialNumber { get; set; }
		Boolean bTrackBatchNumber { get; set; }
		Boolean bTrackExpiry { get; set; }
		Double? nDensity { get; set; }
		Int64? ixDensityUnit { get; set; }
		Double? nShelflife { get; set; }
		Int64? ixShelflifeUnit { get; set; }
		Double? nLength { get; set; }
		Int64? ixLengthUnit { get; set; }
		Double? nWidth { get; set; }
		Int64? ixWidthUnit { get; set; }
		Double? nHeight { get; set; }
		Int64? ixHeightUnit { get; set; }
		Double? nWeight { get; set; }
		Int64? ixWeightUnit { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		MaterialTypes MaterialTypes { get; set; }
		UnitsOfMeasurement UnitsOfMeasurementFKDiffBaseUnit { get; set; }
		UnitsOfMeasurement UnitsOfMeasurementFKDiffDensityUnit { get; set; }
		UnitsOfMeasurement UnitsOfMeasurementFKDiffShelflifeUnit { get; set; }
		UnitsOfMeasurement UnitsOfMeasurementFKDiffLengthUnit { get; set; }
		UnitsOfMeasurement UnitsOfMeasurementFKDiffWidthUnit { get; set; }
		UnitsOfMeasurement UnitsOfMeasurementFKDiffHeightUnit { get; set; }
		UnitsOfMeasurement UnitsOfMeasurementFKDiffWeightUnit { get; set; }
    }
}
  

