using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class OutboundOrderLinePackingService : IOutboundOrderLinePackingService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IOutboundOrderLinePackingRepository _outboundorderlinepackingRepository;

        public OutboundOrderLinePackingService(IOutboundOrderLinePackingRepository outboundorderlinepackingRepository)
        {
            _outboundorderlinepackingRepository = outboundorderlinepackingRepository;
        }

        public OutboundOrderLinePackingPost GetPost(Int64 ixOutboundOrderLinePack) => _outboundorderlinepackingRepository.GetPost(ixOutboundOrderLinePack);
        public OutboundOrderLinePacking Get(Int64 ixOutboundOrderLinePack) => _outboundorderlinepackingRepository.Get(ixOutboundOrderLinePack);
        public IQueryable<OutboundOrderLinePacking> Index() => _outboundorderlinepackingRepository.Index();
        public IQueryable<OutboundOrderLinePacking> IndexDb() => _outboundorderlinepackingRepository.IndexDb();
       public IQueryable<Statuses> selectStatuses() => _outboundorderlinepackingRepository.selectStatuses();
        public IQueryable<HandlingUnits> selectHandlingUnits() => _outboundorderlinepackingRepository.selectHandlingUnits();
        public IQueryable<OutboundOrderLines> selectOutboundOrderLines() => _outboundorderlinepackingRepository.selectOutboundOrderLines();
       public IQueryable<Statuses> StatusesDb() => _outboundorderlinepackingRepository.StatusesDb();
        public IQueryable<HandlingUnits> HandlingUnitsDb() => _outboundorderlinepackingRepository.HandlingUnitsDb();
        public IQueryable<OutboundOrderLines> OutboundOrderLinesDb() => _outboundorderlinepackingRepository.OutboundOrderLinesDb();
        public bool VerifyOutboundOrderLinePackUnique(Int64 ixOutboundOrderLinePack, string sOutboundOrderLinePack) => _outboundorderlinepackingRepository.VerifyOutboundOrderLinePackUnique(ixOutboundOrderLinePack, sOutboundOrderLinePack);
        public List<string> VerifyOutboundOrderLinePackDeleteOK(Int64 ixOutboundOrderLinePack, string sOutboundOrderLinePack) => _outboundorderlinepackingRepository.VerifyOutboundOrderLinePackDeleteOK(ixOutboundOrderLinePack, sOutboundOrderLinePack);

        public Task<Int64> Create(OutboundOrderLinePackingPost outboundorderlinepackingPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundorderlinepackingRepository.RegisterCreate(outboundorderlinepackingPost);
            try
            {
                this._outboundorderlinepackingRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundorderlinepackingRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(outboundorderlinepackingPost.ixOutboundOrderLinePack);

        }
        public Task Edit(OutboundOrderLinePackingPost outboundorderlinepackingPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundorderlinepackingRepository.RegisterEdit(outboundorderlinepackingPost);
            try
            {
                this._outboundorderlinepackingRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundorderlinepackingRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(OutboundOrderLinePackingPost outboundorderlinepackingPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundorderlinepackingRepository.RegisterDelete(outboundorderlinepackingPost);
            try
            {
                this._outboundorderlinepackingRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundorderlinepackingRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

