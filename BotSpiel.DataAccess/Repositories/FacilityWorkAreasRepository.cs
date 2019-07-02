using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class FacilityWorkAreasRepository : IFacilityWorkAreasRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly FacilityWorkAreasDB _context;
       private readonly InventoryLocationsDB _contextInventoryLocations;
  
        public FacilityWorkAreasRepository(FacilityWorkAreasDB context, InventoryLocationsDB contextInventoryLocations)
        {
            _context = context;
           _contextInventoryLocations = contextInventoryLocations;
  
        }

        public FacilityWorkAreasPost GetPost(Int64 ixFacilityWorkArea) => _context.FacilityWorkAreasPost.AsNoTracking().Where(x => x.ixFacilityWorkArea == ixFacilityWorkArea).First();
         
		public FacilityWorkAreas Get(Int64 ixFacilityWorkArea)
        {
            FacilityWorkAreas facilityworkareas = _context.FacilityWorkAreas.AsNoTracking().Where(x => x.ixFacilityWorkArea == ixFacilityWorkArea).First();
            return facilityworkareas;
        }

        public IQueryable<FacilityWorkAreas> Index()
        {
            var facilityworkareas = _context.FacilityWorkAreas.AsNoTracking(); 
            return facilityworkareas;
        }
        public bool VerifyFacilityWorkAreaUnique(Int64 ixFacilityWorkArea, string sFacilityWorkArea)
        {
            if (_context.FacilityWorkAreas.AsNoTracking().Where(x => x.sFacilityWorkArea == sFacilityWorkArea).Any() && ixFacilityWorkArea == 0L) return false;
            else if (_context.FacilityWorkAreas.AsNoTracking().Where(x => x.sFacilityWorkArea == sFacilityWorkArea && x.ixFacilityWorkArea != ixFacilityWorkArea).Any() && ixFacilityWorkArea != 0L) return false;
            else return true;
        }

        public List<string> VerifyFacilityWorkAreaDeleteOK(Int64 ixFacilityWorkArea, string sFacilityWorkArea)
        {
            List<string> existInEntities = new List<string>();
           if (_contextInventoryLocations.InventoryLocations.AsNoTracking().Where(x => x.ixFacilityWorkArea == ixFacilityWorkArea).Any()) existInEntities.Add("InventoryLocations");

            return existInEntities;
        }


        public void RegisterCreate(FacilityWorkAreasPost facilityworkareasPost)
		{
            _context.FacilityWorkAreasPost.Add(facilityworkareasPost); 
        }

        public void RegisterEdit(FacilityWorkAreasPost facilityworkareasPost)
        {
            _context.Entry(facilityworkareasPost).State = EntityState.Modified;
        }

        public void RegisterDelete(FacilityWorkAreasPost facilityworkareasPost)
        {
            _context.FacilityWorkAreasPost.Remove(facilityworkareasPost);
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
  

