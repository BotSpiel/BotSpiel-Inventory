using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class CountrySubDivisionsPost : ICountrySubDivisionsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Country Sub Division ID")]
		public virtual Int64 ixCountrySubDivision { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyCountrySubDivision", controller: "CountrySubDivisions", AdditionalFields = nameof(ixCountrySubDivision))]
		[Display(Name = "Country Sub Division")]
		public virtual String sCountrySubDivision { get; set; }
		[Required]
		[Display(Name = "Country ID")]
		public virtual Int64 ixCountry { get; set; }
		[StringLength(100)]
		[Required]
		[Display(Name = "Country Sub Division Code")]
		public virtual String sCountrySubDivisionCode { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

