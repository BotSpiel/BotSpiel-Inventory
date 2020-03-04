using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class HandlingUnitsService : IHandlingUnitsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IHandlingUnitsRepository _handlingunitsRepository;

        public HandlingUnitsService(IHandlingUnitsRepository handlingunitsRepository)
        {
            _handlingunitsRepository = handlingunitsRepository;
        }

        public HandlingUnitsPost GetPost(Int64 ixHandlingUnit) => _handlingunitsRepository.GetPost(ixHandlingUnit);
        public HandlingUnits Get(Int64 ixHandlingUnit) => _handlingunitsRepository.Get(ixHandlingUnit);
        public IQueryable<HandlingUnits> Index() => _handlingunitsRepository.Index();
        public IQueryable<HandlingUnits> IndexDb() => _handlingunitsRepository.IndexDb();
       public IQueryable<Statuses> selectStatuses() => _handlingunitsRepository.selectStatuses();
        public IQueryable<UnitsOfMeasurement> selectUnitsOfMeasurement() => _handlingunitsRepository.selectUnitsOfMeasurement();
        public IQueryable<Materials> selectMaterials() => _handlingunitsRepository.selectMaterials();
        public IQueryable<HandlingUnits> selectHandlingUnits() => _handlingunitsRepository.selectHandlingUnits();
        public IQueryable<HandlingUnitTypes> selectHandlingUnitTypes() => _handlingunitsRepository.selectHandlingUnitTypes();
        public IQueryable<MaterialHandlingUnitConfigurations> selectMaterialHandlingUnitConfigurations() => _handlingunitsRepository.selectMaterialHandlingUnitConfigurations();
       public IQueryable<Statuses> StatusesDb() => _handlingunitsRepository.StatusesDb();
        public IQueryable<UnitsOfMeasurement> UnitsOfMeasurementDb() => _handlingunitsRepository.UnitsOfMeasurementDb();
        public IQueryable<Materials> MaterialsDb() => _handlingunitsRepository.MaterialsDb();
        public IQueryable<HandlingUnits> HandlingUnitsDb() => _handlingunitsRepository.HandlingUnitsDb();
        public IQueryable<HandlingUnitTypes> HandlingUnitTypesDb() => _handlingunitsRepository.HandlingUnitTypesDb();
        public IQueryable<MaterialHandlingUnitConfigurations> MaterialHandlingUnitConfigurationsDb() => _handlingunitsRepository.MaterialHandlingUnitConfigurationsDb();
       public List<KeyValuePair<Int64?, string>> selectStatusesNullable() => _handlingunitsRepository.selectStatusesNullable();
        public List<KeyValuePair<Int64?, string>> selectUnitsOfMeasurementNullable() => _handlingunitsRepository.selectUnitsOfMeasurementNullable();
        public List<KeyValuePair<Int64?, string>> selectMaterialsNullable() => _handlingunitsRepository.selectMaterialsNullable();
        public List<KeyValuePair<Int64?, string>> selectHandlingUnitsNullable() => _handlingunitsRepository.selectHandlingUnitsNullable();
        public List<KeyValuePair<Int64?, string>> selectMaterialHandlingUnitConfigurationsNullable() => _handlingunitsRepository.selectMaterialHandlingUnitConfigurationsNullable();
        public bool VerifyHandlingUnitUnique(Int64 ixHandlingUnit, string sHandlingUnit) => _handlingunitsRepository.VerifyHandlingUnitUnique(ixHandlingUnit, sHandlingUnit);
        public List<string> VerifyHandlingUnitDeleteOK(Int64 ixHandlingUnit, string sHandlingUnit) => _handlingunitsRepository.VerifyHandlingUnitDeleteOK(ixHandlingUnit, sHandlingUnit);

        public Task<Int64> Create(HandlingUnitsPost handlingunitsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._handlingunitsRepository.RegisterCreate(handlingunitsPost);
            try
            {
                this._handlingunitsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._handlingunitsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(handlingunitsPost.ixHandlingUnit);

        }
        public Task Edit(HandlingUnitsPost handlingunitsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._handlingunitsRepository.RegisterEdit(handlingunitsPost);
            try
            {
                this._handlingunitsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._handlingunitsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(HandlingUnitsPost handlingunitsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._handlingunitsRepository.RegisterDelete(handlingunitsPost);
            try
            {
                this._handlingunitsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._handlingunitsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

