using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IQuestionsRepository
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
        void RegisterCreate(QuestionsPost questionsPost);
        void RegisterEdit(QuestionsPost questionsPost);
        void RegisterDelete(QuestionsPost questionsPost);
        void Rollback();
        void Commit();
    }
}
  

