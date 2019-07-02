using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class LocationFunctionsRepository : ILocationFunctionsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly LocationFunctionsDB _context;
       private readonly FacilityAisleFacesDB _contextFacilityAisleFaces;
        private readonly InventoryLocationsDB _contextInventoryLocations;
  
        public LocationFunctionsRepository(LocationFunctionsDB context, FacilityAisleFacesDB contextFacilityAisleFaces, InventoryLocationsDB contextInventoryLocations)
        {
            _context = context;
           _contextFacilityAisleFaces = contextFacilityAisleFaces;
            _contextInventoryLocations = contextInventoryLocations;
  
        }

        public LocationFunctionsPost GetPost(Int64 ixLocationFunction) => _context.LocationFunctionsPost.AsNoTracking().Where(x => x.ixLocationFunction == ixLocationFunction).First();
         
		public LocationFunctions Get(Int64 ixLocationFunction)
        {
            LocationFunctions locationfunctions = _context.LocationFunctions.AsNoTracking().Where(x => x.ixLocationFunction == ixLocationFunction).First();
            return locationfunctions;
        }

        public IQueryable<LocationFunctions> Index()
        {
            var locationfunctions = _context.LocationFunctions.AsNoTracking(); 
            return locationfunctions;
        }
        public bool VerifyLocationFunctionUnique(Int64 ixLocationFunction, string sLocationFunction)
        {
            if (_context.LocationFunctions.AsNoTracking().Where(x => x.sLocationFunction == sLocationFunction).Any() && ixLocationFunction == 0L) return false;
            else if (_context.LocationFunctions.AsNoTracking().Where(x => x.sLocationFunction == sLocationFunction && x.ixLocationFunction != ixLocationFunction).Any() && ixLocationFunction != 0L) return false;
            else return true;
        }

        public List<string> VerifyLocationFunctionDeleteOK(Int64 ixLocationFunction, string sLocationFunction)
        {
            List<string> existInEntities = new List<string>();
           if (_contextFacilityAisleFaces.FacilityAisleFaces.AsNoTracking().Where(x => x.ixDefaultLocationFunction == ixLocationFunction).Any()) existInEntities.Add("FacilityAisleFaces");
            if (_contextInventoryLocations.InventoryLocations.AsNoTracking().Where(x => x.ixLocationFunction == ixLocationFunction).Any()) existInEntities.Add("InventoryLocations");

            return existInEntities;
        }


        public void RegisterCreate(LocationFunctionsPost locationfunctionsPost)
		{
            _context.LocationFunctionsPost.Add(locationfunctionsPost); 
        }

        public void RegisterEdit(LocationFunctionsPost locationfunctionsPost)
        {
            _context.Entry(locationfunctionsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(LocationFunctionsPost locationfunctionsPost)
        {
            _context.LocationFunctionsPost.Remove(locationfunctionsPost);
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
  

