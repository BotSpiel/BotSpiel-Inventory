using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class CountriesService : ICountriesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly ICountriesRepository _countriesRepository;

        public CountriesService(ICountriesRepository countriesRepository)
        {
            _countriesRepository = countriesRepository;
        }

        public CountriesPost GetPost(Int64 ixCountry) => _countriesRepository.GetPost(ixCountry);
        public Countries Get(Int64 ixCountry) => _countriesRepository.Get(ixCountry);
        public IQueryable<Countries> Index() => _countriesRepository.Index();
        public IQueryable<Countries> IndexDb() => _countriesRepository.IndexDb();
       public IQueryable<PlanetSubRegions> selectPlanetSubRegions() => _countriesRepository.selectPlanetSubRegions();
       public IQueryable<PlanetSubRegions> PlanetSubRegionsDb() => _countriesRepository.PlanetSubRegionsDb();
        public bool VerifyCountryUnique(Int64 ixCountry, string sCountry) => _countriesRepository.VerifyCountryUnique(ixCountry, sCountry);
        public List<string> VerifyCountryDeleteOK(Int64 ixCountry, string sCountry) => _countriesRepository.VerifyCountryDeleteOK(ixCountry, sCountry);

        public Task<Int64> Create(CountriesPost countriesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._countriesRepository.RegisterCreate(countriesPost);
            try
            {
                this._countriesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._countriesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(countriesPost.ixCountry);

        }
        public Task Edit(CountriesPost countriesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._countriesRepository.RegisterEdit(countriesPost);
            try
            {
                this._countriesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._countriesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(CountriesPost countriesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._countriesRepository.RegisterDelete(countriesPost);
            try
            {
                this._countriesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._countriesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

