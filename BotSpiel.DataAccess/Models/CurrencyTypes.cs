using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class CurrencyTypes : ICurrencyTypes
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Currency Type ID")]
		public virtual Int64 ixCurrencyType { get; set; }
		[Display(Name = "Currency Type ID")]
		public virtual Int64 ixCurrencyTypeEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Currency Type")]
		public virtual String sCurrencyType { get; set; }
		[StringLength(30)]
		[Required]
		[Display(Name = "Currency Type Code")]
		public virtual String sCurrencyTypeCode { get; set; }
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
  

