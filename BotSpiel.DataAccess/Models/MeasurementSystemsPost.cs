using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class MeasurementSystemsPost : IMeasurementSystemsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Measurement System ID")]
		public virtual Int64 ixMeasurementSystem { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyMeasurementSystem", controller: "MeasurementSystems", AdditionalFields = nameof(ixMeasurementSystem))]
		[Display(Name = "Measurement System")]
		public virtual String sMeasurementSystem { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

