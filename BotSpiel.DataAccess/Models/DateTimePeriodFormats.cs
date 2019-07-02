using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class DateTimePeriodFormats : IDateTimePeriodFormats
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Date Time Period Format ID")]
		public virtual Int64 ixDateTimePeriodFormat { get; set; }
		[Display(Name = "Date Time Period Format ID")]
		public virtual Int64 ixDateTimePeriodFormatEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Date Time Period Format")]
		public virtual String sDateTimePeriodFormat { get; set; }
		[StringLength(30)]
		[Required]
		[Display(Name = "Date Time Period Format Code")]
		public virtual String sDateTimePeriodFormatCode { get; set; }
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
  

