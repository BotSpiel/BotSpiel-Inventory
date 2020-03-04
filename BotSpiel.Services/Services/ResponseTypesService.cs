using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class ResponseTypesService : IResponseTypesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IResponseTypesRepository _responsetypesRepository;

        public ResponseTypesService(IResponseTypesRepository responsetypesRepository)
        {
            _responsetypesRepository = responsetypesRepository;
        }

        public ResponseTypesPost GetPost(Int64 ixResponseType) => _responsetypesRepository.GetPost(ixResponseType);
        public ResponseTypes Get(Int64 ixResponseType) => _responsetypesRepository.Get(ixResponseType);
        public IQueryable<ResponseTypes> Index() => _responsetypesRepository.Index();
        public IQueryable<ResponseTypes> IndexDb() => _responsetypesRepository.IndexDb();
        public List<string> VerifyResponseTypeDeleteOK(Int64 ixResponseType, string sResponseType) => _responsetypesRepository.VerifyResponseTypeDeleteOK(ixResponseType, sResponseType);

        public Task<Int64> Create(ResponseTypesPost responsetypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._responsetypesRepository.RegisterCreate(responsetypesPost);
            try
            {
                this._responsetypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._responsetypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(responsetypesPost.ixResponseType);

        }
        public Task Edit(ResponseTypesPost responsetypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._responsetypesRepository.RegisterEdit(responsetypesPost);
            try
            {
                this._responsetypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._responsetypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(ResponseTypesPost responsetypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._responsetypesRepository.RegisterDelete(responsetypesPost);
            try
            {
                this._responsetypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._responsetypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

