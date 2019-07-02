using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class AddressesService : IAddressesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IAddressesRepository _addressesRepository;

        public AddressesService(IAddressesRepository addressesRepository)
        {
            _addressesRepository = addressesRepository;
        }

        public AddressesPost GetPost(Int64 ixAddress) => _addressesRepository.GetPost(ixAddress);
        public Addresses Get(Int64 ixAddress) => _addressesRepository.Get(ixAddress);
        public IQueryable<Addresses> Index() => _addressesRepository.Index();
       public IQueryable<Countries> selectCountries() => _addressesRepository.selectCountries();
        public IQueryable<CountrySubDivisions> selectCountrySubDivisions() => _addressesRepository.selectCountrySubDivisions();
        public bool VerifyAddressUnique(Int64 ixAddress, string sAddress) => _addressesRepository.VerifyAddressUnique(ixAddress, sAddress);
        public List<string> VerifyAddressDeleteOK(Int64 ixAddress, string sAddress) => _addressesRepository.VerifyAddressDeleteOK(ixAddress, sAddress);

        public Task<Int64> Create(AddressesPost addressesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._addressesRepository.RegisterCreate(addressesPost);
            try
            {
                this._addressesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._addressesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(addressesPost.ixAddress);

        }
        public Task Edit(AddressesPost addressesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._addressesRepository.RegisterEdit(addressesPost);
            try
            {
                this._addressesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._addressesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(AddressesPost addressesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._addressesRepository.RegisterDelete(addressesPost);
            try
            {
                this._addressesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._addressesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        //Custom Code Start | Added Code Block 
        public List<KeyValuePair<Int64?, string>> selectEmptyCountrySubDivisionsDropdown() => _addressesRepository.selectEmptyCountrySubDivisionsDropdown();
        //Custom Code End
    }
}
  
