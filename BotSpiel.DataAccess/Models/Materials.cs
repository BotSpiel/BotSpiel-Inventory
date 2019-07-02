using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class Materials : IMaterials
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public Materials()
        {
		MaterialTypes _MaterialTypes = new MaterialTypes();
		MaterialTypes = _MaterialTypes;
		UnitsOfMeasurement _UnitsOfMeasurementFKDiffBaseUnit = new UnitsOfMeasurement();
		UnitsOfMeasurementFKDiffBaseUnit = _UnitsOfMeasurementFKDiffBaseUnit;
		UnitsOfMeasurement _UnitsOfMeasurementFKDiffDensityUnit = new UnitsOfMeasurement();
		UnitsOfMeasurementFKDiffDensityUnit = _UnitsOfMeasurementFKDiffDensityUnit;
		UnitsOfMeasurement _UnitsOfMeasurementFKDiffShelflifeUnit = new UnitsOfMeasurement();
		UnitsOfMeasurementFKDiffShelflifeUnit = _UnitsOfMeasurementFKDiffShelflifeUnit;
		UnitsOfMeasurement _UnitsOfMeasurementFKDiffLengthUnit = new UnitsOfMeasurement();
		UnitsOfMeasurementFKDiffLengthUnit = _UnitsOfMeasurementFKDiffLengthUnit;
		UnitsOfMeasurement _UnitsOfMeasurementFKDiffWidthUnit = new UnitsOfMeasurement();
		UnitsOfMeasurementFKDiffWidthUnit = _UnitsOfMeasurementFKDiffWidthUnit;
		UnitsOfMeasurement _UnitsOfMeasurementFKDiffHeightUnit = new UnitsOfMeasurement();
		UnitsOfMeasurementFKDiffHeightUnit = _UnitsOfMeasurementFKDiffHeightUnit;
		UnitsOfMeasurement _UnitsOfMeasurementFKDiffWeightUnit = new UnitsOfMeasurement();
		UnitsOfMeasurementFKDiffWeightUnit = _UnitsOfMeasurementFKDiffWeightUnit;

        }
		[Display(Name = "Material ID")]
		public virtual Int64 ixMaterial { get; set; }
		[Display(Name = "Material ID")]
		public virtual Int64 ixMaterialEdit { get; set; }
		[Required]
		[StringLength(300)]
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
		[Required]
		[Display(Name = "Created At")]
		public virtual DateTime dtCreatedAt { get; set; }
		[Required]
		[Display(Name = "Changed At")]
		public virtual DateTime dtChangedAt { get; set; }
		[StringLength(256)]
		[Required]
		[Display(Name = "Created By")]
		public virtual String sCreatedBy { get; set; }
		[StringLength(256)]
		[Required]
		[Display(Name = "Changed By")]
		public virtual String sChangedBy { get; set; }
		[ForeignKey("ixMaterialType")]
		public virtual MaterialTypes MaterialTypes { get; set; }
		[ForeignKey("ixBaseUnit")]
		public virtual UnitsOfMeasurement UnitsOfMeasurementFKDiffBaseUnit { get; set; }
		[ForeignKey("ixDensityUnit")]
		public virtual UnitsOfMeasurement UnitsOfMeasurementFKDiffDensityUnit { get; set; }
		[ForeignKey("ixShelflifeUnit")]
		public virtual UnitsOfMeasurement UnitsOfMeasurementFKDiffShelflifeUnit { get; set; }
		[ForeignKey("ixLengthUnit")]
		public virtual UnitsOfMeasurement UnitsOfMeasurementFKDiffLengthUnit { get; set; }
		[ForeignKey("ixWidthUnit")]
		public virtual UnitsOfMeasurement UnitsOfMeasurementFKDiffWidthUnit { get; set; }
		[ForeignKey("ixHeightUnit")]
		public virtual UnitsOfMeasurement UnitsOfMeasurementFKDiffHeightUnit { get; set; }
		[ForeignKey("ixWeightUnit")]
		public virtual UnitsOfMeasurement UnitsOfMeasurementFKDiffWeightUnit { get; set; }
    }
}
  

