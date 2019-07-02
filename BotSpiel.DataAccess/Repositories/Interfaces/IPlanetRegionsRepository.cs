using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IPlanetRegionsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        PlanetRegionsPost GetPost(Int64 ixPlanetRegion);        
		PlanetRegions Get(Int64 ixPlanetRegion);
        IQueryable<PlanetRegions> Index();
       IQueryable<Planets> selectPlanets();
        bool VerifyPlanetRegionUnique(Int64 ixPlanetRegion, string sPlanetRegion);
        List<string> VerifyPlanetRegionDeleteOK(Int64 ixPlanetRegion, string sPlanetRegion);
        void RegisterCreate(PlanetRegionsPost planetregionsPost);
        void RegisterEdit(PlanetRegionsPost planetregionsPost);
        void RegisterDelete(PlanetRegionsPost planetregionsPost);
        void Rollback();
        void Commit();
    }
}
  

