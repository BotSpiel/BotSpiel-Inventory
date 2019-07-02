using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class CountrySubDivisions : ICountrySubDivisions
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public CountrySubDivisions()
        {
		Countries _Countries = new Countries();
		Countries = _Countries;

        }
		[Display(Name = "Country Sub Division ID")]
		public virtual Int64 ixCountrySubDivision { get; set; }
		[Display(Name = "Country Sub Division ID")]
		public virtual Int64 ixCountrySubDivisionEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Country Sub Division")]
		public virtual String sCountrySubDivision { get; set; }
		[Required]
		[Display(Name = "Country ID")]
		public virtual Int64 ixCountry { get; set; }
		[StringLength(100)]
		[Required]
		[Display(Name = "Country Sub Division Code")]
		public virtual String sCountrySubDivisionCode { get; set; }
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
		[ForeignKey("ixCountry")]
		public virtual Countries Countries { get; set; }
    }
}
  

