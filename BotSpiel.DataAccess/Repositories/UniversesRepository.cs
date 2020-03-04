using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class UniversesRepository : IUniversesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly UniversesDB _context;
       private readonly GalaxiesDB _contextGalaxies;
  
        public UniversesRepository(UniversesDB context, GalaxiesDB contextGalaxies)
        {
            _context = context;
           _contextGalaxies = contextGalaxies;
  
        }

        public UniversesPost GetPost(Int64 ixUniverse) => _context.UniversesPost.AsNoTracking().Where(x => x.ixUniverse == ixUniverse).First();
         
		public Universes Get(Int64 ixUniverse)
        {
            Universes universes = _context.Universes.AsNoTracking().Where(x => x.ixUniverse == ixUniverse).First();
            return universes;
        }

        public IQueryable<Universes> Index()
        {
            var universes = _context.Universes.AsNoTracking(); 
            return universes;
        }

        public IQueryable<Universes> IndexDb()
        {
            var universes = _context.Universes.AsNoTracking(); 
            return universes;
        }
        public bool VerifyUniverseUnique(Int64 ixUniverse, string sUniverse)
        {
            if (_context.Universes.AsNoTracking().Where(x => x.sUniverse == sUniverse).Any() && ixUniverse == 0L) return false;
            else if (_context.Universes.AsNoTracking().Where(x => x.sUniverse == sUniverse && x.ixUniverse != ixUniverse).Any() && ixUniverse != 0L) return false;
            else return true;
        }

        public List<string> VerifyUniverseDeleteOK(Int64 ixUniverse, string sUniverse)
        {
            List<string> existInEntities = new List<string>();
           if (_contextGalaxies.Galaxies.AsNoTracking().Where(x => x.ixUniverse == ixUniverse).Any()) existInEntities.Add("Galaxies");

            return existInEntities;
        }


        public void RegisterCreate(UniversesPost universesPost)
		{
            _context.UniversesPost.Add(universesPost); 
        }

        public void RegisterEdit(UniversesPost universesPost)
        {
            _context.Entry(universesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(UniversesPost universesPost)
        {
            _context.UniversesPost.Remove(universesPost);
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
  

