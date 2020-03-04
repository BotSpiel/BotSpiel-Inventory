using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;
//Custom Code Start | Added Code Block 
using BotSpiel.DataAccess.Utilities;
//Custom Code End

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
        private readonly IInventoryLocationsService _inventorylocationsService;
        private InventoryLocationsPost _inventorylocations;
        private readonly VolumeAndWeight _volumeAndWeight;
        //Custom Code End

        //Custom Code Start | Replaced Code Block
        //Replaced Code Block Start
        //public InventoryUnitsService(IInventoryUnitsRepository inventoryunitsRepository, IInventoryUnitTransactionsService inventoryunittransactionsService)
        //Replaced Code Block End
        public InventoryUnitsService(IInventoryUnitsRepository inventoryunitsRepository, IInventoryUnitTransactionsService inventoryunittransactionsService, InventoryUnitTransactionsPost inventoryunittransactions, IInventoryLocationsService inventorylocationsService, InventoryLocationsPost inventorylocations, VolumeAndWeight volumeAndWeight)
        {
            //Custom Code End
            _inventoryunitsRepository = inventoryunitsRepository;
            //Custom Code Start | Added Code Block
            _inventoryunittransactionsService = inventoryunittransactionsService;
            _inventoryunittransactions = inventoryunittransactions;
            _inventorylocationsService = inventorylocationsService;
            _inventorylocations = inventorylocations;
            _volumeAndWeight = volumeAndWeight;
            //Custom Code End
        }

        public InventoryUnitsPost GetPost(Int64 ixInventoryUnit) => _inventoryunitsRepository.GetPost(ixInventoryUnit);
        public InventoryUnits Get(Int64 ixInventoryUnit) => _inventoryunitsRepository.Get(ixInventoryUnit);
        public IQueryable<InventoryUnits> Index() => _inventoryunitsRepository.Index();
        public IQueryable<InventoryUnits> IndexDb() => _inventoryunitsRepository.IndexDb();
        public IQueryable<InventoryUnitsPost> IndexDbPost() => _inventoryunitsRepository.IndexDbPost();
        public IQueryable<Statuses> selectStatuses() => _inventoryunitsRepository.selectStatuses();
        public IQueryable<Companies> selectCompanies() => _inventoryunitsRepository.selectCompanies();
        public IQueryable<Facilities> selectFacilities() => _inventoryunitsRepository.selectFacilities();
        public IQueryable<Materials> selectMaterials() => _inventoryunitsRepository.selectMaterials();
        public IQueryable<InventoryStates> selectInventoryStates() => _inventoryunitsRepository.selectInventoryStates();
        public IQueryable<HandlingUnits> selectHandlingUnits() => _inventoryunitsRepository.selectHandlingUnits();
        public IQueryable<InventoryLocations> selectInventoryLocations() => _inventoryunitsRepository.selectInventoryLocations();
       public IQueryable<Statuses> StatusesDb() => _inventoryunitsRepository.StatusesDb();
        public IQueryable<Companies> CompaniesDb() => _inventoryunitsRepository.CompaniesDb();
        public IQueryable<Facilities> FacilitiesDb() => _inventoryunitsRepository.FacilitiesDb();
        public IQueryable<Materials> MaterialsDb() => _inventoryunitsRepository.MaterialsDb();
        public IQueryable<InventoryStates> InventoryStatesDb() => _inventoryunitsRepository.InventoryStatesDb();
        public IQueryable<HandlingUnits> HandlingUnitsDb() => _inventoryunitsRepository.HandlingUnitsDb();
        public IQueryable<InventoryLocations> InventoryLocationsDb() => _inventoryunitsRepository.InventoryLocationsDb();
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
            //Custom Code Start | Added Code Block
            _inventorylocations = _inventorylocationsService.GetPost(inventoryunitsPost.ixInventoryLocation);
            var newUtilizationPercent = _volumeAndWeight.getNewLocationUtilisationPercent(inventoryunitsPost, _inventorylocations);
            _inventorylocations.nUtilisationPercent = newUtilizationPercent;
            _inventorylocations.UserName = inventoryunitsPost.UserName;
            _inventorylocationsService.Edit(_inventorylocations);
            //Custom Code End

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

            //If bTrackUtilisation is true we update the location utilisation
            //Check if orders need to be batched (which means allocated)

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
            _inventorylocations = _inventorylocationsService.GetPost(inventoryunitsPost.ixInventoryLocation);
            var newUtilizationPercent = _volumeAndWeight.getNewLocationUtilisationPercent(inventoryunitsPost, _inventorylocations);
            _inventorylocations.nUtilisationPercent = newUtilizationPercent;
            _inventorylocations.UserName = inventoryunitsPost.UserName;
            _inventorylocationsService.Edit(_inventorylocations);

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

            if (inventoryunitsPost.ixInventoryLocation != inventoryunitsPre.ixInventoryLocation)
            {
                InventoryUnitsPost inventoryunitsPreLocChange = _inventoryunitsRepository.GetPost(inventoryunitsPost.ixInventoryUnit);
                _inventorylocations = _inventorylocationsService.GetPost(inventoryunitsPre.ixInventoryLocation);
                inventoryunitsPreLocChange.nBaseUnitQuantity = inventoryunitsPreLocChange.nBaseUnitQuantity - inventoryunitsPost.nBaseUnitQuantity;
                var newUtilizationPercentPre = _volumeAndWeight.getNewLocationUtilisationPercent(inventoryunitsPreLocChange, _inventorylocations);
                _inventorylocations.nUtilisationPercent = newUtilizationPercentPre;
                _inventorylocations.UserName = inventoryunitsPost.UserName;
                _inventorylocationsService.Edit(_inventorylocations);
            }

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
  

