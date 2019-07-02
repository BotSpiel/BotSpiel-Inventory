using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class HandlingUnitsShippingService : IHandlingUnitsShippingService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IHandlingUnitsShippingRepository _handlingunitsshippingRepository;

        public HandlingUnitsShippingService(IHandlingUnitsShippingRepository handlingunitsshippingRepository)
        {
            _handlingunitsshippingRepository = handlingunitsshippingRepository;
        }

        public HandlingUnitsShippingPost GetPost(Int64 ixHandlingUnitShipping) => _handlingunitsshippingRepository.GetPost(ixHandlingUnitShipping);
        public HandlingUnitsShipping Get(Int64 ixHandlingUnitShipping) => _handlingunitsshippingRepository.Get(ixHandlingUnitShipping);
        public IQueryable<HandlingUnitsShipping> Index() => _handlingunitsshippingRepository.Index();
       public IQueryable<Statuses> selectStatuses() => _handlingunitsshippingRepository.selectStatuses();
        public IQueryable<HandlingUnits> selectHandlingUnits() => _handlingunitsshippingRepository.selectHandlingUnits();
        public bool VerifyHandlingUnitShippingUnique(Int64 ixHandlingUnitShipping, string sHandlingUnitShipping) => _handlingunitsshippingRepository.VerifyHandlingUnitShippingUnique(ixHandlingUnitShipping, sHandlingUnitShipping);
        public List<string> VerifyHandlingUnitShippingDeleteOK(Int64 ixHandlingUnitShipping, string sHandlingUnitShipping) => _handlingunitsshippingRepository.VerifyHandlingUnitShippingDeleteOK(ixHandlingUnitShipping, sHandlingUnitShipping);

        public Task<Int64> Create(HandlingUnitsShippingPost handlingunitsshippingPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._handlingunitsshippingRepository.RegisterCreate(handlingunitsshippingPost);
            try
            {
                this._handlingunitsshippingRepository.Commit();
            }
            catch(Exception ex)
            {
                this._handlingunitsshippingRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(handlingunitsshippingPost.ixHandlingUnitShipping);

        }
        public Task Edit(HandlingUnitsShippingPost handlingunitsshippingPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._handlingunitsshippingRepository.RegisterEdit(handlingunitsshippingPost);
            try
            {
                this._handlingunitsshippingRepository.Commit();
            }
            catch(Exception ex)
            {
                this._handlingunitsshippingRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(HandlingUnitsShippingPost handlingunitsshippingPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._handlingunitsshippingRepository.RegisterDelete(handlingunitsshippingPost);
            try
            {
                this._handlingunitsshippingRepository.Commit();
            }
            catch(Exception ex)
            {
                this._handlingunitsshippingRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

