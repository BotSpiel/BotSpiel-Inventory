using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class InboundOrdersRepository : IInboundOrdersRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly InboundOrdersDB _context;
       private readonly InboundOrderLinesDB _contextInboundOrderLines;
        private readonly ReceivingDB _contextReceiving;
  
        public InboundOrdersRepository(InboundOrdersDB context, InboundOrderLinesDB contextInboundOrderLines, ReceivingDB contextReceiving)
        {
            _context = context;
           _contextInboundOrderLines = contextInboundOrderLines;
            _contextReceiving = contextReceiving;
  
        }

        public InboundOrdersPost GetPost(Int64 ixInboundOrder) => _context.InboundOrdersPost.AsNoTracking().Where(x => x.ixInboundOrder == ixInboundOrder).First();
         
		public InboundOrders Get(Int64 ixInboundOrder)
        {
            InboundOrders inboundorders = _context.InboundOrders.AsNoTracking().Where(x => x.ixInboundOrder == ixInboundOrder).First();
            inboundorders.BusinessPartners = _context.BusinessPartners.Find(inboundorders.ixBusinessPartner);
            inboundorders.Companies = _context.Companies.Find(inboundorders.ixCompany);
            inboundorders.Facilities = _context.Facilities.Find(inboundorders.ixFacility);
            inboundorders.InboundOrderTypes = _context.InboundOrderTypes.Find(inboundorders.ixInboundOrderType);
            inboundorders.Statuses = _context.Statuses.Find(inboundorders.ixStatus);

            return inboundorders;
        }

        public IQueryable<InboundOrders> Index()
        {
            var inboundorders = _context.InboundOrders.Include(a => a.InboundOrderTypes).Include(a => a.Facilities).Include(a => a.Companies).Include(a => a.BusinessPartners).Include(a => a.Statuses).AsNoTracking(); 
            return inboundorders;
        }
       public IQueryable<BusinessPartners> selectBusinessPartners()
        {
            List<BusinessPartners> businesspartners = new List<BusinessPartners>();
            _context.BusinessPartners.Include(a => a.Addresses).Include(a => a.BusinessPartnerTypes).Include(a => a.Companies).AsNoTracking()
                .ToList()
                .ForEach(x => businesspartners.Add(x));
            return businesspartners.AsQueryable();
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
        public IQueryable<InboundOrderTypes> selectInboundOrderTypes()
        {
            List<InboundOrderTypes> inboundordertypes = new List<InboundOrderTypes>();
            _context.InboundOrderTypes.AsNoTracking()
                .ToList()
                .ForEach(x => inboundordertypes.Add(x));
            return inboundordertypes.AsQueryable();
        }
        public IQueryable<Statuses> selectStatuses()
        {
            List<Statuses> statuses = new List<Statuses>();
            _context.Statuses.AsNoTracking()
                .ToList()
                .ForEach(x => statuses.Add(x));
            return statuses.AsQueryable();
        }
        public bool VerifyInboundOrderUnique(Int64 ixInboundOrder, string sInboundOrder)
        {
            if (_context.InboundOrders.AsNoTracking().Where(x => x.sInboundOrder == sInboundOrder).Any() && ixInboundOrder == 0L) return false;
            else if (_context.InboundOrders.AsNoTracking().Where(x => x.sInboundOrder == sInboundOrder && x.ixInboundOrder != ixInboundOrder).Any() && ixInboundOrder != 0L) return false;
            else return true;
        }

        public List<string> VerifyInboundOrderDeleteOK(Int64 ixInboundOrder, string sInboundOrder)
        {
            List<string> existInEntities = new List<string>();
           if (_contextInboundOrderLines.InboundOrderLines.AsNoTracking().Where(x => x.ixInboundOrder == ixInboundOrder).Any()) existInEntities.Add("InboundOrderLines");
            if (_contextReceiving.Receiving.AsNoTracking().Where(x => x.ixInboundOrder == ixInboundOrder).Any()) existInEntities.Add("Receiving");

            return existInEntities;
        }


        public void RegisterCreate(InboundOrdersPost inboundordersPost)
		{
            _context.InboundOrdersPost.Add(inboundordersPost); 
        }

        public void RegisterEdit(InboundOrdersPost inboundordersPost)
        {
            _context.Entry(inboundordersPost).State = EntityState.Modified;
        }

        public void RegisterDelete(InboundOrdersPost inboundordersPost)
        {
            _context.InboundOrdersPost.Remove(inboundordersPost);
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
  

