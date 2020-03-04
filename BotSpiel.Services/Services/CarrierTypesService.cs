using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class CarrierTypesService : ICarrierTypesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly ICarrierTypesRepository _carriertypesRepository;

        public CarrierTypesService(ICarrierTypesRepository carriertypesRepository)
        {
            _carriertypesRepository = carriertypesRepository;
        }

        public CarrierTypesPost GetPost(Int64 ixCarrierType) => _carriertypesRepository.GetPost(ixCarrierType);
        public CarrierTypes Get(Int64 ixCarrierType) => _carriertypesRepository.Get(ixCarrierType);
        public IQueryable<CarrierTypes> Index() => _carriertypesRepository.Index();
        public IQueryable<CarrierTypes> IndexDb() => _carriertypesRepository.IndexDb();
        public bool VerifyCarrierTypeUnique(Int64 ixCarrierType, string sCarrierType) => _carriertypesRepository.VerifyCarrierTypeUnique(ixCarrierType, sCarrierType);
        public List<string> VerifyCarrierTypeDeleteOK(Int64 ixCarrierType, string sCarrierType) => _carriertypesRepository.VerifyCarrierTypeDeleteOK(ixCarrierType, sCarrierType);

        public Task<Int64> Create(CarrierTypesPost carriertypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._carriertypesRepository.RegisterCreate(carriertypesPost);
            try
            {
                this._carriertypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._carriertypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(carriertypesPost.ixCarrierType);

        }
        public Task Edit(CarrierTypesPost carriertypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._carriertypesRepository.RegisterEdit(carriertypesPost);
            try
            {
                this._carriertypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._carriertypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(CarrierTypesPost carriertypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._carriertypesRepository.RegisterDelete(carriertypesPost);
            try
            {
                this._carriertypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._carriertypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

