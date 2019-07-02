using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IMaterialsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixMaterial { get; set; }
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
		String UserName { get; set; }
    }
}
  

