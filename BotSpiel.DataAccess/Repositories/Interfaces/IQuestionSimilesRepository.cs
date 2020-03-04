using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IQuestionSimilesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        QuestionSimilesPost GetPost(Int64 ixQuestionSimile);        
		QuestionSimiles Get(Int64 ixQuestionSimile);
        IQueryable<QuestionSimiles> Index();
        IQueryable<QuestionSimiles> IndexDb();
       IQueryable<Questions> selectQuestions();
       IQueryable<Questions> QuestionsDb();
        List<string> VerifyQuestionSimileDeleteOK(Int64 ixQuestionSimile, string sQuestionSimile);
        void RegisterCreate(QuestionSimilesPost questionsimilesPost);
        void RegisterEdit(QuestionSimilesPost questionsimilesPost);
        void RegisterDelete(QuestionSimilesPost questionsimilesPost);
        void Rollback();
        void Commit();
    }
}
  

