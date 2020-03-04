using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class BusinessPartnerTypesService : IBusinessPartnerTypesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IBusinessPartnerTypesRepository _businesspartnertypesRepository;

        public BusinessPartnerTypesService(IBusinessPartnerTypesRepository businesspartnertypesRepository)
        {
            _businesspartnertypesRepository = businesspartnertypesRepository;
        }

        public BusinessPartnerTypesPost GetPost(Int64 ixBusinessPartnerType) => _businesspartnertypesRepository.GetPost(ixBusinessPartnerType);
        public BusinessPartnerTypes Get(Int64 ixBusinessPartnerType) => _businesspartnertypesRepository.Get(ixBusinessPartnerType);
        public IQueryable<BusinessPartnerTypes> Index() => _businesspartnertypesRepository.Index();
        public IQueryable<BusinessPartnerTypes> IndexDb() => _businesspartnertypesRepository.IndexDb();
        public bool VerifyBusinessPartnerTypeUnique(Int64 ixBusinessPartnerType, string sBusinessPartnerType) => _businesspartnertypesRepository.VerifyBusinessPartnerTypeUnique(ixBusinessPartnerType, sBusinessPartnerType);
        public List<string> VerifyBusinessPartnerTypeDeleteOK(Int64 ixBusinessPartnerType, string sBusinessPartnerType) => _businesspartnertypesRepository.VerifyBusinessPartnerTypeDeleteOK(ixBusinessPartnerType, sBusinessPartnerType);

        public Task<Int64> Create(BusinessPartnerTypesPost businesspartnertypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._businesspartnertypesRepository.RegisterCreate(businesspartnertypesPost);
            try
            {
                this._businesspartnertypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._businesspartnertypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(businesspartnertypesPost.ixBusinessPartnerType);

        }
        public Task Edit(BusinessPartnerTypesPost businesspartnertypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._businesspartnertypesRepository.RegisterEdit(businesspartnertypesPost);
            try
            {
                this._businesspartnertypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._businesspartnertypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(BusinessPartnerTypesPost businesspartnertypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._businesspartnertypesRepository.RegisterDelete(businesspartnertypesPost);
            try
            {
                this._businesspartnertypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._businesspartnertypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

