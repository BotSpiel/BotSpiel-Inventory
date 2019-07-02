using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class CountryLocationsPost : ICountryLocationsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Country Location ID")]
		public virtual Int64 ixCountryLocation { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyCountryLocation", controller: "CountryLocations", AdditionalFields = nameof(ixCountryLocation))]
		[Display(Name = "Country Location")]
		public virtual String sCountryLocation { get; set; }
		[Required]
		[Display(Name = "Country Sub Division ID")]
		public virtual Int64 ixCountrySubDivision { get; set; }
		[StringLength(30)]
		[Display(Name = "Location Code")]
		public virtual String sLocationCode { get; set; }
		[StringLength(300)]
		[Display(Name = "Name Without Diacritics")]
		public virtual String sNameWithoutDiacritics { get; set; }
		[StringLength(30)]
		[Display(Name = "Latitude")]
		public virtual String sLatitude { get; set; }
		[StringLength(30)]
		[Display(Name = "Longitude")]
		public virtual String sLongitude { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

