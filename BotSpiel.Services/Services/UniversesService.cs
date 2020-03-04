using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class UniversesService : IUniversesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IUniversesRepository _universesRepository;

        public UniversesService(IUniversesRepository universesRepository)
        {
            _universesRepository = universesRepository;
        }

        public UniversesPost GetPost(Int64 ixUniverse) => _universesRepository.GetPost(ixUniverse);
        public Universes Get(Int64 ixUniverse) => _universesRepository.Get(ixUniverse);
        public IQueryable<Universes> Index() => _universesRepository.Index();
        public IQueryable<Universes> IndexDb() => _universesRepository.IndexDb();
        public bool VerifyUniverseUnique(Int64 ixUniverse, string sUniverse) => _universesRepository.VerifyUniverseUnique(ixUniverse, sUniverse);
        public List<string> VerifyUniverseDeleteOK(Int64 ixUniverse, string sUniverse) => _universesRepository.VerifyUniverseDeleteOK(ixUniverse, sUniverse);

        public Task<Int64> Create(UniversesPost universesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._universesRepository.RegisterCreate(universesPost);
            try
            {
                this._universesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._universesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(universesPost.ixUniverse);

        }
        public Task Edit(UniversesPost universesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._universesRepository.RegisterEdit(universesPost);
            try
            {
                this._universesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._universesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(UniversesPost universesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._universesRepository.RegisterDelete(universesPost);
            try
            {
                this._universesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._universesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

