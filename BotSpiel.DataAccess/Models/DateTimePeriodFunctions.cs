using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class DateTimePeriodFunctions : IDateTimePeriodFunctions
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Date Time Period Function ID")]
		public virtual Int64 ixDateTimePeriodFunction { get; set; }
		[Display(Name = "Date Time Period Function ID")]
		public virtual Int64 ixDateTimePeriodFunctionEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Date Time Period Function")]
		public virtual String sDateTimePeriodFunction { get; set; }
		[StringLength(30)]
		[Required]
		[Display(Name = "Date Time Period Function Code")]
		public virtual String sDateTimePeriodFunctionCode { get; set; }
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
  

