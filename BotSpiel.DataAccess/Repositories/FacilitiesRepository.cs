using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class FacilitiesRepository : IFacilitiesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly FacilitiesDB _context;
       private readonly InboundOrdersDB _contextInboundOrders;
        private readonly InventoryUnitsDB _contextInventoryUnits;
        private readonly InventoryUnitTransactionsDB _contextInventoryUnitTransactions;
        private readonly OutboundOrdersDB _contextOutboundOrders;
        private readonly OutboundShipmentsDB _contextOutboundShipments;
  
        public FacilitiesRepository(FacilitiesDB context, InboundOrdersDB contextInboundOrders, InventoryUnitsDB contextInventoryUnits, InventoryUnitTransactionsDB contextInventoryUnitTransactions, OutboundOrdersDB contextOutboundOrders, OutboundShipmentsDB contextOutboundShipments)
        {
            _context = context;
           _contextInboundOrders = contextInboundOrders;
            _contextInventoryUnits = contextInventoryUnits;
            _contextInventoryUnitTransactions = contextInventoryUnitTransactions;
            _contextOutboundOrders = contextOutboundOrders;
            _contextOutboundShipments = contextOutboundShipments;
  
        }

        public FacilitiesPost GetPost(Int64 ixFacility) => _context.FacilitiesPost.AsNoTracking().Where(x => x.ixFacility == ixFacility).First();
         
		public Facilities Get(Int64 ixFacility)
        {
            Facilities facilities = _context.Facilities.AsNoTracking().Where(x => x.ixFacility == ixFacility).First();
            facilities.Addresses = _context.Addresses.Find(facilities.ixAddress);

            return facilities;
        }

        public IQueryable<Facilities> Index()
        {
            var facilities = _context.Facilities.Include(a => a.Addresses).AsNoTracking(); 
            return facilities;
        }

        public IQueryable<Facilities> IndexDb()
        {
            var facilities = _context.Facilities.Include(a => a.Addresses).AsNoTracking(); 
            return facilities;
        }
       public IQueryable<Addresses> selectAddresses()
        {
            List<Addresses> addresses = new List<Addresses>();
            _context.Addresses.Include(a => a.Countries).Include(a => a.CountrySubDivisionsFKDiffStateOrProvince).AsNoTracking()
                .ToList()
                .ForEach(x => addresses.Add(x));
            return addresses.AsQueryable();
        }
       public IQueryable<Addresses> AddressesDb()
        {
            List<Addresses> addresses = new List<Addresses>();
            _context.Addresses.Include(a => a.Countries).Include(a => a.CountrySubDivisionsFKDiffStateOrProvince).AsNoTracking()
                .ToList()
                .ForEach(x => addresses.Add(x));
            return addresses.AsQueryable();
        }
        public bool VerifyFacilityUnique(Int64 ixFacility, string sFacility)
        {
            if (_context.Facilities.AsNoTracking().Where(x => x.sFacility == sFacility).Any() && ixFacility == 0L) return false;
            else if (_context.Facilities.AsNoTracking().Where(x => x.sFacility == sFacility && x.ixFacility != ixFacility).Any() && ixFacility != 0L) return false;
            else return true;
        }

        public List<string> VerifyFacilityDeleteOK(Int64 ixFacility, string sFacility)
        {
            List<string> existInEntities = new List<string>();
           if (_contextInboundOrders.InboundOrders.AsNoTracking().Where(x => x.ixFacility == ixFacility).Any()) existInEntities.Add("InboundOrders");
            if (_contextInventoryUnits.InventoryUnits.AsNoTracking().Where(x => x.ixFacility == ixFacility).Any()) existInEntities.Add("InventoryUnits");
            if (_contextInventoryUnitTransactions.InventoryUnitTransactions.AsNoTracking().Where(x => x.ixFacilityAfter == ixFacility).Any()) existInEntities.Add("InventoryUnitTransactions");
            if (_contextInventoryUnitTransactions.InventoryUnitTransactions.AsNoTracking().Where(x => x.ixFacilityBefore == ixFacility).Any()) existInEntities.Add("InventoryUnitTransactions");
            if (_contextOutboundOrders.OutboundOrders.AsNoTracking().Where(x => x.ixFacility == ixFacility).Any()) existInEntities.Add("OutboundOrders");
            if (_contextOutboundShipments.OutboundShipments.AsNoTracking().Where(x => x.ixFacility == ixFacility).Any()) existInEntities.Add("OutboundShipments");

            return existInEntities;
        }


        public void RegisterCreate(FacilitiesPost facilitiesPost)
		{
            _context.FacilitiesPost.Add(facilitiesPost); 
        }

        public void RegisterEdit(FacilitiesPost facilitiesPost)
        {
            _context.Entry(facilitiesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(FacilitiesPost facilitiesPost)
        {
            _context.FacilitiesPost.Remove(facilitiesPost);
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
  

