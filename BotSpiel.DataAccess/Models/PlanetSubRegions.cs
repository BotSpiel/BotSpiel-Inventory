using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class PlanetSubRegions : IPlanetSubRegions
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public PlanetSubRegions()
        {
		PlanetRegions _PlanetRegions = new PlanetRegions();
		PlanetRegions = _PlanetRegions;

        }
		[Display(Name = "Planet Sub Region ID")]
		public virtual Int64 ixPlanetSubRegion { get; set; }
		[Display(Name = "Planet Sub Region ID")]
		public virtual Int64 ixPlanetSubRegionEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Planet Sub Region")]
		public virtual String sPlanetSubRegion { get; set; }
		[Required]
		[Display(Name = "Planet Region ID")]
		public virtual Int64 ixPlanetRegion { get; set; }
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
		[ForeignKey("ixPlanetRegion")]
		public virtual PlanetRegions PlanetRegions { get; set; }
    }
}
  

