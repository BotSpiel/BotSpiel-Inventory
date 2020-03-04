using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class RequestsForInformationService : IRequestsForInformationService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IRequestsForInformationRepository _requestsforinformationRepository;

        public RequestsForInformationService(IRequestsForInformationRepository requestsforinformationRepository)
        {
            _requestsforinformationRepository = requestsforinformationRepository;
        }

        public RequestsForInformationPost GetPost(Int64 ixRequestForInformation) => _requestsforinformationRepository.GetPost(ixRequestForInformation);
        public RequestsForInformation Get(Int64 ixRequestForInformation) => _requestsforinformationRepository.Get(ixRequestForInformation);
        public IQueryable<RequestsForInformation> Index() => _requestsforinformationRepository.Index();
        public IQueryable<RequestsForInformation> IndexDb() => _requestsforinformationRepository.IndexDb();
       public IQueryable<Languages> selectLanguages() => _requestsforinformationRepository.selectLanguages();
        public IQueryable<Topics> selectTopics() => _requestsforinformationRepository.selectTopics();
        public IQueryable<LanguageStyles> selectLanguageStyles() => _requestsforinformationRepository.selectLanguageStyles();
        public IQueryable<ResponseTypes> selectResponseTypes() => _requestsforinformationRepository.selectResponseTypes();
       public IQueryable<Languages> LanguagesDb() => _requestsforinformationRepository.LanguagesDb();
        public IQueryable<Topics> TopicsDb() => _requestsforinformationRepository.TopicsDb();
        public IQueryable<LanguageStyles> LanguageStylesDb() => _requestsforinformationRepository.LanguageStylesDb();
        public IQueryable<ResponseTypes> ResponseTypesDb() => _requestsforinformationRepository.ResponseTypesDb();
        public List<string> VerifyRequestForInformationDeleteOK(Int64 ixRequestForInformation, string sRequestForInformation) => _requestsforinformationRepository.VerifyRequestForInformationDeleteOK(ixRequestForInformation, sRequestForInformation);

        public Task<Int64> Create(RequestsForInformationPost requestsforinformationPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._requestsforinformationRepository.RegisterCreate(requestsforinformationPost);
            try
            {
                this._requestsforinformationRepository.Commit();
            }
            catch(Exception ex)
            {
                this._requestsforinformationRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(requestsforinformationPost.ixRequestForInformation);

        }
        public Task Edit(RequestsForInformationPost requestsforinformationPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._requestsforinformationRepository.RegisterEdit(requestsforinformationPost);
            try
            {
                this._requestsforinformationRepository.Commit();
            }
            catch(Exception ex)
            {
                this._requestsforinformationRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(RequestsForInformationPost requestsforinformationPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._requestsforinformationRepository.RegisterDelete(requestsforinformationPost);
            try
            {
                this._requestsforinformationRepository.Commit();
            }
            catch(Exception ex)
            {
                this._requestsforinformationRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

