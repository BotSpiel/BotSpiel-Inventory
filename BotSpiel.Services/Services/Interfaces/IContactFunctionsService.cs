using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IContactFunctionsService
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
        IQueryable<ContactFunctions> IndexDb();
        bool VerifyContactFunctionUnique(Int64 ixContactFunction, string sContactFunction);
        List<string> VerifyContactFunctionDeleteOK(Int64 ixContactFunction, string sContactFunction);

        Task<Int64> Create(ContactFunctionsPost contactfunctionsPost);
        Task Edit(ContactFunctionsPost contactfunctionsPost);
        Task Delete(ContactFunctionsPost contactfunctionsPost);
    }
}
  

