using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IUniversesService
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
        IQueryable<Universes> IndexDb();
        bool VerifyUniverseUnique(Int64 ixUniverse, string sUniverse);
        List<string> VerifyUniverseDeleteOK(Int64 ixUniverse, string sUniverse);

        Task<Int64> Create(UniversesPost universesPost);
        Task Edit(UniversesPost universesPost);
        Task Delete(UniversesPost universesPost);
    }
}
  

