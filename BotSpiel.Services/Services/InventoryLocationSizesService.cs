using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class InventoryLocationSizesService : IInventoryLocationSizesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IInventoryLocationSizesRepository _inventorylocationsizesRepository;

        public InventoryLocationSizesService(IInventoryLocationSizesRepository inventorylocationsizesRepository)
        {
            _inventorylocationsizesRepository = inventorylocationsizesRepository;
        }

        public InventoryLocationSizesPost GetPost(Int64 ixInventoryLocationSize) => _inventorylocationsizesRepository.GetPost(ixInventoryLocationSize);
        public InventoryLocationSizes Get(Int64 ixInventoryLocationSize) => _inventorylocationsizesRepository.Get(ixInventoryLocationSize);
        public IQueryable<InventoryLocationSizes> Index() => _inventorylocationsizesRepository.Index();
        public IQueryable<InventoryLocationSizes> IndexDb() => _inventorylocationsizesRepository.IndexDb();
       public IQueryable<UnitsOfMeasurement> selectUnitsOfMeasurement() => _inventorylocationsizesRepository.selectUnitsOfMeasurement();
       public IQueryable<UnitsOfMeasurement> UnitsOfMeasurementDb() => _inventorylocationsizesRepository.UnitsOfMeasurementDb();
        public bool VerifyInventoryLocationSizeUnique(Int64 ixInventoryLocationSize, string sInventoryLocationSize) => _inventorylocationsizesRepository.VerifyInventoryLocationSizeUnique(ixInventoryLocationSize, sInventoryLocationSize);
        public List<string> VerifyInventoryLocationSizeDeleteOK(Int64 ixInventoryLocationSize, string sInventoryLocationSize) => _inventorylocationsizesRepository.VerifyInventoryLocationSizeDeleteOK(ixInventoryLocationSize, sInventoryLocationSize);

        public Task<Int64> Create(InventoryLocationSizesPost inventorylocationsizesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._inventorylocationsizesRepository.RegisterCreate(inventorylocationsizesPost);
            try
            {
                this._inventorylocationsizesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inventorylocationsizesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(inventorylocationsizesPost.ixInventoryLocationSize);

        }
        public Task Edit(InventoryLocationSizesPost inventorylocationsizesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._inventorylocationsizesRepository.RegisterEdit(inventorylocationsizesPost);
            try
            {
                this._inventorylocationsizesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inventorylocationsizesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(InventoryLocationSizesPost inventorylocationsizesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._inventorylocationsizesRepository.RegisterDelete(inventorylocationsizesPost);
            try
            {
                this._inventorylocationsizesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inventorylocationsizesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

