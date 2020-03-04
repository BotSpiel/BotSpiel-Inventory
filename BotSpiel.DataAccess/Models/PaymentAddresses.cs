using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class PaymentAddresses : IPaymentAddresses
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Payment Address ID")]
		public virtual Int64 ixPaymentAddress { get; set; }
		[Display(Name = "Payment Address ID")]
		public virtual Int64 ixPaymentAddressEdit { get; set; }
		[Display(Name = "Payment Address")]
		public virtual String sPaymentAddress { get; set; }
		[StringLength(300)]
		[Required]
		[Display(Name = "Street And Number Or Post Office Box One")]
		public virtual String sStreetAndNumberOrPostOfficeBoxOne { get; set; }
		[StringLength(300)]
		[Display(Name = "Street And Number Or Post Office Box Two")]
		public virtual String sStreetAndNumberOrPostOfficeBoxTwo { get; set; }
		[StringLength(300)]
		[Required]
		[Display(Name = "City Or Suburb")]
		public virtual String sCityOrSuburb { get; set; }
		[StringLength(100)]
		[Required]
		[Display(Name = "Country Sub Division Code")]
		public virtual String sCountrySubDivisionCode { get; set; }
		[StringLength(100)]
		[Required]
		[Display(Name = "Zip Or Post Code")]
		public virtual String sZipOrPostCode { get; set; }
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
    }
}
  

