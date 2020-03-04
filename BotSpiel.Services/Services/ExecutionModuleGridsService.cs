using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class ExecutionModuleGridsService : IExecutionModuleGridsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IExecutionModuleGridsRepository _executionmodulegridsRepository;

        public ExecutionModuleGridsService(IExecutionModuleGridsRepository executionmodulegridsRepository)
        {
            _executionmodulegridsRepository = executionmodulegridsRepository;
        }

        public ExecutionModuleGridsPost GetPost(Int64 ixExecutionModuleGrid) => _executionmodulegridsRepository.GetPost(ixExecutionModuleGrid);
        public ExecutionModuleGrids Get(Int64 ixExecutionModuleGrid) => _executionmodulegridsRepository.Get(ixExecutionModuleGrid);
        public IQueryable<ExecutionModuleGrids> Index() => _executionmodulegridsRepository.Index();
        public IQueryable<ExecutionModuleGrids> IndexDb() => _executionmodulegridsRepository.IndexDb();
		public IQueryable<ExecutionModuleGridsconfig> Indexconfig() => _executionmodulegridsRepository.Indexconfig();
		public IQueryable<ExecutionModuleGridsmd> Indexmd() => _executionmodulegridsRepository.Indexmd();
		public IQueryable<ExecutionModuleGridstx> Indextx() => _executionmodulegridsRepository.Indextx();
		public IQueryable<ExecutionModuleGridsanalytics> Indexanalytics() => _executionmodulegridsRepository.Indexanalytics();
        public List<string> VerifyExecutionModuleGridDeleteOK(Int64 ixExecutionModuleGrid, string sExecutionModuleGrid) => _executionmodulegridsRepository.VerifyExecutionModuleGridDeleteOK(ixExecutionModuleGrid, sExecutionModuleGrid);

        public Task<Int64> Create(ExecutionModuleGridsPost executionmodulegridsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._executionmodulegridsRepository.RegisterCreate(executionmodulegridsPost);
            try
            {
                this._executionmodulegridsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._executionmodulegridsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(executionmodulegridsPost.ixExecutionModuleGrid);

        }
        public Task Edit(ExecutionModuleGridsPost executionmodulegridsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._executionmodulegridsRepository.RegisterEdit(executionmodulegridsPost);
            try
            {
                this._executionmodulegridsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._executionmodulegridsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(ExecutionModuleGridsPost executionmodulegridsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._executionmodulegridsRepository.RegisterDelete(executionmodulegridsPost);
            try
            {
                this._executionmodulegridsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._executionmodulegridsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

