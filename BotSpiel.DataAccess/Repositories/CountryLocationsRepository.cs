using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class CountryLocationsRepository : ICountryLocationsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly CountryLocationsDB _context;
  
        public CountryLocationsRepository(CountryLocationsDB context)
        {
            _context = context;
  
        }

        public CountryLocationsPost GetPost(Int64 ixCountryLocation) => _context.CountryLocationsPost.AsNoTracking().Where(x => x.ixCountryLocation == ixCountryLocation).First();
         
		public CountryLocations Get(Int64 ixCountryLocation)
        {
            CountryLocations countrylocations = _context.CountryLocations.AsNoTracking().Where(x => x.ixCountryLocation == ixCountryLocation).First();
            countrylocations.CountrySubDivisions = _context.CountrySubDivisions.Find(countrylocations.ixCountrySubDivision);

            return countrylocations;
        }

        public IQueryable<CountryLocations> Index()
        {
            var countrylocations = _context.CountryLocations.Include(a => a.CountrySubDivisions).AsNoTracking(); 
            return countrylocations;
        }

        public IQueryable<CountryLocations> IndexDb()
        {
            var countrylocations = _context.CountryLocations.Include(a => a.CountrySubDivisions).AsNoTracking(); 
            return countrylocations;
        }
       public IQueryable<CountrySubDivisions> selectCountrySubDivisions()
        {
            List<CountrySubDivisions> countrysubdivisions = new List<CountrySubDivisions>();
            _context.CountrySubDivisions.Include(a => a.Countries).AsNoTracking()
                .ToList()
                .ForEach(x => countrysubdivisions.Add(x));
            return countrysubdivisions.AsQueryable();
        }
       public IQueryable<CountrySubDivisions> CountrySubDivisionsDb()
        {
            List<CountrySubDivisions> countrysubdivisions = new List<CountrySubDivisions>();
            _context.CountrySubDivisions.Include(a => a.Countries).AsNoTracking()
                .ToList()
                .ForEach(x => countrysubdivisions.Add(x));
            return countrysubdivisions.AsQueryable();
        }
        public bool VerifyCountryLocationUnique(Int64 ixCountryLocation, string sCountryLocation)
        {
            if (_context.CountryLocations.AsNoTracking().Where(x => x.sCountryLocation == sCountryLocation).Any() && ixCountryLocation == 0L) return false;
            else if (_context.CountryLocations.AsNoTracking().Where(x => x.sCountryLocation == sCountryLocation && x.ixCountryLocation != ixCountryLocation).Any() && ixCountryLocation != 0L) return false;
            else return true;
        }

        public List<string> VerifyCountryLocationDeleteOK(Int64 ixCountryLocation, string sCountryLocation)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(CountryLocationsPost countrylocationsPost)
		{
            _context.CountryLocationsPost.Add(countrylocationsPost); 
        }

        public void RegisterEdit(CountryLocationsPost countrylocationsPost)
        {
            _context.Entry(countrylocationsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(CountryLocationsPost countrylocationsPost)
        {
            _context.CountryLocationsPost.Remove(countrylocationsPost);
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
  

