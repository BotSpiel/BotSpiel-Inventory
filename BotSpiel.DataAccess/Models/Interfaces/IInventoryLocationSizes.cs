using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IInventoryLocationSizes
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixInventoryLocationSize { get; set; }
		Int64 ixInventoryLocationSizeEdit { get; set; }
		String sInventoryLocationSize { get; set; }
		Double nLength { get; set; }
		Int64 ixLengthUnit { get; set; }
		Double nWidth { get; set; }
		Int64 ixWidthUnit { get; set; }
		Double nHeight { get; set; }
		Int64 ixHeightUnit { get; set; }
		Double nUsableVolume { get; set; }
		Int64 ixUsableVolumeUnit { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		UnitsOfMeasurement UnitsOfMeasurementFKDiffLengthUnit { get; set; }
		UnitsOfMeasurement UnitsOfMeasurementFKDiffWidthUnit { get; set; }
		UnitsOfMeasurement UnitsOfMeasurementFKDiffHeightUnit { get; set; }
		UnitsOfMeasurement UnitsOfMeasurementFKDiffUsableVolumeUnit { get; set; }
    }
}
  

