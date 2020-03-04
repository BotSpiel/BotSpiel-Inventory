using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class PlanetarySystemsService : IPlanetarySystemsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IPlanetarySystemsRepository _planetarysystemsRepository;

        public PlanetarySystemsService(IPlanetarySystemsRepository planetarysystemsRepository)
        {
            _planetarysystemsRepository = planetarysystemsRepository;
        }

        public PlanetarySystemsPost GetPost(Int64 ixPlanetarySystem) => _planetarysystemsRepository.GetPost(ixPlanetarySystem);
        public PlanetarySystems Get(Int64 ixPlanetarySystem) => _planetarysystemsRepository.Get(ixPlanetarySystem);
        public IQueryable<PlanetarySystems> Index() => _planetarysystemsRepository.Index();
        public IQueryable<PlanetarySystems> IndexDb() => _planetarysystemsRepository.IndexDb();
       public IQueryable<Galaxies> selectGalaxies() => _planetarysystemsRepository.selectGalaxies();
       public IQueryable<Galaxies> GalaxiesDb() => _planetarysystemsRepository.GalaxiesDb();
        public bool VerifyPlanetarySystemUnique(Int64 ixPlanetarySystem, string sPlanetarySystem) => _planetarysystemsRepository.VerifyPlanetarySystemUnique(ixPlanetarySystem, sPlanetarySystem);
        public List<string> VerifyPlanetarySystemDeleteOK(Int64 ixPlanetarySystem, string sPlanetarySystem) => _planetarysystemsRepository.VerifyPlanetarySystemDeleteOK(ixPlanetarySystem, sPlanetarySystem);

        public Task<Int64> Create(PlanetarySystemsPost planetarysystemsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._planetarysystemsRepository.RegisterCreate(planetarysystemsPost);
            try
            {
                this._planetarysystemsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._planetarysystemsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(planetarysystemsPost.ixPlanetarySystem);

        }
        public Task Edit(PlanetarySystemsPost planetarysystemsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._planetarysystemsRepository.RegisterEdit(planetarysystemsPost);
            try
            {
                this._planetarysystemsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._planetarysystemsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(PlanetarySystemsPost planetarysystemsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._planetarysystemsRepository.RegisterDelete(planetarysystemsPost);
            try
            {
                this._planetarysystemsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._planetarysystemsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

