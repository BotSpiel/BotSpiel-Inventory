using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class MaterialHandlingUnitConfigurations : IMaterialHandlingUnitConfigurations
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public MaterialHandlingUnitConfigurations()
        {
		Materials _Materials = new Materials();
		Materials = _Materials;
		HandlingUnitTypes _HandlingUnitTypes = new HandlingUnitTypes();
		HandlingUnitTypes = _HandlingUnitTypes;
		UnitsOfMeasurement _UnitsOfMeasurementFKDiffLengthUnit = new UnitsOfMeasurement();
		UnitsOfMeasurementFKDiffLengthUnit = _UnitsOfMeasurementFKDiffLengthUnit;
		UnitsOfMeasurement _UnitsOfMeasurementFKDiffWidthUnit = new UnitsOfMeasurement();
		UnitsOfMeasurementFKDiffWidthUnit = _UnitsOfMeasurementFKDiffWidthUnit;
		UnitsOfMeasurement _UnitsOfMeasurementFKDiffHeightUnit = new UnitsOfMeasurement();
		UnitsOfMeasurementFKDiffHeightUnit = _UnitsOfMeasurementFKDiffHeightUnit;

        }
		[Display(Name = "Material Handling Unit Configuration ID")]
		public virtual Int64 ixMaterialHandlingUnitConfiguration { get; set; }
		[Display(Name = "Material Handling Unit Configuration ID")]
		public virtual Int64 ixMaterialHandlingUnitConfigurationEdit { get; set; }
		[Display(Name = "Material Handling Unit Configuration")]
        //Custom Code Start | Replaced Code Block
        //Replaced Code Block Start
        //public virtual String sMaterialHandlingUnitConfiguration { get; set; }
        //Replaced Code Block End
        public virtual String sMaterialHandlingUnitConfiguration { get { return this.Materials.sMaterial + "-" + this.HandlingUnitTypes.sHandlingUnitType + " Qty:" + this.nQuantity.ToString();  } set { } }
        //Custom Code End
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
		[ForeignKey("ixMaterial")]
		public virtual Materials Materials { get; set; }
		[ForeignKey("ixHandlingUnitType")]
		public virtual HandlingUnitTypes HandlingUnitTypes { get; set; }
		[ForeignKey("ixLengthUnit")]
		public virtual UnitsOfMeasurement UnitsOfMeasurementFKDiffLengthUnit { get; set; }
		[ForeignKey("ixWidthUnit")]
		public virtual UnitsOfMeasurement UnitsOfMeasurementFKDiffWidthUnit { get; set; }
		[ForeignKey("ixHeightUnit")]
		public virtual UnitsOfMeasurement UnitsOfMeasurementFKDiffHeightUnit { get; set; }
    }
}
  

