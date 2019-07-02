using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class BaySequenceTypesRepository : IBaySequenceTypesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly BaySequenceTypesDB _context;
       private readonly FacilityAisleFacesDB _contextFacilityAisleFaces;
  
        public BaySequenceTypesRepository(BaySequenceTypesDB context, FacilityAisleFacesDB contextFacilityAisleFaces)
        {
            _context = context;
           _contextFacilityAisleFaces = contextFacilityAisleFaces;
  
        }

        public BaySequenceTypesPost GetPost(Int64 ixBaySequenceType) => _context.BaySequenceTypesPost.AsNoTracking().Where(x => x.ixBaySequenceType == ixBaySequenceType).First();
         
		public BaySequenceTypes Get(Int64 ixBaySequenceType)
        {
            BaySequenceTypes baysequencetypes = _context.BaySequenceTypes.AsNoTracking().Where(x => x.ixBaySequenceType == ixBaySequenceType).First();
            return baysequencetypes;
        }

        public IQueryable<BaySequenceTypes> Index()
        {
            var baysequencetypes = _context.BaySequenceTypes.AsNoTracking(); 
            return baysequencetypes;
        }
        public bool VerifyBaySequenceTypeUnique(Int64 ixBaySequenceType, string sBaySequenceType)
        {
            if (_context.BaySequenceTypes.AsNoTracking().Where(x => x.sBaySequenceType == sBaySequenceType).Any() && ixBaySequenceType == 0L) return false;
            else if (_context.BaySequenceTypes.AsNoTracking().Where(x => x.sBaySequenceType == sBaySequenceType && x.ixBaySequenceType != ixBaySequenceType).Any() && ixBaySequenceType != 0L) return false;
            else return true;
        }

        public List<string> VerifyBaySequenceTypeDeleteOK(Int64 ixBaySequenceType, string sBaySequenceType)
        {
            List<string> existInEntities = new List<string>();
           if (_contextFacilityAisleFaces.FacilityAisleFaces.AsNoTracking().Where(x => x.ixBaySequenceType == ixBaySequenceType).Any()) existInEntities.Add("FacilityAisleFaces");

            return existInEntities;
        }


        public void RegisterCreate(BaySequenceTypesPost baysequencetypesPost)
		{
            _context.BaySequenceTypesPost.Add(baysequencetypesPost); 
        }

        public void RegisterEdit(BaySequenceTypesPost baysequencetypesPost)
        {
            _context.Entry(baysequencetypesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(BaySequenceTypesPost baysequencetypesPost)
        {
            _context.BaySequenceTypesPost.Remove(baysequencetypesPost);
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
  

