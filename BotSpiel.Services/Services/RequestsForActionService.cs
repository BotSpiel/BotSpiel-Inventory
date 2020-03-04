using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class RequestsForActionService : IRequestsForActionService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IRequestsForActionRepository _requestsforactionRepository;

        public RequestsForActionService(IRequestsForActionRepository requestsforactionRepository)
        {
            _requestsforactionRepository = requestsforactionRepository;
        }

        public RequestsForActionPost GetPost(Int64 ixRequestForAction) => _requestsforactionRepository.GetPost(ixRequestForAction);
        public RequestsForAction Get(Int64 ixRequestForAction) => _requestsforactionRepository.Get(ixRequestForAction);
        public IQueryable<RequestsForAction> Index() => _requestsforactionRepository.Index();
        public IQueryable<RequestsForAction> IndexDb() => _requestsforactionRepository.IndexDb();
       public IQueryable<Languages> selectLanguages() => _requestsforactionRepository.selectLanguages();
        public IQueryable<LanguageStyles> selectLanguageStyles() => _requestsforactionRepository.selectLanguageStyles();
       public IQueryable<Languages> LanguagesDb() => _requestsforactionRepository.LanguagesDb();
        public IQueryable<LanguageStyles> LanguageStylesDb() => _requestsforactionRepository.LanguageStylesDb();
        public List<string> VerifyRequestForActionDeleteOK(Int64 ixRequestForAction, string sRequestForAction) => _requestsforactionRepository.VerifyRequestForActionDeleteOK(ixRequestForAction, sRequestForAction);

        public Task<Int64> Create(RequestsForActionPost requestsforactionPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._requestsforactionRepository.RegisterCreate(requestsforactionPost);
            try
            {
                this._requestsforactionRepository.Commit();
            }
            catch(Exception ex)
            {
                this._requestsforactionRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(requestsforactionPost.ixRequestForAction);

        }
        public Task Edit(RequestsForActionPost requestsforactionPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._requestsforactionRepository.RegisterEdit(requestsforactionPost);
            try
            {
                this._requestsforactionRepository.Commit();
            }
            catch(Exception ex)
            {
                this._requestsforactionRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(RequestsForActionPost requestsforactionPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._requestsforactionRepository.RegisterDelete(requestsforactionPost);
            try
            {
                this._requestsforactionRepository.Commit();
            }
            catch(Exception ex)
            {
                this._requestsforactionRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

