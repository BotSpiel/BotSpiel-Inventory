using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IQuestionSimilesService
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

        Task<Int64> Create(QuestionSimilesPost questionsimilesPost);
        Task Edit(QuestionSimilesPost questionsimilesPost);
        Task Delete(QuestionSimilesPost questionsimilesPost);
    }
}
  

