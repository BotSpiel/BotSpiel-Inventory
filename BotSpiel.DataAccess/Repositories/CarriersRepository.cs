using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class CarriersRepository : ICarriersRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly CarriersDB _context;
       private readonly CarrierServicesDB _contextCarrierServices;
        private readonly OutboundCarrierManifestsDB _contextOutboundCarrierManifests;
        private readonly OutboundShipmentsDB _contextOutboundShipments;
  
        public CarriersRepository(CarriersDB context, CarrierServicesDB contextCarrierServices, OutboundCarrierManifestsDB contextOutboundCarrierManifests, OutboundShipmentsDB contextOutboundShipments)
        {
            _context = context;
           _contextCarrierServices = contextCarrierServices;
            _contextOutboundCarrierManifests = contextOutboundCarrierManifests;
            _contextOutboundShipments = contextOutboundShipments;
  
        }

        public CarriersPost GetPost(Int64 ixCarrier) => _context.CarriersPost.AsNoTracking().Where(x => x.ixCarrier == ixCarrier).First();
         
		public Carriers Get(Int64 ixCarrier)
        {
            Carriers carriers = _context.Carriers.AsNoTracking().Where(x => x.ixCarrier == ixCarrier).First();
            carriers.CarrierTypes = _context.CarrierTypes.Find(carriers.ixCarrierType);

            return carriers;
        }

        public IQueryable<Carriers> Index()
        {
            var carriers = _context.Carriers.Include(a => a.CarrierTypes).AsNoTracking(); 
            return carriers;
        }
       public IQueryable<CarrierTypes> selectCarrierTypes()
        {
            List<CarrierTypes> carriertypes = new List<CarrierTypes>();
            _context.CarrierTypes.AsNoTracking()
                .ToList()
                .ForEach(x => carriertypes.Add(x));
            return carriertypes.AsQueryable();
        }
        public bool VerifyCarrierUnique(Int64 ixCarrier, string sCarrier)
        {
            if (_context.Carriers.AsNoTracking().Where(x => x.sCarrier == sCarrier).Any() && ixCarrier == 0L) return false;
            else if (_context.Carriers.AsNoTracking().Where(x => x.sCarrier == sCarrier && x.ixCarrier != ixCarrier).Any() && ixCarrier != 0L) return false;
            else return true;
        }

        public List<string> VerifyCarrierDeleteOK(Int64 ixCarrier, string sCarrier)
        {
            List<string> existInEntities = new List<string>();
           if (_contextCarrierServices.CarrierServices.AsNoTracking().Where(x => x.ixCarrier == ixCarrier).Any()) existInEntities.Add("CarrierServices");
            if (_contextOutboundCarrierManifests.OutboundCarrierManifests.AsNoTracking().Where(x => x.ixCarrier == ixCarrier).Any()) existInEntities.Add("OutboundCarrierManifests");
            if (_contextOutboundShipments.OutboundShipments.AsNoTracking().Where(x => x.ixCarrier == ixCarrier).Any()) existInEntities.Add("OutboundShipments");

            return existInEntities;
        }


        public void RegisterCreate(CarriersPost carriersPost)
		{
            _context.CarriersPost.Add(carriersPost); 
        }

        public void RegisterEdit(CarriersPost carriersPost)
        {
            _context.Entry(carriersPost).State = EntityState.Modified;
        }

        public void RegisterDelete(CarriersPost carriersPost)
        {
            _context.CarriersPost.Remove(carriersPost);
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
  

