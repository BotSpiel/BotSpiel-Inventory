using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IQuestionsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        QuestionsPost GetPost(Int64 ixQuestion);        
		Questions Get(Int64 ixQuestion);
        IQueryable<Questions> Index();
        IQueryable<Questions> IndexDb();
       IQueryable<Topics> selectTopics();
        IQueryable<LanguageStyles> selectLanguageStyles();
        IQueryable<ResponseTypes> selectResponseTypes();
       IQueryable<Topics> TopicsDb();
        IQueryable<LanguageStyles> LanguageStylesDb();
        IQueryable<ResponseTypes> ResponseTypesDb();
        List<string> VerifyQuestionDeleteOK(Int64 ixQuestion, string sQuestion);

        Task<Int64> Create(QuestionsPost questionsPost);
        Task Edit(QuestionsPost questionsPost);
        Task Delete(QuestionsPost questionsPost);
    }
}
  

