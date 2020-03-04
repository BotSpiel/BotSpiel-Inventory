using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class QuestionSimilesService : IQuestionSimilesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IQuestionSimilesRepository _questionsimilesRepository;

        public QuestionSimilesService(IQuestionSimilesRepository questionsimilesRepository)
        {
            _questionsimilesRepository = questionsimilesRepository;
        }

        public QuestionSimilesPost GetPost(Int64 ixQuestionSimile) => _questionsimilesRepository.GetPost(ixQuestionSimile);
        public QuestionSimiles Get(Int64 ixQuestionSimile) => _questionsimilesRepository.Get(ixQuestionSimile);
        public IQueryable<QuestionSimiles> Index() => _questionsimilesRepository.Index();
        public IQueryable<QuestionSimiles> IndexDb() => _questionsimilesRepository.IndexDb();
       public IQueryable<Questions> selectQuestions() => _questionsimilesRepository.selectQuestions();
       public IQueryable<Questions> QuestionsDb() => _questionsimilesRepository.QuestionsDb();
        public List<string> VerifyQuestionSimileDeleteOK(Int64 ixQuestionSimile, string sQuestionSimile) => _questionsimilesRepository.VerifyQuestionSimileDeleteOK(ixQuestionSimile, sQuestionSimile);

        public Task<Int64> Create(QuestionSimilesPost questionsimilesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._questionsimilesRepository.RegisterCreate(questionsimilesPost);
            try
            {
                this._questionsimilesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._questionsimilesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(questionsimilesPost.ixQuestionSimile);

        }
        public Task Edit(QuestionSimilesPost questionsimilesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._questionsimilesRepository.RegisterEdit(questionsimilesPost);
            try
            {
                this._questionsimilesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._questionsimilesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(QuestionSimilesPost questionsimilesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._questionsimilesRepository.RegisterDelete(questionsimilesPost);
            try
            {
                this._questionsimilesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._questionsimilesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

