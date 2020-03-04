using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class AssemblyModuleGridsRepository : IAssemblyModuleGridsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly AssemblyModuleGridsDB _context;
  
        public AssemblyModuleGridsRepository(AssemblyModuleGridsDB context)
        {
            _context = context;
  
        }

        public AssemblyModuleGridsPost GetPost(Int64 ixAssemblyModuleGrid) => _context.AssemblyModuleGridsPost.AsNoTracking().Where(x => x.ixAssemblyModuleGrid == ixAssemblyModuleGrid).First();
         
		public AssemblyModuleGrids Get(Int64 ixAssemblyModuleGrid)
        {
            AssemblyModuleGrids assemblymodulegrids = _context.AssemblyModuleGrids.AsNoTracking().Where(x => x.ixAssemblyModuleGrid == ixAssemblyModuleGrid).First();
            return assemblymodulegrids;
        }

        public IQueryable<AssemblyModuleGrids> Index()
        {
            var assemblymodulegrids = _context.AssemblyModuleGrids.AsNoTracking(); 
            return assemblymodulegrids;
        }

        public IQueryable<AssemblyModuleGrids> IndexDb()
        {
            var assemblymodulegrids = _context.AssemblyModuleGrids.AsNoTracking(); 
            return assemblymodulegrids;
        }
		public IQueryable<AssemblyModuleGridsconfig> Indexconfig() => _context.AssemblyModuleGridsconfig;
		public IQueryable<AssemblyModuleGridsmd> Indexmd() => _context.AssemblyModuleGridsmd;
		public IQueryable<AssemblyModuleGridstx> Indextx() => _context.AssemblyModuleGridstx;
		public IQueryable<AssemblyModuleGridsanalytics> Indexanalytics() => _context.AssemblyModuleGridsanalytics;
        public List<string> VerifyAssemblyModuleGridDeleteOK(Int64 ixAssemblyModuleGrid, string sAssemblyModuleGrid)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(AssemblyModuleGridsPost assemblymodulegridsPost)
		{
            _context.AssemblyModuleGridsPost.Add(assemblymodulegridsPost); 
        }

        public void RegisterEdit(AssemblyModuleGridsPost assemblymodulegridsPost)
        {
            _context.Entry(assemblymodulegridsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(AssemblyModuleGridsPost assemblymodulegridsPost)
        {
            _context.AssemblyModuleGridsPost.Remove(assemblymodulegridsPost);
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
  

