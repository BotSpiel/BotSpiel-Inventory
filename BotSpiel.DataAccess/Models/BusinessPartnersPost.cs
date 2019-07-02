using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class BusinessPartnersPost : IBusinessPartnersPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Business Partner ID")]
		public virtual Int64 ixBusinessPartner { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyBusinessPartner", controller: "BusinessPartners", AdditionalFields = nameof(ixBusinessPartner))]
		[Display(Name = "Business Partner")]
		public virtual String sBusinessPartner { get; set; }
		[Required]
		[Display(Name = "Business Partner Type ID")]
		public virtual Int64 ixBusinessPartnerType { get; set; }
		[Required]
		[Display(Name = "Company ID")]
		public virtual Int64 ixCompany { get; set; }
		[Required]
		[Display(Name = "Address ID")]
		public virtual Int64 ixAddress { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

