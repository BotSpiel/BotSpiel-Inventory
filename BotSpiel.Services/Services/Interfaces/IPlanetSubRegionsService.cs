using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IPlanetSubRegionsService
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

        Task<Int64> Create(PlanetSubRegionsPost planetsubregionsPost);
        Task Edit(PlanetSubRegionsPost planetsubregionsPost);
        Task Delete(PlanetSubRegionsPost planetsubregionsPost);
    }
}
  

