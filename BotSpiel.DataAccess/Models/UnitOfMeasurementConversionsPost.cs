using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class UnitOfMeasurementConversionsPost : IUnitOfMeasurementConversionsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Unit Of Measurement Conversion ID")]
		public virtual Int64 ixUnitOfMeasurementConversion { get; set; }
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
		public virtual String UserName { get; set; }
    }
}
  

