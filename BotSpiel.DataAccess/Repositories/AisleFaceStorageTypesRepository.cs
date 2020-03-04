using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class AisleFaceStorageTypesRepository : IAisleFaceStorageTypesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly AisleFaceStorageTypesDB _context;
       private readonly FacilityAisleFacesDB _contextFacilityAisleFaces;
  
        public AisleFaceStorageTypesRepository(AisleFaceStorageTypesDB context, FacilityAisleFacesDB contextFacilityAisleFaces)
        {
            _context = context;
           _contextFacilityAisleFaces = contextFacilityAisleFaces;
  
        }

        public AisleFaceStorageTypesPost GetPost(Int64 ixAisleFaceStorageType) => _context.AisleFaceStorageTypesPost.AsNoTracking().Where(x => x.ixAisleFaceStorageType == ixAisleFaceStorageType).First();
         
		public AisleFaceStorageTypes Get(Int64 ixAisleFaceStorageType)
        {
            AisleFaceStorageTypes aislefacestoragetypes = _context.AisleFaceStorageTypes.AsNoTracking().Where(x => x.ixAisleFaceStorageType == ixAisleFaceStorageType).First();
            return aislefacestoragetypes;
        }

        public IQueryable<AisleFaceStorageTypes> Index()
        {
            var aislefacestoragetypes = _context.AisleFaceStorageTypes.AsNoTracking(); 
            return aislefacestoragetypes;
        }

        public IQueryable<AisleFaceStorageTypes> IndexDb()
        {
            var aislefacestoragetypes = _context.AisleFaceStorageTypes.AsNoTracking(); 
            return aislefacestoragetypes;
        }
        public bool VerifyAisleFaceStorageTypeUnique(Int64 ixAisleFaceStorageType, string sAisleFaceStorageType)
        {
            if (_context.AisleFaceStorageTypes.AsNoTracking().Where(x => x.sAisleFaceStorageType == sAisleFaceStorageType).Any() && ixAisleFaceStorageType == 0L) return false;
            else if (_context.AisleFaceStorageTypes.AsNoTracking().Where(x => x.sAisleFaceStorageType == sAisleFaceStorageType && x.ixAisleFaceStorageType != ixAisleFaceStorageType).Any() && ixAisleFaceStorageType != 0L) return false;
            else return true;
        }

        public List<string> VerifyAisleFaceStorageTypeDeleteOK(Int64 ixAisleFaceStorageType, string sAisleFaceStorageType)
        {
            List<string> existInEntities = new List<string>();
           if (_contextFacilityAisleFaces.FacilityAisleFaces.AsNoTracking().Where(x => x.ixAisleFaceStorageType == ixAisleFaceStorageType).Any()) existInEntities.Add("FacilityAisleFaces");

            return existInEntities;
        }


        public void RegisterCreate(AisleFaceStorageTypesPost aislefacestoragetypesPost)
		{
            _context.AisleFaceStorageTypesPost.Add(aislefacestoragetypesPost); 
        }

        public void RegisterEdit(AisleFaceStorageTypesPost aislefacestoragetypesPost)
        {
            _context.Entry(aislefacestoragetypesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(AisleFaceStorageTypesPost aislefacestoragetypesPost)
        {
            _context.AisleFaceStorageTypesPost.Remove(aislefacestoragetypesPost);
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
  

