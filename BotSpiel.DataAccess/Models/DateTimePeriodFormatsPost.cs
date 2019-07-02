using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class DateTimePeriodFormatsPost : IDateTimePeriodFormatsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Date Time Period Format ID")]
		public virtual Int64 ixDateTimePeriodFormat { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyDateTimePeriodFormat", controller: "DateTimePeriodFormats", AdditionalFields = nameof(ixDateTimePeriodFormat))]
		[Display(Name = "Date Time Period Format")]
		public virtual String sDateTimePeriodFormat { get; set; }
		[StringLength(30)]
		[Required]
		[Display(Name = "Date Time Period Format Code")]
		public virtual String sDateTimePeriodFormatCode { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

