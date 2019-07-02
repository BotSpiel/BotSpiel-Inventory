using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IContactFunctionsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        ContactFunctionsPost GetPost(Int64 ixContactFunction);        
		ContactFunctions Get(Int64 ixContactFunction);
        IQueryable<ContactFunctions> Index();
        bool VerifyContactFunctionUnique(Int64 ixContactFunction, string sContactFunction);
        List<string> VerifyContactFunctionDeleteOK(Int64 ixContactFunction, string sContactFunction);
        void RegisterCreate(ContactFunctionsPost contactfunctionsPost);
        void RegisterEdit(ContactFunctionsPost contactfunctionsPost);
        void RegisterDelete(ContactFunctionsPost contactfunctionsPost);
        void Rollback();
        void Commit();
    }
}
  

