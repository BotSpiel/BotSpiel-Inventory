using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class BotspielBotMessages : IBotspielBotMessages
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Botspiel Bot Message ID")]
		public virtual Int64 ixBotspielBotMessage { get; set; }
		[Display(Name = "Botspiel Bot Message ID")]
		public virtual Int64 ixBotspielBotMessageEdit { get; set; }
		[Display(Name = "Botspiel Bot Message")]
		public virtual String sBotspielBotMessage { get; set; }
		[StringLength(4000)]
		[Required]
		[Display(Name = "My Message")]
		public virtual String sMyMessage { get; set; }
		[StringLength(4000)]
		[Required]
		[Display(Name = "Your Reply")]
		public virtual String sYourReply { get; set; }
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
  

