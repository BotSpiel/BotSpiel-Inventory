using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class MaterialHandlingUnitConfigurationsPost : IMaterialHandlingUnitConfigurationsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Material Handling Unit Configuration ID")]
		public virtual Int64 ixMaterialHandlingUnitConfiguration { get; set; }
		[Display(Name = "Material Handling Unit Configuration")]
		public virtual String sMaterialHandlingUnitConfiguration { get; set; }
		[Required]
		[Display(Name = "Material ID")]
		public virtual Int64 ixMaterial { get; set; }
		[Required]
		[Display(Name = "Nesting Level")]
		public virtual Int32 nNestingLevel { get; set; }
		[Required]
		[Display(Name = "Handling Unit Type ID")]
		public virtual Int64 ixHandlingUnitType { get; set; }
		[Required]
		[Display(Name = "Quantity")]
		public virtual Double nQuantity { get; set; }
		[Display(Name = "Length")]
		public virtual Double? nLength { get; set; }
		[Display(Name = "Length Unit ID")]
		public virtual Int64? ixLengthUnit { get; set; }
		[Display(Name = "Width")]
		public virtual Double? nWidth { get; set; }
		[Display(Name = "Width Unit ID")]
		public virtual Int64? ixWidthUnit { get; set; }
		[Display(Name = "Height")]
		public virtual Double? nHeight { get; set; }
		[Display(Name = "Height Unit ID")]
		public virtual Int64? ixHeightUnit { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

