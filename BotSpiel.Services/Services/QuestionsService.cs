using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class QuestionsService : IQuestionsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IQuestionsRepository _questionsRepository;

        public QuestionsService(IQuestionsRepository questionsRepository)
        {
            _questionsRepository = questionsRepository;
        }

        public QuestionsPost GetPost(Int64 ixQuestion) => _questionsRepository.GetPost(ixQuestion);
        public Questions Get(Int64 ixQuestion) => _questionsRepository.Get(ixQuestion);
        public IQueryable<Questions> Index() => _questionsRepository.Index();
        public IQueryable<Questions> IndexDb() => _questionsRepository.IndexDb();
       public IQueryable<Topics> selectTopics() => _questionsRepository.selectTopics();
        public IQueryable<LanguageStyles> selectLanguageStyles() => _questionsRepository.selectLanguageStyles();
        public IQueryable<ResponseTypes> selectResponseTypes() => _questionsRepository.selectResponseTypes();
       public IQueryable<Topics> TopicsDb() => _questionsRepository.TopicsDb();
        public IQueryable<LanguageStyles> LanguageStylesDb() => _questionsRepository.LanguageStylesDb();
        public IQueryable<ResponseTypes> ResponseTypesDb() => _questionsRepository.ResponseTypesDb();
        public List<string> VerifyQuestionDeleteOK(Int64 ixQuestion, string sQuestion) => _questionsRepository.VerifyQuestionDeleteOK(ixQuestion, sQuestion);

        public Task<Int64> Create(QuestionsPost questionsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._questionsRepository.RegisterCreate(questionsPost);
            try
            {
                this._questionsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._questionsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(questionsPost.ixQuestion);

        }
        public Task Edit(QuestionsPost questionsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._questionsRepository.RegisterEdit(questionsPost);
            try
            {
                this._questionsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._questionsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(QuestionsPost questionsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._questionsRepository.RegisterDelete(questionsPost);
            try
            {
                this._questionsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._questionsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

