using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class FoundationModuleGridsRepository : IFoundationModuleGridsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly FoundationModuleGridsDB _context;
  
        public FoundationModuleGridsRepository(FoundationModuleGridsDB context)
        {
            _context = context;
  
        }

        public FoundationModuleGridsPost GetPost(Int64 ixFoundationModuleGrid) => _context.FoundationModuleGridsPost.AsNoTracking().Where(x => x.ixFoundationModuleGrid == ixFoundationModuleGrid).First();
         
		public FoundationModuleGrids Get(Int64 ixFoundationModuleGrid)
        {
            FoundationModuleGrids foundationmodulegrids = _context.FoundationModuleGrids.AsNoTracking().Where(x => x.ixFoundationModuleGrid == ixFoundationModuleGrid).First();
            return foundationmodulegrids;
        }

        public IQueryable<FoundationModuleGrids> Index()
        {
            var foundationmodulegrids = _context.FoundationModuleGrids.AsNoTracking(); 
            return foundationmodulegrids;
        }
		public IQueryable<FoundationModuleGridsconfig> Indexconfig() => _context.FoundationModuleGridsconfig;
		public IQueryable<FoundationModuleGridsmd> Indexmd() => _context.FoundationModuleGridsmd;
		public IQueryable<FoundationModuleGridstx> Indextx() => _context.FoundationModuleGridstx;
		public IQueryable<FoundationModuleGridsanalytics> Indexanalytics() => _context.FoundationModuleGridsanalytics;
        public List<string> VerifyFoundationModuleGridDeleteOK(Int64 ixFoundationModuleGrid, string sFoundationModuleGrid)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(FoundationModuleGridsPost foundationmodulegridsPost)
		{
            _context.FoundationModuleGridsPost.Add(foundationmodulegridsPost); 
        }

        public void RegisterEdit(FoundationModuleGridsPost foundationmodulegridsPost)
        {
            _context.Entry(foundationmodulegridsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(FoundationModuleGridsPost foundationmodulegridsPost)
        {
            _context.FoundationModuleGridsPost.Remove(foundationmodulegridsPost);
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
  

