using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface ISendTextMessagesService
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

        Task<Int64> Create(SendTextMessagesPost sendtextmessagesPost);
        Task Edit(SendTextMessagesPost sendtextmessagesPost);
        Task Delete(SendTextMessagesPost sendtextmessagesPost);
    }
}
  

