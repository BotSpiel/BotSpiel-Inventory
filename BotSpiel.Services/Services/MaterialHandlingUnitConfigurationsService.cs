using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class MaterialHandlingUnitConfigurationsService : IMaterialHandlingUnitConfigurationsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IMaterialHandlingUnitConfigurationsRepository _materialhandlingunitconfigurationsRepository;

        public MaterialHandlingUnitConfigurationsService(IMaterialHandlingUnitConfigurationsRepository materialhandlingunitconfigurationsRepository)
        {
            _materialhandlingunitconfigurationsRepository = materialhandlingunitconfigurationsRepository;
        }

        public MaterialHandlingUnitConfigurationsPost GetPost(Int64 ixMaterialHandlingUnitConfiguration) => _materialhandlingunitconfigurationsRepository.GetPost(ixMaterialHandlingUnitConfiguration);
        public MaterialHandlingUnitConfigurations Get(Int64 ixMaterialHandlingUnitConfiguration) => _materialhandlingunitconfigurationsRepository.Get(ixMaterialHandlingUnitConfiguration);
        public IQueryable<MaterialHandlingUnitConfigurations> Index() => _materialhandlingunitconfigurationsRepository.Index();
       public IQueryable<UnitsOfMeasurement> selectUnitsOfMeasurement() => _materialhandlingunitconfigurationsRepository.selectUnitsOfMeasurement();
        public IQueryable<Materials> selectMaterials() => _materialhandlingunitconfigurationsRepository.selectMaterials();
        public IQueryable<HandlingUnitTypes> selectHandlingUnitTypes() => _materialhandlingunitconfigurationsRepository.selectHandlingUnitTypes();
       public List<KeyValuePair<Int64?, string>> selectUnitsOfMeasurementNullable() => _materialhandlingunitconfigurationsRepository.selectUnitsOfMeasurementNullable();
        public bool VerifyMaterialHandlingUnitConfigurationUnique(Int64 ixMaterialHandlingUnitConfiguration, string sMaterialHandlingUnitConfiguration) => _materialhandlingunitconfigurationsRepository.VerifyMaterialHandlingUnitConfigurationUnique(ixMaterialHandlingUnitConfiguration, sMaterialHandlingUnitConfiguration);
        public List<string> VerifyMaterialHandlingUnitConfigurationDeleteOK(Int64 ixMaterialHandlingUnitConfiguration, string sMaterialHandlingUnitConfiguration) => _materialhandlingunitconfigurationsRepository.VerifyMaterialHandlingUnitConfigurationDeleteOK(ixMaterialHandlingUnitConfiguration, sMaterialHandlingUnitConfiguration);

        public Task<Int64> Create(MaterialHandlingUnitConfigurationsPost materialhandlingunitconfigurationsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._materialhandlingunitconfigurationsRepository.RegisterCreate(materialhandlingunitconfigurationsPost);
            try
            {
                this._materialhandlingunitconfigurationsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._materialhandlingunitconfigurationsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(materialhandlingunitconfigurationsPost.ixMaterialHandlingUnitConfiguration);

        }
        public Task Edit(MaterialHandlingUnitConfigurationsPost materialhandlingunitconfigurationsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._materialhandlingunitconfigurationsRepository.RegisterEdit(materialhandlingunitconfigurationsPost);
            try
            {
                this._materialhandlingunitconfigurationsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._materialhandlingunitconfigurationsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(MaterialHandlingUnitConfigurationsPost materialhandlingunitconfigurationsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._materialhandlingunitconfigurationsRepository.RegisterDelete(materialhandlingunitconfigurationsPost);
            try
            {
                this._materialhandlingunitconfigurationsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._materialhandlingunitconfigurationsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

