using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class SendTextMessagesService : ISendTextMessagesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly ISendTextMessagesRepository _sendtextmessagesRepository;

        public SendTextMessagesService(ISendTextMessagesRepository sendtextmessagesRepository)
        {
            _sendtextmessagesRepository = sendtextmessagesRepository;
        }

        public SendTextMessagesPost GetPost(Int64 ixSendTextMessage) => _sendtextmessagesRepository.GetPost(ixSendTextMessage);
        public SendTextMessages Get(Int64 ixSendTextMessage) => _sendtextmessagesRepository.Get(ixSendTextMessage);
        public IQueryable<SendTextMessages> Index() => _sendtextmessagesRepository.Index();
        public IQueryable<SendTextMessages> IndexDb() => _sendtextmessagesRepository.IndexDb();
       public IQueryable<People> selectPeople() => _sendtextmessagesRepository.selectPeople();
       public IQueryable<People> PeopleDb() => _sendtextmessagesRepository.PeopleDb();
        public bool VerifySendTextMessageUnique(Int64 ixSendTextMessage, string sSendTextMessage) => _sendtextmessagesRepository.VerifySendTextMessageUnique(ixSendTextMessage, sSendTextMessage);
        public List<string> VerifySendTextMessageDeleteOK(Int64 ixSendTextMessage, string sSendTextMessage) => _sendtextmessagesRepository.VerifySendTextMessageDeleteOK(ixSendTextMessage, sSendTextMessage);

        public Task<Int64> Create(SendTextMessagesPost sendtextmessagesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._sendtextmessagesRepository.RegisterCreate(sendtextmessagesPost);
            try
            {
                this._sendtextmessagesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._sendtextmessagesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(sendtextmessagesPost.ixSendTextMessage);

        }
        public Task Edit(SendTextMessagesPost sendtextmessagesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._sendtextmessagesRepository.RegisterEdit(sendtextmessagesPost);
            try
            {
                this._sendtextmessagesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._sendtextmessagesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(SendTextMessagesPost sendtextmessagesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._sendtextmessagesRepository.RegisterDelete(sendtextmessagesPost);
            try
            {
                this._sendtextmessagesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._sendtextmessagesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

