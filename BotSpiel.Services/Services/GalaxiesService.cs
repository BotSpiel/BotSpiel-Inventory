using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class GalaxiesService : IGalaxiesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IGalaxiesRepository _galaxiesRepository;

        public GalaxiesService(IGalaxiesRepository galaxiesRepository)
        {
            _galaxiesRepository = galaxiesRepository;
        }

        public GalaxiesPost GetPost(Int64 ixGalaxy) => _galaxiesRepository.GetPost(ixGalaxy);
        public Galaxies Get(Int64 ixGalaxy) => _galaxiesRepository.Get(ixGalaxy);
        public IQueryable<Galaxies> Index() => _galaxiesRepository.Index();
        public IQueryable<Galaxies> IndexDb() => _galaxiesRepository.IndexDb();
       public IQueryable<Universes> selectUniverses() => _galaxiesRepository.selectUniverses();
       public IQueryable<Universes> UniversesDb() => _galaxiesRepository.UniversesDb();
        public bool VerifyGalaxyUnique(Int64 ixGalaxy, string sGalaxy) => _galaxiesRepository.VerifyGalaxyUnique(ixGalaxy, sGalaxy);
        public List<string> VerifyGalaxyDeleteOK(Int64 ixGalaxy, string sGalaxy) => _galaxiesRepository.VerifyGalaxyDeleteOK(ixGalaxy, sGalaxy);

        public Task<Int64> Create(GalaxiesPost galaxiesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._galaxiesRepository.RegisterCreate(galaxiesPost);
            try
            {
                this._galaxiesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._galaxiesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(galaxiesPost.ixGalaxy);

        }
        public Task Edit(GalaxiesPost galaxiesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._galaxiesRepository.RegisterEdit(galaxiesPost);
            try
            {
                this._galaxiesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._galaxiesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(GalaxiesPost galaxiesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._galaxiesRepository.RegisterDelete(galaxiesPost);
            try
            {
                this._galaxiesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._galaxiesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

