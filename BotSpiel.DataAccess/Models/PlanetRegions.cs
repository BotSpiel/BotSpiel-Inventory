using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class PlanetRegions : IPlanetRegions
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public PlanetRegions()
        {
		Planets _Planets = new Planets();
		Planets = _Planets;

        }
		[Display(Name = "Planet Region ID")]
		public virtual Int64 ixPlanetRegion { get; set; }
		[Display(Name = "Planet Region ID")]
		public virtual Int64 ixPlanetRegionEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Planet Region")]
		public virtual String sPlanetRegion { get; set; }
		[Required]
		[Display(Name = "Planet ID")]
		public virtual Int64 ixPlanet { get; set; }
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
		[ForeignKey("ixPlanet")]
		public virtual Planets Planets { get; set; }
    }
}
  

