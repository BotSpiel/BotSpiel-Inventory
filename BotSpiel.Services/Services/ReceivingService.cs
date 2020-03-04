using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;
//Custom Code Start | Added Code Block 
using BotSpiel.Services.Utilities;
using BotSpiel.DataAccess.Utilities;
//Custom Code End

namespace BotSpiel.Services
{

    public class ReceivingService : IReceivingService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IReceivingRepository _receivingRepository;
        //Custom Code Start | Added Code Block 
        private readonly IHandlingUnitsService _handlingunitsService;
        private readonly IInventoryUnitsService _inventoryunitsService;
        private readonly IInventoryUnitTransactionContextsService _inventoryunittransactioncontextsService;
        private readonly IInboundOrdersService _inboundordersService;
        private readonly IInboundOrderLinesService _inboundorderlinesService;
        private readonly CommonLookUps _commonLookUps;
        private Int64 _ixInventoryUnitTransactionContext;
        //Custom Code End
        //Custom Code Start | Replaced Code Block
        //Replaced Code Block Start
        //public ReceivingService(IReceivingRepository receivingRepository)
        //Replaced Code Block End
        public ReceivingService(IReceivingRepository receivingRepository, IHandlingUnitsService handlingunitsService, IInventoryUnitsService inventoryunitsService, IInventoryUnitTransactionContextsService inventoryunittransactioncontextsService, IInboundOrdersService inboundordersService, IInboundOrderLinesService inboundorderlinesService, CommonLookUps commonLookUps)
        //Custom Code End
        {
            _receivingRepository = receivingRepository;
            //Custom Code Start | Added Code Block 
            _handlingunitsService = handlingunitsService;
            _inventoryunitsService = inventoryunitsService;
            _inventoryunittransactioncontextsService = inventoryunittransactioncontextsService;
            _ixInventoryUnitTransactionContext = _inventoryunittransactioncontextsService.IndexDb().Where(x => x.sInventoryUnitTransactionContext == "Receiving").Select(x => x.ixInventoryUnitTransactionContext).FirstOrDefault();
            _inboundordersService = inboundordersService;
            _inboundorderlinesService = inboundorderlinesService;
            _commonLookUps = commonLookUps;
            //Custom Code End
        }

        public ReceivingPost GetPost(Int64 ixReceipt) => _receivingRepository.GetPost(ixReceipt);
        public Receiving Get(Int64 ixReceipt) => _receivingRepository.Get(ixReceipt);
        public IQueryable<Receiving> Index() => _receivingRepository.Index();
        public IQueryable<Receiving> IndexDb() => _receivingRepository.IndexDb();
       public IQueryable<Statuses> selectStatuses() => _receivingRepository.selectStatuses();
        public IQueryable<Materials> selectMaterials() => _receivingRepository.selectMaterials();
        public IQueryable<InventoryStates> selectInventoryStates() => _receivingRepository.selectInventoryStates();
        public IQueryable<HandlingUnits> selectHandlingUnits() => _receivingRepository.selectHandlingUnits();
        public IQueryable<HandlingUnitTypes> selectHandlingUnitTypes() => _receivingRepository.selectHandlingUnitTypes();
        public IQueryable<InventoryLocations> selectInventoryLocations() => _receivingRepository.selectInventoryLocations();
        public IQueryable<MaterialHandlingUnitConfigurations> selectMaterialHandlingUnitConfigurations() => _receivingRepository.selectMaterialHandlingUnitConfigurations();
        public IQueryable<InboundOrders> selectInboundOrders() => _receivingRepository.selectInboundOrders();
        //Custom Code Start | Added Code Block 
        public List<KeyValuePair<Int64?, string>> selectInboundOrdersFirst() => _receivingRepository.selectInboundOrdersFirst();
        //Custom Code End
        public IQueryable<Statuses> StatusesDb() => _receivingRepository.StatusesDb();
        public IQueryable<Materials> MaterialsDb() => _receivingRepository.MaterialsDb();
        public IQueryable<InventoryStates> InventoryStatesDb() => _receivingRepository.InventoryStatesDb();
        public IQueryable<HandlingUnits> HandlingUnitsDb() => _receivingRepository.HandlingUnitsDb();
        public IQueryable<HandlingUnitTypes> HandlingUnitTypesDb() => _receivingRepository.HandlingUnitTypesDb();
        public IQueryable<InventoryLocations> InventoryLocationsDb() => _receivingRepository.InventoryLocationsDb();
        public IQueryable<MaterialHandlingUnitConfigurations> MaterialHandlingUnitConfigurationsDb() => _receivingRepository.MaterialHandlingUnitConfigurationsDb();
        public IQueryable<InboundOrders> InboundOrdersDb() => _receivingRepository.InboundOrdersDb();
       public List<KeyValuePair<Int64?, string>> selectHandlingUnitTypesNullable() => _receivingRepository.selectHandlingUnitTypesNullable();
        public List<KeyValuePair<Int64?, string>> selectMaterialHandlingUnitConfigurationsNullable() => _receivingRepository.selectMaterialHandlingUnitConfigurationsNullable();
        public bool VerifyReceiptUnique(Int64 ixReceipt, string sReceipt) => _receivingRepository.VerifyReceiptUnique(ixReceipt, sReceipt);
        public List<string> VerifyReceiptDeleteOK(Int64 ixReceipt, string sReceipt) => _receivingRepository.VerifyReceiptDeleteOK(ixReceipt, sReceipt);

