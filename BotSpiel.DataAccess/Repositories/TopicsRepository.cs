using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class TopicsRepository : ITopicsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly TopicsDB _context;
       private readonly QuestionsDB _contextQuestions;
        private readonly RequestsForInformationDB _contextRequestsForInformation;
  
        public TopicsRepository(TopicsDB context, QuestionsDB contextQuestions, RequestsForInformationDB contextRequestsForInformation)
        {
            _context = context;
           _contextQuestions = contextQuestions;
            _contextRequestsForInformation = contextRequestsForInformation;
  
        }

        public TopicsPost GetPost(Int64 ixTopic) => _context.TopicsPost.AsNoTracking().Where(x => x.ixTopic == ixTopic).First();
         
		public Topics Get(Int64 ixTopic)
        {
            Topics topics = _context.Topics.AsNoTracking().Where(x => x.ixTopic == ixTopic).First();
            return topics;
        }

        public IQueryable<Topics> Index()
        {
            var topics = _context.Topics.AsNoTracking(); 
            return topics;
        }

        public IQueryable<Topics> IndexDb()
        {
            var topics = _context.Topics.AsNoTracking(); 
            return topics;
        }
        public List<string> VerifyTopicDeleteOK(Int64 ixTopic, string sTopic)
        {
            List<string> existInEntities = new List<string>();
           if (_contextQuestions.Questions.AsNoTracking().Where(x => x.ixTopic == ixTopic).Any()) existInEntities.Add("Questions");
            if (_contextRequestsForInformation.RequestsForInformation.AsNoTracking().Where(x => x.ixTopic == ixTopic).Any()) existInEntities.Add("RequestsForInformation");

            return existInEntities;
        }


        public void RegisterCreate(TopicsPost topicsPost)
		{
            _context.TopicsPost.Add(topicsPost); 
        }

        public void RegisterEdit(TopicsPost topicsPost)
        {
            _context.Entry(topicsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(TopicsPost topicsPost)
        {
            _context.TopicsPost.Remove(topicsPost);
        }

        public void Rollback()
        {
            _context.Rollback();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

    }
}
  

