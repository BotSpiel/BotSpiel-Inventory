using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class CompaniesPost : ICompaniesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Company ID")]
		public virtual Int64 ixCompany { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyCompany", controller: "Companies", AdditionalFields = nameof(ixCompany))]
		[Display(Name = "Company")]
		public virtual String sCompany { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

