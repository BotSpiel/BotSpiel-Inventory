using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class MeasurementUnitsOf : IMeasurementUnitsOf
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Measurement Unit Of ID")]
		public virtual Int64 ixMeasurementUnitOf { get; set; }
		[Display(Name = "Measurement Unit Of ID")]
		public virtual Int64 ixMeasurementUnitOfEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Measurement Unit Of")]
		public virtual String sMeasurementUnitOf { get; set; }
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
    }
}
  

