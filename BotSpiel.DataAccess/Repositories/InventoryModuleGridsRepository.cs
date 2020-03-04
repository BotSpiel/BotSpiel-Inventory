using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class InventoryModuleGridsRepository : IInventoryModuleGridsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly InventoryModuleGridsDB _context;
  
        public InventoryModuleGridsRepository(InventoryModuleGridsDB context)
        {
            _context = context;
  
        }

        public InventoryModuleGridsPost GetPost(Int64 ixInventoryModuleGrid) => _context.InventoryModuleGridsPost.AsNoTracking().Where(x => x.ixInventoryModuleGrid == ixInventoryModuleGrid).First();
         
		public InventoryModuleGrids Get(Int64 ixInventoryModuleGrid)
        {
            InventoryModuleGrids inventorymodulegrids = _context.InventoryModuleGrids.AsNoTracking().Where(x => x.ixInventoryModuleGrid == ixInventoryModuleGrid).First();
            return inventorymodulegrids;
        }

        public IQueryable<InventoryModuleGrids> Index()
        {
            var inventorymodulegrids = _context.InventoryModuleGrids.AsNoTracking(); 
            return inventorymodulegrids;
        }

        public IQueryable<InventoryModuleGrids> IndexDb()
        {
            var inventorymodulegrids = _context.InventoryModuleGrids.AsNoTracking(); 
            return inventorymodulegrids;
        }
		public IQueryable<InventoryModuleGridsconfig> Indexconfig() => _context.InventoryModuleGridsconfig;
		public IQueryable<InventoryModuleGridsmd> Indexmd() => _context.InventoryModuleGridsmd;
		public IQueryable<InventoryModuleGridstx> Indextx() => _context.InventoryModuleGridstx;
		public IQueryable<InventoryModuleGridsanalytics> Indexanalytics() => _context.InventoryModuleGridsanalytics;
        public List<string> VerifyInventoryModuleGridDeleteOK(Int64 ixInventoryModuleGrid, string sInventoryModuleGrid)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(InventoryModuleGridsPost inventorymodulegridsPost)
		{
            _context.InventoryModuleGridsPost.Add(inventorymodulegridsPost); 
        }

        public void RegisterEdit(InventoryModuleGridsPost inventorymodulegridsPost)
        {
            _context.Entry(inventorymodulegridsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(InventoryModuleGridsPost inventorymodulegridsPost)
        {
            _context.InventoryModuleGridsPost.Remove(inventorymodulegridsPost);
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
  

