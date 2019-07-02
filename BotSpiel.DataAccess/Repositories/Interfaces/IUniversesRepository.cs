using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IUniversesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        UniversesPost GetPost(Int64 ixUniverse);        
		Universes Get(Int64 ixUniverse);
        IQueryable<Universes> Index();
        bool VerifyUniverseUnique(Int64 ixUniverse, string sUniverse);
        List<string> VerifyUniverseDeleteOK(Int64 ixUniverse, string sUniverse);
        void RegisterCreate(UniversesPost universesPost);
        void RegisterEdit(UniversesPost universesPost);
        void RegisterDelete(UniversesPost universesPost);
        void Rollback();
        void Commit();
    }
}
  

