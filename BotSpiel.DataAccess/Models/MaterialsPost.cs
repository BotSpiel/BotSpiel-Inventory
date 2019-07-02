using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class MaterialsPost : IMaterialsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Material ID")]
		public virtual Int64 ixMaterial { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyMaterial", controller: "Materials", AdditionalFields = nameof(ixMaterial))]
		[Display(Name = "Material")]
		public virtual String sMaterial { get; set; }
		[StringLength(1000)]
		[Required]
		[Display(Name = "Description")]
		public virtual String sDescription { get; set; }
		[Required]
		[Display(Name = "Material Type ID")]
		public virtual Int64 ixMaterialType { get; set; }
		[Required]
		[Display(Name = "Base Unit ID")]
		public virtual Int64 ixBaseUnit { get; set; }
		[Required]
		[Display(Name = "Track Serial Number")]
		public virtual Boolean bTrackSerialNumber { get; set; }
		[Required]
		[Display(Name = "Track Batch Number")]
		public virtual Boolean bTrackBatchNumber { get; set; }
		[Required]
		[Display(Name = "Track Expiry")]
		public virtual Boolean bTrackExpiry { get; set; }
		[Display(Name = "Density")]
		public virtual Double? nDensity { get; set; }
		[Display(Name = "Density Unit ID")]
		public virtual Int64? ixDensityUnit { get; set; }
		[Display(Name = "Shelflife")]
		public virtual Double? nShelflife { get; set; }
		[Display(Name = "Shelflife Unit ID")]
		public virtual Int64? ixShelflifeUnit { get; set; }
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
		[Display(Name = "Weight")]
		public virtual Double? nWeight { get; set; }
		[Display(Name = "Weight Unit ID")]
		public virtual Int64? ixWeightUnit { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

