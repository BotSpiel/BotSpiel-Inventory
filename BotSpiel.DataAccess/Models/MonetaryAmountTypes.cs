using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class MonetaryAmountTypes : IMonetaryAmountTypes
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Monetary Amount Type ID")]
		public virtual Int64 ixMonetaryAmountType { get; set; }
		[Display(Name = "Monetary Amount Type ID")]
		public virtual Int64 ixMonetaryAmountTypeEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Monetary Amount Type")]
		public virtual String sMonetaryAmountType { get; set; }
		[StringLength(30)]
		[Required]
		[Display(Name = "Monetary Amount Type Code")]
		public virtual String sMonetaryAmountTypeCode { get; set; }
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
  

