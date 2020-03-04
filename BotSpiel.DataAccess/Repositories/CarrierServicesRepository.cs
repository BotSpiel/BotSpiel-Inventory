using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class CarrierServicesRepository : ICarrierServicesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly CarrierServicesDB _context;
       private readonly OutboundOrdersDB _contextOutboundOrders;
  
        public CarrierServicesRepository(CarrierServicesDB context, OutboundOrdersDB contextOutboundOrders)
        {
            _context = context;
           _contextOutboundOrders = contextOutboundOrders;
  
        }

        public CarrierServicesPost GetPost(Int64 ixCarrierService) => _context.CarrierServicesPost.AsNoTracking().Where(x => x.ixCarrierService == ixCarrierService).First();
         
		public CarrierServices Get(Int64 ixCarrierService)
        {
            CarrierServices carrierservices = _context.CarrierServices.AsNoTracking().Where(x => x.ixCarrierService == ixCarrierService).First();
            carrierservices.Carriers = _context.Carriers.Find(carrierservices.ixCarrier);

            return carrierservices;
        }

        public IQueryable<CarrierServices> Index()
        {
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //var carrierservices = _context.CarrierServices.Include(a => a.Carriers).AsNoTracking();
            //Replaced Code Block End
            var carrierservices = _context.CarrierServices.Where(a => a.ixCarrierService > 0).Include(a => a.Carriers).AsNoTracking();
            //Custom Code End
            return carrierservices;
        }

        public IQueryable<CarrierServices> IndexDb()
        {
            var carrierservices = _context.CarrierServices.Include(a => a.Carriers).AsNoTracking(); 
            return carrierservices;
        }
       public IQueryable<Carriers> selectCarriers()
        {
            List<Carriers> carriers = new List<Carriers>();
            _context.Carriers.Include(a => a.CarrierTypes).AsNoTracking()
                .ToList()
                .ForEach(x => carriers.Add(x));
            return carriers.AsQueryable();
        }
       public IQueryable<Carriers> CarriersDb()
        {
            List<Carriers> carriers = new List<Carriers>();
            _context.Carriers.Include(a => a.CarrierTypes).AsNoTracking()
                .ToList()
                .ForEach(x => carriers.Add(x));
            return carriers.AsQueryable();
        }
        public bool VerifyCarrierServiceUnique(Int64 ixCarrierService, string sCarrierService)
        {
            if (_context.CarrierServices.AsNoTracking().Where(x => x.sCarrierService == sCarrierService).Any() && ixCarrierService == 0L) return false;
            else if (_context.CarrierServices.AsNoTracking().Where(x => x.sCarrierService == sCarrierService && x.ixCarrierService != ixCarrierService).Any() && ixCarrierService != 0L) return false;
            else return true;
        }

        public List<string> VerifyCarrierServiceDeleteOK(Int64 ixCarrierService, string sCarrierService)
        {
            List<string> existInEntities = new List<string>();
           if (_contextOutboundOrders.OutboundOrders.AsNoTracking().Where(x => x.ixCarrierService == ixCarrierService).Any()) existInEntities.Add("OutboundOrders");

            return existInEntities;
        }


        public void RegisterCreate(CarrierServicesPost carrierservicesPost)
		{
            _context.CarrierServicesPost.Add(carrierservicesPost); 
        }

        public void RegisterEdit(CarrierServicesPost carrierservicesPost)
        {
            _context.Entry(carrierservicesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(CarrierServicesPost carrierservicesPost)
        {
            _context.CarrierServicesPost.Remove(carrierservicesPost);
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
  

