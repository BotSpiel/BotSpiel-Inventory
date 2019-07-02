using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IBotspielBotMessagesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        BotspielBotMessagesPost GetPost(Int64 ixBotspielBotMessage);        
		BotspielBotMessages Get(Int64 ixBotspielBotMessage);
        IQueryable<BotspielBotMessages> Index();
        bool VerifyBotspielBotMessageUnique(Int64 ixBotspielBotMessage, string sBotspielBotMessage);
        List<string> VerifyBotspielBotMessageDeleteOK(Int64 ixBotspielBotMessage, string sBotspielBotMessage);

        Task<Int64> Create(BotspielBotMessagesPost botspielbotmessagesPost);
        Task Edit(BotspielBotMessagesPost botspielbotmessagesPost);
        Task Delete(BotspielBotMessagesPost botspielbotmessagesPost);
    }
}
  

