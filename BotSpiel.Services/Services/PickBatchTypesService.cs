using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class PickBatchTypesService : IPickBatchTypesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IPickBatchTypesRepository _pickbatchtypesRepository;

        public PickBatchTypesService(IPickBatchTypesRepository pickbatchtypesRepository)
        {
            _pickbatchtypesRepository = pickbatchtypesRepository;
        }

        public PickBatchTypesPost GetPost(Int64 ixPickBatchType) => _pickbatchtypesRepository.GetPost(ixPickBatchType);
        public PickBatchTypes Get(Int64 ixPickBatchType) => _pickbatchtypesRepository.Get(ixPickBatchType);
        public IQueryable<PickBatchTypes> Index() => _pickbatchtypesRepository.Index();
        public IQueryable<PickBatchTypes> IndexDb() => _pickbatchtypesRepository.IndexDb();
        public bool VerifyPickBatchTypeUnique(Int64 ixPickBatchType, string sPickBatchType) => _pickbatchtypesRepository.VerifyPickBatchTypeUnique(ixPickBatchType, sPickBatchType);
        public List<string> VerifyPickBatchTypeDeleteOK(Int64 ixPickBatchType, string sPickBatchType) => _pickbatchtypesRepository.VerifyPickBatchTypeDeleteOK(ixPickBatchType, sPickBatchType);

        public Task<Int64> Create(PickBatchTypesPost pickbatchtypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._pickbatchtypesRepository.RegisterCreate(pickbatchtypesPost);
            try
            {
                this._pickbatchtypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._pickbatchtypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(pickbatchtypesPost.ixPickBatchType);

        }
        public Task Edit(PickBatchTypesPost pickbatchtypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._pickbatchtypesRepository.RegisterEdit(pickbatchtypesPost);
            try
            {
                this._pickbatchtypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._pickbatchtypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(PickBatchTypesPost pickbatchtypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._pickbatchtypesRepository.RegisterDelete(pickbatchtypesPost);
            try
            {
                this._pickbatchtypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._pickbatchtypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

