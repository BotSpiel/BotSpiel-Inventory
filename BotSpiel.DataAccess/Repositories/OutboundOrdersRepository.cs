using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class OutboundOrdersRepository : IOutboundOrdersRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly OutboundOrdersDB _context;
       private readonly OutboundOrderLinesDB _contextOutboundOrderLines;
  
        public OutboundOrdersRepository(OutboundOrdersDB context, OutboundOrderLinesDB contextOutboundOrderLines)
        {
            _context = context;
           _contextOutboundOrderLines = contextOutboundOrderLines;
  
        }

        public OutboundOrdersPost GetPost(Int64 ixOutboundOrder) => _context.OutboundOrdersPost.AsNoTracking().Where(x => x.ixOutboundOrder == ixOutboundOrder).First();
         
		public OutboundOrders Get(Int64 ixOutboundOrder)
        {
            OutboundOrders outboundorders = _context.OutboundOrders.AsNoTracking().Where(x => x.ixOutboundOrder == ixOutboundOrder).First();
            outboundorders.BusinessPartners = _context.BusinessPartners.Find(outboundorders.ixBusinessPartner);
            outboundorders.CarrierServices = _context.CarrierServices.Find(outboundorders.ixCarrierService);
            outboundorders.Companies = _context.Companies.Find(outboundorders.ixCompany);
            outboundorders.Facilities = _context.Facilities.Find(outboundorders.ixFacility);
            outboundorders.OutboundOrderTypes = _context.OutboundOrderTypes.Find(outboundorders.ixOutboundOrderType);
            if (outboundorders.ixOutboundShipment != null)
        {
            outboundorders.OutboundShipments = _context.OutboundShipments.Find(outboundorders.ixOutboundShipment);
        }
            if (outboundorders.ixPickBatch != null)
        {
            outboundorders.PickBatches = _context.PickBatches.Find(outboundorders.ixPickBatch);
        }
            outboundorders.Statuses = _context.Statuses.Find(outboundorders.ixStatus);

            return outboundorders;
        }

        public IQueryable<OutboundOrders> Index()
        {
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //var outboundorders = _context.OutboundOrders.Include(a => a.OutboundOrderTypes).Include(a => a.Facilities).Include(a => a.Companies).Include(a => a.BusinessPartners).Include(a => a.CarrierServices).Include(a => a.Statuses).AsNoTracking();
            //Replaced Code Block End
            var outboundorders = _context.OutboundOrders.OrderByDescending(a => a.ixOutboundOrder).Include(a => a.OutboundOrderTypes).Include(a => a.Facilities).Include(a => a.Companies).Include(a => a.BusinessPartners).Include(a => a.CarrierServices).Include(a => a.Statuses).Include(a => a.OutboundShipments).Include(a => a.PickBatches).AsNoTracking();
            //Custom Code End
            return outboundorders;
        }

        public IQueryable<OutboundOrders> IndexDb()
        {
            var outboundorders = _context.OutboundOrders.Include(a => a.OutboundOrderTypes).Include(a => a.Facilities).Include(a => a.Companies).Include(a => a.BusinessPartners).Include(a => a.CarrierServices).Include(a => a.Statuses).AsNoTracking(); 
            return outboundorders;
        }

        //Custom Code Start | Added Code Block 
        public IQueryable<OutboundOrdersPost> IndexDbPost()
        {
            var outboundorders = _context.OutboundOrdersPost.AsNoTracking();
            return outboundorders;
        }
        //Custom Code End

        public IQueryable<BusinessPartners> selectBusinessPartners()
        {
            List<BusinessPartners> businesspartners = new List<BusinessPartners>();
            _context.BusinessPartners.Include(a => a.Addresses).Include(a => a.BusinessPartnerTypes).Include(a => a.Companies).AsNoTracking()
                .ToList()
                .ForEach(x => businesspartners.Add(x));
            return businesspartners.AsQueryable();
        }
        public IQueryable<CarrierServices> selectCarrierServices()
        {
            List<CarrierServices> carrierservices = new List<CarrierServices>();
            _context.CarrierServices.Include(a => a.Carriers).AsNoTracking()
                .ToList()
                .ForEach(x => carrierservices.Add(x));
            return carrierservices.AsQueryable();
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
        public IQueryable<OutboundOrderTypes> selectOutboundOrderTypes()
        {
            List<OutboundOrderTypes> outboundordertypes = new List<OutboundOrderTypes>();
            _context.OutboundOrderTypes.AsNoTracking()
                .ToList()
                .ForEach(x => outboundordertypes.Add(x));
            return outboundordertypes.AsQueryable();
        }
        public IQueryable<OutboundShipments> selectOutboundShipments()
        {
            List<OutboundShipments> outboundshipments = new List<OutboundShipments>();
            _context.OutboundShipments.Include(a => a.Addresses).Include(a => a.Carriers).Include(a => a.Companies).Include(a => a.Facilities).Include(a => a.OutboundCarrierManifests).Include(a => a.Statuses).AsNoTracking()
                .ToList()
                .ForEach(x => outboundshipments.Add(x));
            return outboundshipments.AsQueryable();
        }
        public IQueryable<PickBatches> selectPickBatches()
        {
            List<PickBatches> pickbatches = new List<PickBatches>();
            _context.PickBatches.Include(a => a.PickBatchTypes).Include(a => a.Statuses).AsNoTracking()
                .ToList()
                .ForEach(x => pickbatches.Add(x));
            return pickbatches.AsQueryable();
        }
        public IQueryable<Statuses> selectStatuses()
        {
            List<Statuses> statuses = new List<Statuses>();
            _context.Statuses.AsNoTracking()
                .ToList()
                .ForEach(x => statuses.Add(x));
            return statuses.AsQueryable();
        }
       public IQueryable<BusinessPartners> BusinessPartnersDb()
        {
            List<BusinessPartners> businesspartners = new List<BusinessPartners>();
            _context.BusinessPartners.Include(a => a.Addresses).Include(a => a.BusinessPartnerTypes).Include(a => a.Companies).AsNoTracking()
                .ToList()
                .ForEach(x => businesspartners.Add(x));
            return businesspartners.AsQueryable();
        }
        public IQueryable<CarrierServices> CarrierServicesDb()
        {
            List<CarrierServices> carrierservices = new List<CarrierServices>();
            _context.CarrierServices.Include(a => a.Carriers).AsNoTracking()
                .ToList()
                .ForEach(x => carrierservices.Add(x));
            return carrierservices.AsQueryable();
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
        public IQueryable<OutboundOrderTypes> OutboundOrderTypesDb()
        {
            List<OutboundOrderTypes> outboundordertypes = new List<OutboundOrderTypes>();
            _context.OutboundOrderTypes.AsNoTracking()
                .ToList()
                .ForEach(x => outboundordertypes.Add(x));
            return outboundordertypes.AsQueryable();
        }
        public IQueryable<OutboundShipments> OutboundShipmentsDb()
        {
            List<OutboundShipments> outboundshipments = new List<OutboundShipments>();
            _context.OutboundShipments.Include(a => a.Addresses).Include(a => a.Carriers).Include(a => a.Companies).Include(a => a.Facilities).Include(a => a.OutboundCarrierManifests).Include(a => a.Statuses).AsNoTracking()
                .ToList()
                .ForEach(x => outboundshipments.Add(x));
            return outboundshipments.AsQueryable();
        }
        public IQueryable<PickBatches> PickBatchesDb()
        {
            List<PickBatches> pickbatches = new List<PickBatches>();
            _context.PickBatches.Include(a => a.PickBatchTypes).Include(a => a.Statuses).AsNoTracking()
                .ToList()
                .ForEach(x => pickbatches.Add(x));
            return pickbatches.AsQueryable();
        }
        public IQueryable<Statuses> StatusesDb()
        {
            List<Statuses> statuses = new List<Statuses>();
            _context.Statuses.AsNoTracking()
                .ToList()
                .ForEach(x => statuses.Add(x));
            return statuses.AsQueryable();
        }
       public List<KeyValuePair<Int64?, string>> selectOutboundShipmentsNullable()
        {
            List<KeyValuePair<Int64?, string>> outboundshipmentsNullable = new List<KeyValuePair<Int64?, string>>();
            outboundshipmentsNullable.Add(new KeyValuePair<Int64?, string>(null, "None"));
            _context.OutboundShipments
                .OrderBy(k => k.sOutboundShipment)
                .ToList()
                .ForEach(k => outboundshipmentsNullable.Add(new KeyValuePair<Int64?, string>(k.ixOutboundShipment, k.sOutboundShipment)));
            return outboundshipmentsNullable;
        }
        public List<KeyValuePair<Int64?, string>> selectPickBatchesNullable()
        {
            List<KeyValuePair<Int64?, string>> pickbatchesNullable = new List<KeyValuePair<Int64?, string>>();
            pickbatchesNullable.Add(new KeyValuePair<Int64?, string>(null, "None"));
            _context.PickBatches
                .OrderBy(k => k.sPickBatch)
                .ToList()
                .ForEach(k => pickbatchesNullable.Add(new KeyValuePair<Int64?, string>(k.ixPickBatch, k.sPickBatch)));
            return pickbatchesNullable;
        }
        public bool VerifyOutboundOrderUnique(Int64 ixOutboundOrder, string sOutboundOrder)
        {
            if (_context.OutboundOrders.AsNoTracking().Where(x => x.sOutboundOrder == sOutboundOrder).Any() && ixOutboundOrder == 0L) return false;
            else if (_context.OutboundOrders.AsNoTracking().Where(x => x.sOutboundOrder == sOutboundOrder && x.ixOutboundOrder != ixOutboundOrder).Any() && ixOutboundOrder != 0L) return false;
            else return true;
        }

        public List<string> VerifyOutboundOrderDeleteOK(Int64 ixOutboundOrder, string sOutboundOrder)
        {
            List<string> existInEntities = new List<string>();
           if (_contextOutboundOrderLines.OutboundOrderLines.AsNoTracking().Where(x => x.ixOutboundOrder == ixOutboundOrder).Any()) existInEntities.Add("OutboundOrderLines");

            return existInEntities;
        }


        public void RegisterCreate(OutboundOrdersPost outboundordersPost)
		{
            _context.OutboundOrdersPost.Add(outboundordersPost); 
        }

        public void RegisterEdit(OutboundOrdersPost outboundordersPost)
        {
            _context.Entry(outboundordersPost).State = EntityState.Modified;
        }

        public void RegisterDelete(OutboundOrdersPost outboundordersPost)
        {
            _context.OutboundOrdersPost.Remove(outboundordersPost);
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
  

