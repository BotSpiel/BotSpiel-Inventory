using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class PlanetsRepository : IPlanetsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly PlanetsDB _context;
       private readonly PlanetRegionsDB _contextPlanetRegions;
  
        public PlanetsRepository(PlanetsDB context, PlanetRegionsDB contextPlanetRegions)
        {
            _context = context;
           _contextPlanetRegions = contextPlanetRegions;
  
        }

        public PlanetsPost GetPost(Int64 ixPlanet) => _context.PlanetsPost.AsNoTracking().Where(x => x.ixPlanet == ixPlanet).First();
         
		public Planets Get(Int64 ixPlanet)
        {
            Planets planets = _context.Planets.AsNoTracking().Where(x => x.ixPlanet == ixPlanet).First();
            planets.PlanetarySystems = _context.PlanetarySystems.Find(planets.ixPlanetarySystem);

            return planets;
        }

        public IQueryable<Planets> Index()
        {
            var planets = _context.Planets.Include(a => a.PlanetarySystems).AsNoTracking(); 
            return planets;
        }

        public IQueryable<Planets> IndexDb()
        {
            var planets = _context.Planets.Include(a => a.PlanetarySystems).AsNoTracking(); 
            return planets;
        }
       public IQueryable<PlanetarySystems> selectPlanetarySystems()
        {
            List<PlanetarySystems> planetarysystems = new List<PlanetarySystems>();
            _context.PlanetarySystems.Include(a => a.Galaxies).AsNoTracking()
                .ToList()
                .ForEach(x => planetarysystems.Add(x));
            return planetarysystems.AsQueryable();
        }
       public IQueryable<PlanetarySystems> PlanetarySystemsDb()
        {
            List<PlanetarySystems> planetarysystems = new List<PlanetarySystems>();
            _context.PlanetarySystems.Include(a => a.Galaxies).AsNoTracking()
                .ToList()
                .ForEach(x => planetarysystems.Add(x));
            return planetarysystems.AsQueryable();
        }
        public bool VerifyPlanetUnique(Int64 ixPlanet, string sPlanet)
        {
            if (_context.Planets.AsNoTracking().Where(x => x.sPlanet == sPlanet).Any() && ixPlanet == 0L) return false;
            else if (_context.Planets.AsNoTracking().Where(x => x.sPlanet == sPlanet && x.ixPlanet != ixPlanet).Any() && ixPlanet != 0L) return false;
            else return true;
        }

        public List<string> VerifyPlanetDeleteOK(Int64 ixPlanet, string sPlanet)
        {
            List<string> existInEntities = new List<string>();
           if (_contextPlanetRegions.PlanetRegions.AsNoTracking().Where(x => x.ixPlanet == ixPlanet).Any()) existInEntities.Add("PlanetRegions");

            return existInEntities;
        }


        public void RegisterCreate(PlanetsPost planetsPost)
		{
            _context.PlanetsPost.Add(planetsPost); 
        }

        public void RegisterEdit(PlanetsPost planetsPost)
        {
            _context.Entry(planetsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(PlanetsPost planetsPost)
        {
            _context.PlanetsPost.Remove(planetsPost);
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
  

