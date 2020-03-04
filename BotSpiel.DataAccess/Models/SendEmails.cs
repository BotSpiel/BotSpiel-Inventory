using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class SendEmails : ISendEmails
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public SendEmails()
        {
		People _People = new People();
		People = _People;

        }
		[Display(Name = "Send Email ID")]
		public virtual Int64 ixSendEmail { get; set; }
		[Display(Name = "Send Email ID")]
		public virtual Int64 ixSendEmailEdit { get; set; }
		[Display(Name = "Send Email")]
		public virtual String sSendEmail { get; set; }
		[Required]
		[Display(Name = "Person ID")]
		public virtual Int64 ixPerson { get; set; }
		[StringLength(1000)]
		[Required]
		[Display(Name = "Subject")]
		public virtual String sSubject { get; set; }
		[Required]
		[Display(Name = "Content")]
		public virtual String sContent { get; set; }
		[StringLength(1000)]
		[Display(Name = "Attachment")]
		public virtual String sAttachment { get; set; }
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
		[ForeignKey("ixPerson")]
		public virtual People People { get; set; }
    }
}
  

