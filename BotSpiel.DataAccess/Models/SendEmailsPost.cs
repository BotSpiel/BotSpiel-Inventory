using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class SendEmailsPost : ISendEmailsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Send Email ID")]
		public virtual Int64 ixSendEmail { get; set; }
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
		public virtual String UserName { get; set; }
    }
}
  

