using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IBotspielBotMessagesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixBotspielBotMessage { get; set; }
		String sBotspielBotMessage { get; set; }
		String sMyMessage { get; set; }
		String sYourReply { get; set; }
		String UserName { get; set; }
    }
}
  

