using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IPeopleService
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

        Task<Int64> Create(PeoplePost peoplePost);
        Task Edit(PeoplePost peoplePost);
        Task Delete(PeoplePost peoplePost);
    }
}
  

