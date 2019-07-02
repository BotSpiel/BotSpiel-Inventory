using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class HandlingUnitTypesService : IHandlingUnitTypesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IHandlingUnitTypesRepository _handlingunittypesRepository;

        public HandlingUnitTypesService(IHandlingUnitTypesRepository handlingunittypesRepository)
        {
            _handlingunittypesRepository = handlingunittypesRepository;
        }

        public HandlingUnitTypesPost GetPost(Int64 ixHandlingUnitType) => _handlingunittypesRepository.GetPost(ixHandlingUnitType);
        public HandlingUnitTypes Get(Int64 ixHandlingUnitType) => _handlingunittypesRepository.Get(ixHandlingUnitType);
        public IQueryable<HandlingUnitTypes> Index() => _handlingunittypesRepository.Index();
        public bool VerifyHandlingUnitTypeUnique(Int64 ixHandlingUnitType, string sHandlingUnitType) => _handlingunittypesRepository.VerifyHandlingUnitTypeUnique(ixHandlingUnitType, sHandlingUnitType);
        public List<string> VerifyHandlingUnitTypeDeleteOK(Int64 ixHandlingUnitType, string sHandlingUnitType) => _handlingunittypesRepository.VerifyHandlingUnitTypeDeleteOK(ixHandlingUnitType, sHandlingUnitType);

        public Task<Int64> Create(HandlingUnitTypesPost handlingunittypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._handlingunittypesRepository.RegisterCreate(handlingunittypesPost);
            try
            {
                this._handlingunittypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._handlingunittypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(handlingunittypesPost.ixHandlingUnitType);

        }
        public Task Edit(HandlingUnitTypesPost handlingunittypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._handlingunittypesRepository.RegisterEdit(handlingunittypesPost);
            try
            {
                this._handlingunittypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._handlingunittypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(HandlingUnitTypesPost handlingunittypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._handlingunittypesRepository.RegisterDelete(handlingunittypesPost);
            try
            {
                this._handlingunittypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._handlingunittypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

