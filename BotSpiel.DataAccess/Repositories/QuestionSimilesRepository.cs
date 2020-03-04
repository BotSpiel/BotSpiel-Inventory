using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class QuestionSimilesRepository : IQuestionSimilesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly QuestionSimilesDB _context;
  
        public QuestionSimilesRepository(QuestionSimilesDB context)
        {
            _context = context;
  
        }

        public QuestionSimilesPost GetPost(Int64 ixQuestionSimile) => _context.QuestionSimilesPost.AsNoTracking().Where(x => x.ixQuestionSimile == ixQuestionSimile).First();
         
		public QuestionSimiles Get(Int64 ixQuestionSimile)
        {
            QuestionSimiles questionsimiles = _context.QuestionSimiles.AsNoTracking().Where(x => x.ixQuestionSimile == ixQuestionSimile).First();
            questionsimiles.Questions = _context.Questions.Find(questionsimiles.ixQuestion);

            return questionsimiles;
        }

        public IQueryable<QuestionSimiles> Index()
        {
            var questionsimiles = _context.QuestionSimiles.Include(a => a.Questions).AsNoTracking(); 
            return questionsimiles;
        }

        public IQueryable<QuestionSimiles> IndexDb()
        {
            var questionsimiles = _context.QuestionSimiles.Include(a => a.Questions).AsNoTracking(); 
            return questionsimiles;
        }
       public IQueryable<Questions> selectQuestions()
        {
            List<Questions> questions = new List<Questions>();
            _context.Questions.Include(a => a.LanguageStylesFKDiffLanguage).Include(a => a.LanguageStyles).Include(a => a.ResponseTypes).Include(a => a.Topics).AsNoTracking()
                .ToList()
                .ForEach(x => questions.Add(x));
            return questions.AsQueryable();
        }
       public IQueryable<Questions> QuestionsDb()
        {
            List<Questions> questions = new List<Questions>();
            _context.Questions.Include(a => a.LanguageStylesFKDiffLanguage).Include(a => a.LanguageStyles).Include(a => a.ResponseTypes).Include(a => a.Topics).AsNoTracking()
                .ToList()
                .ForEach(x => questions.Add(x));
            return questions.AsQueryable();
        }
        public List<string> VerifyQuestionSimileDeleteOK(Int64 ixQuestionSimile, string sQuestionSimile)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(QuestionSimilesPost questionsimilesPost)
		{
            _context.QuestionSimilesPost.Add(questionsimilesPost); 
        }

        public void RegisterEdit(QuestionSimilesPost questionsimilesPost)
        {
            _context.Entry(questionsimilesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(QuestionSimilesPost questionsimilesPost)
        {
            _context.QuestionSimilesPost.Remove(questionsimilesPost);
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
  

