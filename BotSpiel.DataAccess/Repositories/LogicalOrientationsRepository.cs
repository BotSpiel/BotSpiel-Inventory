using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class LogicalOrientationsRepository : ILogicalOrientationsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly LogicalOrientationsDB _context;
       private readonly FacilityAisleFacesDB _contextFacilityAisleFaces;
  
        public LogicalOrientationsRepository(LogicalOrientationsDB context, FacilityAisleFacesDB contextFacilityAisleFaces)
        {
            _context = context;
           _contextFacilityAisleFaces = contextFacilityAisleFaces;
  
        }

        public LogicalOrientationsPost GetPost(Int64 ixLogicalOrientation) => _context.LogicalOrientationsPost.AsNoTracking().Where(x => x.ixLogicalOrientation == ixLogicalOrientation).First();
         
		public LogicalOrientations Get(Int64 ixLogicalOrientation)
        {
            LogicalOrientations logicalorientations = _context.LogicalOrientations.AsNoTracking().Where(x => x.ixLogicalOrientation == ixLogicalOrientation).First();
            return logicalorientations;
        }

        public IQueryable<LogicalOrientations> Index()
        {
            var logicalorientations = _context.LogicalOrientations.AsNoTracking(); 
            return logicalorientations;
        }
        public bool VerifyLogicalOrientationUnique(Int64 ixLogicalOrientation, string sLogicalOrientation)
        {
            if (_context.LogicalOrientations.AsNoTracking().Where(x => x.sLogicalOrientation == sLogicalOrientation).Any() && ixLogicalOrientation == 0L) return false;
            else if (_context.LogicalOrientations.AsNoTracking().Where(x => x.sLogicalOrientation == sLogicalOrientation && x.ixLogicalOrientation != ixLogicalOrientation).Any() && ixLogicalOrientation != 0L) return false;
            else return true;
        }

        public List<string> VerifyLogicalOrientationDeleteOK(Int64 ixLogicalOrientation, string sLogicalOrientation)
        {
            List<string> existInEntities = new List<string>();
           if (_contextFacilityAisleFaces.FacilityAisleFaces.AsNoTracking().Where(x => x.ixLogicalOrientation == ixLogicalOrientation).Any()) existInEntities.Add("FacilityAisleFaces");

            return existInEntities;
        }


        public void RegisterCreate(LogicalOrientationsPost logicalorientationsPost)
		{
            _context.LogicalOrientationsPost.Add(logicalorientationsPost); 
        }

        public void RegisterEdit(LogicalOrientationsPost logicalorientationsPost)
        {
            _context.Entry(logicalorientationsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(LogicalOrientationsPost logicalorientationsPost)
        {
            _context.LogicalOrientationsPost.Remove(logicalorientationsPost);
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
  

