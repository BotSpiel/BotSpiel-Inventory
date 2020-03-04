using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class OutboundShipmentsRepository : IOutboundShipmentsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly OutboundShipmentsDB _context;
       private readonly OutboundOrdersDB _contextOutboundOrders;
  
        public OutboundShipmentsRepository(OutboundShipmentsDB context, OutboundOrdersDB contextOutboundOrders)
        {
            _context = context;
           _contextOutboundOrders = contextOutboundOrders;
  
        }

        public OutboundShipmentsPost GetPost(Int64 ixOutboundShipment) => _context.OutboundShipmentsPost.AsNoTracking().Where(x => x.ixOutboundShipment == ixOutboundShipment).First();
         
		public OutboundShipments Get(Int64 ixOutboundShipment)
        {
            OutboundShipments outboundshipments = _context.OutboundShipments.AsNoTracking().Where(x => x.ixOutboundShipment == ixOutboundShipment).First();
            outboundshipments.Addresses = _context.Addresses.Find(outboundshipments.ixAddress);
            outboundshipments.Carriers = _context.Carriers.Find(outboundshipments.ixCarrier);
            outboundshipments.Companies = _context.Companies.Find(outboundshipments.ixCompany);
            outboundshipments.Facilities = _context.Facilities.Find(outboundshipments.ixFacility);
            if (outboundshipments.ixOutboundCarrierManifest != null)
        {
            outboundshipments.OutboundCarrierManifests = _context.OutboundCarrierManifests.Find(outboundshipments.ixOutboundCarrierManifest);
        }
            outboundshipments.Statuses = _context.Statuses.Find(outboundshipments.ixStatus);

            return outboundshipments;
        }

        public IQueryable<OutboundShipments> Index()
        {
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //var outboundshipments = _context.OutboundShipments.Include(a => a.Carriers).Include(a => a.Statuses).Include(a => a.Addresses).Include(a => a.Facilities).Include(a => a.Companies).AsNoTracking();
            //Replaced Code Block End
            var outboundshipments = _context.OutboundShipments.OrderByDescending(a => a.ixOutboundShipment).Include(a => a.Carriers).Include(a => a.Statuses).Include(a => a.Addresses).Include(a => a.Facilities).Include(a => a.Companies).AsNoTracking();
            //Custom Code End
            return outboundshipments;
        }

        public IQueryable<OutboundShipments> IndexDb()
        {
            var outboundshipments = _context.OutboundShipments.Include(a => a.Carriers).Include(a => a.Statuses).Include(a => a.Addresses).Include(a => a.Facilities).Include(a => a.Companies).AsNoTracking(); 
            return outboundshipments;
        }
       public IQueryable<Addresses> selectAddresses()
        {
            List<Addresses> addresses = new List<Addresses>();
            _context.Addresses.Include(a => a.Countries).Include(a => a.CountrySubDivisionsFKDiffStateOrProvince).AsNoTracking()
                .ToList()
                .ForEach(x => addresses.Add(x));
            return addresses.AsQueryable();
        }
        public IQueryable<Carriers> selectCarriers()
        {
            List<Carriers> carriers = new List<Carriers>();
            _context.Carriers.Include(a => a.CarrierTypes).AsNoTracking()
                .ToList()
                .ForEach(x => carriers.Add(x));
            return carriers.AsQueryable();
        }
        public IQueryable<Companies> selectCompanies()
        {
            List<Companies> companies = new List<Companies>();
            _context.Companies.AsNoTracking()
                .ToList()
                .ForEach(x => companies.Add(x));
            return companies.AsQueryable();
        }
        public IQueryable<Facilities> selectFacilities()
        {
            List<Facilities> facilities = new List<Facilities>();
            _context.Facilities.Include(a => a.Addresses).AsNoTracking()
                .ToList()
                .ForEach(x => facilities.Add(x));
            return facilities.AsQueryable();
        }
        public IQueryable<OutboundCarrierManifests> selectOutboundCarrierManifests()
        {
            List<OutboundCarrierManifests> outboundcarriermanifests = new List<OutboundCarrierManifests>();
            _context.OutboundCarrierManifests.Include(a => a.Carriers).Include(a => a.InventoryLocationsFKDiffPickupInventoryLocation).Include(a => a.Statuses).AsNoTracking()
                .ToList()
                .ForEach(x => outboundcarriermanifests.Add(x));
            return outboundcarriermanifests.AsQueryable();
        }
        public IQueryable<Statuses> selectStatuses()
        {
            List<Statuses> statuses = new List<Statuses>();
            _context.Statuses.AsNoTracking()
                .ToList()
                .ForEach(x => statuses.Add(x));
            return statuses.AsQueryable();
        }
       public IQueryable<Addresses> AddressesDb()
        {
            List<Addresses> addresses = new List<Addresses>();
            _context.Addresses.Include(a => a.Countries).Include(a => a.CountrySubDivisionsFKDiffStateOrProvince).AsNoTracking()
                .ToList()
                .ForEach(x => addresses.Add(x));
            return addresses.AsQueryable();
        }
        public IQueryable<Carriers> CarriersDb()
        {
            List<Carriers> carriers = new List<Carriers>();
            _context.Carriers.Include(a => a.CarrierTypes).AsNoTracking()
                .ToList()
                .ForEach(x => carriers.Add(x));
            return carriers.AsQueryable();
        }
        public IQueryable<Companies> CompaniesDb()
        {
            List<Companies> companies = new List<Companies>();
            _context.Companies.AsNoTracking()
                .ToList()
                .ForEach(x => companies.Add(x));
            return companies.AsQueryable();
        }
        public IQueryable<Facilities> FacilitiesDb()
        {
            List<Facilities> facilities = new List<Facilities>();
            _context.Facilities.Include(a => a.Addresses).AsNoTracking()
                .ToList()
                .ForEach(x => facilities.Add(x));
            return facilities.AsQueryable();
        }
        public IQueryable<OutboundCarrierManifests> OutboundCarrierManifestsDb()
        {
            List<OutboundCarrierManifests> outboundcarriermanifests = new List<OutboundCarrierManifests>();
            _context.OutboundCarrierManifests.Include(a => a.Carriers).Include(a => a.InventoryLocationsFKDiffPickupInventoryLocation).Include(a => a.Statuses).AsNoTracking()
                .ToList()
                .ForEach(x => outboundcarriermanifests.Add(x));
            return outboundcarriermanifests.AsQueryable();
        }
        public IQueryable<Statuses> StatusesDb()
        {
            List<Statuses> statuses = new List<Statuses>();
            _context.Statuses.AsNoTracking()
                .ToList()
                .ForEach(x => statuses.Add(x));
            return statuses.AsQueryable();
        }
       public List<KeyValuePair<Int64?, string>> selectOutboundCarrierManifestsNullable()
        {
            List<KeyValuePair<Int64?, string>> outboundcarriermanifestsNullable = new List<KeyValuePair<Int64?, string>>();
            outboundcarriermanifestsNullable.Add(new KeyValuePair<Int64?, string>(null, "None"));
            _context.OutboundCarrierManifests
                .OrderBy(k => k.sOutboundCarrierManifest)
                .ToList()
                .ForEach(k => outboundcarriermanifestsNullable.Add(new KeyValuePair<Int64?, string>(k.ixOutboundCarrierManifest, k.sOutboundCarrierManifest)));
            return outboundcarriermanifestsNullable;
        }
        public bool VerifyOutboundShipmentUnique(Int64 ixOutboundShipment, string sOutboundShipment)
        {
            if (_context.OutboundShipments.AsNoTracking().Where(x => x.sOutboundShipment == sOutboundShipment).Any() && ixOutboundShipment == 0L) return false;
            else if (_context.OutboundShipments.AsNoTracking().Where(x => x.sOutboundShipment == sOutboundShipment && x.ixOutboundShipment != ixOutboundShipment).Any() && ixOutboundShipment != 0L) return false;
            else return true;
        }

        public List<string> VerifyOutboundShipmentDeleteOK(Int64 ixOutboundShipment, string sOutboundShipment)
        {
            List<string> existInEntities = new List<string>();
           if (_contextOutboundOrders.OutboundOrders.AsNoTracking().Where(x => x.ixOutboundShipment == ixOutboundShipment).Any()) existInEntities.Add("OutboundOrders");

            return existInEntities;
        }


        public void RegisterCreate(OutboundShipmentsPost outboundshipmentsPost)
		{
            _context.OutboundShipmentsPost.Add(outboundshipmentsPost); 
        }

        public void RegisterEdit(OutboundShipmentsPost outboundshipmentsPost)
        {
            _context.Entry(outboundshipmentsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(OutboundShipmentsPost outboundshipmentsPost)
        {
            _context.OutboundShipmentsPost.Remove(outboundshipmentsPost);
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
  

