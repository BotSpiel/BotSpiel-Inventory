using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class PutAwayHandlingUnitsService : IPutAwayHandlingUnitsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IPutAwayHandlingUnitsRepository _putawayhandlingunitsRepository;

        public PutAwayHandlingUnitsService(IPutAwayHandlingUnitsRepository putawayhandlingunitsRepository)
        {
            _putawayhandlingunitsRepository = putawayhandlingunitsRepository;
        }

        public PutAwayHandlingUnitsPost GetPost(Int64 ixPutAwayHandlingUnit) => _putawayhandlingunitsRepository.GetPost(ixPutAwayHandlingUnit);
        public PutAwayHandlingUnits Get(Int64 ixPutAwayHandlingUnit) => _putawayhandlingunitsRepository.Get(ixPutAwayHandlingUnit);
        public IQueryable<PutAwayHandlingUnits> Index() => _putawayhandlingunitsRepository.Index();
        public IQueryable<PutAwayHandlingUnits> IndexDb() => _putawayhandlingunitsRepository.IndexDb();
       public IQueryable<HandlingUnits> selectHandlingUnits() => _putawayhandlingunitsRepository.selectHandlingUnits();
        public IQueryable<InventoryLocations> selectInventoryLocations() => _putawayhandlingunitsRepository.selectInventoryLocations();
       public IQueryable<HandlingUnits> HandlingUnitsDb() => _putawayhandlingunitsRepository.HandlingUnitsDb();
        public IQueryable<InventoryLocations> InventoryLocationsDb() => _putawayhandlingunitsRepository.InventoryLocationsDb();
        public List<string> VerifyPutAwayHandlingUnitDeleteOK(Int64 ixPutAwayHandlingUnit, string sPutAwayHandlingUnit) => _putawayhandlingunitsRepository.VerifyPutAwayHandlingUnitDeleteOK(ixPutAwayHandlingUnit, sPutAwayHandlingUnit);

        public Task<Int64> Create(PutAwayHandlingUnitsPost putawayhandlingunitsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._putawayhandlingunitsRepository.RegisterCreate(putawayhandlingunitsPost);
            try
            {
                this._putawayhandlingunitsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._putawayhandlingunitsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(putawayhandlingunitsPost.ixPutAwayHandlingUnit);

        }
        public Task Edit(PutAwayHandlingUnitsPost putawayhandlingunitsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._putawayhandlingunitsRepository.RegisterEdit(putawayhandlingunitsPost);
            try
            {
                this._putawayhandlingunitsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._putawayhandlingunitsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(PutAwayHandlingUnitsPost putawayhandlingunitsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._putawayhandlingunitsRepository.RegisterDelete(putawayhandlingunitsPost);
            try
            {
                this._putawayhandlingunitsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._putawayhandlingunitsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

