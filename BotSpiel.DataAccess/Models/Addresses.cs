using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class Addresses : IAddresses
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public Addresses()
        {
		CountrySubDivisions _CountrySubDivisionsFKDiffStateOrProvince = new CountrySubDivisions();
		CountrySubDivisionsFKDiffStateOrProvince = _CountrySubDivisionsFKDiffStateOrProvince;
		Countries _Countries = new Countries();
		Countries = _Countries;

        }
		[Display(Name = "Address ID")]
		public virtual Int64 ixAddress { get; set; }
		[Display(Name = "Address ID")]
		public virtual Int64 ixAddressEdit { get; set; }
		[Display(Name = "Address")]
        //Custom Code Start | Replaced Code Block
        //Replaced Code Block Start
        //public virtual String sAddress { get; set; }
        //Replaced Code Block End
        public virtual String sAddress { get { return this.sStreetAndNumberOrPostOfficeBoxOne + " " + this.sStreetAndNumberOrPostOfficeBoxTwo + " " + this.sStreetAndNumberOrPostOfficeBoxThree + " " + this.sCityOrSuburb + " " + this.sZipOrPostCode + " " + this.CountrySubDivisionsFKDiffStateOrProvince.sCountrySubDivision + " " + this.Countries.sCountry; } set { } }
        //Custom Code End
		[StringLength(300)]
		[Required]
		[Display(Name = "Street And Number Or Post Office Box One")]
		public virtual String sStreetAndNumberOrPostOfficeBoxOne { get; set; }
		[StringLength(300)]
		[Display(Name = "Street And Number Or Post Office Box Two")]
		public virtual String sStreetAndNumberOrPostOfficeBoxTwo { get; set; }
		[StringLength(300)]
		[Display(Name = "Street And Number Or Post Office Box Three")]
		public virtual String sStreetAndNumberOrPostOfficeBoxThree { get; set; }
		[StringLength(300)]
		[Required]
		[Display(Name = "City Or Suburb")]
		public virtual String sCityOrSuburb { get; set; }
		[StringLength(100)]
		[Required]
		[Display(Name = "Zip Or Post Code")]
		public virtual String sZipOrPostCode { get; set; }
		[Required]
		[Display(Name = "State Or Province ID")]
		public virtual Int64 ixStateOrProvince { get; set; }
		[Required]
		[Display(Name = "Country ID")]
		public virtual Int64 ixCountry { get; set; }
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
		[ForeignKey("ixStateOrProvince")]
		public virtual CountrySubDivisions CountrySubDivisionsFKDiffStateOrProvince { get; set; }
		[ForeignKey("ixCountry")]
		public virtual Countries Countries { get; set; }
    }
}
  
