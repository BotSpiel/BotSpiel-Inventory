using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class DocumentsPost : IDocumentsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Document ID")]
		public virtual Int64 ixDocument { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyDocument", controller: "Documents", AdditionalFields = nameof(ixDocument))]
		[Display(Name = "Document")]
		public virtual String sDocument { get; set; }
		[Required]
		[Display(Name = "Document Message Type ID")]
		public virtual Int64 ixDocumentMessageType { get; set; }
		[StringLength(30)]
		[Display(Name = "Version")]
		public virtual String sVersion { get; set; }
		[StringLength(30)]
		[Display(Name = "Revision")]
		public virtual String sRevision { get; set; }
		[Required]
		[Display(Name = "Status ID")]
		public virtual Int64 ixStatus { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

