using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class BusinessPartnersRepository : IBusinessPartnersRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly BusinessPartnersDB _context;
       private readonly InboundOrdersDB _contextInboundOrders;
        private readonly OutboundOrdersDB _contextOutboundOrders;
  
        public BusinessPartnersRepository(BusinessPartnersDB context, InboundOrdersDB contextInboundOrders, OutboundOrdersDB contextOutboundOrders)
        {
            _context = context;
           _contextInboundOrders = contextInboundOrders;
            _contextOutboundOrders = contextOutboundOrders;
  
        }

        public BusinessPartnersPost GetPost(Int64 ixBusinessPartner) => _context.BusinessPartnersPost.AsNoTracking().Where(x => x.ixBusinessPartner == ixBusinessPartner).First();
         
		public BusinessPartners Get(Int64 ixBusinessPartner)
        {
            BusinessPartners businesspartners = _context.BusinessPartners.AsNoTracking().Where(x => x.ixBusinessPartner == ixBusinessPartner).First();
            businesspartners.Addresses = _context.Addresses.Find(businesspartners.ixAddress);
            businesspartners.BusinessPartnerTypes = _context.BusinessPartnerTypes.Find(businesspartners.ixBusinessPartnerType);
            businesspartners.Companies = _context.Companies.Find(businesspartners.ixCompany);

            return businesspartners;
        }

        public IQueryable<BusinessPartners> Index()
        {
            var businesspartners = _context.BusinessPartners.Include(a => a.BusinessPartnerTypes).Include(a => a.Companies).Include(a => a.Addresses).AsNoTracking(); 
            return businesspartners;
        }

        public IQueryable<BusinessPartners> IndexDb()
        {
            var businesspartners = _context.BusinessPartners.Include(a => a.BusinessPartnerTypes).Include(a => a.Companies).Include(a => a.Addresses).AsNoTracking(); 
            return businesspartners;
        }
       public IQueryable<Addresses> selectAddresses()
        {
            List<Addresses> addresses = new List<Addresses>();
            _context.Addresses.Include(a => a.Countries).Include(a => a.CountrySubDivisionsFKDiffStateOrProvince).AsNoTracking()
                .ToList()
                .ForEach(x => addresses.Add(x));
            return addresses.AsQueryable();
        }
        public IQueryable<BusinessPartnerTypes> selectBusinessPartnerTypes()
        {
            List<BusinessPartnerTypes> businesspartnertypes = new List<BusinessPartnerTypes>();
            _context.BusinessPartnerTypes.AsNoTracking()
                .ToList()
                .ForEach(x => businesspartnertypes.Add(x));
            return businesspartnertypes.AsQueryable();
        }
        public IQueryable<Companies> selectCompanies()
        {
            List<Companies> companies = new List<Companies>();
            _context.Companies.AsNoTracking()
                .ToList()
                .ForEach(x => companies.Add(x));
            return companies.AsQueryable();
        }
       public IQueryable<Addresses> AddressesDb()
        {
            List<Addresses> addresses = new List<Addresses>();
            _context.Addresses.Include(a => a.Countries).Include(a => a.CountrySubDivisionsFKDiffStateOrProvince).AsNoTracking()
                .ToList()
                .ForEach(x => addresses.Add(x));
            return addresses.AsQueryable();
        }
        public IQueryable<BusinessPartnerTypes> BusinessPartnerTypesDb()
        {
            List<BusinessPartnerTypes> businesspartnertypes = new List<BusinessPartnerTypes>();
            _context.BusinessPartnerTypes.AsNoTracking()
                .ToList()
                .ForEach(x => businesspartnertypes.Add(x));
            return businesspartnertypes.AsQueryable();
        }
        public IQueryable<Companies> CompaniesDb()
        {
            List<Companies> companies = new List<Companies>();
            _context.Companies.AsNoTracking()
                .ToList()
                .ForEach(x => companies.Add(x));
            return companies.AsQueryable();
        }
        public bool VerifyBusinessPartnerUnique(Int64 ixBusinessPartner, string sBusinessPartner)
        {
            if (_context.BusinessPartners.AsNoTracking().Where(x => x.sBusinessPartner == sBusinessPartner).Any() && ixBusinessPartner == 0L) return false;
            else if (_context.BusinessPartners.AsNoTracking().Where(x => x.sBusinessPartner == sBusinessPartner && x.ixBusinessPartner != ixBusinessPartner).Any() && ixBusinessPartner != 0L) return false;
            else return true;
        }

        public List<string> VerifyBusinessPartnerDeleteOK(Int64 ixBusinessPartner, string sBusinessPartner)
        {
            List<string> existInEntities = new List<string>();
           if (_contextInboundOrders.InboundOrders.AsNoTracking().Where(x => x.ixBusinessPartner == ixBusinessPartner).Any()) existInEntities.Add("InboundOrders");
            if (_contextOutboundOrders.OutboundOrders.AsNoTracking().Where(x => x.ixBusinessPartner == ixBusinessPartner).Any()) existInEntities.Add("OutboundOrders");

            return existInEntities;
        }


        public void RegisterCreate(BusinessPartnersPost businesspartnersPost)
		{
            _context.BusinessPartnersPost.Add(businesspartnersPost); 
        }

        public void RegisterEdit(BusinessPartnersPost businesspartnersPost)
        {
            _context.Entry(businesspartnersPost).State = EntityState.Modified;
        }

        public void RegisterDelete(BusinessPartnersPost businesspartnersPost)
        {
            _context.BusinessPartnersPost.Remove(businesspartnersPost);
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
  

