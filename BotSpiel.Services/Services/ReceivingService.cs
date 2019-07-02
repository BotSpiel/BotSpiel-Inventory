using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


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

        public ReceivingService(IReceivingRepository receivingRepository)
        {
            _receivingRepository = receivingRepository;
        }

        public ReceivingPost GetPost(Int64 ixReceipt) => _receivingRepository.GetPost(ixReceipt);
        public Receiving Get(Int64 ixReceipt) => _receivingRepository.Get(ixReceipt);
        public IQueryable<Receiving> Index() => _receivingRepository.Index();
       public IQueryable<Statuses> selectStatuses() => _receivingRepository.selectStatuses();
        public IQueryable<Materials> selectMaterials() => _receivingRepository.selectMaterials();
        public IQueryable<HandlingUnits> selectHandlingUnits() => _receivingRepository.selectHandlingUnits();
        public IQueryable<HandlingUnitTypes> selectHandlingUnitTypes() => _receivingRepository.selectHandlingUnitTypes();
        public IQueryable<InventoryLocations> selectInventoryLocations() => _receivingRepository.selectInventoryLocations();
        public IQueryable<MaterialHandlingUnitConfigurations> selectMaterialHandlingUnitConfigurations() => _receivingRepository.selectMaterialHandlingUnitConfigurations();
        public IQueryable<InboundOrders> selectInboundOrders() => _receivingRepository.selectInboundOrders();
       public List<KeyValuePair<Int64?, string>> selectHandlingUnitTypesNullable() => _receivingRepository.selectHandlingUnitTypesNullable();
        public List<KeyValuePair<Int64?, string>> selectMaterialHandlingUnitConfigurationsNullable() => _receivingRepository.selectMaterialHandlingUnitConfigurationsNullable();
        public bool VerifyReceiptUnique(Int64 ixReceipt, string sReceipt) => _receivingRepository.VerifyReceiptUnique(ixReceipt, sReceipt);
        public List<string> VerifyReceiptDeleteOK(Int64 ixReceipt, string sReceipt) => _receivingRepository.VerifyReceiptDeleteOK(ixReceipt, sReceipt);

        public Task<Int64> Create(ReceivingPost receivingPost)
        {
            // Additional validations

            // Pre-process

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
    }
}
  

