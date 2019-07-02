using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class HandlingUnits : IHandlingUnits
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public HandlingUnits()
        {
		HandlingUnitTypes _HandlingUnitTypes = new HandlingUnitTypes();
		HandlingUnitTypes = _HandlingUnitTypes;
		Materials _MaterialsFKDiffPackingMaterial = new Materials();
		MaterialsFKDiffPackingMaterial = _MaterialsFKDiffPackingMaterial;
		MaterialHandlingUnitConfigurations _MaterialHandlingUnitConfigurations = new MaterialHandlingUnitConfigurations();
		MaterialHandlingUnitConfigurations = _MaterialHandlingUnitConfigurations;
		UnitsOfMeasurement _UnitsOfMeasurementFKDiffLengthUnit = new UnitsOfMeasurement();
		UnitsOfMeasurementFKDiffLengthUnit = _UnitsOfMeasurementFKDiffLengthUnit;
		UnitsOfMeasurement _UnitsOfMeasurementFKDiffWidthUnit = new UnitsOfMeasurement();
		UnitsOfMeasurementFKDiffWidthUnit = _UnitsOfMeasurementFKDiffWidthUnit;
		UnitsOfMeasurement _UnitsOfMeasurementFKDiffHeightUnit = new UnitsOfMeasurement();
		UnitsOfMeasurementFKDiffHeightUnit = _UnitsOfMeasurementFKDiffHeightUnit;
		UnitsOfMeasurement _UnitsOfMeasurementFKDiffWeightUnit = new UnitsOfMeasurement();
		UnitsOfMeasurementFKDiffWeightUnit = _UnitsOfMeasurementFKDiffWeightUnit;
		Statuses _Statuses = new Statuses();
		Statuses = _Statuses;

        }
		[Display(Name = "Handling Unit ID")]
		public virtual Int64 ixHandlingUnit { get; set; }
		[Display(Name = "Handling Unit ID")]
		public virtual Int64 ixHandlingUnitEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Handling Unit")]
		public virtual String sHandlingUnit { get; set; }
		[Required]
		[Display(Name = "Handling Unit Type ID")]
		public virtual Int64 ixHandlingUnitType { get; set; }
		[Display(Name = "Parent Handling Unit ID")]
		public virtual Int64? ixParentHandlingUnit { get; set; }
		[Display(Name = "Packing Material ID")]
		public virtual Int64? ixPackingMaterial { get; set; }
		[Display(Name = "Material Handling Unit Configuration ID")]
		public virtual Int64? ixMaterialHandlingUnitConfiguration { get; set; }
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
		[Display(Name = "Status ID")]
		public virtual Int64? ixStatus { get; set; }
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
		[ForeignKey("ixHandlingUnitType")]
		public virtual HandlingUnitTypes HandlingUnitTypes { get; set; }
		[ForeignKey("ixParentHandlingUnit")]
		public virtual HandlingUnits HandlingUnitsFKDiffParentHandlingUnit { get; set; }
		[ForeignKey("ixPackingMaterial")]
		public virtual Materials MaterialsFKDiffPackingMaterial { get; set; }
		[ForeignKey("ixMaterialHandlingUnitConfiguration")]
		public virtual MaterialHandlingUnitConfigurations MaterialHandlingUnitConfigurations { get; set; }
		[ForeignKey("ixLengthUnit")]
		public virtual UnitsOfMeasurement UnitsOfMeasurementFKDiffLengthUnit { get; set; }
		[ForeignKey("ixWidthUnit")]
		public virtual UnitsOfMeasurement UnitsOfMeasurementFKDiffWidthUnit { get; set; }
		[ForeignKey("ixHeightUnit")]
		public virtual UnitsOfMeasurement UnitsOfMeasurementFKDiffHeightUnit { get; set; }
		[ForeignKey("ixWeightUnit")]
		public virtual UnitsOfMeasurement UnitsOfMeasurementFKDiffWeightUnit { get; set; }
		[ForeignKey("ixStatus")]
		public virtual Statuses Statuses { get; set; }
    }
}
  

