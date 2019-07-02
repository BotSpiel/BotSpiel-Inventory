using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IMaterialHandlingUnitConfigurationsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixMaterialHandlingUnitConfiguration { get; set; }
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
		String UserName { get; set; }
    }
}
  

