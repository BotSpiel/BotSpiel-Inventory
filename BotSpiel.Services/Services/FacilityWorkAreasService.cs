using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class FacilityWorkAreasService : IFacilityWorkAreasService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IFacilityWorkAreasRepository _facilityworkareasRepository;

        public FacilityWorkAreasService(IFacilityWorkAreasRepository facilityworkareasRepository)
        {
            _facilityworkareasRepository = facilityworkareasRepository;
        }

        public FacilityWorkAreasPost GetPost(Int64 ixFacilityWorkArea) => _facilityworkareasRepository.GetPost(ixFacilityWorkArea);
        public FacilityWorkAreas Get(Int64 ixFacilityWorkArea) => _facilityworkareasRepository.Get(ixFacilityWorkArea);
        public IQueryable<FacilityWorkAreas> Index() => _facilityworkareasRepository.Index();
        public IQueryable<FacilityWorkAreas> IndexDb() => _facilityworkareasRepository.IndexDb();
        public bool VerifyFacilityWorkAreaUnique(Int64 ixFacilityWorkArea, string sFacilityWorkArea) => _facilityworkareasRepository.VerifyFacilityWorkAreaUnique(ixFacilityWorkArea, sFacilityWorkArea);
        public List<string> VerifyFacilityWorkAreaDeleteOK(Int64 ixFacilityWorkArea, string sFacilityWorkArea) => _facilityworkareasRepository.VerifyFacilityWorkAreaDeleteOK(ixFacilityWorkArea, sFacilityWorkArea);

        public Task<Int64> Create(FacilityWorkAreasPost facilityworkareasPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._facilityworkareasRepository.RegisterCreate(facilityworkareasPost);
            try
            {
                this._facilityworkareasRepository.Commit();
            }
            catch(Exception ex)
            {
                this._facilityworkareasRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(facilityworkareasPost.ixFacilityWorkArea);

        }
        public Task Edit(FacilityWorkAreasPost facilityworkareasPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._facilityworkareasRepository.RegisterEdit(facilityworkareasPost);
            try
            {
                this._facilityworkareasRepository.Commit();
            }
            catch(Exception ex)
            {
                this._facilityworkareasRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(FacilityWorkAreasPost facilityworkareasPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._facilityworkareasRepository.RegisterDelete(facilityworkareasPost);
            try
            {
                this._facilityworkareasRepository.Commit();
            }
            catch(Exception ex)
            {
                this._facilityworkareasRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

