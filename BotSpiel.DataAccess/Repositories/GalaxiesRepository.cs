using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class GalaxiesRepository : IGalaxiesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly GalaxiesDB _context;
       private readonly PlanetarySystemsDB _contextPlanetarySystems;
  
        public GalaxiesRepository(GalaxiesDB context, PlanetarySystemsDB contextPlanetarySystems)
        {
            _context = context;
           _contextPlanetarySystems = contextPlanetarySystems;
  
        }

        public GalaxiesPost GetPost(Int64 ixGalaxy) => _context.GalaxiesPost.AsNoTracking().Where(x => x.ixGalaxy == ixGalaxy).First();
         
		public Galaxies Get(Int64 ixGalaxy)
        {
            Galaxies galaxies = _context.Galaxies.AsNoTracking().Where(x => x.ixGalaxy == ixGalaxy).First();
            galaxies.Universes = _context.Universes.Find(galaxies.ixUniverse);

            return galaxies;
        }

        public IQueryable<Galaxies> Index()
        {
            var galaxies = _context.Galaxies.Include(a => a.Universes).AsNoTracking(); 
            return galaxies;
        }

        public IQueryable<Galaxies> IndexDb()
        {
            var galaxies = _context.Galaxies.Include(a => a.Universes).AsNoTracking(); 
            return galaxies;
        }
       public IQueryable<Universes> selectUniverses()
        {
            List<Universes> universes = new List<Universes>();
            _context.Universes.AsNoTracking()
                .ToList()
                .ForEach(x => universes.Add(x));
            return universes.AsQueryable();
        }
       public IQueryable<Universes> UniversesDb()
        {
            List<Universes> universes = new List<Universes>();
            _context.Universes.AsNoTracking()
                .ToList()
                .ForEach(x => universes.Add(x));
            return universes.AsQueryable();
        }
        public bool VerifyGalaxyUnique(Int64 ixGalaxy, string sGalaxy)
        {
            if (_context.Galaxies.AsNoTracking().Where(x => x.sGalaxy == sGalaxy).Any() && ixGalaxy == 0L) return false;
            else if (_context.Galaxies.AsNoTracking().Where(x => x.sGalaxy == sGalaxy && x.ixGalaxy != ixGalaxy).Any() && ixGalaxy != 0L) return false;
            else return true;
        }

        public List<string> VerifyGalaxyDeleteOK(Int64 ixGalaxy, string sGalaxy)
        {
            List<string> existInEntities = new List<string>();
           if (_contextPlanetarySystems.PlanetarySystems.AsNoTracking().Where(x => x.ixGalaxy == ixGalaxy).Any()) existInEntities.Add("PlanetarySystems");

            return existInEntities;
        }


        public void RegisterCreate(GalaxiesPost galaxiesPost)
		{
            _context.GalaxiesPost.Add(galaxiesPost); 
        }

        public void RegisterEdit(GalaxiesPost galaxiesPost)
        {
            _context.Entry(galaxiesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(GalaxiesPost galaxiesPost)
        {
            _context.GalaxiesPost.Remove(galaxiesPost);
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
  

