using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class Planets : IPlanets
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public Planets()
        {
		PlanetarySystems _PlanetarySystems = new PlanetarySystems();
		PlanetarySystems = _PlanetarySystems;

        }
		[Display(Name = "Planet ID")]
		public virtual Int64 ixPlanet { get; set; }
		[Display(Name = "Planet ID")]
		public virtual Int64 ixPlanetEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Planet")]
		public virtual String sPlanet { get; set; }
		[Required]
		[Display(Name = "Planetary System ID")]
		public virtual Int64 ixPlanetarySystem { get; set; }
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
		[ForeignKey("ixPlanetarySystem")]
		public virtual PlanetarySystems PlanetarySystems { get; set; }
    }
}
  

