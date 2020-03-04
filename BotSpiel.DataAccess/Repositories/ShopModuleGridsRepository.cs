using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class ShopModuleGridsRepository : IShopModuleGridsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly ShopModuleGridsDB _context;
  
        public ShopModuleGridsRepository(ShopModuleGridsDB context)
        {
            _context = context;
  
        }

        public ShopModuleGridsPost GetPost(Int64 ixShopModuleGrid) => _context.ShopModuleGridsPost.AsNoTracking().Where(x => x.ixShopModuleGrid == ixShopModuleGrid).First();
         
		public ShopModuleGrids Get(Int64 ixShopModuleGrid)
        {
            ShopModuleGrids shopmodulegrids = _context.ShopModuleGrids.AsNoTracking().Where(x => x.ixShopModuleGrid == ixShopModuleGrid).First();
            return shopmodulegrids;
        }

        public IQueryable<ShopModuleGrids> Index()
        {
            var shopmodulegrids = _context.ShopModuleGrids.AsNoTracking(); 
            return shopmodulegrids;
        }

        public IQueryable<ShopModuleGrids> IndexDb()
        {
            var shopmodulegrids = _context.ShopModuleGrids.AsNoTracking(); 
            return shopmodulegrids;
        }
		public IQueryable<ShopModuleGridsconfig> Indexconfig() => _context.ShopModuleGridsconfig;
		public IQueryable<ShopModuleGridsmd> Indexmd() => _context.ShopModuleGridsmd;
		public IQueryable<ShopModuleGridstx> Indextx() => _context.ShopModuleGridstx;
		public IQueryable<ShopModuleGridsanalytics> Indexanalytics() => _context.ShopModuleGridsanalytics;
        public List<string> VerifyShopModuleGridDeleteOK(Int64 ixShopModuleGrid, string sShopModuleGrid)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(ShopModuleGridsPost shopmodulegridsPost)
		{
            _context.ShopModuleGridsPost.Add(shopmodulegridsPost); 
        }

        public void RegisterEdit(ShopModuleGridsPost shopmodulegridsPost)
        {
            _context.Entry(shopmodulegridsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(ShopModuleGridsPost shopmodulegridsPost)
        {
            _context.ShopModuleGridsPost.Remove(shopmodulegridsPost);
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
  

