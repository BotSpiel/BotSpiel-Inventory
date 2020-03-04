using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class FacilitiesService : IFacilitiesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IFacilitiesRepository _facilitiesRepository;

        public FacilitiesService(IFacilitiesRepository facilitiesRepository)
        {
            _facilitiesRepository = facilitiesRepository;
        }

        public FacilitiesPost GetPost(Int64 ixFacility) => _facilitiesRepository.GetPost(ixFacility);
        public Facilities Get(Int64 ixFacility) => _facilitiesRepository.Get(ixFacility);
        public IQueryable<Facilities> Index() => _facilitiesRepository.Index();
        public IQueryable<Facilities> IndexDb() => _facilitiesRepository.IndexDb();
       public IQueryable<Addresses> selectAddresses() => _facilitiesRepository.selectAddresses();
       public IQueryable<Addresses> AddressesDb() => _facilitiesRepository.AddressesDb();
        public bool VerifyFacilityUnique(Int64 ixFacility, string sFacility) => _facilitiesRepository.VerifyFacilityUnique(ixFacility, sFacility);
        public List<string> VerifyFacilityDeleteOK(Int64 ixFacility, string sFacility) => _facilitiesRepository.VerifyFacilityDeleteOK(ixFacility, sFacility);

        public Task<Int64> Create(FacilitiesPost facilitiesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._facilitiesRepository.RegisterCreate(facilitiesPost);
            try
            {
                this._facilitiesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._facilitiesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(facilitiesPost.ixFacility);

        }
        public Task Edit(FacilitiesPost facilitiesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._facilitiesRepository.RegisterEdit(facilitiesPost);
            try
            {
                this._facilitiesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._facilitiesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(FacilitiesPost facilitiesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._facilitiesRepository.RegisterDelete(facilitiesPost);
            try
            {
                this._facilitiesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._facilitiesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

