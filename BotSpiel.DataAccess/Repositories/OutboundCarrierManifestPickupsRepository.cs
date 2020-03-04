using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class OutboundCarrierManifestPickupsRepository : IOutboundCarrierManifestPickupsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly OutboundCarrierManifestPickupsDB _context;
  
        public OutboundCarrierManifestPickupsRepository(OutboundCarrierManifestPickupsDB context)
        {
            _context = context;
  
        }

        public OutboundCarrierManifestPickupsPost GetPost(Int64 ixOutboundCarrierManifestPickup) => _context.OutboundCarrierManifestPickupsPost.AsNoTracking().Where(x => x.ixOutboundCarrierManifestPickup == ixOutboundCarrierManifestPickup).First();
         
		public OutboundCarrierManifestPickups Get(Int64 ixOutboundCarrierManifestPickup)
        {
            OutboundCarrierManifestPickups outboundcarriermanifestpickups = _context.OutboundCarrierManifestPickups.AsNoTracking().Where(x => x.ixOutboundCarrierManifestPickup == ixOutboundCarrierManifestPickup).First();
            outboundcarriermanifestpickups.OutboundCarrierManifests = _context.OutboundCarrierManifests.Find(outboundcarriermanifestpickups.ixOutboundCarrierManifest);
            outboundcarriermanifestpickups.Statuses = _context.Statuses.Find(outboundcarriermanifestpickups.ixStatus);

            return outboundcarriermanifestpickups;
        }

        public IQueryable<OutboundCarrierManifestPickups> Index()
        {
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //var outboundcarriermanifestpickups = _context.OutboundCarrierManifestPickups.Include(a => a.OutboundCarrierManifests).Include(a => a.Statuses).AsNoTracking();
            //Replaced Code Block End
            var outboundcarriermanifestpickups = _context.OutboundCarrierManifestPickups.OrderByDescending(a => a.ixOutboundCarrierManifestPickup).Include(a => a.OutboundCarrierManifests).Include(a => a.Statuses).AsNoTracking();
            //Custom Code End
            return outboundcarriermanifestpickups;
        }

        public IQueryable<OutboundCarrierManifestPickups> IndexDb()
        {
            var outboundcarriermanifestpickups = _context.OutboundCarrierManifestPickups.Include(a => a.OutboundCarrierManifests).Include(a => a.Statuses).AsNoTracking(); 
            return outboundcarriermanifestpickups;
        }
       public IQueryable<OutboundCarrierManifests> selectOutboundCarrierManifests()
        {
            List<OutboundCarrierManifests> outboundcarriermanifests = new List<OutboundCarrierManifests>();
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //_context.OutboundCarrierManifests.Include(a => a.Carriers).Include(a => a.InventoryLocationsFKDiffPickupInventoryLocation).Include(a => a.Statuses).AsNoTracking()
            //Replaced Code Block End
            _context.OutboundCarrierManifests.Where(a => a.ixStatus == StatusesDb().Where(s => s.sStatus == "Active").Select(s => s.ixStatus).FirstOrDefault()).Include(a => a.Carriers).Include(a => a.InventoryLocationsFKDiffPickupInventoryLocation).Include(a => a.Statuses).AsNoTracking()
            //Custom Code End
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
        public bool VerifyOutboundCarrierManifestPickupUnique(Int64 ixOutboundCarrierManifestPickup, string sOutboundCarrierManifestPickup)
        {
            if (_context.OutboundCarrierManifestPickups.AsNoTracking().Where(x => x.sOutboundCarrierManifestPickup == sOutboundCarrierManifestPickup).Any() && ixOutboundCarrierManifestPickup == 0L) return false;
            else if (_context.OutboundCarrierManifestPickups.AsNoTracking().Where(x => x.sOutboundCarrierManifestPickup == sOutboundCarrierManifestPickup && x.ixOutboundCarrierManifestPickup != ixOutboundCarrierManifestPickup).Any() && ixOutboundCarrierManifestPickup != 0L) return false;
            else return true;
        }

        public List<string> VerifyOutboundCarrierManifestPickupDeleteOK(Int64 ixOutboundCarrierManifestPickup, string sOutboundCarrierManifestPickup)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(OutboundCarrierManifestPickupsPost outboundcarriermanifestpickupsPost)
		{
            _context.OutboundCarrierManifestPickupsPost.Add(outboundcarriermanifestpickupsPost); 
        }

        public void RegisterEdit(OutboundCarrierManifestPickupsPost outboundcarriermanifestpickupsPost)
        {
            _context.Entry(outboundcarriermanifestpickupsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(OutboundCarrierManifestPickupsPost outboundcarriermanifestpickupsPost)
        {
            _context.OutboundCarrierManifestPickupsPost.Remove(outboundcarriermanifestpickupsPost);
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
  

