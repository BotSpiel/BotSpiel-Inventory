using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class OutboundCarrierManifestsRepository : IOutboundCarrierManifestsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly OutboundCarrierManifestsDB _context;
       private readonly OutboundCarrierManifestPickupsDB _contextOutboundCarrierManifestPickups;
        private readonly OutboundShipmentsDB _contextOutboundShipments;

        //Custom Code Start | Added Code Block 
        private readonly ILocationFunctionsRepository _locationfunctionsRepository;
        //Custom Code End

        //Custom Code Start | Replaced Code Block
        //Replaced Code Block Start
        //public OutboundCarrierManifestsRepository(OutboundCarrierManifestsDB context, OutboundCarrierManifestPickupsDB contextOutboundCarrierManifestPickups, OutboundShipmentsDB contextOutboundShipments)
        //Replaced Code Block End
        public OutboundCarrierManifestsRepository(OutboundCarrierManifestsDB context, OutboundCarrierManifestPickupsDB contextOutboundCarrierManifestPickups, OutboundShipmentsDB contextOutboundShipments, ILocationFunctionsRepository locationfunctionsRepository)
        //Custom Code End
        {
            _context = context;
           _contextOutboundCarrierManifestPickups = contextOutboundCarrierManifestPickups;
            _contextOutboundShipments = contextOutboundShipments;
            //Custom Code Start | Added Code Block 
            _locationfunctionsRepository = locationfunctionsRepository;
            //Custom Code End

        }

        public OutboundCarrierManifestsPost GetPost(Int64 ixOutboundCarrierManifest) => _context.OutboundCarrierManifestsPost.AsNoTracking().Where(x => x.ixOutboundCarrierManifest == ixOutboundCarrierManifest).First();
         
		public OutboundCarrierManifests Get(Int64 ixOutboundCarrierManifest)
        {
            OutboundCarrierManifests outboundcarriermanifests = _context.OutboundCarrierManifests.AsNoTracking().Where(x => x.ixOutboundCarrierManifest == ixOutboundCarrierManifest).First();
            outboundcarriermanifests.Carriers = _context.Carriers.Find(outboundcarriermanifests.ixCarrier);
            outboundcarriermanifests.Facilities = _context.Facilities.Find(outboundcarriermanifests.ixFacility);
            if (outboundcarriermanifests.ixPickupInventoryLocation != null)
        {
            outboundcarriermanifests.InventoryLocationsFKDiffPickupInventoryLocation = _context.InventoryLocations.Find(outboundcarriermanifests.ixPickupInventoryLocation);
        }
            outboundcarriermanifests.Statuses = _context.Statuses.Find(outboundcarriermanifests.ixStatus);

            return outboundcarriermanifests;
        }

        public IQueryable<OutboundCarrierManifests> Index()
        {
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //var outboundcarriermanifests = _context.OutboundCarrierManifests.Include(a => a.Carriers).Include(a => a.Statuses).Include(a => a.Facilities).AsNoTracking();
            //Replaced Code Block End
            var outboundcarriermanifests = _context.OutboundCarrierManifests.OrderByDescending(a => a.ixOutboundCarrierManifest).Include(a => a.Carriers).Include(a => a.Statuses).Include(a => a.Facilities).AsNoTracking();
            //Custom Code End
            return outboundcarriermanifests;
        }

        public IQueryable<OutboundCarrierManifests> IndexDb()
        {
            var outboundcarriermanifests = _context.OutboundCarrierManifests.Include(a => a.Carriers).Include(a => a.Statuses).Include(a => a.Facilities).AsNoTracking(); 
            return outboundcarriermanifests;
        }
       public IQueryable<Carriers> selectCarriers()
        {
            List<Carriers> carriers = new List<Carriers>();
            _context.Carriers.Include(a => a.CarrierTypes).AsNoTracking()
                .ToList()
                .ForEach(x => carriers.Add(x));
            return carriers.AsQueryable();
        }
        public IQueryable<Facilities> selectFacilities()
        {
            List<Facilities> facilities = new List<Facilities>();
            _context.Facilities.Include(a => a.Addresses).AsNoTracking()
                .ToList()
                .ForEach(x => facilities.Add(x));
            return facilities.AsQueryable();
        }
        public IQueryable<InventoryLocations> selectInventoryLocations()
        {
            //Custom Code Start | Added Code Block 
            var allowedLocationFunctions = _locationfunctionsRepository.IndexDb().Where(x =>
                x.sLocationFunctionCode == "CN" ||
                x.sLocationFunctionCode == "SH" ||
                x.sLocationFunctionCode == "TR" ||
                x.sLocationFunctionCode == "ST"
                ).Select(x => x.ixLocationFunction).ToList();
            //Custom Code End

            List<InventoryLocations> inventorylocations = new List<InventoryLocations>();
            _context.InventoryLocations.Include(a => a.Companies).Include(a => a.Facilities).Include(a => a.FacilityAisleFaces).Include(a => a.FacilityFloors).Include(a => a.FacilityWorkAreas).Include(a => a.FacilityZones).Include(a => a.InventoryLocationSizes).Include(a => a.LocationFunctions).Include(a => a.UnitsOfMeasurementFKDiffXOffsetUnit).Include(a => a.UnitsOfMeasurementFKDiffYOffsetUnit).Include(a => a.UnitsOfMeasurementFKDiffZOffsetUnit).AsNoTracking()
                .ToList()
                //Custom Code Start | Replaced Code Block
                //Replaced Code Block Start
                //.ForEach(x => inventorylocations.Add(x));
                //Replaced Code Block End
                .ForEach(x =>
                    {
                        if (allowedLocationFunctions.Contains(x.LocationFunctions.ixLocationFunction))
                        {
                            inventorylocations.Add(x);
                        }
                    }

                    );
                //Custom Code End
            return inventorylocations.AsQueryable();
        }
        public IQueryable<Statuses> selectStatuses()
        {
            List<Statuses> statuses = new List<Statuses>();
            _context.Statuses.AsNoTracking()
                .ToList()
                .ForEach(x => statuses.Add(x));
            return statuses.AsQueryable();
        }
       public IQueryable<Carriers> CarriersDb()
        {
            List<Carriers> carriers = new List<Carriers>();
            _context.Carriers.Include(a => a.CarrierTypes).AsNoTracking()
                .ToList()
                .ForEach(x => carriers.Add(x));
            return carriers.AsQueryable();
        }
        public IQueryable<Facilities> FacilitiesDb()
        {
            List<Facilities> facilities = new List<Facilities>();
            _context.Facilities.Include(a => a.Addresses).AsNoTracking()
                .ToList()
                .ForEach(x => facilities.Add(x));
            return facilities.AsQueryable();
        }
        public IQueryable<InventoryLocations> InventoryLocationsDb()
        {
            List<InventoryLocations> inventorylocations = new List<InventoryLocations>();
            _context.InventoryLocations.Include(a => a.Companies).Include(a => a.Facilities).Include(a => a.FacilityAisleFaces).Include(a => a.FacilityFloors).Include(a => a.FacilityWorkAreas).Include(a => a.FacilityZones).Include(a => a.InventoryLocationSizes).Include(a => a.LocationFunctions).Include(a => a.UnitsOfMeasurementFKDiffXOffsetUnit).Include(a => a.UnitsOfMeasurementFKDiffYOffsetUnit).Include(a => a.UnitsOfMeasurementFKDiffZOffsetUnit).AsNoTracking()
                .ToList()
                .ForEach(x => inventorylocations.Add(x));
            return inventorylocations.AsQueryable();
        }
        public IQueryable<Statuses> StatusesDb()
        {
            List<Statuses> statuses = new List<Statuses>();
            _context.Statuses.AsNoTracking()
                .ToList()
                .ForEach(x => statuses.Add(x));
            return statuses.AsQueryable();
        }
       public List<KeyValuePair<Int64?, string>> selectInventoryLocationsNullable()
        {
            List<KeyValuePair<Int64?, string>> inventorylocationsNullable = new List<KeyValuePair<Int64?, string>>();
            inventorylocationsNullable.Add(new KeyValuePair<Int64?, string>(null, "None"));
            _context.InventoryLocations
                .OrderBy(k => k.sInventoryLocation)
                .ToList()
                .ForEach(k => inventorylocationsNullable.Add(new KeyValuePair<Int64?, string>(k.ixInventoryLocation, k.sInventoryLocation)));
            return inventorylocationsNullable;
        }
        public bool VerifyOutboundCarrierManifestUnique(Int64 ixOutboundCarrierManifest, string sOutboundCarrierManifest)
        {
            if (_context.OutboundCarrierManifests.AsNoTracking().Where(x => x.sOutboundCarrierManifest == sOutboundCarrierManifest).Any() && ixOutboundCarrierManifest == 0L) return false;
            else if (_context.OutboundCarrierManifests.AsNoTracking().Where(x => x.sOutboundCarrierManifest == sOutboundCarrierManifest && x.ixOutboundCarrierManifest != ixOutboundCarrierManifest).Any() && ixOutboundCarrierManifest != 0L) return false;
            else return true;
        }

        public List<string> VerifyOutboundCarrierManifestDeleteOK(Int64 ixOutboundCarrierManifest, string sOutboundCarrierManifest)
        {
            List<string> existInEntities = new List<string>();
           if (_contextOutboundCarrierManifestPickups.OutboundCarrierManifestPickups.AsNoTracking().Where(x => x.ixOutboundCarrierManifest == ixOutboundCarrierManifest).Any()) existInEntities.Add("OutboundCarrierManifestPickups");
            if (_contextOutboundShipments.OutboundShipments.AsNoTracking().Where(x => x.ixOutboundCarrierManifest == ixOutboundCarrierManifest).Any()) existInEntities.Add("OutboundShipments");

            return existInEntities;
        }


        public void RegisterCreate(OutboundCarrierManifestsPost outboundcarriermanifestsPost)
		{
            _context.OutboundCarrierManifestsPost.Add(outboundcarriermanifestsPost); 
        }

        public void RegisterEdit(OutboundCarrierManifestsPost outboundcarriermanifestsPost)
        {
            _context.Entry(outboundcarriermanifestsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(OutboundCarrierManifestsPost outboundcarriermanifestsPost)
        {
            _context.OutboundCarrierManifestsPost.Remove(outboundcarriermanifestsPost);
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
  

