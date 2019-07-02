using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class UnitsOfMeasurement : IUnitsOfMeasurement
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public UnitsOfMeasurement()
        {
		MeasurementUnitsOf _MeasurementUnitsOf = new MeasurementUnitsOf();
		MeasurementUnitsOf = _MeasurementUnitsOf;
		MeasurementSystems _MeasurementSystems = new MeasurementSystems();
		MeasurementSystems = _MeasurementSystems;

        }
		[Display(Name = "Unit Of Measurement ID")]
		public virtual Int64 ixUnitOfMeasurement { get; set; }
		[Display(Name = "Unit Of Measurement ID")]
		public virtual Int64 ixUnitOfMeasurementEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Unit Of Measurement")]
		public virtual String sUnitOfMeasurement { get; set; }
		[Required]
		[Display(Name = "Measurement Unit Of ID")]
		public virtual Int64 ixMeasurementUnitOf { get; set; }
		[Required]
		[Display(Name = "Measurement System ID")]
		public virtual Int64 ixMeasurementSystem { get; set; }
		[StringLength(300)]
		[Display(Name = "Symbol")]
		public virtual String sSymbol { get; set; }
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
		[ForeignKey("ixMeasurementUnitOf")]
		public virtual MeasurementUnitsOf MeasurementUnitsOf { get; set; }
		[ForeignKey("ixMeasurementSystem")]
		public virtual MeasurementSystems MeasurementSystems { get; set; }
    }
}
  

