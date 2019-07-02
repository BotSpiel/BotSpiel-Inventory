using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class DateTimePeriodFunctionsPost : IDateTimePeriodFunctionsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Date Time Period Function ID")]
		public virtual Int64 ixDateTimePeriodFunction { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyDateTimePeriodFunction", controller: "DateTimePeriodFunctions", AdditionalFields = nameof(ixDateTimePeriodFunction))]
		[Display(Name = "Date Time Period Function")]
		public virtual String sDateTimePeriodFunction { get; set; }
		[StringLength(30)]
		[Required]
		[Display(Name = "Date Time Period Function Code")]
		public virtual String sDateTimePeriodFunctionCode { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

