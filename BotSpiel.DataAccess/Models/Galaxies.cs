using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class Galaxies : IGalaxies
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public Galaxies()
        {
		Universes _Universes = new Universes();
		Universes = _Universes;

        }
		[Display(Name = "Galaxy ID")]
		public virtual Int64 ixGalaxy { get; set; }
		[Display(Name = "Galaxy ID")]
		public virtual Int64 ixGalaxyEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Galaxy")]
		public virtual String sGalaxy { get; set; }
		[Required]
		[Display(Name = "Universe ID")]
		public virtual Int64 ixUniverse { get; set; }
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
		[ForeignKey("ixUniverse")]
		public virtual Universes Universes { get; set; }
    }
}
  

