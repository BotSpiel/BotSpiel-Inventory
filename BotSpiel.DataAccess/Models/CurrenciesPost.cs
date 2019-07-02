using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class CurrenciesPost : ICurrenciesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Currency ID")]
		public virtual Int64 ixCurrency { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyCurrency", controller: "Currencies", AdditionalFields = nameof(ixCurrency))]
		[Display(Name = "Currency")]
		public virtual String sCurrency { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

