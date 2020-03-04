using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class InventoryLocationsSlottingService : IInventoryLocationsSlottingService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IInventoryLocationsSlottingRepository _inventorylocationsslottingRepository;

        public InventoryLocationsSlottingService(IInventoryLocationsSlottingRepository inventorylocationsslottingRepository)
        {
            _inventorylocationsslottingRepository = inventorylocationsslottingRepository;
        }

        public InventoryLocationsSlottingPost GetPost(Int64 ixInventoryLocationSlotting) => _inventorylocationsslottingRepository.GetPost(ixInventoryLocationSlotting);
        public InventoryLocationsSlotting Get(Int64 ixInventoryLocationSlotting) => _inventorylocationsslottingRepository.Get(ixInventoryLocationSlotting);
        public IQueryable<InventoryLocationsSlotting> Index() => _inventorylocationsslottingRepository.Index();
        public IQueryable<InventoryLocationsSlotting> IndexDb() => _inventorylocationsslottingRepository.IndexDb();
       public IQueryable<Materials> selectMaterials() => _inventorylocationsslottingRepository.selectMaterials();
        public IQueryable<InventoryLocations> selectInventoryLocations() => _inventorylocationsslottingRepository.selectInventoryLocations();
       public IQueryable<Materials> MaterialsDb() => _inventorylocationsslottingRepository.MaterialsDb();
        public IQueryable<InventoryLocations> InventoryLocationsDb() => _inventorylocationsslottingRepository.InventoryLocationsDb();
        public bool VerifyInventoryLocationSlottingUnique(Int64 ixInventoryLocationSlotting, string sInventoryLocationSlotting) => _inventorylocationsslottingRepository.VerifyInventoryLocationSlottingUnique(ixInventoryLocationSlotting, sInventoryLocationSlotting);
        public List<string> VerifyInventoryLocationSlottingDeleteOK(Int64 ixInventoryLocationSlotting, string sInventoryLocationSlotting) => _inventorylocationsslottingRepository.VerifyInventoryLocationSlottingDeleteOK(ixInventoryLocationSlotting, sInventoryLocationSlotting);

        public Task<Int64> Create(InventoryLocationsSlottingPost inventorylocationsslottingPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._inventorylocationsslottingRepository.RegisterCreate(inventorylocationsslottingPost);
            try
            {
                this._inventorylocationsslottingRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inventorylocationsslottingRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(inventorylocationsslottingPost.ixInventoryLocationSlotting);

        }
        public Task Edit(InventoryLocationsSlottingPost inventorylocationsslottingPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._inventorylocationsslottingRepository.RegisterEdit(inventorylocationsslottingPost);
            try
            {
                this._inventorylocationsslottingRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inventorylocationsslottingRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(InventoryLocationsSlottingPost inventorylocationsslottingPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._inventorylocationsslottingRepository.RegisterDelete(inventorylocationsslottingPost);
            try
            {
                this._inventorylocationsslottingRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inventorylocationsslottingRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

