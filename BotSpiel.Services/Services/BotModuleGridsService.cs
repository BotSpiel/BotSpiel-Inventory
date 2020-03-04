using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class BotModuleGridsService : IBotModuleGridsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IBotModuleGridsRepository _botmodulegridsRepository;

        public BotModuleGridsService(IBotModuleGridsRepository botmodulegridsRepository)
        {
            _botmodulegridsRepository = botmodulegridsRepository;
        }

        public BotModuleGridsPost GetPost(Int64 ixBotModuleGrid) => _botmodulegridsRepository.GetPost(ixBotModuleGrid);
        public BotModuleGrids Get(Int64 ixBotModuleGrid) => _botmodulegridsRepository.Get(ixBotModuleGrid);
        public IQueryable<BotModuleGrids> Index() => _botmodulegridsRepository.Index();
        public IQueryable<BotModuleGrids> IndexDb() => _botmodulegridsRepository.IndexDb();
		public IQueryable<BotModuleGridsconfig> Indexconfig() => _botmodulegridsRepository.Indexconfig();
		public IQueryable<BotModuleGridsmd> Indexmd() => _botmodulegridsRepository.Indexmd();
		public IQueryable<BotModuleGridstx> Indextx() => _botmodulegridsRepository.Indextx();
		public IQueryable<BotModuleGridsanalytics> Indexanalytics() => _botmodulegridsRepository.Indexanalytics();
        public List<string> VerifyBotModuleGridDeleteOK(Int64 ixBotModuleGrid, string sBotModuleGrid) => _botmodulegridsRepository.VerifyBotModuleGridDeleteOK(ixBotModuleGrid, sBotModuleGrid);

        public Task<Int64> Create(BotModuleGridsPost botmodulegridsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._botmodulegridsRepository.RegisterCreate(botmodulegridsPost);
            try
            {
                this._botmodulegridsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._botmodulegridsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(botmodulegridsPost.ixBotModuleGrid);

        }
        public Task Edit(BotModuleGridsPost botmodulegridsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._botmodulegridsRepository.RegisterEdit(botmodulegridsPost);
            try
            {
                this._botmodulegridsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._botmodulegridsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(BotModuleGridsPost botmodulegridsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._botmodulegridsRepository.RegisterDelete(botmodulegridsPost);
            try
            {
                this._botmodulegridsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._botmodulegridsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

