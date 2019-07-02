using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class OutboundModuleGridsService : IOutboundModuleGridsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IOutboundModuleGridsRepository _outboundmodulegridsRepository;

        public OutboundModuleGridsService(IOutboundModuleGridsRepository outboundmodulegridsRepository)
        {
            _outboundmodulegridsRepository = outboundmodulegridsRepository;
        }

        public OutboundModuleGridsPost GetPost(Int64 ixOutboundModuleGrid) => _outboundmodulegridsRepository.GetPost(ixOutboundModuleGrid);
        public OutboundModuleGrids Get(Int64 ixOutboundModuleGrid) => _outboundmodulegridsRepository.Get(ixOutboundModuleGrid);
        public IQueryable<OutboundModuleGrids> Index() => _outboundmodulegridsRepository.Index();
		public IQueryable<OutboundModuleGridsconfig> Indexconfig() => _outboundmodulegridsRepository.Indexconfig();
		public IQueryable<OutboundModuleGridsmd> Indexmd() => _outboundmodulegridsRepository.Indexmd();
		public IQueryable<OutboundModuleGridstx> Indextx() => _outboundmodulegridsRepository.Indextx();
		public IQueryable<OutboundModuleGridsanalytics> Indexanalytics() => _outboundmodulegridsRepository.Indexanalytics();
        public List<string> VerifyOutboundModuleGridDeleteOK(Int64 ixOutboundModuleGrid, string sOutboundModuleGrid) => _outboundmodulegridsRepository.VerifyOutboundModuleGridDeleteOK(ixOutboundModuleGrid, sOutboundModuleGrid);

        public Task<Int64> Create(OutboundModuleGridsPost outboundmodulegridsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundmodulegridsRepository.RegisterCreate(outboundmodulegridsPost);
            try
            {
                this._outboundmodulegridsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundmodulegridsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(outboundmodulegridsPost.ixOutboundModuleGrid);

        }
        public Task Edit(OutboundModuleGridsPost outboundmodulegridsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundmodulegridsRepository.RegisterEdit(outboundmodulegridsPost);
            try
            {
                this._outboundmodulegridsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundmodulegridsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(OutboundModuleGridsPost outboundmodulegridsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundmodulegridsRepository.RegisterDelete(outboundmodulegridsPost);
            try
            {
                this._outboundmodulegridsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundmodulegridsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

