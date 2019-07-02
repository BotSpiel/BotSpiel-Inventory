using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IPlanetSubRegionsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        PlanetSubRegionsPost GetPost(Int64 ixPlanetSubRegion);        
		PlanetSubRegions Get(Int64 ixPlanetSubRegion);
        IQueryable<PlanetSubRegions> Index();
       IQueryable<PlanetRegions> selectPlanetRegions();
        bool VerifyPlanetSubRegionUnique(Int64 ixPlanetSubRegion, string sPlanetSubRegion);
        List<string> VerifyPlanetSubRegionDeleteOK(Int64 ixPlanetSubRegion, string sPlanetSubRegion);
        void RegisterCreate(PlanetSubRegionsPost planetsubregionsPost);
        void RegisterEdit(PlanetSubRegionsPost planetsubregionsPost);
        void RegisterDelete(PlanetSubRegionsPost planetsubregionsPost);
        void Rollback();
        void Commit();
    }
}
  

