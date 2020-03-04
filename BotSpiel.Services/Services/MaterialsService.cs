using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class MaterialsService : IMaterialsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IMaterialsRepository _materialsRepository;

        public MaterialsService(IMaterialsRepository materialsRepository)
        {
            _materialsRepository = materialsRepository;
        }

        public MaterialsPost GetPost(Int64 ixMaterial) => _materialsRepository.GetPost(ixMaterial);
        public Materials Get(Int64 ixMaterial) => _materialsRepository.Get(ixMaterial);
        public IQueryable<Materials> Index() => _materialsRepository.Index();
        public IQueryable<Materials> IndexDb() => _materialsRepository.IndexDb();
       public IQueryable<UnitsOfMeasurement> selectUnitsOfMeasurement() => _materialsRepository.selectUnitsOfMeasurement();
        public IQueryable<MaterialTypes> selectMaterialTypes() => _materialsRepository.selectMaterialTypes();
       public IQueryable<UnitsOfMeasurement> UnitsOfMeasurementDb() => _materialsRepository.UnitsOfMeasurementDb();
        public IQueryable<MaterialTypes> MaterialTypesDb() => _materialsRepository.MaterialTypesDb();
       public List<KeyValuePair<Int64?, string>> selectUnitsOfMeasurementNullable() => _materialsRepository.selectUnitsOfMeasurementNullable();
        public bool VerifyMaterialUnique(Int64 ixMaterial, string sMaterial) => _materialsRepository.VerifyMaterialUnique(ixMaterial, sMaterial);
        public List<string> VerifyMaterialDeleteOK(Int64 ixMaterial, string sMaterial) => _materialsRepository.VerifyMaterialDeleteOK(ixMaterial, sMaterial);

        public Task<Int64> Create(MaterialsPost materialsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._materialsRepository.RegisterCreate(materialsPost);
            try
            {
                this._materialsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._materialsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(materialsPost.ixMaterial);

        }
        public Task Edit(MaterialsPost materialsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._materialsRepository.RegisterEdit(materialsPost);
            try
            {
                this._materialsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._materialsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(MaterialsPost materialsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._materialsRepository.RegisterDelete(materialsPost);
            try
            {
                this._materialsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._materialsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

