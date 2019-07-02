using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class PlanetRegionsService : IPlanetRegionsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IPlanetRegionsRepository _planetregionsRepository;

        public PlanetRegionsService(IPlanetRegionsRepository planetregionsRepository)
        {
            _planetregionsRepository = planetregionsRepository;
        }

        public PlanetRegionsPost GetPost(Int64 ixPlanetRegion) => _planetregionsRepository.GetPost(ixPlanetRegion);
        public PlanetRegions Get(Int64 ixPlanetRegion) => _planetregionsRepository.Get(ixPlanetRegion);
        public IQueryable<PlanetRegions> Index() => _planetregionsRepository.Index();
       public IQueryable<Planets> selectPlanets() => _planetregionsRepository.selectPlanets();
        public bool VerifyPlanetRegionUnique(Int64 ixPlanetRegion, string sPlanetRegion) => _planetregionsRepository.VerifyPlanetRegionUnique(ixPlanetRegion, sPlanetRegion);
        public List<string> VerifyPlanetRegionDeleteOK(Int64 ixPlanetRegion, string sPlanetRegion) => _planetregionsRepository.VerifyPlanetRegionDeleteOK(ixPlanetRegion, sPlanetRegion);

        public Task<Int64> Create(PlanetRegionsPost planetregionsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._planetregionsRepository.RegisterCreate(planetregionsPost);
            try
            {
                this._planetregionsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._planetregionsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(planetregionsPost.ixPlanetRegion);

        }
        public Task Edit(PlanetRegionsPost planetregionsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._planetregionsRepository.RegisterEdit(planetregionsPost);
            try
            {
                this._planetregionsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._planetregionsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(PlanetRegionsPost planetregionsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._planetregionsRepository.RegisterDelete(planetregionsPost);
            try
            {
                this._planetregionsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._planetregionsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

