using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class BaySequenceTypesService : IBaySequenceTypesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IBaySequenceTypesRepository _baysequencetypesRepository;

        public BaySequenceTypesService(IBaySequenceTypesRepository baysequencetypesRepository)
        {
            _baysequencetypesRepository = baysequencetypesRepository;
        }

        public BaySequenceTypesPost GetPost(Int64 ixBaySequenceType) => _baysequencetypesRepository.GetPost(ixBaySequenceType);
        public BaySequenceTypes Get(Int64 ixBaySequenceType) => _baysequencetypesRepository.Get(ixBaySequenceType);
        public IQueryable<BaySequenceTypes> Index() => _baysequencetypesRepository.Index();
        public IQueryable<BaySequenceTypes> IndexDb() => _baysequencetypesRepository.IndexDb();
        public bool VerifyBaySequenceTypeUnique(Int64 ixBaySequenceType, string sBaySequenceType) => _baysequencetypesRepository.VerifyBaySequenceTypeUnique(ixBaySequenceType, sBaySequenceType);
        public List<string> VerifyBaySequenceTypeDeleteOK(Int64 ixBaySequenceType, string sBaySequenceType) => _baysequencetypesRepository.VerifyBaySequenceTypeDeleteOK(ixBaySequenceType, sBaySequenceType);

        public Task<Int64> Create(BaySequenceTypesPost baysequencetypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._baysequencetypesRepository.RegisterCreate(baysequencetypesPost);
            try
            {
                this._baysequencetypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._baysequencetypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(baysequencetypesPost.ixBaySequenceType);

        }
        public Task Edit(BaySequenceTypesPost baysequencetypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._baysequencetypesRepository.RegisterEdit(baysequencetypesPost);
            try
            {
                this._baysequencetypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._baysequencetypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(BaySequenceTypesPost baysequencetypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._baysequencetypesRepository.RegisterDelete(baysequencetypesPost);
            try
            {
                this._baysequencetypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._baysequencetypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

