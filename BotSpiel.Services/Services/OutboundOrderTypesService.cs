using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class OutboundOrderTypesService : IOutboundOrderTypesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IOutboundOrderTypesRepository _outboundordertypesRepository;

        public OutboundOrderTypesService(IOutboundOrderTypesRepository outboundordertypesRepository)
        {
            _outboundordertypesRepository = outboundordertypesRepository;
        }

        public OutboundOrderTypesPost GetPost(Int64 ixOutboundOrderType) => _outboundordertypesRepository.GetPost(ixOutboundOrderType);
        public OutboundOrderTypes Get(Int64 ixOutboundOrderType) => _outboundordertypesRepository.Get(ixOutboundOrderType);
        public IQueryable<OutboundOrderTypes> Index() => _outboundordertypesRepository.Index();
        public bool VerifyOutboundOrderTypeUnique(Int64 ixOutboundOrderType, string sOutboundOrderType) => _outboundordertypesRepository.VerifyOutboundOrderTypeUnique(ixOutboundOrderType, sOutboundOrderType);
        public List<string> VerifyOutboundOrderTypeDeleteOK(Int64 ixOutboundOrderType, string sOutboundOrderType) => _outboundordertypesRepository.VerifyOutboundOrderTypeDeleteOK(ixOutboundOrderType, sOutboundOrderType);

        public Task<Int64> Create(OutboundOrderTypesPost outboundordertypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundordertypesRepository.RegisterCreate(outboundordertypesPost);
            try
            {
                this._outboundordertypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundordertypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(outboundordertypesPost.ixOutboundOrderType);

        }
        public Task Edit(OutboundOrderTypesPost outboundordertypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundordertypesRepository.RegisterEdit(outboundordertypesPost);
            try
            {
                this._outboundordertypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundordertypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(OutboundOrderTypesPost outboundordertypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._outboundordertypesRepository.RegisterDelete(outboundordertypesPost);
            try
            {
                this._outboundordertypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._outboundordertypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

