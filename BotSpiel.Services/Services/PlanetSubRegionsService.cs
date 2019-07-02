using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class PlanetSubRegionsService : IPlanetSubRegionsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IPlanetSubRegionsRepository _planetsubregionsRepository;

        public PlanetSubRegionsService(IPlanetSubRegionsRepository planetsubregionsRepository)
        {
            _planetsubregionsRepository = planetsubregionsRepository;
        }

        public PlanetSubRegionsPost GetPost(Int64 ixPlanetSubRegion) => _planetsubregionsRepository.GetPost(ixPlanetSubRegion);
        public PlanetSubRegions Get(Int64 ixPlanetSubRegion) => _planetsubregionsRepository.Get(ixPlanetSubRegion);
        public IQueryable<PlanetSubRegions> Index() => _planetsubregionsRepository.Index();
       public IQueryable<PlanetRegions> selectPlanetRegions() => _planetsubregionsRepository.selectPlanetRegions();
        public bool VerifyPlanetSubRegionUnique(Int64 ixPlanetSubRegion, string sPlanetSubRegion) => _planetsubregionsRepository.VerifyPlanetSubRegionUnique(ixPlanetSubRegion, sPlanetSubRegion);
        public List<string> VerifyPlanetSubRegionDeleteOK(Int64 ixPlanetSubRegion, string sPlanetSubRegion) => _planetsubregionsRepository.VerifyPlanetSubRegionDeleteOK(ixPlanetSubRegion, sPlanetSubRegion);

        public Task<Int64> Create(PlanetSubRegionsPost planetsubregionsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._planetsubregionsRepository.RegisterCreate(planetsubregionsPost);
            try
            {
                this._planetsubregionsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._planetsubregionsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(planetsubregionsPost.ixPlanetSubRegion);

        }
        public Task Edit(PlanetSubRegionsPost planetsubregionsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._planetsubregionsRepository.RegisterEdit(planetsubregionsPost);
            try
            {
                this._planetsubregionsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._planetsubregionsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(PlanetSubRegionsPost planetsubregionsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._planetsubregionsRepository.RegisterDelete(planetsubregionsPost);
            try
            {
                this._planetsubregionsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._planetsubregionsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

