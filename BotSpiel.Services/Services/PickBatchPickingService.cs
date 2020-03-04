using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class PickBatchPickingService : IPickBatchPickingService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IPickBatchPickingRepository _pickbatchpickingRepository;

        public PickBatchPickingService(IPickBatchPickingRepository pickbatchpickingRepository)
        {
            _pickbatchpickingRepository = pickbatchpickingRepository;
        }

        public PickBatchPickingPost GetPost(Int64 ixPickBatchPick) => _pickbatchpickingRepository.GetPost(ixPickBatchPick);
        public PickBatchPicking Get(Int64 ixPickBatchPick) => _pickbatchpickingRepository.Get(ixPickBatchPick);
        public IQueryable<PickBatchPicking> Index() => _pickbatchpickingRepository.Index();
        public IQueryable<PickBatchPicking> IndexDb() => _pickbatchpickingRepository.IndexDb();
       public IQueryable<InventoryUnits> selectInventoryUnits() => _pickbatchpickingRepository.selectInventoryUnits();
        public IQueryable<HandlingUnits> selectHandlingUnits() => _pickbatchpickingRepository.selectHandlingUnits();
        public IQueryable<PickBatches> selectPickBatches() => _pickbatchpickingRepository.selectPickBatches();
       public IQueryable<InventoryUnits> InventoryUnitsDb() => _pickbatchpickingRepository.InventoryUnitsDb();
        public IQueryable<HandlingUnits> HandlingUnitsDb() => _pickbatchpickingRepository.HandlingUnitsDb();
        public IQueryable<PickBatches> PickBatchesDb() => _pickbatchpickingRepository.PickBatchesDb();
        public List<string> VerifyPickBatchPickDeleteOK(Int64 ixPickBatchPick, string sPickBatchPick) => _pickbatchpickingRepository.VerifyPickBatchPickDeleteOK(ixPickBatchPick, sPickBatchPick);

        public Task<Int64> Create(PickBatchPickingPost pickbatchpickingPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._pickbatchpickingRepository.RegisterCreate(pickbatchpickingPost);
            try
            {
                this._pickbatchpickingRepository.Commit();
            }
            catch(Exception ex)
            {
                this._pickbatchpickingRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(pickbatchpickingPost.ixPickBatchPick);

        }
        public Task Edit(PickBatchPickingPost pickbatchpickingPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._pickbatchpickingRepository.RegisterEdit(pickbatchpickingPost);
            try
            {
                this._pickbatchpickingRepository.Commit();
            }
            catch(Exception ex)
            {
                this._pickbatchpickingRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(PickBatchPickingPost pickbatchpickingPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._pickbatchpickingRepository.RegisterDelete(pickbatchpickingPost);
            try
            {
                this._pickbatchpickingRepository.Commit();
            }
            catch(Exception ex)
            {
                this._pickbatchpickingRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

