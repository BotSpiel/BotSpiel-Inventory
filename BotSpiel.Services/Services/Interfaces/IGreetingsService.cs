using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IGreetingsService
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

        Task<Int64> Create(GreetingsPost greetingsPost);
        Task Edit(GreetingsPost greetingsPost);
        Task Delete(GreetingsPost greetingsPost);
    }
}
  

