using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class PlanetarySystems : IPlanetarySystems
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public PlanetarySystems()
        {
		Galaxies _Galaxies = new Galaxies();
		Galaxies = _Galaxies;

        }
		[Display(Name = "Planetary System ID")]
		public virtual Int64 ixPlanetarySystem { get; set; }
		[Display(Name = "Planetary System ID")]
		public virtual Int64 ixPlanetarySystemEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Planetary System")]
		public virtual String sPlanetarySystem { get; set; }
		[Required]
		[Display(Name = "Galaxy ID")]
		public virtual Int64 ixGalaxy { get; set; }
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
		[ForeignKey("ixGalaxy")]
		public virtual Galaxies Galaxies { get; set; }
    }
}
  

