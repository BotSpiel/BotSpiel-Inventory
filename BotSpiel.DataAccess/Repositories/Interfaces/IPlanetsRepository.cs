using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IPlanetsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        PlanetsPost GetPost(Int64 ixPlanet);        
		Planets Get(Int64 ixPlanet);
        IQueryable<Planets> Index();
       IQueryable<PlanetarySystems> selectPlanetarySystems();
        bool VerifyPlanetUnique(Int64 ixPlanet, string sPlanet);
        List<string> VerifyPlanetDeleteOK(Int64 ixPlanet, string sPlanet);
        void RegisterCreate(PlanetsPost planetsPost);
        void RegisterEdit(PlanetsPost planetsPost);
        void RegisterDelete(PlanetsPost planetsPost);
        void Rollback();
        void Commit();
    }
}
  

