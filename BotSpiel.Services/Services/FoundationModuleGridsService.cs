using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class FoundationModuleGridsService : IFoundationModuleGridsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IFoundationModuleGridsRepository _foundationmodulegridsRepository;

        public FoundationModuleGridsService(IFoundationModuleGridsRepository foundationmodulegridsRepository)
        {
            _foundationmodulegridsRepository = foundationmodulegridsRepository;
        }

        public FoundationModuleGridsPost GetPost(Int64 ixFoundationModuleGrid) => _foundationmodulegridsRepository.GetPost(ixFoundationModuleGrid);
        public FoundationModuleGrids Get(Int64 ixFoundationModuleGrid) => _foundationmodulegridsRepository.Get(ixFoundationModuleGrid);
        public IQueryable<FoundationModuleGrids> Index() => _foundationmodulegridsRepository.Index();
        public IQueryable<FoundationModuleGrids> IndexDb() => _foundationmodulegridsRepository.IndexDb();
		public IQueryable<FoundationModuleGridsconfig> Indexconfig() => _foundationmodulegridsRepository.Indexconfig();
		public IQueryable<FoundationModuleGridsmd> Indexmd() => _foundationmodulegridsRepository.Indexmd();
		public IQueryable<FoundationModuleGridstx> Indextx() => _foundationmodulegridsRepository.Indextx();
		public IQueryable<FoundationModuleGridsanalytics> Indexanalytics() => _foundationmodulegridsRepository.Indexanalytics();
        public List<string> VerifyFoundationModuleGridDeleteOK(Int64 ixFoundationModuleGrid, string sFoundationModuleGrid) => _foundationmodulegridsRepository.VerifyFoundationModuleGridDeleteOK(ixFoundationModuleGrid, sFoundationModuleGrid);

        public Task<Int64> Create(FoundationModuleGridsPost foundationmodulegridsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._foundationmodulegridsRepository.RegisterCreate(foundationmodulegridsPost);
            try
            {
                this._foundationmodulegridsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._foundationmodulegridsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(foundationmodulegridsPost.ixFoundationModuleGrid);

        }
        public Task Edit(FoundationModuleGridsPost foundationmodulegridsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._foundationmodulegridsRepository.RegisterEdit(foundationmodulegridsPost);
            try
            {
                this._foundationmodulegridsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._foundationmodulegridsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(FoundationModuleGridsPost foundationmodulegridsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._foundationmodulegridsRepository.RegisterDelete(foundationmodulegridsPost);
            try
            {
                this._foundationmodulegridsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._foundationmodulegridsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

