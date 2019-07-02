using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class MaterialTypesService : IMaterialTypesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IMaterialTypesRepository _materialtypesRepository;

        public MaterialTypesService(IMaterialTypesRepository materialtypesRepository)
        {
            _materialtypesRepository = materialtypesRepository;
        }

        public MaterialTypesPost GetPost(Int64 ixMaterialType) => _materialtypesRepository.GetPost(ixMaterialType);
        public MaterialTypes Get(Int64 ixMaterialType) => _materialtypesRepository.Get(ixMaterialType);
        public IQueryable<MaterialTypes> Index() => _materialtypesRepository.Index();
        public bool VerifyMaterialTypeUnique(Int64 ixMaterialType, string sMaterialType) => _materialtypesRepository.VerifyMaterialTypeUnique(ixMaterialType, sMaterialType);
        public List<string> VerifyMaterialTypeDeleteOK(Int64 ixMaterialType, string sMaterialType) => _materialtypesRepository.VerifyMaterialTypeDeleteOK(ixMaterialType, sMaterialType);

        public Task<Int64> Create(MaterialTypesPost materialtypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._materialtypesRepository.RegisterCreate(materialtypesPost);
            try
            {
                this._materialtypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._materialtypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(materialtypesPost.ixMaterialType);

        }
        public Task Edit(MaterialTypesPost materialtypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._materialtypesRepository.RegisterEdit(materialtypesPost);
            try
            {
                this._materialtypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._materialtypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(MaterialTypesPost materialtypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._materialtypesRepository.RegisterDelete(materialtypesPost);
            try
            {
                this._materialtypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._materialtypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

