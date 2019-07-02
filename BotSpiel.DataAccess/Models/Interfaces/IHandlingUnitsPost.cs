using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IHandlingUnitsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixHandlingUnit { get; set; }
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
		String UserName { get; set; }
    }
}
  

