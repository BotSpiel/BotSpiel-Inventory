using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class CurrencyTypesPost : ICurrencyTypesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Currency Type ID")]
		public virtual Int64 ixCurrencyType { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyCurrencyType", controller: "CurrencyTypes", AdditionalFields = nameof(ixCurrencyType))]
		[Display(Name = "Currency Type")]
		public virtual String sCurrencyType { get; set; }
		[StringLength(30)]
		[Required]
		[Display(Name = "Currency Type Code")]
		public virtual String sCurrencyTypeCode { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

