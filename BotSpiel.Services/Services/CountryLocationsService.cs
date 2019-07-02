using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class CountryLocationsService : ICountryLocationsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly ICountryLocationsRepository _countrylocationsRepository;

        public CountryLocationsService(ICountryLocationsRepository countrylocationsRepository)
        {
            _countrylocationsRepository = countrylocationsRepository;
        }

        public CountryLocationsPost GetPost(Int64 ixCountryLocation) => _countrylocationsRepository.GetPost(ixCountryLocation);
        public CountryLocations Get(Int64 ixCountryLocation) => _countrylocationsRepository.Get(ixCountryLocation);
        public IQueryable<CountryLocations> Index() => _countrylocationsRepository.Index();
       public IQueryable<CountrySubDivisions> selectCountrySubDivisions() => _countrylocationsRepository.selectCountrySubDivisions();
        public bool VerifyCountryLocationUnique(Int64 ixCountryLocation, string sCountryLocation) => _countrylocationsRepository.VerifyCountryLocationUnique(ixCountryLocation, sCountryLocation);
        public List<string> VerifyCountryLocationDeleteOK(Int64 ixCountryLocation, string sCountryLocation) => _countrylocationsRepository.VerifyCountryLocationDeleteOK(ixCountryLocation, sCountryLocation);

        public Task<Int64> Create(CountryLocationsPost countrylocationsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._countrylocationsRepository.RegisterCreate(countrylocationsPost);
            try
            {
                this._countrylocationsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._countrylocationsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(countrylocationsPost.ixCountryLocation);

        }
        public Task Edit(CountryLocationsPost countrylocationsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._countrylocationsRepository.RegisterEdit(countrylocationsPost);
            try
            {
                this._countrylocationsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._countrylocationsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(CountryLocationsPost countrylocationsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._countrylocationsRepository.RegisterDelete(countrylocationsPost);
            try
            {
                this._countrylocationsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._countrylocationsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

