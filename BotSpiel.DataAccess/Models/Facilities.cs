using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class Facilities : IFacilities
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public Facilities()
        {
		Addresses _Addresses = new Addresses();
		Addresses = _Addresses;

        }
		[Display(Name = "Facility ID")]
		public virtual Int64 ixFacility { get; set; }
		[Display(Name = "Facility ID")]
		public virtual Int64 ixFacilityEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Facility")]
		public virtual String sFacility { get; set; }
		[Required]
		[Display(Name = "Address ID")]
		public virtual Int64 ixAddress { get; set; }
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
		[ForeignKey("ixAddress")]
		public virtual Addresses Addresses { get; set; }
    }
}
  