        public Task<Int64> Create(ReceivingPost receivingPost)
        {
            // Additional validations

            // Pre-process

            //Custom Code Start | Added Code Block 
            //If the handling unit does not exist we create it
            if (!HandlingUnitsDb().Where(x => x.sHandlingUnit == receivingPost.sReceipt.Trim()).Any())
            {
                HandlingUnitsPost handlingUnitsPost = new HandlingUnitsPost();
                handlingUnitsPost.sHandlingUnit = receivingPost.sReceipt;
                handlingUnitsPost.ixHandlingUnitType = receivingPost.ixHandlingUnitType ?? 0;
                handlingUnitsPost.UserName = receivingPost.UserName;
                receivingPost.ixHandlingUnit = _handlingunitsService.Create(handlingUnitsPost).Result;
            }
            else
            {
                receivingPost.ixHandlingUnit = HandlingUnitsDb().Where(x => x.sHandlingUnit == receivingPost.sReceipt.Trim()).Select(x => x.ixHandlingUnit).FirstOrDefault();
            }

            InventoryUnitsPost inventoryUnit = new InventoryUnitsPost();
            var inboundOrder = _inboundordersService.GetPost(receivingPost.ixInboundOrder);
            inventoryUnit.ixFacility = inboundOrder.ixFacility;
            inventoryUnit.ixCompany = inboundOrder.ixCompany;
            inventoryUnit.ixMaterial = receivingPost.ixMaterial;
            inventoryUnit.ixInventoryState = receivingPost.ixInventoryState;
            inventoryUnit.ixHandlingUnit = receivingPost.ixHandlingUnit;
            inventoryUnit.ixInventoryLocation = receivingPost.ixInventoryLocation;
            inventoryUnit.sSerialNumber = receivingPost.sSerialNumber;
            inventoryUnit.sBatchNumber = receivingPost.sBatchNumber;
            inventoryUnit.dtExpireAt = receivingPost.dtExpireAt;
            inventoryUnit.UserName = receivingPost.UserName;

            if ((inventoryUnit.sSerialNumber ?? "").Trim() != "")
            {
                //We create an iu
                inventoryUnit.nBaseUnitQuantity = receivingPost.nBaseUnitQuantityReceived;
                _inventoryunitsService.Create(inventoryUnit, _ixInventoryUnitTransactionContext);
            }
            else if (_inventoryunitsService.IndexDb().Where(x =>
                x.ixFacility == inventoryUnit.ixFacility &&
                x.ixCompany == inventoryUnit.ixCompany &&
                x.ixMaterial == inventoryUnit.ixMaterial &&
                x.ixInventoryState == inventoryUnit.ixInventoryState &&
                x.ixHandlingUnit == inventoryUnit.ixHandlingUnit &&
                x.ixInventoryLocation == inventoryUnit.ixInventoryLocation &&
                x.sBatchNumber == inventoryUnit.sBatchNumber &&
                x.dtExpireAt == inventoryUnit.dtExpireAt && x.ixStatus == 5
                ).Select(x => x.ixInventoryUnit).Any()
                )
            {
                //We edit the iu
                inventoryUnit.ixInventoryUnit = _inventoryunitsService.IndexDb().Where(x =>
                x.ixFacility == inventoryUnit.ixFacility &&
                x.ixCompany == inventoryUnit.ixCompany &&
                x.ixMaterial == inventoryUnit.ixMaterial &&
                x.ixInventoryState == inventoryUnit.ixInventoryState &&
                x.ixHandlingUnit == inventoryUnit.ixHandlingUnit &&
                x.ixInventoryLocation == inventoryUnit.ixInventoryLocation &&
                x.sBatchNumber == inventoryUnit.sBatchNumber &&
                x.dtExpireAt == inventoryUnit.dtExpireAt && x.ixStatus == 5
                ).Select(x => x.ixInventoryUnit).FirstOrDefault();
                inventoryUnit.nBaseUnitQuantity = _inventoryunitsService.GetPost(inventoryUnit.ixInventoryUnit).nBaseUnitQuantity + receivingPost.nBaseUnitQuantityReceived;
                _inventoryunitsService.Edit(inventoryUnit, _ixInventoryUnitTransactionContext);
            }
            else
            {
                //We create an iu
                inventoryUnit.nBaseUnitQuantity = receivingPost.nBaseUnitQuantityReceived;
                _inventoryunitsService.Create(inventoryUnit, _ixInventoryUnitTransactionContext);
            }


            //Custom Code End

            // Process
            this._receivingRepository.RegisterCreate(receivingPost);
            try
            {
                this._receivingRepository.Commit();
            }
            catch(Exception ex)
            {
                this._receivingRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            //Custom Code Start | Added Code Block 
            //Now we update the inbound order lines - for now we apply entire qty to the matching line 
            var _ixInboundOrderLine = _inboundorderlinesService.IndexDb().Where(x => x.ixInboundOrder == receivingPost.ixInboundOrder && x.ixMaterial == receivingPost.ixMaterial).Select(x => x.ixInboundOrderLine).FirstOrDefault();
            var inboundOrderLine = _inboundorderlinesService.GetPost(_ixInboundOrderLine);
            inboundOrderLine.nBaseUnitQuantityReceived += receivingPost.nBaseUnitQuantityReceived;
            inboundOrderLine.UserName = receivingPost.UserName;
            if ((inboundOrderLine.nBaseUnitQuantityExpected - inboundOrderLine.nBaseUnitQuantityReceived) == 0)
            {
                inboundOrderLine.ixStatus = _commonLookUps.getStatuses().Where(x => x.sStatus == "Complete").Select(x => x.ixStatus).FirstOrDefault();
            }
            _inboundorderlinesService.Edit(inboundOrderLine);

            //If there are no open qtys we set the inbound order/line status to complete
            if (_inboundorderlinesService.IndexDb().Where(x => x.ixInboundOrder == receivingPost.ixInboundOrder).Select(x => x.nBaseUnitQuantityExpected - x.nBaseUnitQuantityReceived).Sum() == 0)
            {
                var inboundorder = _inboundordersService.GetPost(receivingPost.ixInboundOrder);
                inboundorder.ixStatus = _commonLookUps.getStatuses().Where(x => x.sStatus == "Complete").Select(x => x.ixStatus).FirstOrDefault();
                _inboundordersService.Edit(inboundorder);
            }

            //Custom Code End

            return Task.FromResult(receivingPost.ixReceipt);

        }
        public Task Edit(ReceivingPost receivingPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._receivingRepository.RegisterEdit(receivingPost);
            try
            {
                this._receivingRepository.Commit();
            }
            catch(Exception ex)
            {
                this._receivingRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(ReceivingPost receivingPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._receivingRepository.RegisterDelete(receivingPost);
            try
            {
                this._receivingRepository.Commit();
            }
            catch(Exception ex)
            {
                this._receivingRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }

        //Custom Code Start | Added Code Block 
        public List<KeyValuePair<Int64?, string>> selectEmptyMaterialsDropdown() => _receivingRepository.selectEmptyMaterialsDropdown();
        //Custom Code End
    }
}
  

