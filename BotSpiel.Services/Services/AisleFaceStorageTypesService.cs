using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class AisleFaceStorageTypesService : IAisleFaceStorageTypesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IAisleFaceStorageTypesRepository _aislefacestoragetypesRepository;

        public AisleFaceStorageTypesService(IAisleFaceStorageTypesRepository aislefacestoragetypesRepository)
        {
            _aislefacestoragetypesRepository = aislefacestoragetypesRepository;
        }

        public AisleFaceStorageTypesPost GetPost(Int64 ixAisleFaceStorageType) => _aislefacestoragetypesRepository.GetPost(ixAisleFaceStorageType);
        public AisleFaceStorageTypes Get(Int64 ixAisleFaceStorageType) => _aislefacestoragetypesRepository.Get(ixAisleFaceStorageType);
        public IQueryable<AisleFaceStorageTypes> Index() => _aislefacestoragetypesRepository.Index();
        public IQueryable<AisleFaceStorageTypes> IndexDb() => _aislefacestoragetypesRepository.IndexDb();
        public bool VerifyAisleFaceStorageTypeUnique(Int64 ixAisleFaceStorageType, string sAisleFaceStorageType) => _aislefacestoragetypesRepository.VerifyAisleFaceStorageTypeUnique(ixAisleFaceStorageType, sAisleFaceStorageType);
        public List<string> VerifyAisleFaceStorageTypeDeleteOK(Int64 ixAisleFaceStorageType, string sAisleFaceStorageType) => _aislefacestoragetypesRepository.VerifyAisleFaceStorageTypeDeleteOK(ixAisleFaceStorageType, sAisleFaceStorageType);

        public Task<Int64> Create(AisleFaceStorageTypesPost aislefacestoragetypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._aislefacestoragetypesRepository.RegisterCreate(aislefacestoragetypesPost);
            try
            {
                this._aislefacestoragetypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._aislefacestoragetypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(aislefacestoragetypesPost.ixAisleFaceStorageType);

        }
        public Task Edit(AisleFaceStorageTypesPost aislefacestoragetypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._aislefacestoragetypesRepository.RegisterEdit(aislefacestoragetypesPost);
            try
            {
                this._aislefacestoragetypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._aislefacestoragetypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(AisleFaceStorageTypesPost aislefacestoragetypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._aislefacestoragetypesRepository.RegisterDelete(aislefacestoragetypesPost);
            try
            {
                this._aislefacestoragetypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._aislefacestoragetypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

