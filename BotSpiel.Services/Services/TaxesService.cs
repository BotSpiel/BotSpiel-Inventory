using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class TaxesService : ITaxesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly ITaxesRepository _taxesRepository;

        public TaxesService(ITaxesRepository taxesRepository)
        {
            _taxesRepository = taxesRepository;
        }

        public TaxesPost GetPost(Int64 ixTax) => _taxesRepository.GetPost(ixTax);
        public Taxes Get(Int64 ixTax) => _taxesRepository.Get(ixTax);
        public IQueryable<Taxes> Index() => _taxesRepository.Index();
        public IQueryable<Taxes> IndexDb() => _taxesRepository.IndexDb();
       public IQueryable<Countries> selectCountries() => _taxesRepository.selectCountries();
        public IQueryable<CountrySubDivisions> selectCountrySubDivisions() => _taxesRepository.selectCountrySubDivisions();
       public IQueryable<Countries> CountriesDb() => _taxesRepository.CountriesDb();
        public IQueryable<CountrySubDivisions> CountrySubDivisionsDb() => _taxesRepository.CountrySubDivisionsDb();
        public bool VerifyTaxUnique(Int64 ixTax, string sTax) => _taxesRepository.VerifyTaxUnique(ixTax, sTax);
        public List<string> VerifyTaxDeleteOK(Int64 ixTax, string sTax) => _taxesRepository.VerifyTaxDeleteOK(ixTax, sTax);

        public Task<Int64> Create(TaxesPost taxesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._taxesRepository.RegisterCreate(taxesPost);
            try
            {
                this._taxesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._taxesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(taxesPost.ixTax);

        }
        public Task Edit(TaxesPost taxesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._taxesRepository.RegisterEdit(taxesPost);
            try
            {
                this._taxesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._taxesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(TaxesPost taxesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._taxesRepository.RegisterDelete(taxesPost);
            try
            {
                this._taxesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._taxesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

