using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class CurrenciesService : ICurrenciesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly ICurrenciesRepository _currenciesRepository;

        public CurrenciesService(ICurrenciesRepository currenciesRepository)
        {
            _currenciesRepository = currenciesRepository;
        }

        public CurrenciesPost GetPost(Int64 ixCurrency) => _currenciesRepository.GetPost(ixCurrency);
        public Currencies Get(Int64 ixCurrency) => _currenciesRepository.Get(ixCurrency);
        public IQueryable<Currencies> Index() => _currenciesRepository.Index();
        public IQueryable<Currencies> IndexDb() => _currenciesRepository.IndexDb();
        public bool VerifyCurrencyUnique(Int64 ixCurrency, string sCurrency) => _currenciesRepository.VerifyCurrencyUnique(ixCurrency, sCurrency);
        public List<string> VerifyCurrencyDeleteOK(Int64 ixCurrency, string sCurrency) => _currenciesRepository.VerifyCurrencyDeleteOK(ixCurrency, sCurrency);

        public Task<Int64> Create(CurrenciesPost currenciesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._currenciesRepository.RegisterCreate(currenciesPost);
            try
            {
                this._currenciesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._currenciesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(currenciesPost.ixCurrency);

        }
        public Task Edit(CurrenciesPost currenciesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._currenciesRepository.RegisterEdit(currenciesPost);
            try
            {
                this._currenciesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._currenciesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(CurrenciesPost currenciesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._currenciesRepository.RegisterDelete(currenciesPost);
            try
            {
                this._currenciesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._currenciesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

