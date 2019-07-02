using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class CompaniesRepository : ICompaniesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly CompaniesDB _context;
       private readonly BusinessPartnersDB _contextBusinessPartners;
        private readonly InboundOrdersDB _contextInboundOrders;
        private readonly InventoryLocationsDB _contextInventoryLocations;
        private readonly InventoryUnitsDB _contextInventoryUnits;
        private readonly InventoryUnitTransactionsDB _contextInventoryUnitTransactions;
        private readonly OutboundOrdersDB _contextOutboundOrders;
        private readonly OutboundShipmentsDB _contextOutboundShipments;
  
        public CompaniesRepository(CompaniesDB context, BusinessPartnersDB contextBusinessPartners, InboundOrdersDB contextInboundOrders, InventoryLocationsDB contextInventoryLocations, InventoryUnitsDB contextInventoryUnits, InventoryUnitTransactionsDB contextInventoryUnitTransactions, OutboundOrdersDB contextOutboundOrders, OutboundShipmentsDB contextOutboundShipments)
        {
            _context = context;
           _contextBusinessPartners = contextBusinessPartners;
            _contextInboundOrders = contextInboundOrders;
            _contextInventoryLocations = contextInventoryLocations;
            _contextInventoryUnits = contextInventoryUnits;
            _contextInventoryUnitTransactions = contextInventoryUnitTransactions;
            _contextOutboundOrders = contextOutboundOrders;
            _contextOutboundShipments = contextOutboundShipments;
  
        }

        public CompaniesPost GetPost(Int64 ixCompany) => _context.CompaniesPost.AsNoTracking().Where(x => x.ixCompany == ixCompany).First();
         
		public Companies Get(Int64 ixCompany)
        {
            Companies companies = _context.Companies.AsNoTracking().Where(x => x.ixCompany == ixCompany).First();
            return companies;
        }

        public IQueryable<Companies> Index()
        {
            var companies = _context.Companies.AsNoTracking(); 
            return companies;
        }
        public bool VerifyCompanyUnique(Int64 ixCompany, string sCompany)
        {
            if (_context.Companies.AsNoTracking().Where(x => x.sCompany == sCompany).Any() && ixCompany == 0L) return false;
            else if (_context.Companies.AsNoTracking().Where(x => x.sCompany == sCompany && x.ixCompany != ixCompany).Any() && ixCompany != 0L) return false;
            else return true;
        }

        public List<string> VerifyCompanyDeleteOK(Int64 ixCompany, string sCompany)
        {
            List<string> existInEntities = new List<string>();
           if (_contextBusinessPartners.BusinessPartners.AsNoTracking().Where(x => x.ixCompany == ixCompany).Any()) existInEntities.Add("BusinessPartners");
            if (_contextInboundOrders.InboundOrders.AsNoTracking().Where(x => x.ixCompany == ixCompany).Any()) existInEntities.Add("InboundOrders");
            if (_contextInventoryLocations.InventoryLocations.AsNoTracking().Where(x => x.ixCompany == ixCompany).Any()) existInEntities.Add("InventoryLocations");
            if (_contextInventoryUnits.InventoryUnits.AsNoTracking().Where(x => x.ixCompany == ixCompany).Any()) existInEntities.Add("InventoryUnits");
            if (_contextInventoryUnitTransactions.InventoryUnitTransactions.AsNoTracking().Where(x => x.ixCompanyAfter == ixCompany).Any()) existInEntities.Add("InventoryUnitTransactions");
            if (_contextInventoryUnitTransactions.InventoryUnitTransactions.AsNoTracking().Where(x => x.ixCompanyBefore == ixCompany).Any()) existInEntities.Add("InventoryUnitTransactions");
            if (_contextOutboundOrders.OutboundOrders.AsNoTracking().Where(x => x.ixCompany == ixCompany).Any()) existInEntities.Add("OutboundOrders");
            if (_contextOutboundShipments.OutboundShipments.AsNoTracking().Where(x => x.ixCompany == ixCompany).Any()) existInEntities.Add("OutboundShipments");

            return existInEntities;
        }


        public void RegisterCreate(CompaniesPost companiesPost)
		{
            _context.CompaniesPost.Add(companiesPost); 
        }

        public void RegisterEdit(CompaniesPost companiesPost)
        {
            _context.Entry(companiesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(CompaniesPost companiesPost)
        {
            _context.CompaniesPost.Remove(companiesPost);
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
  

