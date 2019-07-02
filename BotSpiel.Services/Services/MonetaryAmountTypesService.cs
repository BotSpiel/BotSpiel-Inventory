using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class MonetaryAmountTypesService : IMonetaryAmountTypesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IMonetaryAmountTypesRepository _monetaryamounttypesRepository;

        public MonetaryAmountTypesService(IMonetaryAmountTypesRepository monetaryamounttypesRepository)
        {
            _monetaryamounttypesRepository = monetaryamounttypesRepository;
        }

        public MonetaryAmountTypesPost GetPost(Int64 ixMonetaryAmountType) => _monetaryamounttypesRepository.GetPost(ixMonetaryAmountType);
        public MonetaryAmountTypes Get(Int64 ixMonetaryAmountType) => _monetaryamounttypesRepository.Get(ixMonetaryAmountType);
        public IQueryable<MonetaryAmountTypes> Index() => _monetaryamounttypesRepository.Index();
        public bool VerifyMonetaryAmountTypeUnique(Int64 ixMonetaryAmountType, string sMonetaryAmountType) => _monetaryamounttypesRepository.VerifyMonetaryAmountTypeUnique(ixMonetaryAmountType, sMonetaryAmountType);
        public List<string> VerifyMonetaryAmountTypeDeleteOK(Int64 ixMonetaryAmountType, string sMonetaryAmountType) => _monetaryamounttypesRepository.VerifyMonetaryAmountTypeDeleteOK(ixMonetaryAmountType, sMonetaryAmountType);

        public Task<Int64> Create(MonetaryAmountTypesPost monetaryamounttypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._monetaryamounttypesRepository.RegisterCreate(monetaryamounttypesPost);
            try
            {
                this._monetaryamounttypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._monetaryamounttypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(monetaryamounttypesPost.ixMonetaryAmountType);

        }
        public Task Edit(MonetaryAmountTypesPost monetaryamounttypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._monetaryamounttypesRepository.RegisterEdit(monetaryamounttypesPost);
            try
            {
                this._monetaryamounttypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._monetaryamounttypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(MonetaryAmountTypesPost monetaryamounttypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._monetaryamounttypesRepository.RegisterDelete(monetaryamounttypesPost);
            try
            {
                this._monetaryamounttypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._monetaryamounttypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

