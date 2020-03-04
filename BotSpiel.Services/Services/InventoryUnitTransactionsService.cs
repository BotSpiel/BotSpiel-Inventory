using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class InventoryUnitTransactionsService : IInventoryUnitTransactionsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IInventoryUnitTransactionsRepository _inventoryunittransactionsRepository;

        public InventoryUnitTransactionsService(IInventoryUnitTransactionsRepository inventoryunittransactionsRepository)
        {
            _inventoryunittransactionsRepository = inventoryunittransactionsRepository;
        }

        public InventoryUnitTransactionsPost GetPost(Int64 ixInventoryUnitTransaction) => _inventoryunittransactionsRepository.GetPost(ixInventoryUnitTransaction);
        public InventoryUnitTransactions Get(Int64 ixInventoryUnitTransaction) => _inventoryunittransactionsRepository.Get(ixInventoryUnitTransaction);
        public IQueryable<InventoryUnitTransactions> Index() => _inventoryunittransactionsRepository.Index();
        public IQueryable<InventoryUnitTransactions> IndexDb() => _inventoryunittransactionsRepository.IndexDb();
       public IQueryable<Statuses> selectStatuses() => _inventoryunittransactionsRepository.selectStatuses();
        public IQueryable<Companies> selectCompanies() => _inventoryunittransactionsRepository.selectCompanies();
        public IQueryable<Facilities> selectFacilities() => _inventoryunittransactionsRepository.selectFacilities();
        public IQueryable<Materials> selectMaterials() => _inventoryunittransactionsRepository.selectMaterials();
        public IQueryable<InventoryUnits> selectInventoryUnits() => _inventoryunittransactionsRepository.selectInventoryUnits();
        public IQueryable<InventoryStates> selectInventoryStates() => _inventoryunittransactionsRepository.selectInventoryStates();
        public IQueryable<HandlingUnits> selectHandlingUnits() => _inventoryunittransactionsRepository.selectHandlingUnits();
        public IQueryable<InventoryLocations> selectInventoryLocations() => _inventoryunittransactionsRepository.selectInventoryLocations();
        public IQueryable<InventoryUnitTransactionContexts> selectInventoryUnitTransactionContexts() => _inventoryunittransactionsRepository.selectInventoryUnitTransactionContexts();
       public IQueryable<Statuses> StatusesDb() => _inventoryunittransactionsRepository.StatusesDb();
        public IQueryable<Companies> CompaniesDb() => _inventoryunittransactionsRepository.CompaniesDb();
        public IQueryable<Facilities> FacilitiesDb() => _inventoryunittransactionsRepository.FacilitiesDb();
        public IQueryable<Materials> MaterialsDb() => _inventoryunittransactionsRepository.MaterialsDb();
        public IQueryable<InventoryUnits> InventoryUnitsDb() => _inventoryunittransactionsRepository.InventoryUnitsDb();
        public IQueryable<InventoryStates> InventoryStatesDb() => _inventoryunittransactionsRepository.InventoryStatesDb();
        public IQueryable<HandlingUnits> HandlingUnitsDb() => _inventoryunittransactionsRepository.HandlingUnitsDb();
        public IQueryable<InventoryLocations> InventoryLocationsDb() => _inventoryunittransactionsRepository.InventoryLocationsDb();
        public IQueryable<InventoryUnitTransactionContexts> InventoryUnitTransactionContextsDb() => _inventoryunittransactionsRepository.InventoryUnitTransactionContextsDb();
       public List<KeyValuePair<Int64?, string>> selectStatusesNullable() => _inventoryunittransactionsRepository.selectStatusesNullable();
        public List<KeyValuePair<Int64?, string>> selectCompaniesNullable() => _inventoryunittransactionsRepository.selectCompaniesNullable();
        public List<KeyValuePair<Int64?, string>> selectFacilitiesNullable() => _inventoryunittransactionsRepository.selectFacilitiesNullable();
        public List<KeyValuePair<Int64?, string>> selectMaterialsNullable() => _inventoryunittransactionsRepository.selectMaterialsNullable();
        public List<KeyValuePair<Int64?, string>> selectInventoryStatesNullable() => _inventoryunittransactionsRepository.selectInventoryStatesNullable();
        public List<KeyValuePair<Int64?, string>> selectHandlingUnitsNullable() => _inventoryunittransactionsRepository.selectHandlingUnitsNullable();
        public List<KeyValuePair<Int64?, string>> selectInventoryLocationsNullable() => _inventoryunittransactionsRepository.selectInventoryLocationsNullable();
        public bool VerifyInventoryUnitTransactionUnique(Int64 ixInventoryUnitTransaction, string sInventoryUnitTransaction) => _inventoryunittransactionsRepository.VerifyInventoryUnitTransactionUnique(ixInventoryUnitTransaction, sInventoryUnitTransaction);
        public List<string> VerifyInventoryUnitTransactionDeleteOK(Int64 ixInventoryUnitTransaction, string sInventoryUnitTransaction) => _inventoryunittransactionsRepository.VerifyInventoryUnitTransactionDeleteOK(ixInventoryUnitTransaction, sInventoryUnitTransaction);

        public Task<Int64> Create(InventoryUnitTransactionsPost inventoryunittransactionsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._inventoryunittransactionsRepository.RegisterCreate(inventoryunittransactionsPost);
            try
            {
                this._inventoryunittransactionsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inventoryunittransactionsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(inventoryunittransactionsPost.ixInventoryUnitTransaction);

        }
        public Task Edit(InventoryUnitTransactionsPost inventoryunittransactionsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._inventoryunittransactionsRepository.RegisterEdit(inventoryunittransactionsPost);
            try
            {
                this._inventoryunittransactionsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inventoryunittransactionsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(InventoryUnitTransactionsPost inventoryunittransactionsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._inventoryunittransactionsRepository.RegisterDelete(inventoryunittransactionsPost);
            try
            {
                this._inventoryunittransactionsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inventoryunittransactionsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

