using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class OutboundCarrierManifestsService : IOutboundCarrierManifestsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IOutboundCarrierManifestsRepository _outboundcarriermanifestsRepository;

        public OutboundCarrierManifestsService(IOutboundCarrierManifestsRepository outboundcarriermanifestsRepository)
        {
            _outboundcarriermanifestsRepository = outboundcarriermanifestsRepository;
        }

        public OutboundCarrierManifestsPost GetPost(Int64 ixOutboundCarrierManifest) => _outboundcarriermanifestsRepository.GetPost(ixOutboundCarrierManifest);
        public OutboundCarrierManifests Get(Int64 ixOutboundCarrierManifest) => _outboundcarriermanifestsRepository.Get(ixOutboundCarrierManifest);
        public IQueryable<OutboundCarrierManifests> Index() => _outboundcarriermanifestsRepository.Index();
       public IQueryable<Statuses> selectStatuses() => _outboundcarriermanifestsRepository.selectStatuses();
        public IQueryable<InventoryLocations> selectInventoryLocations() => _outboundcarriermanifestsRepository.selectInventoryLocations();
        public IQueryable<Carriers> selectCarriers() => _outboundcarriermanifestsRepository.selectCarriers();
       public List<KeyValuePair<Int64?, string>> selectInventoryLocationsNullable() => _outboundcarriermanifestsRepository.selectInventoryLocationsNullable();
        public bool VerifyOutboundCarrierManifestUnique(Int64 ixOutboundCarrierManifest, string sOutboundCarrierManifest) => _outboundcarriermanifestsRepository.VerifyOutboundCarrierManifestUnique(ixOutboundCarrierManifest, sOutboundCarrierManifest);
        public List<string> VerifyOutboundCarrierManifestDeleteOK(Int64 ixOutboundCarrierManifest, string sOutboundCarrierManifest) => _outboundcarriermanifestsRepository.VerifyOutboundCarrierManifestDeleteOK(ixOutboundCarrierManifest, sOutboundCarrierManifest);

        public Task<Int64> Create(OutboundCarrierManifestsPost outboundcarriermanifestsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundcarriermanifestsRepository.RegisterCreate(outboundcarriermanifestsPost);
            try
            {
                this._outboundcarriermanifestsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundcarriermanifestsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(outboundcarriermanifestsPost.ixOutboundCarrierManifest);

        }
        public Task Edit(OutboundCarrierManifestsPost outboundcarriermanifestsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundcarriermanifestsRepository.RegisterEdit(outboundcarriermanifestsPost);
            try
            {
                this._outboundcarriermanifestsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundcarriermanifestsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(OutboundCarrierManifestsPost outboundcarriermanifestsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundcarriermanifestsRepository.RegisterDelete(outboundcarriermanifestsPost);
            try
            {
                this._outboundcarriermanifestsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundcarriermanifestsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

