using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class MonetaryAmountTypesPost : IMonetaryAmountTypesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Monetary Amount Type ID")]
		public virtual Int64 ixMonetaryAmountType { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyMonetaryAmountType", controller: "MonetaryAmountTypes", AdditionalFields = nameof(ixMonetaryAmountType))]
		[Display(Name = "Monetary Amount Type")]
		public virtual String sMonetaryAmountType { get; set; }
		[StringLength(30)]
		[Required]
		[Display(Name = "Monetary Amount Type Code")]
		public virtual String sMonetaryAmountTypeCode { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

