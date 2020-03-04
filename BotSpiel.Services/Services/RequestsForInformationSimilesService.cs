using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class RequestsForInformationSimilesService : IRequestsForInformationSimilesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IRequestsForInformationSimilesRepository _requestsforinformationsimilesRepository;

        public RequestsForInformationSimilesService(IRequestsForInformationSimilesRepository requestsforinformationsimilesRepository)
        {
            _requestsforinformationsimilesRepository = requestsforinformationsimilesRepository;
        }

        public RequestsForInformationSimilesPost GetPost(Int64 ixRequestsForInformationSimile) => _requestsforinformationsimilesRepository.GetPost(ixRequestsForInformationSimile);
        public RequestsForInformationSimiles Get(Int64 ixRequestsForInformationSimile) => _requestsforinformationsimilesRepository.Get(ixRequestsForInformationSimile);
        public IQueryable<RequestsForInformationSimiles> Index() => _requestsforinformationsimilesRepository.Index();
        public IQueryable<RequestsForInformationSimiles> IndexDb() => _requestsforinformationsimilesRepository.IndexDb();
       public IQueryable<RequestsForInformation> selectRequestsForInformation() => _requestsforinformationsimilesRepository.selectRequestsForInformation();
       public IQueryable<RequestsForInformation> RequestsForInformationDb() => _requestsforinformationsimilesRepository.RequestsForInformationDb();
        public List<string> VerifyRequestsForInformationSimileDeleteOK(Int64 ixRequestsForInformationSimile, string sRequestsForInformationSimile) => _requestsforinformationsimilesRepository.VerifyRequestsForInformationSimileDeleteOK(ixRequestsForInformationSimile, sRequestsForInformationSimile);

        public Task<Int64> Create(RequestsForInformationSimilesPost requestsforinformationsimilesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._requestsforinformationsimilesRepository.RegisterCreate(requestsforinformationsimilesPost);
            try
            {
                this._requestsforinformationsimilesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._requestsforinformationsimilesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(requestsforinformationsimilesPost.ixRequestsForInformationSimile);

        }
        public Task Edit(RequestsForInformationSimilesPost requestsforinformationsimilesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._requestsforinformationsimilesRepository.RegisterEdit(requestsforinformationsimilesPost);
            try
            {
                this._requestsforinformationsimilesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._requestsforinformationsimilesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(RequestsForInformationSimilesPost requestsforinformationsimilesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._requestsforinformationsimilesRepository.RegisterDelete(requestsforinformationsimilesPost);
            try
            {
                this._requestsforinformationsimilesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._requestsforinformationsimilesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

