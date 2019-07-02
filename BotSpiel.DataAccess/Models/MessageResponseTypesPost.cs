using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class MessageResponseTypesPost : IMessageResponseTypesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Message Response Type ID")]
		public virtual Int64 ixMessageResponseType { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyMessageResponseType", controller: "MessageResponseTypes", AdditionalFields = nameof(ixMessageResponseType))]
		[Display(Name = "Message Response Type")]
		public virtual String sMessageResponseType { get; set; }
		[StringLength(30)]
		[Required]
		[Display(Name = "Message Response Type Code")]
		public virtual String sMessageResponseTypeCode { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

