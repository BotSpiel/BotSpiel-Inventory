using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class CurrencyTypesService : ICurrencyTypesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly ICurrencyTypesRepository _currencytypesRepository;

        public CurrencyTypesService(ICurrencyTypesRepository currencytypesRepository)
        {
            _currencytypesRepository = currencytypesRepository;
        }

        public CurrencyTypesPost GetPost(Int64 ixCurrencyType) => _currencytypesRepository.GetPost(ixCurrencyType);
        public CurrencyTypes Get(Int64 ixCurrencyType) => _currencytypesRepository.Get(ixCurrencyType);
        public IQueryable<CurrencyTypes> Index() => _currencytypesRepository.Index();
        public IQueryable<CurrencyTypes> IndexDb() => _currencytypesRepository.IndexDb();
        public bool VerifyCurrencyTypeUnique(Int64 ixCurrencyType, string sCurrencyType) => _currencytypesRepository.VerifyCurrencyTypeUnique(ixCurrencyType, sCurrencyType);
        public List<string> VerifyCurrencyTypeDeleteOK(Int64 ixCurrencyType, string sCurrencyType) => _currencytypesRepository.VerifyCurrencyTypeDeleteOK(ixCurrencyType, sCurrencyType);

        public Task<Int64> Create(CurrencyTypesPost currencytypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._currencytypesRepository.RegisterCreate(currencytypesPost);
            try
            {
                this._currencytypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._currencytypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(currencytypesPost.ixCurrencyType);

        }
        public Task Edit(CurrencyTypesPost currencytypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._currencytypesRepository.RegisterEdit(currencytypesPost);
            try
            {
                this._currencytypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._currencytypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(CurrencyTypesPost currencytypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._currencytypesRepository.RegisterDelete(currencytypesPost);
            try
            {
                this._currencytypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._currencytypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

