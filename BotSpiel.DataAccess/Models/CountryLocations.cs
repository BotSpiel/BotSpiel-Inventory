using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class CountryLocations : ICountryLocations
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public CountryLocations()
        {
		CountrySubDivisions _CountrySubDivisions = new CountrySubDivisions();
		CountrySubDivisions = _CountrySubDivisions;

        }
		[Display(Name = "Country Location ID")]
		public virtual Int64 ixCountryLocation { get; set; }
		[Display(Name = "Country Location ID")]
		public virtual Int64 ixCountryLocationEdit { get; set; }
		[Required]
		[StringLength(300)]
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
		[ForeignKey("ixCountrySubDivision")]
		public virtual CountrySubDivisions CountrySubDivisions { get; set; }
    }
}
  

