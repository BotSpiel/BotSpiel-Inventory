using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class AddressesPost : IAddressesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Address ID")]
		public virtual Int64 ixAddress { get; set; }
		[Display(Name = "Address")]
		public virtual String sAddress { get; set; }
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
		public virtual String UserName { get; set; }
    }
}
  

