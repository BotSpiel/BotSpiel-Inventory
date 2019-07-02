using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class NetworkModuleGridsService : INetworkModuleGridsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly INetworkModuleGridsRepository _networkmodulegridsRepository;

        public NetworkModuleGridsService(INetworkModuleGridsRepository networkmodulegridsRepository)
        {
            _networkmodulegridsRepository = networkmodulegridsRepository;
        }

        public NetworkModuleGridsPost GetPost(Int64 ixNetworkModuleGrid) => _networkmodulegridsRepository.GetPost(ixNetworkModuleGrid);
        public NetworkModuleGrids Get(Int64 ixNetworkModuleGrid) => _networkmodulegridsRepository.Get(ixNetworkModuleGrid);
        public IQueryable<NetworkModuleGrids> Index() => _networkmodulegridsRepository.Index();
		public IQueryable<NetworkModuleGridsconfig> Indexconfig() => _networkmodulegridsRepository.Indexconfig();
		public IQueryable<NetworkModuleGridsmd> Indexmd() => _networkmodulegridsRepository.Indexmd();
		public IQueryable<NetworkModuleGridstx> Indextx() => _networkmodulegridsRepository.Indextx();
		public IQueryable<NetworkModuleGridsanalytics> Indexanalytics() => _networkmodulegridsRepository.Indexanalytics();
        public List<string> VerifyNetworkModuleGridDeleteOK(Int64 ixNetworkModuleGrid, string sNetworkModuleGrid) => _networkmodulegridsRepository.VerifyNetworkModuleGridDeleteOK(ixNetworkModuleGrid, sNetworkModuleGrid);

        public Task<Int64> Create(NetworkModuleGridsPost networkmodulegridsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._networkmodulegridsRepository.RegisterCreate(networkmodulegridsPost);
            try
            {
                this._networkmodulegridsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._networkmodulegridsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(networkmodulegridsPost.ixNetworkModuleGrid);

        }
        public Task Edit(NetworkModuleGridsPost networkmodulegridsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._networkmodulegridsRepository.RegisterEdit(networkmodulegridsPost);
            try
            {
                this._networkmodulegridsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._networkmodulegridsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(NetworkModuleGridsPost networkmodulegridsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._networkmodulegridsRepository.RegisterDelete(networkmodulegridsPost);
            try
            {
                this._networkmodulegridsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._networkmodulegridsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

