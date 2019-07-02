using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class CountrySubDivisionsRepository : ICountrySubDivisionsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly CountrySubDivisionsDB _context;
       private readonly AddressesDB _contextAddresses;
        private readonly CountryLocationsDB _contextCountryLocations;
  
        public CountrySubDivisionsRepository(CountrySubDivisionsDB context, AddressesDB contextAddresses, CountryLocationsDB contextCountryLocations)
        {
            _context = context;
           _contextAddresses = contextAddresses;
            _contextCountryLocations = contextCountryLocations;
  
        }

        public CountrySubDivisionsPost GetPost(Int64 ixCountrySubDivision) => _context.CountrySubDivisionsPost.AsNoTracking().Where(x => x.ixCountrySubDivision == ixCountrySubDivision).First();
         
		public CountrySubDivisions Get(Int64 ixCountrySubDivision)
        {
            CountrySubDivisions countrysubdivisions = _context.CountrySubDivisions.AsNoTracking().Where(x => x.ixCountrySubDivision == ixCountrySubDivision).First();
            countrysubdivisions.Countries = _context.Countries.Find(countrysubdivisions.ixCountry);

            return countrysubdivisions;
        }

        public IQueryable<CountrySubDivisions> Index()
        {
            var countrysubdivisions = _context.CountrySubDivisions.Include(a => a.Countries).AsNoTracking(); 
            return countrysubdivisions;
        }
       public IQueryable<Countries> selectCountries()
        {
            List<Countries> countries = new List<Countries>();
            _context.Countries.Include(a => a.PlanetSubRegions).AsNoTracking()
                .ToList()
                .ForEach(x => countries.Add(x));
            return countries.AsQueryable();
        }
        public bool VerifyCountrySubDivisionUnique(Int64 ixCountrySubDivision, string sCountrySubDivision)
        {
            if (_context.CountrySubDivisions.AsNoTracking().Where(x => x.sCountrySubDivision == sCountrySubDivision).Any() && ixCountrySubDivision == 0L) return false;
            else if (_context.CountrySubDivisions.AsNoTracking().Where(x => x.sCountrySubDivision == sCountrySubDivision && x.ixCountrySubDivision != ixCountrySubDivision).Any() && ixCountrySubDivision != 0L) return false;
            else return true;
        }

        public List<string> VerifyCountrySubDivisionDeleteOK(Int64 ixCountrySubDivision, string sCountrySubDivision)
        {
            List<string> existInEntities = new List<string>();
           if (_contextAddresses.Addresses.AsNoTracking().Where(x => x.ixStateOrProvince == ixCountrySubDivision).Any()) existInEntities.Add("Addresses");
            if (_contextCountryLocations.CountryLocations.AsNoTracking().Where(x => x.ixCountrySubDivision == ixCountrySubDivision).Any()) existInEntities.Add("CountryLocations");

            return existInEntities;
        }


        public void RegisterCreate(CountrySubDivisionsPost countrysubdivisionsPost)
		{
            _context.CountrySubDivisionsPost.Add(countrysubdivisionsPost); 
        }

        public void RegisterEdit(CountrySubDivisionsPost countrysubdivisionsPost)
        {
            _context.Entry(countrysubdivisionsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(CountrySubDivisionsPost countrysubdivisionsPost)
        {
            _context.CountrySubDivisionsPost.Remove(countrysubdivisionsPost);
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
  

