using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class ContactFunctionsPost : IContactFunctionsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Contact Function ID")]
		public virtual Int64 ixContactFunction { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyContactFunction", controller: "ContactFunctions", AdditionalFields = nameof(ixContactFunction))]
		[Display(Name = "Contact Function")]
		public virtual String sContactFunction { get; set; }
		[StringLength(30)]
		[Required]
		[Display(Name = "Contact Function Code")]
		public virtual String sContactFunctionCode { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

