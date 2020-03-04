using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class SendTextMessages : ISendTextMessages
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public SendTextMessages()
        {
		People _People = new People();
		People = _People;

        }
		[Display(Name = "Send Text Message ID")]
		public virtual Int64 ixSendTextMessage { get; set; }
		[Display(Name = "Send Text Message ID")]
		public virtual Int64 ixSendTextMessageEdit { get; set; }
		[Display(Name = "Send Text Message")]
		public virtual String sSendTextMessage { get; set; }
		[Required]
		[Display(Name = "Person ID")]
		public virtual Int64 ixPerson { get; set; }
		[Required]
		[Display(Name = "Content")]
		public virtual String sContent { get; set; }
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
  

