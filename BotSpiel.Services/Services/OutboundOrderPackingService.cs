using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class OutboundOrderPackingService : IOutboundOrderPackingService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IOutboundOrderPackingRepository _outboundorderpackingRepository;

        public OutboundOrderPackingService(IOutboundOrderPackingRepository outboundorderpackingRepository)
        {
            _outboundorderpackingRepository = outboundorderpackingRepository;
        }

        public OutboundOrderPackingPost GetPost(Int64 ixOutboundOrderPack) => _outboundorderpackingRepository.GetPost(ixOutboundOrderPack);
        public OutboundOrderPacking Get(Int64 ixOutboundOrderPack) => _outboundorderpackingRepository.Get(ixOutboundOrderPack);
        public IQueryable<OutboundOrderPacking> Index() => _outboundorderpackingRepository.Index();
       public IQueryable<Statuses> selectStatuses() => _outboundorderpackingRepository.selectStatuses();
        public IQueryable<HandlingUnits> selectHandlingUnits() => _outboundorderpackingRepository.selectHandlingUnits();
        public IQueryable<OutboundOrderLines> selectOutboundOrderLines() => _outboundorderpackingRepository.selectOutboundOrderLines();
        public bool VerifyOutboundOrderPackUnique(Int64 ixOutboundOrderPack, string sOutboundOrderPack) => _outboundorderpackingRepository.VerifyOutboundOrderPackUnique(ixOutboundOrderPack, sOutboundOrderPack);
        public List<string> VerifyOutboundOrderPackDeleteOK(Int64 ixOutboundOrderPack, string sOutboundOrderPack) => _outboundorderpackingRepository.VerifyOutboundOrderPackDeleteOK(ixOutboundOrderPack, sOutboundOrderPack);

        public Task<Int64> Create(OutboundOrderPackingPost outboundorderpackingPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundorderpackingRepository.RegisterCreate(outboundorderpackingPost);
            try
            {
                this._outboundorderpackingRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundorderpackingRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(outboundorderpackingPost.ixOutboundOrderPack);

        }
        public Task Edit(OutboundOrderPackingPost outboundorderpackingPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundorderpackingRepository.RegisterEdit(outboundorderpackingPost);
            try
            {
                this._outboundorderpackingRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundorderpackingRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(OutboundOrderPackingPost outboundorderpackingPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundorderpackingRepository.RegisterDelete(outboundorderpackingPost);
            try
            {
                this._outboundorderpackingRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundorderpackingRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

