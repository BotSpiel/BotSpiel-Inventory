using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class MessageResponseTypesService : IMessageResponseTypesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IMessageResponseTypesRepository _messageresponsetypesRepository;

        public MessageResponseTypesService(IMessageResponseTypesRepository messageresponsetypesRepository)
        {
            _messageresponsetypesRepository = messageresponsetypesRepository;
        }

        public MessageResponseTypesPost GetPost(Int64 ixMessageResponseType) => _messageresponsetypesRepository.GetPost(ixMessageResponseType);
        public MessageResponseTypes Get(Int64 ixMessageResponseType) => _messageresponsetypesRepository.Get(ixMessageResponseType);
        public IQueryable<MessageResponseTypes> Index() => _messageresponsetypesRepository.Index();
        public IQueryable<MessageResponseTypes> IndexDb() => _messageresponsetypesRepository.IndexDb();
        public bool VerifyMessageResponseTypeUnique(Int64 ixMessageResponseType, string sMessageResponseType) => _messageresponsetypesRepository.VerifyMessageResponseTypeUnique(ixMessageResponseType, sMessageResponseType);
        public List<string> VerifyMessageResponseTypeDeleteOK(Int64 ixMessageResponseType, string sMessageResponseType) => _messageresponsetypesRepository.VerifyMessageResponseTypeDeleteOK(ixMessageResponseType, sMessageResponseType);

        public Task<Int64> Create(MessageResponseTypesPost messageresponsetypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._messageresponsetypesRepository.RegisterCreate(messageresponsetypesPost);
            try
            {
                this._messageresponsetypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._messageresponsetypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(messageresponsetypesPost.ixMessageResponseType);

        }
        public Task Edit(MessageResponseTypesPost messageresponsetypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._messageresponsetypesRepository.RegisterEdit(messageresponsetypesPost);
            try
            {
                this._messageresponsetypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._messageresponsetypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(MessageResponseTypesPost messageresponsetypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._messageresponsetypesRepository.RegisterDelete(messageresponsetypesPost);
            try
            {
                this._messageresponsetypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._messageresponsetypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

