using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class CountrySubDivisionsService : ICountrySubDivisionsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly ICountrySubDivisionsRepository _countrysubdivisionsRepository;

        public CountrySubDivisionsService(ICountrySubDivisionsRepository countrysubdivisionsRepository)
        {
            _countrysubdivisionsRepository = countrysubdivisionsRepository;
        }

        public CountrySubDivisionsPost GetPost(Int64 ixCountrySubDivision) => _countrysubdivisionsRepository.GetPost(ixCountrySubDivision);
        public CountrySubDivisions Get(Int64 ixCountrySubDivision) => _countrysubdivisionsRepository.Get(ixCountrySubDivision);
        public IQueryable<CountrySubDivisions> Index() => _countrysubdivisionsRepository.Index();
        public IQueryable<CountrySubDivisions> IndexDb() => _countrysubdivisionsRepository.IndexDb();
       public IQueryable<Countries> selectCountries() => _countrysubdivisionsRepository.selectCountries();
       public IQueryable<Countries> CountriesDb() => _countrysubdivisionsRepository.CountriesDb();
        public bool VerifyCountrySubDivisionUnique(Int64 ixCountrySubDivision, string sCountrySubDivision) => _countrysubdivisionsRepository.VerifyCountrySubDivisionUnique(ixCountrySubDivision, sCountrySubDivision);
        public List<string> VerifyCountrySubDivisionDeleteOK(Int64 ixCountrySubDivision, string sCountrySubDivision) => _countrysubdivisionsRepository.VerifyCountrySubDivisionDeleteOK(ixCountrySubDivision, sCountrySubDivision);

        public Task<Int64> Create(CountrySubDivisionsPost countrysubdivisionsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._countrysubdivisionsRepository.RegisterCreate(countrysubdivisionsPost);
            try
            {
                this._countrysubdivisionsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._countrysubdivisionsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(countrysubdivisionsPost.ixCountrySubDivision);

        }
        public Task Edit(CountrySubDivisionsPost countrysubdivisionsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._countrysubdivisionsRepository.RegisterEdit(countrysubdivisionsPost);
            try
            {
                this._countrysubdivisionsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._countrysubdivisionsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(CountrySubDivisionsPost countrysubdivisionsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._countrysubdivisionsRepository.RegisterDelete(countrysubdivisionsPost);
            try
            {
                this._countrysubdivisionsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._countrysubdivisionsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

