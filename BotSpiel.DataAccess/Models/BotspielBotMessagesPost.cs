using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class BotspielBotMessagesPost : IBotspielBotMessagesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Botspiel Bot Message ID")]
		public virtual Int64 ixBotspielBotMessage { get; set; }
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
		public virtual String UserName { get; set; }
    }
}
  

