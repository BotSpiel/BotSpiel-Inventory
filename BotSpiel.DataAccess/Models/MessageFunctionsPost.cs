using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class MessageFunctionsPost : IMessageFunctionsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Message Function ID")]
		public virtual Int64 ixMessageFunction { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyMessageFunction", controller: "MessageFunctions", AdditionalFields = nameof(ixMessageFunction))]
		[Display(Name = "Message Function")]
		public virtual String sMessageFunction { get; set; }
		[StringLength(30)]
		[Required]
		[Display(Name = "Message Function Code")]
		public virtual String sMessageFunctionCode { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

