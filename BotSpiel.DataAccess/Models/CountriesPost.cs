using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class CountriesPost : ICountriesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Country ID")]
		public virtual Int64 ixCountry { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyCountry", controller: "Countries", AdditionalFields = nameof(ixCountry))]
		[Display(Name = "Country")]
		public virtual String sCountry { get; set; }
		[Required]
		[Display(Name = "Planet Sub Region ID")]
		public virtual Int64 ixPlanetSubRegion { get; set; }
		[StringLength(10)]
		[Required]
		[Display(Name = "Country Code")]
		public virtual String sCountryCode { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

