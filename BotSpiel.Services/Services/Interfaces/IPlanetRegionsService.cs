using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IPlanetRegionsService
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
        IQueryable<PlanetRegions> IndexDb();
       IQueryable<Planets> selectPlanets();
       IQueryable<Planets> PlanetsDb();
        bool VerifyPlanetRegionUnique(Int64 ixPlanetRegion, string sPlanetRegion);
        List<string> VerifyPlanetRegionDeleteOK(Int64 ixPlanetRegion, string sPlanetRegion);

        Task<Int64> Create(PlanetRegionsPost planetregionsPost);
        Task Edit(PlanetRegionsPost planetregionsPost);
        Task Delete(PlanetRegionsPost planetregionsPost);
    }
}
  

