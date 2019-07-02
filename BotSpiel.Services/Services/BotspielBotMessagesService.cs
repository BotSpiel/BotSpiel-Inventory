using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class BotspielBotMessagesService : IBotspielBotMessagesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IBotspielBotMessagesRepository _botspielbotmessagesRepository;

        public BotspielBotMessagesService(IBotspielBotMessagesRepository botspielbotmessagesRepository)
        {
            _botspielbotmessagesRepository = botspielbotmessagesRepository;
        }

        public BotspielBotMessagesPost GetPost(Int64 ixBotspielBotMessage) => _botspielbotmessagesRepository.GetPost(ixBotspielBotMessage);
        public BotspielBotMessages Get(Int64 ixBotspielBotMessage) => _botspielbotmessagesRepository.Get(ixBotspielBotMessage);
        public IQueryable<BotspielBotMessages> Index() => _botspielbotmessagesRepository.Index();
        public bool VerifyBotspielBotMessageUnique(Int64 ixBotspielBotMessage, string sBotspielBotMessage) => _botspielbotmessagesRepository.VerifyBotspielBotMessageUnique(ixBotspielBotMessage, sBotspielBotMessage);
        public List<string> VerifyBotspielBotMessageDeleteOK(Int64 ixBotspielBotMessage, string sBotspielBotMessage) => _botspielbotmessagesRepository.VerifyBotspielBotMessageDeleteOK(ixBotspielBotMessage, sBotspielBotMessage);

        public Task<Int64> Create(BotspielBotMessagesPost botspielbotmessagesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._botspielbotmessagesRepository.RegisterCreate(botspielbotmessagesPost);
            try
            {
                this._botspielbotmessagesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._botspielbotmessagesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(botspielbotmessagesPost.ixBotspielBotMessage);

        }
        public Task Edit(BotspielBotMessagesPost botspielbotmessagesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._botspielbotmessagesRepository.RegisterEdit(botspielbotmessagesPost);
            try
            {
                this._botspielbotmessagesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._botspielbotmessagesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(BotspielBotMessagesPost botspielbotmessagesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._botspielbotmessagesRepository.RegisterDelete(botspielbotmessagesPost);
            try
            {
                this._botspielbotmessagesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._botspielbotmessagesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

