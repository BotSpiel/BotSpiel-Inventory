using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class OutboundShipmentsService : IOutboundShipmentsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IOutboundShipmentsRepository _outboundshipmentsRepository;

        public OutboundShipmentsService(IOutboundShipmentsRepository outboundshipmentsRepository)
        {
            _outboundshipmentsRepository = outboundshipmentsRepository;
        }

        public OutboundShipmentsPost GetPost(Int64 ixOutboundShipment) => _outboundshipmentsRepository.GetPost(ixOutboundShipment);
        public OutboundShipments Get(Int64 ixOutboundShipment) => _outboundshipmentsRepository.Get(ixOutboundShipment);
        public IQueryable<OutboundShipments> Index() => _outboundshipmentsRepository.Index();
       public IQueryable<Statuses> selectStatuses() => _outboundshipmentsRepository.selectStatuses();
        public IQueryable<Addresses> selectAddresses() => _outboundshipmentsRepository.selectAddresses();
        public IQueryable<Companies> selectCompanies() => _outboundshipmentsRepository.selectCompanies();
        public IQueryable<Facilities> selectFacilities() => _outboundshipmentsRepository.selectFacilities();
        public IQueryable<Carriers> selectCarriers() => _outboundshipmentsRepository.selectCarriers();
        public IQueryable<OutboundCarrierManifests> selectOutboundCarrierManifests() => _outboundshipmentsRepository.selectOutboundCarrierManifests();
       public List<KeyValuePair<Int64?, string>> selectOutboundCarrierManifestsNullable() => _outboundshipmentsRepository.selectOutboundCarrierManifestsNullable();
        public bool VerifyOutboundShipmentUnique(Int64 ixOutboundShipment, string sOutboundShipment) => _outboundshipmentsRepository.VerifyOutboundShipmentUnique(ixOutboundShipment, sOutboundShipment);
        public List<string> VerifyOutboundShipmentDeleteOK(Int64 ixOutboundShipment, string sOutboundShipment) => _outboundshipmentsRepository.VerifyOutboundShipmentDeleteOK(ixOutboundShipment, sOutboundShipment);

        public Task<Int64> Create(OutboundShipmentsPost outboundshipmentsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundshipmentsRepository.RegisterCreate(outboundshipmentsPost);
            try
            {
                this._outboundshipmentsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundshipmentsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(outboundshipmentsPost.ixOutboundShipment);

        }
        public Task Edit(OutboundShipmentsPost outboundshipmentsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundshipmentsRepository.RegisterEdit(outboundshipmentsPost);
            try
            {
                this._outboundshipmentsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundshipmentsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(OutboundShipmentsPost outboundshipmentsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundshipmentsRepository.RegisterDelete(outboundshipmentsPost);
            try
            {
                this._outboundshipmentsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundshipmentsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

