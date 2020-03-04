using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class PlanetsService : IPlanetsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IPlanetsRepository _planetsRepository;

        public PlanetsService(IPlanetsRepository planetsRepository)
        {
            _planetsRepository = planetsRepository;
        }

        public PlanetsPost GetPost(Int64 ixPlanet) => _planetsRepository.GetPost(ixPlanet);
        public Planets Get(Int64 ixPlanet) => _planetsRepository.Get(ixPlanet);
        public IQueryable<Planets> Index() => _planetsRepository.Index();
        public IQueryable<Planets> IndexDb() => _planetsRepository.IndexDb();
       public IQueryable<PlanetarySystems> selectPlanetarySystems() => _planetsRepository.selectPlanetarySystems();
       public IQueryable<PlanetarySystems> PlanetarySystemsDb() => _planetsRepository.PlanetarySystemsDb();
        public bool VerifyPlanetUnique(Int64 ixPlanet, string sPlanet) => _planetsRepository.VerifyPlanetUnique(ixPlanet, sPlanet);
        public List<string> VerifyPlanetDeleteOK(Int64 ixPlanet, string sPlanet) => _planetsRepository.VerifyPlanetDeleteOK(ixPlanet, sPlanet);

        public Task<Int64> Create(PlanetsPost planetsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._planetsRepository.RegisterCreate(planetsPost);
            try
            {
                this._planetsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._planetsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(planetsPost.ixPlanet);

        }
        public Task Edit(PlanetsPost planetsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._planetsRepository.RegisterEdit(planetsPost);
            try
            {
                this._planetsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._planetsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(PlanetsPost planetsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._planetsRepository.RegisterDelete(planetsPost);
            try
            {
                this._planetsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._planetsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

