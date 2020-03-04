using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class SendEmailsService : ISendEmailsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly ISendEmailsRepository _sendemailsRepository;

        public SendEmailsService(ISendEmailsRepository sendemailsRepository)
        {
            _sendemailsRepository = sendemailsRepository;
        }

        public SendEmailsPost GetPost(Int64 ixSendEmail) => _sendemailsRepository.GetPost(ixSendEmail);
        public SendEmails Get(Int64 ixSendEmail) => _sendemailsRepository.Get(ixSendEmail);
        public IQueryable<SendEmails> Index() => _sendemailsRepository.Index();
        public IQueryable<SendEmails> IndexDb() => _sendemailsRepository.IndexDb();
       public IQueryable<People> selectPeople() => _sendemailsRepository.selectPeople();
       public IQueryable<People> PeopleDb() => _sendemailsRepository.PeopleDb();
        public bool VerifySendEmailUnique(Int64 ixSendEmail, string sSendEmail) => _sendemailsRepository.VerifySendEmailUnique(ixSendEmail, sSendEmail);
        public List<string> VerifySendEmailDeleteOK(Int64 ixSendEmail, string sSendEmail) => _sendemailsRepository.VerifySendEmailDeleteOK(ixSendEmail, sSendEmail);

        public Task<Int64> Create(SendEmailsPost sendemailsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._sendemailsRepository.RegisterCreate(sendemailsPost);
            try
            {
                this._sendemailsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._sendemailsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(sendemailsPost.ixSendEmail);

        }
        public Task Edit(SendEmailsPost sendemailsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._sendemailsRepository.RegisterEdit(sendemailsPost);
            try
            {
                this._sendemailsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._sendemailsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(SendEmailsPost sendemailsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._sendemailsRepository.RegisterDelete(sendemailsPost);
            try
            {
                this._sendemailsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._sendemailsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

