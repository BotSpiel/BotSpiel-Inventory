using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class OutboundOrderLinesService : IOutboundOrderLinesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IOutboundOrderLinesRepository _outboundorderlinesRepository;

        public OutboundOrderLinesService(IOutboundOrderLinesRepository outboundorderlinesRepository)
        {
            _outboundorderlinesRepository = outboundorderlinesRepository;
        }

        public OutboundOrderLinesPost GetPost(Int64 ixOutboundOrderLine) => _outboundorderlinesRepository.GetPost(ixOutboundOrderLine);
        public OutboundOrderLines Get(Int64 ixOutboundOrderLine) => _outboundorderlinesRepository.Get(ixOutboundOrderLine);
        public IQueryable<OutboundOrderLines> Index() => _outboundorderlinesRepository.Index();
       public IQueryable<Statuses> selectStatuses() => _outboundorderlinesRepository.selectStatuses();
        public IQueryable<Materials> selectMaterials() => _outboundorderlinesRepository.selectMaterials();
        public bool VerifyOutboundOrderLineUnique(Int64 ixOutboundOrderLine, string sOutboundOrderLine) => _outboundorderlinesRepository.VerifyOutboundOrderLineUnique(ixOutboundOrderLine, sOutboundOrderLine);
        public List<string> VerifyOutboundOrderLineDeleteOK(Int64 ixOutboundOrderLine, string sOutboundOrderLine) => _outboundorderlinesRepository.VerifyOutboundOrderLineDeleteOK(ixOutboundOrderLine, sOutboundOrderLine);

        public Task<Int64> Create(OutboundOrderLinesPost outboundorderlinesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundorderlinesRepository.RegisterCreate(outboundorderlinesPost);
            try
            {
                this._outboundorderlinesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundorderlinesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(outboundorderlinesPost.ixOutboundOrderLine);

        }
        public Task Edit(OutboundOrderLinesPost outboundorderlinesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundorderlinesRepository.RegisterEdit(outboundorderlinesPost);
            try
            {
                this._outboundorderlinesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundorderlinesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(OutboundOrderLinesPost outboundorderlinesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundorderlinesRepository.RegisterDelete(outboundorderlinesPost);
            try
            {
                this._outboundorderlinesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundorderlinesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

