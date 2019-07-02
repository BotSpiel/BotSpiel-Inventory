using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class FacilityFloorsService : IFacilityFloorsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IFacilityFloorsRepository _facilityfloorsRepository;

        public FacilityFloorsService(IFacilityFloorsRepository facilityfloorsRepository)
        {
            _facilityfloorsRepository = facilityfloorsRepository;
        }

        public FacilityFloorsPost GetPost(Int64 ixFacilityFloor) => _facilityfloorsRepository.GetPost(ixFacilityFloor);
        public FacilityFloors Get(Int64 ixFacilityFloor) => _facilityfloorsRepository.Get(ixFacilityFloor);
        public IQueryable<FacilityFloors> Index() => _facilityfloorsRepository.Index();
        public bool VerifyFacilityFloorUnique(Int64 ixFacilityFloor, string sFacilityFloor) => _facilityfloorsRepository.VerifyFacilityFloorUnique(ixFacilityFloor, sFacilityFloor);
        public List<string> VerifyFacilityFloorDeleteOK(Int64 ixFacilityFloor, string sFacilityFloor) => _facilityfloorsRepository.VerifyFacilityFloorDeleteOK(ixFacilityFloor, sFacilityFloor);

        public Task<Int64> Create(FacilityFloorsPost facilityfloorsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._facilityfloorsRepository.RegisterCreate(facilityfloorsPost);
            try
            {
                this._facilityfloorsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._facilityfloorsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(facilityfloorsPost.ixFacilityFloor);

        }
        public Task Edit(FacilityFloorsPost facilityfloorsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._facilityfloorsRepository.RegisterEdit(facilityfloorsPost);
            try
            {
                this._facilityfloorsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._facilityfloorsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(FacilityFloorsPost facilityfloorsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._facilityfloorsRepository.RegisterDelete(facilityfloorsPost);
            try
            {
                this._facilityfloorsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._facilityfloorsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

