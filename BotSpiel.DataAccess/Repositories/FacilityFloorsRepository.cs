using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class FacilityFloorsRepository : IFacilityFloorsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly FacilityFloorsDB _context;
       private readonly FacilityAisleFacesDB _contextFacilityAisleFaces;
        private readonly InventoryLocationsDB _contextInventoryLocations;
  
        public FacilityFloorsRepository(FacilityFloorsDB context, FacilityAisleFacesDB contextFacilityAisleFaces, InventoryLocationsDB contextInventoryLocations)
        {
            _context = context;
           _contextFacilityAisleFaces = contextFacilityAisleFaces;
            _contextInventoryLocations = contextInventoryLocations;
  
        }

        public FacilityFloorsPost GetPost(Int64 ixFacilityFloor) => _context.FacilityFloorsPost.AsNoTracking().Where(x => x.ixFacilityFloor == ixFacilityFloor).First();
         
		public FacilityFloors Get(Int64 ixFacilityFloor)
        {
            FacilityFloors facilityfloors = _context.FacilityFloors.AsNoTracking().Where(x => x.ixFacilityFloor == ixFacilityFloor).First();
            return facilityfloors;
        }

        public IQueryable<FacilityFloors> Index()
        {
            var facilityfloors = _context.FacilityFloors.AsNoTracking(); 
            return facilityfloors;
        }

        public IQueryable<FacilityFloors> IndexDb()
        {
            var facilityfloors = _context.FacilityFloors.AsNoTracking(); 
            return facilityfloors;
        }
        public bool VerifyFacilityFloorUnique(Int64 ixFacilityFloor, string sFacilityFloor)
        {
            if (_context.FacilityFloors.AsNoTracking().Where(x => x.sFacilityFloor == sFacilityFloor).Any() && ixFacilityFloor == 0L) return false;
            else if (_context.FacilityFloors.AsNoTracking().Where(x => x.sFacilityFloor == sFacilityFloor && x.ixFacilityFloor != ixFacilityFloor).Any() && ixFacilityFloor != 0L) return false;
            else return true;
        }

        public List<string> VerifyFacilityFloorDeleteOK(Int64 ixFacilityFloor, string sFacilityFloor)
        {
            List<string> existInEntities = new List<string>();
           if (_contextFacilityAisleFaces.FacilityAisleFaces.AsNoTracking().Where(x => x.ixFacilityFloor == ixFacilityFloor).Any()) existInEntities.Add("FacilityAisleFaces");
            if (_contextInventoryLocations.InventoryLocations.AsNoTracking().Where(x => x.ixFacilityFloor == ixFacilityFloor).Any()) existInEntities.Add("InventoryLocations");

            return existInEntities;
        }


        public void RegisterCreate(FacilityFloorsPost facilityfloorsPost)
		{
            _context.FacilityFloorsPost.Add(facilityfloorsPost); 
        }

        public void RegisterEdit(FacilityFloorsPost facilityfloorsPost)
        {
            _context.Entry(facilityfloorsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(FacilityFloorsPost facilityfloorsPost)
        {
            _context.FacilityFloorsPost.Remove(facilityfloorsPost);
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
  

