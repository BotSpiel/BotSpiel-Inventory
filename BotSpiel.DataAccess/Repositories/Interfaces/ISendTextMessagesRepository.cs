using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface ISendTextMessagesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        SendTextMessagesPost GetPost(Int64 ixSendTextMessage);        
		SendTextMessages Get(Int64 ixSendTextMessage);
        IQueryable<SendTextMessages> Index();
        IQueryable<SendTextMessages> IndexDb();
       IQueryable<People> selectPeople();
       IQueryable<People> PeopleDb();
        bool VerifySendTextMessageUnique(Int64 ixSendTextMessage, string sSendTextMessage);
        List<string> VerifySendTextMessageDeleteOK(Int64 ixSendTextMessage, string sSendTextMessage);
        void RegisterCreate(SendTextMessagesPost sendtextmessagesPost);
        void RegisterEdit(SendTextMessagesPost sendtextmessagesPost);
        void RegisterDelete(SendTextMessagesPost sendtextmessagesPost);
        void Rollback();
        void Commit();
    }
}
  

