using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class PlanetRegionsRepository : IPlanetRegionsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly PlanetRegionsDB _context;
       private readonly PlanetSubRegionsDB _contextPlanetSubRegions;
  
        public PlanetRegionsRepository(PlanetRegionsDB context, PlanetSubRegionsDB contextPlanetSubRegions)
        {
            _context = context;
           _contextPlanetSubRegions = contextPlanetSubRegions;
  
        }

        public PlanetRegionsPost GetPost(Int64 ixPlanetRegion) => _context.PlanetRegionsPost.AsNoTracking().Where(x => x.ixPlanetRegion == ixPlanetRegion).First();
         
		public PlanetRegions Get(Int64 ixPlanetRegion)
        {
            PlanetRegions planetregions = _context.PlanetRegions.AsNoTracking().Where(x => x.ixPlanetRegion == ixPlanetRegion).First();
            planetregions.Planets = _context.Planets.Find(planetregions.ixPlanet);

            return planetregions;
        }

        public IQueryable<PlanetRegions> Index()
        {
            var planetregions = _context.PlanetRegions.Include(a => a.Planets).AsNoTracking(); 
            return planetregions;
        }

        public IQueryable<PlanetRegions> IndexDb()
        {
            var planetregions = _context.PlanetRegions.Include(a => a.Planets).AsNoTracking(); 
            return planetregions;
        }
       public IQueryable<Planets> selectPlanets()
        {
            List<Planets> planets = new List<Planets>();
            _context.Planets.Include(a => a.PlanetarySystems).AsNoTracking()
                .ToList()
                .ForEach(x => planets.Add(x));
            return planets.AsQueryable();
        }
       public IQueryable<Planets> PlanetsDb()
        {
            List<Planets> planets = new List<Planets>();
            _context.Planets.Include(a => a.PlanetarySystems).AsNoTracking()
                .ToList()
                .ForEach(x => planets.Add(x));
            return planets.AsQueryable();
        }
        public bool VerifyPlanetRegionUnique(Int64 ixPlanetRegion, string sPlanetRegion)
        {
            if (_context.PlanetRegions.AsNoTracking().Where(x => x.sPlanetRegion == sPlanetRegion).Any() && ixPlanetRegion == 0L) return false;
            else if (_context.PlanetRegions.AsNoTracking().Where(x => x.sPlanetRegion == sPlanetRegion && x.ixPlanetRegion != ixPlanetRegion).Any() && ixPlanetRegion != 0L) return false;
            else return true;
        }

        public List<string> VerifyPlanetRegionDeleteOK(Int64 ixPlanetRegion, string sPlanetRegion)
        {
            List<string> existInEntities = new List<string>();
           if (_contextPlanetSubRegions.PlanetSubRegions.AsNoTracking().Where(x => x.ixPlanetRegion == ixPlanetRegion).Any()) existInEntities.Add("PlanetSubRegions");

            return existInEntities;
        }


        public void RegisterCreate(PlanetRegionsPost planetregionsPost)
		{
            _context.PlanetRegionsPost.Add(planetregionsPost); 
        }

        public void RegisterEdit(PlanetRegionsPost planetregionsPost)
        {
            _context.Entry(planetregionsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(PlanetRegionsPost planetregionsPost)
        {
            _context.PlanetRegionsPost.Remove(planetregionsPost);
        }

        public void Rollback()
        {
            _context.Rollback();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

    }
}
  

