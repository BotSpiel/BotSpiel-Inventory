using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class PlanetSubRegionsRepository : IPlanetSubRegionsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly PlanetSubRegionsDB _context;
       private readonly CountriesDB _contextCountries;
  
        public PlanetSubRegionsRepository(PlanetSubRegionsDB context, CountriesDB contextCountries)
        {
            _context = context;
           _contextCountries = contextCountries;
  
        }

        public PlanetSubRegionsPost GetPost(Int64 ixPlanetSubRegion) => _context.PlanetSubRegionsPost.AsNoTracking().Where(x => x.ixPlanetSubRegion == ixPlanetSubRegion).First();
         
		public PlanetSubRegions Get(Int64 ixPlanetSubRegion)
        {
            PlanetSubRegions planetsubregions = _context.PlanetSubRegions.AsNoTracking().Where(x => x.ixPlanetSubRegion == ixPlanetSubRegion).First();
            planetsubregions.PlanetRegions = _context.PlanetRegions.Find(planetsubregions.ixPlanetRegion);

            return planetsubregions;
        }

        public IQueryable<PlanetSubRegions> Index()
        {
            var planetsubregions = _context.PlanetSubRegions.Include(a => a.PlanetRegions).AsNoTracking(); 
            return planetsubregions;
        }

        public IQueryable<PlanetSubRegions> IndexDb()
        {
            var planetsubregions = _context.PlanetSubRegions.Include(a => a.PlanetRegions).AsNoTracking(); 
            return planetsubregions;
        }
       public IQueryable<PlanetRegions> selectPlanetRegions()
        {
            List<PlanetRegions> planetregions = new List<PlanetRegions>();
            _context.PlanetRegions.Include(a => a.Planets).AsNoTracking()
                .ToList()
                .ForEach(x => planetregions.Add(x));
            return planetregions.AsQueryable();
        }
       public IQueryable<PlanetRegions> PlanetRegionsDb()
        {
            List<PlanetRegions> planetregions = new List<PlanetRegions>();
            _context.PlanetRegions.Include(a => a.Planets).AsNoTracking()
                .ToList()
                .ForEach(x => planetregions.Add(x));
            return planetregions.AsQueryable();
        }
        public bool VerifyPlanetSubRegionUnique(Int64 ixPlanetSubRegion, string sPlanetSubRegion)
        {
            if (_context.PlanetSubRegions.AsNoTracking().Where(x => x.sPlanetSubRegion == sPlanetSubRegion).Any() && ixPlanetSubRegion == 0L) return false;
            else if (_context.PlanetSubRegions.AsNoTracking().Where(x => x.sPlanetSubRegion == sPlanetSubRegion && x.ixPlanetSubRegion != ixPlanetSubRegion).Any() && ixPlanetSubRegion != 0L) return false;
            else return true;
        }

        public List<string> VerifyPlanetSubRegionDeleteOK(Int64 ixPlanetSubRegion, string sPlanetSubRegion)
        {
            List<string> existInEntities = new List<string>();
           if (_contextCountries.Countries.AsNoTracking().Where(x => x.ixPlanetSubRegion == ixPlanetSubRegion).Any()) existInEntities.Add("Countries");

            return existInEntities;
        }


        public void RegisterCreate(PlanetSubRegionsPost planetsubregionsPost)
		{
            _context.PlanetSubRegionsPost.Add(planetsubregionsPost); 
        }

        public void RegisterEdit(PlanetSubRegionsPost planetsubregionsPost)
        {
            _context.Entry(planetsubregionsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(PlanetSubRegionsPost planetsubregionsPost)
        {
            _context.PlanetSubRegionsPost.Remove(planetsubregionsPost);
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
  

