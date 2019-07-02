using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IPlanetsService
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

        Task<Int64> Create(PlanetsPost planetsPost);
        Task Edit(PlanetsPost planetsPost);
        Task Delete(PlanetsPost planetsPost);
    }
}
  

