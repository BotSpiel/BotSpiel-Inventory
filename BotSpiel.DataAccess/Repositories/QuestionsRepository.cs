using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class QuestionsRepository : IQuestionsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly QuestionsDB _context;
       private readonly QuestionSimilesDB _contextQuestionSimiles;
  
        public QuestionsRepository(QuestionsDB context, QuestionSimilesDB contextQuestionSimiles)
        {
            _context = context;
           _contextQuestionSimiles = contextQuestionSimiles;
  
        }

        public QuestionsPost GetPost(Int64 ixQuestion) => _context.QuestionsPost.AsNoTracking().Where(x => x.ixQuestion == ixQuestion).First();
         
		public Questions Get(Int64 ixQuestion)
        {
            Questions questions = _context.Questions.AsNoTracking().Where(x => x.ixQuestion == ixQuestion).First();
            questions.LanguageStylesFKDiffLanguage = _context.LanguageStyles.Find(questions.ixLanguage);
            questions.LanguageStyles = _context.LanguageStyles.Find(questions.ixLanguageStyle);
            questions.ResponseTypes = _context.ResponseTypes.Find(questions.ixResponseType);
            questions.Topics = _context.Topics.Find(questions.ixTopic);

            return questions;
        }

        public IQueryable<Questions> Index()
        {
            var questions = _context.Questions.Include(a => a.LanguageStylesFKDiffLanguage).Include(a => a.LanguageStyles).Include(a => a.Topics).Include(a => a.ResponseTypes).AsNoTracking(); 
            return questions;
        }

        public IQueryable<Questions> IndexDb()
        {
            var questions = _context.Questions.Include(a => a.LanguageStylesFKDiffLanguage).Include(a => a.LanguageStyles).Include(a => a.Topics).Include(a => a.ResponseTypes).AsNoTracking(); 
            return questions;
        }
       public IQueryable<LanguageStyles> selectLanguageStyles()
        {
            List<LanguageStyles> languagestyles = new List<LanguageStyles>();
            _context.LanguageStyles.AsNoTracking()
                .ToList()
                .ForEach(x => languagestyles.Add(x));
            return languagestyles.AsQueryable();
        }
        public IQueryable<ResponseTypes> selectResponseTypes()
        {
            List<ResponseTypes> responsetypes = new List<ResponseTypes>();
            _context.ResponseTypes.AsNoTracking()
                .ToList()
                .ForEach(x => responsetypes.Add(x));
            return responsetypes.AsQueryable();
        }
        public IQueryable<Topics> selectTopics()
        {
            List<Topics> topics = new List<Topics>();
            _context.Topics.AsNoTracking()
                .ToList()
                .ForEach(x => topics.Add(x));
            return topics.AsQueryable();
        }
       public IQueryable<LanguageStyles> LanguageStylesDb()
        {
            List<LanguageStyles> languagestyles = new List<LanguageStyles>();
            _context.LanguageStyles.AsNoTracking()
                .ToList()
                .ForEach(x => languagestyles.Add(x));
            return languagestyles.AsQueryable();
        }
        public IQueryable<ResponseTypes> ResponseTypesDb()
        {
            List<ResponseTypes> responsetypes = new List<ResponseTypes>();
            _context.ResponseTypes.AsNoTracking()
                .ToList()
                .ForEach(x => responsetypes.Add(x));
            return responsetypes.AsQueryable();
        }
        public IQueryable<Topics> TopicsDb()
        {
            List<Topics> topics = new List<Topics>();
            _context.Topics.AsNoTracking()
                .ToList()
                .ForEach(x => topics.Add(x));
            return topics.AsQueryable();
        }
        public List<string> VerifyQuestionDeleteOK(Int64 ixQuestion, string sQuestion)
        {
            List<string> existInEntities = new List<string>();
           if (_contextQuestionSimiles.QuestionSimiles.AsNoTracking().Where(x => x.ixQuestion == ixQuestion).Any()) existInEntities.Add("QuestionSimiles");

            return existInEntities;
        }


        public void RegisterCreate(QuestionsPost questionsPost)
		{
            _context.QuestionsPost.Add(questionsPost); 
        }

        public void RegisterEdit(QuestionsPost questionsPost)
        {
            _context.Entry(questionsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(QuestionsPost questionsPost)
        {
            _context.QuestionsPost.Remove(questionsPost);
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
  

