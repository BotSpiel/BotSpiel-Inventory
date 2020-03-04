using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IGreetingsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        GreetingsPost GetPost(Int64 ixGreeting);        
		Greetings Get(Int64 ixGreeting);
        IQueryable<Greetings> Index();
        IQueryable<Greetings> IndexDb();
       IQueryable<Languages> selectLanguages();
        IQueryable<LanguageStyles> selectLanguageStyles();
        IQueryable<ResponseTypes> selectResponseTypes();
       IQueryable<Languages> LanguagesDb();
        IQueryable<LanguageStyles> LanguageStylesDb();
        IQueryable<ResponseTypes> ResponseTypesDb();
        List<string> VerifyGreetingDeleteOK(Int64 ixGreeting, string sGreeting);
        void RegisterCreate(GreetingsPost greetingsPost);
        void RegisterEdit(GreetingsPost greetingsPost);
        void RegisterDelete(GreetingsPost greetingsPost);
        void Rollback();
        void Commit();
    }
}
  

