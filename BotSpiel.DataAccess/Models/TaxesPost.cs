using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class TaxesPost : ITaxesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Tax ID")]
		public virtual Int64 ixTax { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyTax", controller: "Taxes", AdditionalFields = nameof(ixTax))]
		[Display(Name = "Tax")]
		public virtual String sTax { get; set; }
		[Required]
		[Display(Name = "Country ID")]
		public virtual Int64 ixCountry { get; set; }
		[Required]
		[Display(Name = "Country Sub Division ID")]
		public virtual Int64 ixCountrySubDivision { get; set; }
		[Required]
		[Display(Name = "Rate")]
		public virtual Decimal nRate { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

