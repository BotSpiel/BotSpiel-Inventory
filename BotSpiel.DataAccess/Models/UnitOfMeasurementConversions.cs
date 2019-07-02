using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class UnitOfMeasurementConversions : IUnitOfMeasurementConversions
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public UnitOfMeasurementConversions()
        {
		UnitsOfMeasurement _UnitsOfMeasurementFKDiffUnitOfMeasurementFrom = new UnitsOfMeasurement();
		UnitsOfMeasurementFKDiffUnitOfMeasurementFrom = _UnitsOfMeasurementFKDiffUnitOfMeasurementFrom;
		UnitsOfMeasurement _UnitsOfMeasurementFKDiffUnitOfMeasurementTo = new UnitsOfMeasurement();
		UnitsOfMeasurementFKDiffUnitOfMeasurementTo = _UnitsOfMeasurementFKDiffUnitOfMeasurementTo;

        }
		[Display(Name = "Unit Of Measurement Conversion ID")]
		public virtual Int64 ixUnitOfMeasurementConversion { get; set; }
		[Display(Name = "Unit Of Measurement Conversion ID")]
		public virtual Int64 ixUnitOfMeasurementConversionEdit { get; set; }
		[Display(Name = "Unit Of Measurement Conversion")]
		public virtual String sUnitOfMeasurementConversion { get; set; }
		[Required]
		[Display(Name = "Unit Of Measurement From ID")]
		public virtual Int64 ixUnitOfMeasurementFrom { get; set; }
		[Required]
		[Display(Name = "Unit Of Measurement To ID")]
		public virtual Int64 ixUnitOfMeasurementTo { get; set; }
		[Required]
		[Display(Name = "Multiplier")]
		public virtual Double nMultiplier { get; set; }
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
		[ForeignKey("ixUnitOfMeasurementFrom")]
		public virtual UnitsOfMeasurement UnitsOfMeasurementFKDiffUnitOfMeasurementFrom { get; set; }
		[ForeignKey("ixUnitOfMeasurementTo")]
		public virtual UnitsOfMeasurement UnitsOfMeasurementFKDiffUnitOfMeasurementTo { get; set; }
    }
}
  

