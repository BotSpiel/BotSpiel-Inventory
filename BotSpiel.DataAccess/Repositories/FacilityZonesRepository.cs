using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class FacilityZonesRepository : IFacilityZonesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly FacilityZonesDB _context;
       private readonly FacilityAisleFacesDB _contextFacilityAisleFaces;
        private readonly InventoryLocationsDB _contextInventoryLocations;
  
        public FacilityZonesRepository(FacilityZonesDB context, FacilityAisleFacesDB contextFacilityAisleFaces, InventoryLocationsDB contextInventoryLocations)
        {
            _context = context;
           _contextFacilityAisleFaces = contextFacilityAisleFaces;
            _contextInventoryLocations = contextInventoryLocations;
  
        }

        public FacilityZonesPost GetPost(Int64 ixFacilityZone) => _context.FacilityZonesPost.AsNoTracking().Where(x => x.ixFacilityZone == ixFacilityZone).First();
         
		public FacilityZones Get(Int64 ixFacilityZone)
        {
            FacilityZones facilityzones = _context.FacilityZones.AsNoTracking().Where(x => x.ixFacilityZone == ixFacilityZone).First();
            return facilityzones;
        }

        public IQueryable<FacilityZones> Index()
        {
            var facilityzones = _context.FacilityZones.AsNoTracking(); 
            return facilityzones;
        }
        public bool VerifyFacilityZoneUnique(Int64 ixFacilityZone, string sFacilityZone)
        {
            if (_context.FacilityZones.AsNoTracking().Where(x => x.sFacilityZone == sFacilityZone).Any() && ixFacilityZone == 0L) return false;
            else if (_context.FacilityZones.AsNoTracking().Where(x => x.sFacilityZone == sFacilityZone && x.ixFacilityZone != ixFacilityZone).Any() && ixFacilityZone != 0L) return false;
            else return true;
        }

        public List<string> VerifyFacilityZoneDeleteOK(Int64 ixFacilityZone, string sFacilityZone)
        {
            List<string> existInEntities = new List<string>();
           if (_contextFacilityAisleFaces.FacilityAisleFaces.AsNoTracking().Where(x => x.ixDefaultFacilityZone == ixFacilityZone).Any()) existInEntities.Add("FacilityAisleFaces");
            if (_contextInventoryLocations.InventoryLocations.AsNoTracking().Where(x => x.ixFacilityZone == ixFacilityZone).Any()) existInEntities.Add("InventoryLocations");

            return existInEntities;
        }


        public void RegisterCreate(FacilityZonesPost facilityzonesPost)
		{
            _context.FacilityZonesPost.Add(facilityzonesPost); 
        }

        public void RegisterEdit(FacilityZonesPost facilityzonesPost)
        {
            _context.Entry(facilityzonesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(FacilityZonesPost facilityzonesPost)
        {
            _context.FacilityZonesPost.Remove(facilityzonesPost);
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
  

