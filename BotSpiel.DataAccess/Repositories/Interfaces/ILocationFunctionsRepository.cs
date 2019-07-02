using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface ILocationFunctionsRepository
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
        bool VerifyLocationFunctionUnique(Int64 ixLocationFunction, string sLocationFunction);
        List<string> VerifyLocationFunctionDeleteOK(Int64 ixLocationFunction, string sLocationFunction);
        void RegisterCreate(LocationFunctionsPost locationfunctionsPost);
        void RegisterEdit(LocationFunctionsPost locationfunctionsPost);
        void RegisterDelete(LocationFunctionsPost locationfunctionsPost);
        void Rollback();
        void Commit();
    }
}
  

