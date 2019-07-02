using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class OutboundCarrierManifestPickupsService : IOutboundCarrierManifestPickupsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IOutboundCarrierManifestPickupsRepository _outboundcarriermanifestpickupsRepository;

        public OutboundCarrierManifestPickupsService(IOutboundCarrierManifestPickupsRepository outboundcarriermanifestpickupsRepository)
        {
            _outboundcarriermanifestpickupsRepository = outboundcarriermanifestpickupsRepository;
        }

        public OutboundCarrierManifestPickupsPost GetPost(Int64 ixOutboundCarrierManifestPickup) => _outboundcarriermanifestpickupsRepository.GetPost(ixOutboundCarrierManifestPickup);
        public OutboundCarrierManifestPickups Get(Int64 ixOutboundCarrierManifestPickup) => _outboundcarriermanifestpickupsRepository.Get(ixOutboundCarrierManifestPickup);
        public IQueryable<OutboundCarrierManifestPickups> Index() => _outboundcarriermanifestpickupsRepository.Index();
       public IQueryable<Statuses> selectStatuses() => _outboundcarriermanifestpickupsRepository.selectStatuses();
        public IQueryable<OutboundCarrierManifests> selectOutboundCarrierManifests() => _outboundcarriermanifestpickupsRepository.selectOutboundCarrierManifests();
        public bool VerifyOutboundCarrierManifestPickupUnique(Int64 ixOutboundCarrierManifestPickup, string sOutboundCarrierManifestPickup) => _outboundcarriermanifestpickupsRepository.VerifyOutboundCarrierManifestPickupUnique(ixOutboundCarrierManifestPickup, sOutboundCarrierManifestPickup);
        public List<string> VerifyOutboundCarrierManifestPickupDeleteOK(Int64 ixOutboundCarrierManifestPickup, string sOutboundCarrierManifestPickup) => _outboundcarriermanifestpickupsRepository.VerifyOutboundCarrierManifestPickupDeleteOK(ixOutboundCarrierManifestPickup, sOutboundCarrierManifestPickup);

        public Task<Int64> Create(OutboundCarrierManifestPickupsPost outboundcarriermanifestpickupsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundcarriermanifestpickupsRepository.RegisterCreate(outboundcarriermanifestpickupsPost);
            try
            {
                this._outboundcarriermanifestpickupsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundcarriermanifestpickupsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(outboundcarriermanifestpickupsPost.ixOutboundCarrierManifestPickup);

        }
        public Task Edit(OutboundCarrierManifestPickupsPost outboundcarriermanifestpickupsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundcarriermanifestpickupsRepository.RegisterEdit(outboundcarriermanifestpickupsPost);
            try
            {
                this._outboundcarriermanifestpickupsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundcarriermanifestpickupsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(OutboundCarrierManifestPickupsPost outboundcarriermanifestpickupsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundcarriermanifestpickupsRepository.RegisterDelete(outboundcarriermanifestpickupsPost);
            try
            {
                this._outboundcarriermanifestpickupsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundcarriermanifestpickupsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

