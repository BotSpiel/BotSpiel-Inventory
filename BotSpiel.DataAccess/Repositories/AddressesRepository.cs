using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class AddressesRepository : IAddressesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly AddressesDB _context;
       private readonly BusinessPartnersDB _contextBusinessPartners;
        private readonly FacilitiesDB _contextFacilities;
        private readonly OutboundShipmentsDB _contextOutboundShipments;
  
        public AddressesRepository(AddressesDB context, BusinessPartnersDB contextBusinessPartners, FacilitiesDB contextFacilities, OutboundShipmentsDB contextOutboundShipments)
        {
            _context = context;
           _contextBusinessPartners = contextBusinessPartners;
            _contextFacilities = contextFacilities;
            _contextOutboundShipments = contextOutboundShipments;
  
        }

        public AddressesPost GetPost(Int64 ixAddress) => _context.AddressesPost.AsNoTracking().Where(x => x.ixAddress == ixAddress).First();
         
		public Addresses Get(Int64 ixAddress)
        {
            Addresses addresses = _context.Addresses.AsNoTracking().Where(x => x.ixAddress == ixAddress).First();
            addresses.Countries = _context.Countries.Find(addresses.ixCountry);
            addresses.CountrySubDivisionsFKDiffStateOrProvince = _context.CountrySubDivisions.Find(addresses.ixStateOrProvince);

            return addresses;
        }

        public IQueryable<Addresses> Index()
        {
            var addresses = _context.Addresses.Include(a => a.CountrySubDivisionsFKDiffStateOrProvince).Include(a => a.Countries).AsNoTracking(); 
            return addresses;
        }

        public IQueryable<Addresses> IndexDb()
        {
            var addresses = _context.Addresses.Include(a => a.CountrySubDivisionsFKDiffStateOrProvince).Include(a => a.Countries).AsNoTracking();
            return addresses;
        }

        public IQueryable<Countries> selectCountries()
        {
            List<Countries> countries = new List<Countries>();
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //_context.Countries.Include(a => a.PlanetSubRegions).AsNoTracking()
            //Replaced Code Block End
            _context.Countries.OrderBy(c => c.sCountry).Include(a => a.PlanetSubRegions).AsNoTracking()
                //Custom Code End
                .ToList()
                .ForEach(x => countries.Add(x));
            return countries.AsQueryable();
        }
        public IQueryable<CountrySubDivisions> selectCountrySubDivisions()
        {
            List<CountrySubDivisions> countrysubdivisions = new List<CountrySubDivisions>();
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //_context.CountrySubDivisions.Include(a => a.Countries).AsNoTracking()
            //Replaced Code Block End
            _context.CountrySubDivisions.OrderBy(cs => cs.sCountrySubDivisionCode).Include(a => a.Countries).AsNoTracking()
            //Custom Code End

                .ToList()
                .ForEach(x => countrysubdivisions.Add(x));
            return countrysubdivisions.AsQueryable();
        }
       public IQueryable<Countries> CountriesDb()
        {
            List<Countries> countries = new List<Countries>();
            _context.Countries.Include(a => a.PlanetSubRegions).AsNoTracking()
                .ToList()
                .ForEach(x => countries.Add(x));
            return countries.AsQueryable();
        }
        public IQueryable<CountrySubDivisions> CountrySubDivisionsDb()
        {
            List<CountrySubDivisions> countrysubdivisions = new List<CountrySubDivisions>();
            _context.CountrySubDivisions.Include(a => a.Countries).AsNoTracking()
                .ToList()
                .ForEach(x => countrysubdivisions.Add(x));
            return countrysubdivisions.AsQueryable();
        }
        public bool VerifyAddressUnique(Int64 ixAddress, string sAddress)
        {
            if (_context.Addresses.AsNoTracking().Where(x => x.sAddress == sAddress).Any() && ixAddress == 0L) return false;
            else if (_context.Addresses.AsNoTracking().Where(x => x.sAddress == sAddress && x.ixAddress != ixAddress).Any() && ixAddress != 0L) return false;
            else return true;
        }

        public List<string> VerifyAddressDeleteOK(Int64 ixAddress, string sAddress)
        {
            List<string> existInEntities = new List<string>();
           if (_contextBusinessPartners.BusinessPartners.AsNoTracking().Where(x => x.ixAddress == ixAddress).Any()) existInEntities.Add("BusinessPartners");
            if (_contextFacilities.Facilities.AsNoTracking().Where(x => x.ixAddress == ixAddress).Any()) existInEntities.Add("Facilities");
            if (_contextOutboundShipments.OutboundShipments.AsNoTracking().Where(x => x.ixAddress == ixAddress).Any()) existInEntities.Add("OutboundShipments");

            return existInEntities;
        }


        public void RegisterCreate(AddressesPost addressesPost)
		{
            _context.AddressesPost.Add(addressesPost); 
        }

        public void RegisterEdit(AddressesPost addressesPost)
        {
            _context.Entry(addressesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(AddressesPost addressesPost)
        {
            _context.AddressesPost.Remove(addressesPost);
        }

        public void Rollback()
        {
            _context.Rollback();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
        //Custom Code Start | Added Code Block 
        public List<KeyValuePair<Int64?, string>> selectEmptyCountrySubDivisionsDropdown()
        {
            List<KeyValuePair<Int64?, string>> emptyDropdown = new List<KeyValuePair<Int64?, string>>();
            emptyDropdown.Add(new KeyValuePair<Int64?, string>(null, "Select Country First"));
            return emptyDropdown;
        }
        //Custom Code End

    }
}
  

