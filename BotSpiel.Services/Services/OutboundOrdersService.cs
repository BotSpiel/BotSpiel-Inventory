using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class OutboundOrdersService : IOutboundOrdersService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IOutboundOrdersRepository _outboundordersRepository;

        public OutboundOrdersService(IOutboundOrdersRepository outboundordersRepository)
        {
            _outboundordersRepository = outboundordersRepository;
        }

        public OutboundOrdersPost GetPost(Int64 ixOutboundOrder) => _outboundordersRepository.GetPost(ixOutboundOrder);
        public OutboundOrders Get(Int64 ixOutboundOrder) => _outboundordersRepository.Get(ixOutboundOrder);
        public IQueryable<OutboundOrders> Index() => _outboundordersRepository.Index();
       public IQueryable<Statuses> selectStatuses() => _outboundordersRepository.selectStatuses();
        public IQueryable<Companies> selectCompanies() => _outboundordersRepository.selectCompanies();
        public IQueryable<Facilities> selectFacilities() => _outboundordersRepository.selectFacilities();
        public IQueryable<BusinessPartners> selectBusinessPartners() => _outboundordersRepository.selectBusinessPartners();
        public IQueryable<OutboundOrderTypes> selectOutboundOrderTypes() => _outboundordersRepository.selectOutboundOrderTypes();
        public IQueryable<CarrierServices> selectCarrierServices() => _outboundordersRepository.selectCarrierServices();
        public IQueryable<OutboundShipments> selectOutboundShipments() => _outboundordersRepository.selectOutboundShipments();
        public IQueryable<PickBatches> selectPickBatches() => _outboundordersRepository.selectPickBatches();
       public List<KeyValuePair<Int64?, string>> selectOutboundShipmentsNullable() => _outboundordersRepository.selectOutboundShipmentsNullable();
        public List<KeyValuePair<Int64?, string>> selectPickBatchesNullable() => _outboundordersRepository.selectPickBatchesNullable();
        public bool VerifyOutboundOrderUnique(Int64 ixOutboundOrder, string sOutboundOrder) => _outboundordersRepository.VerifyOutboundOrderUnique(ixOutboundOrder, sOutboundOrder);
        public List<string> VerifyOutboundOrderDeleteOK(Int64 ixOutboundOrder, string sOutboundOrder) => _outboundordersRepository.VerifyOutboundOrderDeleteOK(ixOutboundOrder, sOutboundOrder);

        public Task<Int64> Create(OutboundOrdersPost outboundordersPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundordersRepository.RegisterCreate(outboundordersPost);
            try
            {
                this._outboundordersRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundordersRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(outboundordersPost.ixOutboundOrder);

        }
        public Task Edit(OutboundOrdersPost outboundordersPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundordersRepository.RegisterEdit(outboundordersPost);
            try
            {
                this._outboundordersRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundordersRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(OutboundOrdersPost outboundordersPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundordersRepository.RegisterDelete(outboundordersPost);
            try
            {
                this._outboundordersRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundordersRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

