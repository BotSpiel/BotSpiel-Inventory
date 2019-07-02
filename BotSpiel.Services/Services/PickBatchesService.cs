using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class PickBatchesService : IPickBatchesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IPickBatchesRepository _pickbatchesRepository;

        public PickBatchesService(IPickBatchesRepository pickbatchesRepository)
        {
            _pickbatchesRepository = pickbatchesRepository;
        }

        public PickBatchesPost GetPost(Int64 ixPickBatch) => _pickbatchesRepository.GetPost(ixPickBatch);
        public PickBatches Get(Int64 ixPickBatch) => _pickbatchesRepository.Get(ixPickBatch);
        public IQueryable<PickBatches> Index() => _pickbatchesRepository.Index();
       public IQueryable<Statuses> selectStatuses() => _pickbatchesRepository.selectStatuses();
        public IQueryable<PickBatchTypes> selectPickBatchTypes() => _pickbatchesRepository.selectPickBatchTypes();
        public bool VerifyPickBatchUnique(Int64 ixPickBatch, string sPickBatch) => _pickbatchesRepository.VerifyPickBatchUnique(ixPickBatch, sPickBatch);
        public List<string> VerifyPickBatchDeleteOK(Int64 ixPickBatch, string sPickBatch) => _pickbatchesRepository.VerifyPickBatchDeleteOK(ixPickBatch, sPickBatch);

        public Task<Int64> Create(PickBatchesPost pickbatchesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._pickbatchesRepository.RegisterCreate(pickbatchesPost);
            try
            {
                this._pickbatchesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._pickbatchesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(pickbatchesPost.ixPickBatch);

        }
        public Task Edit(PickBatchesPost pickbatchesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._pickbatchesRepository.RegisterEdit(pickbatchesPost);
            try
            {
                this._pickbatchesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._pickbatchesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(PickBatchesPost pickbatchesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._pickbatchesRepository.RegisterDelete(pickbatchesPost);
            try
            {
                this._pickbatchesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._pickbatchesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

