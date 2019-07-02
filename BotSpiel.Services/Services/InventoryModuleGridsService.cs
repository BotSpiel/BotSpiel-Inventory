using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class InventoryModuleGridsService : IInventoryModuleGridsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IInventoryModuleGridsRepository _inventorymodulegridsRepository;

        public InventoryModuleGridsService(IInventoryModuleGridsRepository inventorymodulegridsRepository)
        {
            _inventorymodulegridsRepository = inventorymodulegridsRepository;
        }

        public InventoryModuleGridsPost GetPost(Int64 ixInventoryModuleGrid) => _inventorymodulegridsRepository.GetPost(ixInventoryModuleGrid);
        public InventoryModuleGrids Get(Int64 ixInventoryModuleGrid) => _inventorymodulegridsRepository.Get(ixInventoryModuleGrid);
        public IQueryable<InventoryModuleGrids> Index() => _inventorymodulegridsRepository.Index();
		public IQueryable<InventoryModuleGridsconfig> Indexconfig() => _inventorymodulegridsRepository.Indexconfig();
		public IQueryable<InventoryModuleGridsmd> Indexmd() => _inventorymodulegridsRepository.Indexmd();
		public IQueryable<InventoryModuleGridstx> Indextx() => _inventorymodulegridsRepository.Indextx();
		public IQueryable<InventoryModuleGridsanalytics> Indexanalytics() => _inventorymodulegridsRepository.Indexanalytics();
        public List<string> VerifyInventoryModuleGridDeleteOK(Int64 ixInventoryModuleGrid, string sInventoryModuleGrid) => _inventorymodulegridsRepository.VerifyInventoryModuleGridDeleteOK(ixInventoryModuleGrid, sInventoryModuleGrid);

        public Task<Int64> Create(InventoryModuleGridsPost inventorymodulegridsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._inventorymodulegridsRepository.RegisterCreate(inventorymodulegridsPost);
            try
            {
                this._inventorymodulegridsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inventorymodulegridsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(inventorymodulegridsPost.ixInventoryModuleGrid);

        }
        public Task Edit(InventoryModuleGridsPost inventorymodulegridsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._inventorymodulegridsRepository.RegisterEdit(inventorymodulegridsPost);
            try
            {
                this._inventorymodulegridsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inventorymodulegridsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(InventoryModuleGridsPost inventorymodulegridsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._inventorymodulegridsRepository.RegisterDelete(inventorymodulegridsPost);
            try
            {
                this._inventorymodulegridsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inventorymodulegridsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

