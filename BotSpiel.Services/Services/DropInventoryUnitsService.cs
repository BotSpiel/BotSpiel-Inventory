using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class DropInventoryUnitsService : IDropInventoryUnitsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IDropInventoryUnitsRepository _dropinventoryunitsRepository;

        public DropInventoryUnitsService(IDropInventoryUnitsRepository dropinventoryunitsRepository)
        {
            _dropinventoryunitsRepository = dropinventoryunitsRepository;
        }

        public DropInventoryUnitsPost GetPost(Int64 ixDropInventoryUnit) => _dropinventoryunitsRepository.GetPost(ixDropInventoryUnit);
        public DropInventoryUnits Get(Int64 ixDropInventoryUnit) => _dropinventoryunitsRepository.Get(ixDropInventoryUnit);
        public IQueryable<DropInventoryUnits> Index() => _dropinventoryunitsRepository.Index();
        public IQueryable<DropInventoryUnits> IndexDb() => _dropinventoryunitsRepository.IndexDb();
        public List<string> VerifyDropInventoryUnitDeleteOK(Int64 ixDropInventoryUnit, string sDropInventoryUnit) => _dropinventoryunitsRepository.VerifyDropInventoryUnitDeleteOK(ixDropInventoryUnit, sDropInventoryUnit);

        public Task<Int64> Create(DropInventoryUnitsPost dropinventoryunitsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._dropinventoryunitsRepository.RegisterCreate(dropinventoryunitsPost);
            try
            {
                this._dropinventoryunitsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._dropinventoryunitsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(dropinventoryunitsPost.ixDropInventoryUnit);

        }
        public Task Edit(DropInventoryUnitsPost dropinventoryunitsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._dropinventoryunitsRepository.RegisterEdit(dropinventoryunitsPost);
            try
            {
                this._dropinventoryunitsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._dropinventoryunitsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(DropInventoryUnitsPost dropinventoryunitsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._dropinventoryunitsRepository.RegisterDelete(dropinventoryunitsPost);
            try
            {
                this._dropinventoryunitsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._dropinventoryunitsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

