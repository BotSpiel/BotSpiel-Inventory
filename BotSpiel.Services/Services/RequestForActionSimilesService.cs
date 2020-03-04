using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class RequestForActionSimilesService : IRequestForActionSimilesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IRequestForActionSimilesRepository _requestforactionsimilesRepository;

        public RequestForActionSimilesService(IRequestForActionSimilesRepository requestforactionsimilesRepository)
        {
            _requestforactionsimilesRepository = requestforactionsimilesRepository;
        }

        public RequestForActionSimilesPost GetPost(Int64 ixRequestForActionSimile) => _requestforactionsimilesRepository.GetPost(ixRequestForActionSimile);
        public RequestForActionSimiles Get(Int64 ixRequestForActionSimile) => _requestforactionsimilesRepository.Get(ixRequestForActionSimile);
        public IQueryable<RequestForActionSimiles> Index() => _requestforactionsimilesRepository.Index();
        public IQueryable<RequestForActionSimiles> IndexDb() => _requestforactionsimilesRepository.IndexDb();
       public IQueryable<RequestsForAction> selectRequestsForAction() => _requestforactionsimilesRepository.selectRequestsForAction();
       public IQueryable<RequestsForAction> RequestsForActionDb() => _requestforactionsimilesRepository.RequestsForActionDb();
        public List<string> VerifyRequestForActionSimileDeleteOK(Int64 ixRequestForActionSimile, string sRequestForActionSimile) => _requestforactionsimilesRepository.VerifyRequestForActionSimileDeleteOK(ixRequestForActionSimile, sRequestForActionSimile);

        public Task<Int64> Create(RequestForActionSimilesPost requestforactionsimilesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._requestforactionsimilesRepository.RegisterCreate(requestforactionsimilesPost);
            try
            {
                this._requestforactionsimilesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._requestforactionsimilesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(requestforactionsimilesPost.ixRequestForActionSimile);

        }
        public Task Edit(RequestForActionSimilesPost requestforactionsimilesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._requestforactionsimilesRepository.RegisterEdit(requestforactionsimilesPost);
            try
            {
                this._requestforactionsimilesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._requestforactionsimilesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(RequestForActionSimilesPost requestforactionsimilesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._requestforactionsimilesRepository.RegisterDelete(requestforactionsimilesPost);
            try
            {
                this._requestforactionsimilesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._requestforactionsimilesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

