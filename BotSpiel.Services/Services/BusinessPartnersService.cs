using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class BusinessPartnersService : IBusinessPartnersService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IBusinessPartnersRepository _businesspartnersRepository;

        public BusinessPartnersService(IBusinessPartnersRepository businesspartnersRepository)
        {
            _businesspartnersRepository = businesspartnersRepository;
        }

        public BusinessPartnersPost GetPost(Int64 ixBusinessPartner) => _businesspartnersRepository.GetPost(ixBusinessPartner);
        public BusinessPartners Get(Int64 ixBusinessPartner) => _businesspartnersRepository.Get(ixBusinessPartner);
        public IQueryable<BusinessPartners> Index() => _businesspartnersRepository.Index();
        public IQueryable<BusinessPartners> IndexDb() => _businesspartnersRepository.IndexDb();
       public IQueryable<Addresses> selectAddresses() => _businesspartnersRepository.selectAddresses();
        public IQueryable<Companies> selectCompanies() => _businesspartnersRepository.selectCompanies();
        public IQueryable<BusinessPartnerTypes> selectBusinessPartnerTypes() => _businesspartnersRepository.selectBusinessPartnerTypes();
       public IQueryable<Addresses> AddressesDb() => _businesspartnersRepository.AddressesDb();
        public IQueryable<Companies> CompaniesDb() => _businesspartnersRepository.CompaniesDb();
        public IQueryable<BusinessPartnerTypes> BusinessPartnerTypesDb() => _businesspartnersRepository.BusinessPartnerTypesDb();
        public bool VerifyBusinessPartnerUnique(Int64 ixBusinessPartner, string sBusinessPartner) => _businesspartnersRepository.VerifyBusinessPartnerUnique(ixBusinessPartner, sBusinessPartner);
        public List<string> VerifyBusinessPartnerDeleteOK(Int64 ixBusinessPartner, string sBusinessPartner) => _businesspartnersRepository.VerifyBusinessPartnerDeleteOK(ixBusinessPartner, sBusinessPartner);

        public Task<Int64> Create(BusinessPartnersPost businesspartnersPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._businesspartnersRepository.RegisterCreate(businesspartnersPost);
            try
            {
                this._businesspartnersRepository.Commit();
            }
            catch(Exception ex)
            {
                this._businesspartnersRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(businesspartnersPost.ixBusinessPartner);

        }
        public Task Edit(BusinessPartnersPost businesspartnersPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._businesspartnersRepository.RegisterEdit(businesspartnersPost);
            try
            {
                this._businesspartnersRepository.Commit();
            }
            catch(Exception ex)
            {
                this._businesspartnersRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(BusinessPartnersPost businesspartnersPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._businesspartnersRepository.RegisterDelete(businesspartnersPost);
            try
            {
                this._businesspartnersRepository.Commit();
            }
            catch(Exception ex)
            {
                this._businesspartnersRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

