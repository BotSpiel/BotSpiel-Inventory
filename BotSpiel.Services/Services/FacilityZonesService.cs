using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class FacilityZonesService : IFacilityZonesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IFacilityZonesRepository _facilityzonesRepository;

        public FacilityZonesService(IFacilityZonesRepository facilityzonesRepository)
        {
            _facilityzonesRepository = facilityzonesRepository;
        }

        public FacilityZonesPost GetPost(Int64 ixFacilityZone) => _facilityzonesRepository.GetPost(ixFacilityZone);
        public FacilityZones Get(Int64 ixFacilityZone) => _facilityzonesRepository.Get(ixFacilityZone);
        public IQueryable<FacilityZones> Index() => _facilityzonesRepository.Index();
        public bool VerifyFacilityZoneUnique(Int64 ixFacilityZone, string sFacilityZone) => _facilityzonesRepository.VerifyFacilityZoneUnique(ixFacilityZone, sFacilityZone);
        public List<string> VerifyFacilityZoneDeleteOK(Int64 ixFacilityZone, string sFacilityZone) => _facilityzonesRepository.VerifyFacilityZoneDeleteOK(ixFacilityZone, sFacilityZone);

        public Task<Int64> Create(FacilityZonesPost facilityzonesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._facilityzonesRepository.RegisterCreate(facilityzonesPost);
            try
            {
                this._facilityzonesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._facilityzonesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(facilityzonesPost.ixFacilityZone);

        }
        public Task Edit(FacilityZonesPost facilityzonesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._facilityzonesRepository.RegisterEdit(facilityzonesPost);
            try
            {
                this._facilityzonesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._facilityzonesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(FacilityZonesPost facilityzonesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._facilityzonesRepository.RegisterDelete(facilityzonesPost);
            try
            {
                this._facilityzonesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._facilityzonesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

