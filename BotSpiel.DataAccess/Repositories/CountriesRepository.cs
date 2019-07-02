using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class CountriesRepository : ICountriesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly CountriesDB _context;
       private readonly AddressesDB _contextAddresses;
        private readonly CountrySubDivisionsDB _contextCountrySubDivisions;
  
        public CountriesRepository(CountriesDB context, AddressesDB contextAddresses, CountrySubDivisionsDB contextCountrySubDivisions)
        {
            _context = context;
           _contextAddresses = contextAddresses;
            _contextCountrySubDivisions = contextCountrySubDivisions;
  
        }

        public CountriesPost GetPost(Int64 ixCountry) => _context.CountriesPost.AsNoTracking().Where(x => x.ixCountry == ixCountry).First();
         
		public Countries Get(Int64 ixCountry)
        {
            Countries countries = _context.Countries.AsNoTracking().Where(x => x.ixCountry == ixCountry).First();
            countries.PlanetSubRegions = _context.PlanetSubRegions.Find(countries.ixPlanetSubRegion);

            return countries;
        }

        public IQueryable<Countries> Index()
        {
            var countries = _context.Countries.Include(a => a.PlanetSubRegions).AsNoTracking(); 
            return countries;
        }
       public IQueryable<PlanetSubRegions> selectPlanetSubRegions()
        {
            List<PlanetSubRegions> planetsubregions = new List<PlanetSubRegions>();
            _context.PlanetSubRegions.Include(a => a.PlanetRegions).AsNoTracking()
                .ToList()
                .ForEach(x => planetsubregions.Add(x));
            return planetsubregions.AsQueryable();
        }
        public bool VerifyCountryUnique(Int64 ixCountry, string sCountry)
        {
            if (_context.Countries.AsNoTracking().Where(x => x.sCountry == sCountry).Any() && ixCountry == 0L) return false;
            else if (_context.Countries.AsNoTracking().Where(x => x.sCountry == sCountry && x.ixCountry != ixCountry).Any() && ixCountry != 0L) return false;
            else return true;
        }

        public List<string> VerifyCountryDeleteOK(Int64 ixCountry, string sCountry)
        {
            List<string> existInEntities = new List<string>();
           if (_contextAddresses.Addresses.AsNoTracking().Where(x => x.ixCountry == ixCountry).Any()) existInEntities.Add("Addresses");
            if (_contextCountrySubDivisions.CountrySubDivisions.AsNoTracking().Where(x => x.ixCountry == ixCountry).Any()) existInEntities.Add("CountrySubDivisions");

            return existInEntities;
        }


        public void RegisterCreate(CountriesPost countriesPost)
		{
            _context.CountriesPost.Add(countriesPost); 
        }

        public void RegisterEdit(CountriesPost countriesPost)
        {
            _context.Entry(countriesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(CountriesPost countriesPost)
        {
            _context.CountriesPost.Remove(countriesPost);
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
  

