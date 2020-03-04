using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class SendTextMessagesPost : ISendTextMessagesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Send Text Message ID")]
		public virtual Int64 ixSendTextMessage { get; set; }
		[Display(Name = "Send Text Message")]
		public virtual String sSendTextMessage { get; set; }
		[Required]
		[Display(Name = "Person ID")]
		public virtual Int64 ixPerson { get; set; }
		[Required]
		[Display(Name = "Content")]
		public virtual String sContent { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

