using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IPeopleRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        PeoplePost GetPost(Int64 ixPerson);        
		People Get(Int64 ixPerson);
        IQueryable<People> Index();
       IQueryable<Languages> selectLanguages();
        bool VerifyPersonUnique(Int64 ixPerson, string sPerson);
        List<string> VerifyPersonDeleteOK(Int64 ixPerson, string sPerson);
        void RegisterCreate(PeoplePost peoplePost);
        void RegisterEdit(PeoplePost peoplePost);
        void RegisterDelete(PeoplePost peoplePost);
        void Rollback();
        void Commit();
    }
}
  

