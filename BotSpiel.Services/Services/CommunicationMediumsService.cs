using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class CommunicationMediumsService : ICommunicationMediumsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly ICommunicationMediumsRepository _communicationmediumsRepository;

        public CommunicationMediumsService(ICommunicationMediumsRepository communicationmediumsRepository)
        {
            _communicationmediumsRepository = communicationmediumsRepository;
        }

        public CommunicationMediumsPost GetPost(Int64 ixCommunicationMedium) => _communicationmediumsRepository.GetPost(ixCommunicationMedium);
        public CommunicationMediums Get(Int64 ixCommunicationMedium) => _communicationmediumsRepository.Get(ixCommunicationMedium);
        public IQueryable<CommunicationMediums> Index() => _communicationmediumsRepository.Index();
        public IQueryable<CommunicationMediums> IndexDb() => _communicationmediumsRepository.IndexDb();
        public bool VerifyCommunicationMediumUnique(Int64 ixCommunicationMedium, string sCommunicationMedium) => _communicationmediumsRepository.VerifyCommunicationMediumUnique(ixCommunicationMedium, sCommunicationMedium);
        public List<string> VerifyCommunicationMediumDeleteOK(Int64 ixCommunicationMedium, string sCommunicationMedium) => _communicationmediumsRepository.VerifyCommunicationMediumDeleteOK(ixCommunicationMedium, sCommunicationMedium);

        public Task<Int64> Create(CommunicationMediumsPost communicationmediumsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._communicationmediumsRepository.RegisterCreate(communicationmediumsPost);
            try
            {
                this._communicationmediumsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._communicationmediumsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(communicationmediumsPost.ixCommunicationMedium);

        }
        public Task Edit(CommunicationMediumsPost communicationmediumsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._communicationmediumsRepository.RegisterEdit(communicationmediumsPost);
            try
            {
                this._communicationmediumsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._communicationmediumsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(CommunicationMediumsPost communicationmediumsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._communicationmediumsRepository.RegisterDelete(communicationmediumsPost);
            try
            {
                this._communicationmediumsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._communicationmediumsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

