using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class InboundOrderTypesService : IInboundOrderTypesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IInboundOrderTypesRepository _inboundordertypesRepository;

        public InboundOrderTypesService(IInboundOrderTypesRepository inboundordertypesRepository)
        {
            _inboundordertypesRepository = inboundordertypesRepository;
        }

        public InboundOrderTypesPost GetPost(Int64 ixInboundOrderType) => _inboundordertypesRepository.GetPost(ixInboundOrderType);
        public InboundOrderTypes Get(Int64 ixInboundOrderType) => _inboundordertypesRepository.Get(ixInboundOrderType);
        public IQueryable<InboundOrderTypes> Index() => _inboundordertypesRepository.Index();
        public IQueryable<InboundOrderTypes> IndexDb() => _inboundordertypesRepository.IndexDb();
        public bool VerifyInboundOrderTypeUnique(Int64 ixInboundOrderType, string sInboundOrderType) => _inboundordertypesRepository.VerifyInboundOrderTypeUnique(ixInboundOrderType, sInboundOrderType);
        public List<string> VerifyInboundOrderTypeDeleteOK(Int64 ixInboundOrderType, string sInboundOrderType) => _inboundordertypesRepository.VerifyInboundOrderTypeDeleteOK(ixInboundOrderType, sInboundOrderType);

        public Task<Int64> Create(InboundOrderTypesPost inboundordertypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._inboundordertypesRepository.RegisterCreate(inboundordertypesPost);
            try
            {
                this._inboundordertypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inboundordertypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(inboundordertypesPost.ixInboundOrderType);

        }
        public Task Edit(InboundOrderTypesPost inboundordertypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._inboundordertypesRepository.RegisterEdit(inboundordertypesPost);
            try
            {
                this._inboundordertypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inboundordertypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(InboundOrderTypesPost inboundordertypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._inboundordertypesRepository.RegisterDelete(inboundordertypesPost);
            try
            {
                this._inboundordertypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inboundordertypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

