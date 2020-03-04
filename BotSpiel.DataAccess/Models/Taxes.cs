using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class Taxes : ITaxes
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public Taxes()
        {
		Countries _Countries = new Countries();
		Countries = _Countries;
		CountrySubDivisions _CountrySubDivisions = new CountrySubDivisions();
		CountrySubDivisions = _CountrySubDivisions;

        }
		[Display(Name = "Tax ID")]
		public virtual Int64 ixTax { get; set; }
		[Display(Name = "Tax ID")]
		public virtual Int64 ixTaxEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Tax")]
		public virtual String sTax { get; set; }
		[Required]
		[Display(Name = "Country ID")]
		public virtual Int64 ixCountry { get; set; }
		[Required]
		[Display(Name = "Country Sub Division ID")]
		public virtual Int64 ixCountrySubDivision { get; set; }
		[Required]
		[Display(Name = "Rate")]
		public virtual Decimal nRate { get; set; }
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
		[ForeignKey("ixCountrySubDivision")]
		public virtual CountrySubDivisions CountrySubDivisions { get; set; }
    }
}
  

