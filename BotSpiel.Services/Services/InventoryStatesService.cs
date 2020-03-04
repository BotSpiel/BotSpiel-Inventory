using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class InventoryStatesService : IInventoryStatesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IInventoryStatesRepository _inventorystatesRepository;

        public InventoryStatesService(IInventoryStatesRepository inventorystatesRepository)
        {
            _inventorystatesRepository = inventorystatesRepository;
        }

        public InventoryStatesPost GetPost(Int64 ixInventoryState) => _inventorystatesRepository.GetPost(ixInventoryState);
        public InventoryStates Get(Int64 ixInventoryState) => _inventorystatesRepository.Get(ixInventoryState);
        public IQueryable<InventoryStates> Index() => _inventorystatesRepository.Index();
        public IQueryable<InventoryStates> IndexDb() => _inventorystatesRepository.IndexDb();
        public bool VerifyInventoryStateUnique(Int64 ixInventoryState, string sInventoryState) => _inventorystatesRepository.VerifyInventoryStateUnique(ixInventoryState, sInventoryState);
        public List<string> VerifyInventoryStateDeleteOK(Int64 ixInventoryState, string sInventoryState) => _inventorystatesRepository.VerifyInventoryStateDeleteOK(ixInventoryState, sInventoryState);

        public Task<Int64> Create(InventoryStatesPost inventorystatesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._inventorystatesRepository.RegisterCreate(inventorystatesPost);
            try
            {
                this._inventorystatesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inventorystatesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(inventorystatesPost.ixInventoryState);

        }
        public Task Edit(InventoryStatesPost inventorystatesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._inventorystatesRepository.RegisterEdit(inventorystatesPost);
            try
            {
                this._inventorystatesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inventorystatesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(InventoryStatesPost inventorystatesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._inventorystatesRepository.RegisterDelete(inventorystatesPost);
            try
            {
                this._inventorystatesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inventorystatesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

