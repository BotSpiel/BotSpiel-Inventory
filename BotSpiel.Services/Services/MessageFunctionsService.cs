using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class MessageFunctionsService : IMessageFunctionsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IMessageFunctionsRepository _messagefunctionsRepository;

        public MessageFunctionsService(IMessageFunctionsRepository messagefunctionsRepository)
        {
            _messagefunctionsRepository = messagefunctionsRepository;
        }

        public MessageFunctionsPost GetPost(Int64 ixMessageFunction) => _messagefunctionsRepository.GetPost(ixMessageFunction);
        public MessageFunctions Get(Int64 ixMessageFunction) => _messagefunctionsRepository.Get(ixMessageFunction);
        public IQueryable<MessageFunctions> Index() => _messagefunctionsRepository.Index();
        public IQueryable<MessageFunctions> IndexDb() => _messagefunctionsRepository.IndexDb();
        public bool VerifyMessageFunctionUnique(Int64 ixMessageFunction, string sMessageFunction) => _messagefunctionsRepository.VerifyMessageFunctionUnique(ixMessageFunction, sMessageFunction);
        public List<string> VerifyMessageFunctionDeleteOK(Int64 ixMessageFunction, string sMessageFunction) => _messagefunctionsRepository.VerifyMessageFunctionDeleteOK(ixMessageFunction, sMessageFunction);

        public Task<Int64> Create(MessageFunctionsPost messagefunctionsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._messagefunctionsRepository.RegisterCreate(messagefunctionsPost);
            try
            {
                this._messagefunctionsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._messagefunctionsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(messagefunctionsPost.ixMessageFunction);

        }
        public Task Edit(MessageFunctionsPost messagefunctionsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._messagefunctionsRepository.RegisterEdit(messagefunctionsPost);
            try
            {
                this._messagefunctionsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._messagefunctionsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(MessageFunctionsPost messagefunctionsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._messagefunctionsRepository.RegisterDelete(messagefunctionsPost);
            try
            {
                this._messagefunctionsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._messagefunctionsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

