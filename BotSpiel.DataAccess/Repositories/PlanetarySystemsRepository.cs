using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class PlanetarySystemsRepository : IPlanetarySystemsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly PlanetarySystemsDB _context;
       private readonly PlanetsDB _contextPlanets;
  
        public PlanetarySystemsRepository(PlanetarySystemsDB context, PlanetsDB contextPlanets)
        {
            _context = context;
           _contextPlanets = contextPlanets;
  
        }

        public PlanetarySystemsPost GetPost(Int64 ixPlanetarySystem) => _context.PlanetarySystemsPost.AsNoTracking().Where(x => x.ixPlanetarySystem == ixPlanetarySystem).First();
         
		public PlanetarySystems Get(Int64 ixPlanetarySystem)
        {
            PlanetarySystems planetarysystems = _context.PlanetarySystems.AsNoTracking().Where(x => x.ixPlanetarySystem == ixPlanetarySystem).First();
            planetarysystems.Galaxies = _context.Galaxies.Find(planetarysystems.ixGalaxy);

            return planetarysystems;
        }

        public IQueryable<PlanetarySystems> Index()
        {
            var planetarysystems = _context.PlanetarySystems.Include(a => a.Galaxies).AsNoTracking(); 
            return planetarysystems;
        }

        public IQueryable<PlanetarySystems> IndexDb()
        {
            var planetarysystems = _context.PlanetarySystems.Include(a => a.Galaxies).AsNoTracking(); 
            return planetarysystems;
        }
       public IQueryable<Galaxies> selectGalaxies()
        {
            List<Galaxies> galaxies = new List<Galaxies>();
            _context.Galaxies.Include(a => a.Universes).AsNoTracking()
                .ToList()
                .ForEach(x => galaxies.Add(x));
            return galaxies.AsQueryable();
        }
       public IQueryable<Galaxies> GalaxiesDb()
        {
            List<Galaxies> galaxies = new List<Galaxies>();
            _context.Galaxies.Include(a => a.Universes).AsNoTracking()
                .ToList()
                .ForEach(x => galaxies.Add(x));
            return galaxies.AsQueryable();
        }
        public bool VerifyPlanetarySystemUnique(Int64 ixPlanetarySystem, string sPlanetarySystem)
        {
            if (_context.PlanetarySystems.AsNoTracking().Where(x => x.sPlanetarySystem == sPlanetarySystem).Any() && ixPlanetarySystem == 0L) return false;
            else if (_context.PlanetarySystems.AsNoTracking().Where(x => x.sPlanetarySystem == sPlanetarySystem && x.ixPlanetarySystem != ixPlanetarySystem).Any() && ixPlanetarySystem != 0L) return false;
            else return true;
        }

        public List<string> VerifyPlanetarySystemDeleteOK(Int64 ixPlanetarySystem, string sPlanetarySystem)
        {
            List<string> existInEntities = new List<string>();
           if (_contextPlanets.Planets.AsNoTracking().Where(x => x.ixPlanetarySystem == ixPlanetarySystem).Any()) existInEntities.Add("Planets");

            return existInEntities;
        }


        public void RegisterCreate(PlanetarySystemsPost planetarysystemsPost)
		{
            _context.PlanetarySystemsPost.Add(planetarysystemsPost); 
        }

        public void RegisterEdit(PlanetarySystemsPost planetarysystemsPost)
        {
            _context.Entry(planetarysystemsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(PlanetarySystemsPost planetarysystemsPost)
        {
            _context.PlanetarySystemsPost.Remove(planetarysystemsPost);
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
  

