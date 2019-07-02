using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class InventoryLocationSizes : IInventoryLocationSizes
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public InventoryLocationSizes()
        {
		UnitsOfMeasurement _UnitsOfMeasurementFKDiffLengthUnit = new UnitsOfMeasurement();
		UnitsOfMeasurementFKDiffLengthUnit = _UnitsOfMeasurementFKDiffLengthUnit;
		UnitsOfMeasurement _UnitsOfMeasurementFKDiffWidthUnit = new UnitsOfMeasurement();
		UnitsOfMeasurementFKDiffWidthUnit = _UnitsOfMeasurementFKDiffWidthUnit;
		UnitsOfMeasurement _UnitsOfMeasurementFKDiffHeightUnit = new UnitsOfMeasurement();
		UnitsOfMeasurementFKDiffHeightUnit = _UnitsOfMeasurementFKDiffHeightUnit;
		UnitsOfMeasurement _UnitsOfMeasurementFKDiffUsableVolumeUnit = new UnitsOfMeasurement();
		UnitsOfMeasurementFKDiffUsableVolumeUnit = _UnitsOfMeasurementFKDiffUsableVolumeUnit;

        }
		[Display(Name = "Inventory Location Size ID")]
		public virtual Int64 ixInventoryLocationSize { get; set; }
		[Display(Name = "Inventory Location Size ID")]
		public virtual Int64 ixInventoryLocationSizeEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Inventory Location Size")]
		public virtual String sInventoryLocationSize { get; set; }
		[Required]
		[Display(Name = "Length")]
		public virtual Double nLength { get; set; }
		[Required]
		[Display(Name = "Length Unit ID")]
		public virtual Int64 ixLengthUnit { get; set; }
		[Required]
		[Display(Name = "Width")]
		public virtual Double nWidth { get; set; }
		[Required]
		[Display(Name = "Width Unit ID")]
		public virtual Int64 ixWidthUnit { get; set; }
		[Required]
		[Display(Name = "Height")]
		public virtual Double nHeight { get; set; }
		[Required]
		[Display(Name = "Height Unit ID")]
		public virtual Int64 ixHeightUnit { get; set; }
		[Required]
		[Display(Name = "Usable Volume")]
		public virtual Double nUsableVolume { get; set; }
		[Required]
		[Display(Name = "Usable Volume Unit ID")]
		public virtual Int64 ixUsableVolumeUnit { get; set; }
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
		[ForeignKey("ixLengthUnit")]
		public virtual UnitsOfMeasurement UnitsOfMeasurementFKDiffLengthUnit { get; set; }
		[ForeignKey("ixWidthUnit")]
		public virtual UnitsOfMeasurement UnitsOfMeasurementFKDiffWidthUnit { get; set; }
		[ForeignKey("ixHeightUnit")]
		public virtual UnitsOfMeasurement UnitsOfMeasurementFKDiffHeightUnit { get; set; }
		[ForeignKey("ixUsableVolumeUnit")]
		public virtual UnitsOfMeasurement UnitsOfMeasurementFKDiffUsableVolumeUnit { get; set; }
    }
}
  

