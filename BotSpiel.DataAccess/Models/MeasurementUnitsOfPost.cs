using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class MeasurementUnitsOfPost : IMeasurementUnitsOfPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Measurement Unit Of ID")]
		public virtual Int64 ixMeasurementUnitOf { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyMeasurementUnitOf", controller: "MeasurementUnitsOf", AdditionalFields = nameof(ixMeasurementUnitOf))]
		[Display(Name = "Measurement Unit Of")]
		public virtual String sMeasurementUnitOf { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

