using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class InventoryUnitTransactionContextsService : IInventoryUnitTransactionContextsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IInventoryUnitTransactionContextsRepository _inventoryunittransactioncontextsRepository;

        public InventoryUnitTransactionContextsService(IInventoryUnitTransactionContextsRepository inventoryunittransactioncontextsRepository)
        {
            _inventoryunittransactioncontextsRepository = inventoryunittransactioncontextsRepository;
        }

        public InventoryUnitTransactionContextsPost GetPost(Int64 ixInventoryUnitTransactionContext) => _inventoryunittransactioncontextsRepository.GetPost(ixInventoryUnitTransactionContext);
        public InventoryUnitTransactionContexts Get(Int64 ixInventoryUnitTransactionContext) => _inventoryunittransactioncontextsRepository.Get(ixInventoryUnitTransactionContext);
        public IQueryable<InventoryUnitTransactionContexts> Index() => _inventoryunittransactioncontextsRepository.Index();
        public bool VerifyInventoryUnitTransactionContextUnique(Int64 ixInventoryUnitTransactionContext, string sInventoryUnitTransactionContext) => _inventoryunittransactioncontextsRepository.VerifyInventoryUnitTransactionContextUnique(ixInventoryUnitTransactionContext, sInventoryUnitTransactionContext);
        public List<string> VerifyInventoryUnitTransactionContextDeleteOK(Int64 ixInventoryUnitTransactionContext, string sInventoryUnitTransactionContext) => _inventoryunittransactioncontextsRepository.VerifyInventoryUnitTransactionContextDeleteOK(ixInventoryUnitTransactionContext, sInventoryUnitTransactionContext);

        public Task<Int64> Create(InventoryUnitTransactionContextsPost inventoryunittransactioncontextsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._inventoryunittransactioncontextsRepository.RegisterCreate(inventoryunittransactioncontextsPost);
            try
            {
                this._inventoryunittransactioncontextsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inventoryunittransactioncontextsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(inventoryunittransactioncontextsPost.ixInventoryUnitTransactionContext);

        }
        public Task Edit(InventoryUnitTransactionContextsPost inventoryunittransactioncontextsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._inventoryunittransactioncontextsRepository.RegisterEdit(inventoryunittransactioncontextsPost);
            try
            {
                this._inventoryunittransactioncontextsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inventoryunittransactioncontextsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(InventoryUnitTransactionContextsPost inventoryunittransactioncontextsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._inventoryunittransactioncontextsRepository.RegisterDelete(inventoryunittransactioncontextsPost);
            try
            {
                this._inventoryunittransactioncontextsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inventoryunittransactioncontextsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

