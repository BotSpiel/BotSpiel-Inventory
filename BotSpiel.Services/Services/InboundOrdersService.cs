using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class InboundOrdersService : IInboundOrdersService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IInboundOrdersRepository _inboundordersRepository;

        public InboundOrdersService(IInboundOrdersRepository inboundordersRepository)
        {
            _inboundordersRepository = inboundordersRepository;
        }

        public InboundOrdersPost GetPost(Int64 ixInboundOrder) => _inboundordersRepository.GetPost(ixInboundOrder);
        public InboundOrders Get(Int64 ixInboundOrder) => _inboundordersRepository.Get(ixInboundOrder);
        public IQueryable<InboundOrders> Index() => _inboundordersRepository.Index();
        public IQueryable<InboundOrders> IndexDb() => _inboundordersRepository.IndexDb();
       public IQueryable<Statuses> selectStatuses() => _inboundordersRepository.selectStatuses();
        public IQueryable<Companies> selectCompanies() => _inboundordersRepository.selectCompanies();
        public IQueryable<Facilities> selectFacilities() => _inboundordersRepository.selectFacilities();
        public IQueryable<BusinessPartners> selectBusinessPartners() => _inboundordersRepository.selectBusinessPartners();
        public IQueryable<InboundOrderTypes> selectInboundOrderTypes() => _inboundordersRepository.selectInboundOrderTypes();
       public IQueryable<Statuses> StatusesDb() => _inboundordersRepository.StatusesDb();
        public IQueryable<Companies> CompaniesDb() => _inboundordersRepository.CompaniesDb();
        public IQueryable<Facilities> FacilitiesDb() => _inboundordersRepository.FacilitiesDb();
        public IQueryable<BusinessPartners> BusinessPartnersDb() => _inboundordersRepository.BusinessPartnersDb();
        public IQueryable<InboundOrderTypes> InboundOrderTypesDb() => _inboundordersRepository.InboundOrderTypesDb();
        public bool VerifyInboundOrderUnique(Int64 ixInboundOrder, string sInboundOrder) => _inboundordersRepository.VerifyInboundOrderUnique(ixInboundOrder, sInboundOrder);
        public List<string> VerifyInboundOrderDeleteOK(Int64 ixInboundOrder, string sInboundOrder) => _inboundordersRepository.VerifyInboundOrderDeleteOK(ixInboundOrder, sInboundOrder);

        public Task<Int64> Create(InboundOrdersPost inboundordersPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._inboundordersRepository.RegisterCreate(inboundordersPost);
            try
            {
                this._inboundordersRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inboundordersRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(inboundordersPost.ixInboundOrder);

        }
        public Task Edit(InboundOrdersPost inboundordersPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._inboundordersRepository.RegisterEdit(inboundordersPost);
            try
            {
                this._inboundordersRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inboundordersRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(InboundOrdersPost inboundordersPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._inboundordersRepository.RegisterDelete(inboundordersPost);
            try
            {
                this._inboundordersRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inboundordersRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

