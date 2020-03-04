using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IPlanetarySystemsRepository
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
        void RegisterCreate(PlanetarySystemsPost planetarysystemsPost);
        void RegisterEdit(PlanetarySystemsPost planetarysystemsPost);
        void RegisterDelete(PlanetarySystemsPost planetarysystemsPost);
        void Rollback();
        void Commit();
    }
}
  

