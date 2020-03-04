using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class AssemblyModuleGridsService : IAssemblyModuleGridsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IAssemblyModuleGridsRepository _assemblymodulegridsRepository;

        public AssemblyModuleGridsService(IAssemblyModuleGridsRepository assemblymodulegridsRepository)
        {
            _assemblymodulegridsRepository = assemblymodulegridsRepository;
        }

        public AssemblyModuleGridsPost GetPost(Int64 ixAssemblyModuleGrid) => _assemblymodulegridsRepository.GetPost(ixAssemblyModuleGrid);
        public AssemblyModuleGrids Get(Int64 ixAssemblyModuleGrid) => _assemblymodulegridsRepository.Get(ixAssemblyModuleGrid);
        public IQueryable<AssemblyModuleGrids> Index() => _assemblymodulegridsRepository.Index();
        public IQueryable<AssemblyModuleGrids> IndexDb() => _assemblymodulegridsRepository.IndexDb();
		public IQueryable<AssemblyModuleGridsconfig> Indexconfig() => _assemblymodulegridsRepository.Indexconfig();
		public IQueryable<AssemblyModuleGridsmd> Indexmd() => _assemblymodulegridsRepository.Indexmd();
		public IQueryable<AssemblyModuleGridstx> Indextx() => _assemblymodulegridsRepository.Indextx();
		public IQueryable<AssemblyModuleGridsanalytics> Indexanalytics() => _assemblymodulegridsRepository.Indexanalytics();
        public List<string> VerifyAssemblyModuleGridDeleteOK(Int64 ixAssemblyModuleGrid, string sAssemblyModuleGrid) => _assemblymodulegridsRepository.VerifyAssemblyModuleGridDeleteOK(ixAssemblyModuleGrid, sAssemblyModuleGrid);

        public Task<Int64> Create(AssemblyModuleGridsPost assemblymodulegridsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._assemblymodulegridsRepository.RegisterCreate(assemblymodulegridsPost);
            try
            {
                this._assemblymodulegridsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._assemblymodulegridsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(assemblymodulegridsPost.ixAssemblyModuleGrid);

        }
        public Task Edit(AssemblyModuleGridsPost assemblymodulegridsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._assemblymodulegridsRepository.RegisterEdit(assemblymodulegridsPost);
            try
            {
                this._assemblymodulegridsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._assemblymodulegridsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(AssemblyModuleGridsPost assemblymodulegridsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._assemblymodulegridsRepository.RegisterDelete(assemblymodulegridsPost);
            try
            {
                this._assemblymodulegridsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._assemblymodulegridsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

