using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class DocumentMessageTypesPost : IDocumentMessageTypesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Document Message Type ID")]
		public virtual Int64 ixDocumentMessageType { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyDocumentMessageType", controller: "DocumentMessageTypes", AdditionalFields = nameof(ixDocumentMessageType))]
		[Display(Name = "Document Message Type")]
		public virtual String sDocumentMessageType { get; set; }
		[StringLength(30)]
		[Required]
		[Display(Name = "Document Message Type Code")]
		public virtual String sDocumentMessageTypeCode { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

