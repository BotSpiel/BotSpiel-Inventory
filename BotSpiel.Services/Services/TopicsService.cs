using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class TopicsService : ITopicsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly ITopicsRepository _topicsRepository;

        public TopicsService(ITopicsRepository topicsRepository)
        {
            _topicsRepository = topicsRepository;
        }

        public TopicsPost GetPost(Int64 ixTopic) => _topicsRepository.GetPost(ixTopic);
        public Topics Get(Int64 ixTopic) => _topicsRepository.Get(ixTopic);
        public IQueryable<Topics> Index() => _topicsRepository.Index();
        public IQueryable<Topics> IndexDb() => _topicsRepository.IndexDb();
        public List<string> VerifyTopicDeleteOK(Int64 ixTopic, string sTopic) => _topicsRepository.VerifyTopicDeleteOK(ixTopic, sTopic);

        public Task<Int64> Create(TopicsPost topicsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._topicsRepository.RegisterCreate(topicsPost);
            try
            {
                this._topicsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._topicsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(topicsPost.ixTopic);

        }
        public Task Edit(TopicsPost topicsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._topicsRepository.RegisterEdit(topicsPost);
            try
            {
                this._topicsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._topicsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(TopicsPost topicsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._topicsRepository.RegisterDelete(topicsPost);
            try
            {
                this._topicsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._topicsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

