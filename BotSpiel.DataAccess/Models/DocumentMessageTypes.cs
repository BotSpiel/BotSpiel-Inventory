using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class DocumentMessageTypes : IDocumentMessageTypes
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Document Message Type ID")]
		public virtual Int64 ixDocumentMessageType { get; set; }
		[Display(Name = "Document Message Type ID")]
		public virtual Int64 ixDocumentMessageTypeEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Document Message Type")]
		public virtual String sDocumentMessageType { get; set; }
		[StringLength(30)]
		[Required]
		[Display(Name = "Document Message Type Code")]
		public virtual String sDocumentMessageTypeCode { get; set; }
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
  

