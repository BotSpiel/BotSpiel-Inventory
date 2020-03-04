using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface ILocationFunctionsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        LocationFunctionsPost GetPost(Int64 ixLocationFunction);        
		LocationFunctions Get(Int64 ixLocationFunction);
        IQueryable<LocationFunctions> Index();
        IQueryable<LocationFunctions> IndexDb();
        bool VerifyLocationFunctionUnique(Int64 ixLocationFunction, string sLocationFunction);
        List<string> VerifyLocationFunctionDeleteOK(Int64 ixLocationFunction, string sLocationFunction);

        Task<Int64> Create(LocationFunctionsPost locationfunctionsPost);
        Task Edit(LocationFunctionsPost locationfunctionsPost);
        Task Delete(LocationFunctionsPost locationfunctionsPost);
    }
}
  

