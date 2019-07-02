using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class BusinessPartnerTypesPost : IBusinessPartnerTypesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Business Partner Type ID")]
		public virtual Int64 ixBusinessPartnerType { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyBusinessPartnerType", controller: "BusinessPartnerTypes", AdditionalFields = nameof(ixBusinessPartnerType))]
		[Display(Name = "Business Partner Type")]
		public virtual String sBusinessPartnerType { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

