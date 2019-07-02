using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IBotspielBotMessagesRepository
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
        void RegisterCreate(BotspielBotMessagesPost botspielbotmessagesPost);
        void RegisterEdit(BotspielBotMessagesPost botspielbotmessagesPost);
        void RegisterDelete(BotspielBotMessagesPost botspielbotmessagesPost);
        void Rollback();
        void Commit();
    }
}
  

