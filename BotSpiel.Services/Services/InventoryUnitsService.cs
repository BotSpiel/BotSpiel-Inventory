using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class InventoryUnitsService : IInventoryUnitsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IInventoryUnitsRepository _inventoryunitsRepository;
        //Custom Code Start | Added Code Block
        private readonly IInventoryUnitTransactionsService _inventoryunittransactionsService;
        private readonly InventoryUnitTransactionsPost _inventoryunittransactions;
        //Custom Code End

        //Custom Code Start | Replaced Code Block
        //Replaced Code Block Start
        //public InventoryUnitsService(IInventoryUnitsRepository inventoryunitsRepository, IInventoryUnitTransactionsService inventoryunittransactionsService)
        //Replaced Code Block End
        public InventoryUnitsService(IInventoryUnitsRepository inventoryunitsRepository, IInventoryUnitTransactionsService inventoryunittransactionsService, InventoryUnitTransactionsPost inventoryunittransactions)
        {
            //Custom Code End
            _inventoryunitsRepository = inventoryunitsRepository;
            //Custom Code Start | Added Code Block
            _inventoryunittransactionsService = inventoryunittransactionsService;
            _inventoryunittransactions = inventoryunittransactions;
            //Custom Code End
        }

        public InventoryUnitsPost GetPost(Int64 ixInventoryUnit) => _inventoryunitsRepository.GetPost(ixInventoryUnit);
        public InventoryUnits Get(Int64 ixInventoryUnit) => _inventoryunitsRepository.Get(ixInventoryUnit);
        public IQueryable<InventoryUnits> Index() => _inventoryunitsRepository.Index();
       public IQueryable<Statuses> selectStatuses() => _inventoryunitsRepository.selectStatuses();
        public IQueryable<Companies> selectCompanies() => _inventoryunitsRepository.selectCompanies();
        public IQueryable<Facilities> selectFacilities() => _inventoryunitsRepository.selectFacilities();
        public IQueryable<Materials> selectMaterials() => _inventoryunitsRepository.selectMaterials();
        public IQueryable<InventoryStates> selectInventoryStates() => _inventoryunitsRepository.selectInventoryStates();
        public IQueryable<HandlingUnits> selectHandlingUnits() => _inventoryunitsRepository.selectHandlingUnits();
        public IQueryable<InventoryLocations> selectInventoryLocations() => _inventoryunitsRepository.selectInventoryLocations();
       public List<KeyValuePair<Int64?, string>> selectHandlingUnitsNullable() => _inventoryunitsRepository.selectHandlingUnitsNullable();
        public bool VerifyInventoryUnitUnique(Int64 ixInventoryUnit, string sInventoryUnit) => _inventoryunitsRepository.VerifyInventoryUnitUnique(ixInventoryUnit, sInventoryUnit);
        public List<string> VerifyInventoryUnitDeleteOK(Int64 ixInventoryUnit, string sInventoryUnit) => _inventoryunitsRepository.VerifyInventoryUnitDeleteOK(ixInventoryUnit, sInventoryUnit);
        //Custom Code Start | Replaced Code Block
        //Replaced Code Block Start
        //public Task<Int64> Create(InventoryUnitsPost inventoryunitsPost)
        //Replaced Code Block End
        public Task<Int64> Create(InventoryUnitsPost inventoryunitsPost, Int64 ixInventoryUnitTransactionContext)
        {
            //Custom Code End

            // Additional validations

            // Pre-process

            // Process
            this._inventoryunitsRepository.RegisterCreate(inventoryunitsPost);
            try
            {
                this._inventoryunitsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inventoryunitsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process
            //Custom Code Start | Added Code Block
            _inventoryunittransactions.ixInventoryUnit = inventoryunitsPost.ixInventoryUnit;
            _inventoryunittransactions.ixInventoryUnitTransactionContext = ixInventoryUnitTransactionContext;
            _inventoryunittransactions.ixFacilityAfter = inventoryunitsPost.ixFacility;
            _inventoryunittransactions.ixCompanyAfter = inventoryunitsPost.ixCompany;
            _inventoryunittransactions.ixMaterialAfter = inventoryunitsPost.ixMaterial;
            _inventoryunittransactions.ixInventoryStateAfter = inventoryunitsPost.ixInventoryState;
            _inventoryunittransactions.ixHandlingUnitAfter = inventoryunitsPost.ixHandlingUnit;
            _inventoryunittransactions.ixInventoryLocationAfter = inventoryunitsPost.ixInventoryLocation;
            _inventoryunittransactions.nBaseUnitQuantityAfter = inventoryunitsPost.nBaseUnitQuantity;
            _inventoryunittransactions.sSerialNumberAfter = inventoryunitsPost.sSerialNumber;
            _inventoryunittransactions.sBatchNumberAfter = inventoryunitsPost.sBatchNumber;
            _inventoryunittransactions.dtExpireAtAfter = inventoryunitsPost.dtExpireAt;
            _inventoryunittransactions.ixStatusAfter = inventoryunitsPost.ixStatus;
            _inventoryunittransactions.UserName = inventoryunitsPost.UserName;
            this._inventoryunittransactionsService.Create(_inventoryunittransactions);
            //Custom Code End

			return Task.FromResult(inventoryunitsPost.ixInventoryUnit);

        }
        //Custom Code Start | Replaced Code Block
        //Replaced Code Block Start
        //public Task Edit(InventoryUnitsPost inventoryunitsPost)
        //Replaced Code Block End
        public Task Edit(InventoryUnitsPost inventoryunitsPost, Int64 ixInventoryUnitTransactionContext)
        {
            //Custom Code End
            // Additional validations

            // Pre-process

            //Custom Code Start | Added Code Block
            InventoryUnitsPost inventoryunitsPre = _inventoryunitsRepository.GetPost(inventoryunitsPost.ixInventoryUnit);
            _inventoryunittransactions.ixFacilityBefore = inventoryunitsPre.ixFacility;
            _inventoryunittransactions.ixCompanyBefore = inventoryunitsPre.ixCompany;
            _inventoryunittransactions.ixMaterialBefore = inventoryunitsPre.ixMaterial;
            _inventoryunittransactions.ixInventoryStateBefore = inventoryunitsPre.ixInventoryState;
            _inventoryunittransactions.ixHandlingUnitBefore = inventoryunitsPre.ixHandlingUnit;
            _inventoryunittransactions.ixInventoryLocationBefore = inventoryunitsPre.ixInventoryLocation;
            _inventoryunittransactions.nBaseUnitQuantityBefore = inventoryunitsPre.nBaseUnitQuantity;
            _inventoryunittransactions.sSerialNumberBefore = inventoryunitsPre.sSerialNumber;
            _inventoryunittransactions.sBatchNumberBefore = inventoryunitsPre.sBatchNumber;
            _inventoryunittransactions.dtExpireAtBefore = inventoryunitsPre.dtExpireAt;
            _inventoryunittransactions.ixStatusBefore = inventoryunitsPre.ixStatus;
            //Custom Code End
            // Process
            this._inventoryunitsRepository.RegisterEdit(inventoryunitsPost);
            try
            {
                this._inventoryunitsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inventoryunitsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process
            //Custom Code Start | Added Code Block
            _inventoryunittransactions.ixInventoryUnit = inventoryunitsPost.ixInventoryUnit;
            _inventoryunittransactions.ixInventoryUnitTransactionContext = ixInventoryUnitTransactionContext;
            _inventoryunittransactions.ixFacilityAfter = inventoryunitsPost.ixFacility;
            _inventoryunittransactions.ixCompanyAfter = inventoryunitsPost.ixCompany;
            _inventoryunittransactions.ixMaterialAfter = inventoryunitsPost.ixMaterial;
            _inventoryunittransactions.ixInventoryStateAfter = inventoryunitsPost.ixInventoryState;
            _inventoryunittransactions.ixHandlingUnitAfter = inventoryunitsPost.ixHandlingUnit;
            _inventoryunittransactions.ixInventoryLocationAfter = inventoryunitsPost.ixInventoryLocation;
            _inventoryunittransactions.nBaseUnitQuantityAfter = inventoryunitsPost.nBaseUnitQuantity;
            _inventoryunittransactions.sSerialNumberAfter = inventoryunitsPost.sSerialNumber;
            _inventoryunittransactions.sBatchNumberAfter = inventoryunitsPost.sBatchNumber;
            _inventoryunittransactions.dtExpireAtAfter = inventoryunitsPost.dtExpireAt;
            _inventoryunittransactions.ixStatusAfter = inventoryunitsPost.ixStatus;
            _inventoryunittransactions.UserName = inventoryunitsPost.UserName;
            this._inventoryunittransactionsService.Create(_inventoryunittransactions);
            //Custom Code End

            return Task.CompletedTask;

        }
        public Task Delete(InventoryUnitsPost inventoryunitsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._inventoryunitsRepository.RegisterDelete(inventoryunitsPost);
            try
            {
                this._inventoryunitsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inventoryunitsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  
