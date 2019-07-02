using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class InboundModuleGridsService : IInboundModuleGridsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IInboundModuleGridsRepository _inboundmodulegridsRepository;

        public InboundModuleGridsService(IInboundModuleGridsRepository inboundmodulegridsRepository)
        {
            _inboundmodulegridsRepository = inboundmodulegridsRepository;
        }

        public InboundModuleGridsPost GetPost(Int64 ixInboundModuleGrid) => _inboundmodulegridsRepository.GetPost(ixInboundModuleGrid);
        public InboundModuleGrids Get(Int64 ixInboundModuleGrid) => _inboundmodulegridsRepository.Get(ixInboundModuleGrid);
        public IQueryable<InboundModuleGrids> Index() => _inboundmodulegridsRepository.Index();
		public IQueryable<InboundModuleGridsconfig> Indexconfig() => _inboundmodulegridsRepository.Indexconfig();
		public IQueryable<InboundModuleGridsmd> Indexmd() => _inboundmodulegridsRepository.Indexmd();
		public IQueryable<InboundModuleGridstx> Indextx() => _inboundmodulegridsRepository.Indextx();
		public IQueryable<InboundModuleGridsanalytics> Indexanalytics() => _inboundmodulegridsRepository.Indexanalytics();
        public List<string> VerifyInboundModuleGridDeleteOK(Int64 ixInboundModuleGrid, string sInboundModuleGrid) => _inboundmodulegridsRepository.VerifyInboundModuleGridDeleteOK(ixInboundModuleGrid, sInboundModuleGrid);

        public Task<Int64> Create(InboundModuleGridsPost inboundmodulegridsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._inboundmodulegridsRepository.RegisterCreate(inboundmodulegridsPost);
            try
            {
                this._inboundmodulegridsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inboundmodulegridsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(inboundmodulegridsPost.ixInboundModuleGrid);

        }
        public Task Edit(InboundModuleGridsPost inboundmodulegridsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._inboundmodulegridsRepository.RegisterEdit(inboundmodulegridsPost);
            try
            {
                this._inboundmodulegridsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inboundmodulegridsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(InboundModuleGridsPost inboundmodulegridsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._inboundmodulegridsRepository.RegisterDelete(inboundmodulegridsPost);
            try
            {
                this._inboundmodulegridsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inboundmodulegridsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

