using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class Documents : IDocuments
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public Documents()
        {
		DocumentMessageTypes _DocumentMessageTypes = new DocumentMessageTypes();
		DocumentMessageTypes = _DocumentMessageTypes;
		Statuses _Statuses = new Statuses();
		Statuses = _Statuses;

        }
		[Display(Name = "Document ID")]
		public virtual Int64 ixDocument { get; set; }
		[Display(Name = "Document ID")]
		public virtual Int64 ixDocumentEdit { get; set; }
		[Required]
		[StringLength(300)]
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
		[ForeignKey("ixDocumentMessageType")]
		public virtual DocumentMessageTypes DocumentMessageTypes { get; set; }
		[ForeignKey("ixStatus")]
		public virtual Statuses Statuses { get; set; }
    }
}
  

