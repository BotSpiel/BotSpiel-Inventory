using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class UnitsOfMeasurementPost : IUnitsOfMeasurementPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Unit Of Measurement ID")]
		public virtual Int64 ixUnitOfMeasurement { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyUnitOfMeasurement", controller: "UnitsOfMeasurement", AdditionalFields = nameof(ixUnitOfMeasurement))]
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
		public virtual String UserName { get; set; }
    }
}
  

