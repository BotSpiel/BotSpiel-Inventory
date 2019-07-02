using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class Countries : ICountries
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public Countries()
        {
		PlanetSubRegions _PlanetSubRegions = new PlanetSubRegions();
		PlanetSubRegions = _PlanetSubRegions;

        }
		[Display(Name = "Country ID")]
		public virtual Int64 ixCountry { get; set; }
		[Display(Name = "Country ID")]
		public virtual Int64 ixCountryEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Country")]
		public virtual String sCountry { get; set; }
		[Required]
		[Display(Name = "Planet Sub Region ID")]
		public virtual Int64 ixPlanetSubRegion { get; set; }
		[StringLength(10)]
		[Required]
		[Display(Name = "Country Code")]
		public virtual String sCountryCode { get; set; }
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
		[ForeignKey("ixPlanetSubRegion")]
		public virtual PlanetSubRegions PlanetSubRegions { get; set; }
    }
}
  

