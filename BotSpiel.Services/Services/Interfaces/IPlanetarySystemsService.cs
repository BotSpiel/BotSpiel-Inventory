using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IPlanetarySystemsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        PlanetarySystemsPost GetPost(Int64 ixPlanetarySystem);        
		PlanetarySystems Get(Int64 ixPlanetarySystem);
        IQueryable<PlanetarySystems> Index();
        IQueryable<PlanetarySystems> IndexDb();
       IQueryable<Galaxies> selectGalaxies();
       IQueryable<Galaxies> GalaxiesDb();
        bool VerifyPlanetarySystemUnique(Int64 ixPlanetarySystem, string sPlanetarySystem);
        List<string> VerifyPlanetarySystemDeleteOK(Int64 ixPlanetarySystem, string sPlanetarySystem);

        Task<Int64> Create(PlanetarySystemsPost planetarysystemsPost);
        Task Edit(PlanetarySystemsPost planetarysystemsPost);
        Task Delete(PlanetarySystemsPost planetarysystemsPost);
    }
}
  

