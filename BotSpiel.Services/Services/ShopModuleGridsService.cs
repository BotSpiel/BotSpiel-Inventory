using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class ShopModuleGridsService : IShopModuleGridsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IShopModuleGridsRepository _shopmodulegridsRepository;

        public ShopModuleGridsService(IShopModuleGridsRepository shopmodulegridsRepository)
        {
            _shopmodulegridsRepository = shopmodulegridsRepository;
        }

        public ShopModuleGridsPost GetPost(Int64 ixShopModuleGrid) => _shopmodulegridsRepository.GetPost(ixShopModuleGrid);
        public ShopModuleGrids Get(Int64 ixShopModuleGrid) => _shopmodulegridsRepository.Get(ixShopModuleGrid);
        public IQueryable<ShopModuleGrids> Index() => _shopmodulegridsRepository.Index();
        public IQueryable<ShopModuleGrids> IndexDb() => _shopmodulegridsRepository.IndexDb();
		public IQueryable<ShopModuleGridsconfig> Indexconfig() => _shopmodulegridsRepository.Indexconfig();
		public IQueryable<ShopModuleGridsmd> Indexmd() => _shopmodulegridsRepository.Indexmd();
		public IQueryable<ShopModuleGridstx> Indextx() => _shopmodulegridsRepository.Indextx();
		public IQueryable<ShopModuleGridsanalytics> Indexanalytics() => _shopmodulegridsRepository.Indexanalytics();
        public List<string> VerifyShopModuleGridDeleteOK(Int64 ixShopModuleGrid, string sShopModuleGrid) => _shopmodulegridsRepository.VerifyShopModuleGridDeleteOK(ixShopModuleGrid, sShopModuleGrid);

        public Task<Int64> Create(ShopModuleGridsPost shopmodulegridsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._shopmodulegridsRepository.RegisterCreate(shopmodulegridsPost);
            try
            {
                this._shopmodulegridsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._shopmodulegridsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(shopmodulegridsPost.ixShopModuleGrid);

        }
        public Task Edit(ShopModuleGridsPost shopmodulegridsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._shopmodulegridsRepository.RegisterEdit(shopmodulegridsPost);
            try
            {
                this._shopmodulegridsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._shopmodulegridsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(ShopModuleGridsPost shopmodulegridsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._shopmodulegridsRepository.RegisterDelete(shopmodulegridsPost);
            try
            {
                this._shopmodulegridsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._shopmodulegridsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

