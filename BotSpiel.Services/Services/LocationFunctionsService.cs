using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class LocationFunctionsService : ILocationFunctionsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly ILocationFunctionsRepository _locationfunctionsRepository;

        public LocationFunctionsService(ILocationFunctionsRepository locationfunctionsRepository)
        {
            _locationfunctionsRepository = locationfunctionsRepository;
        }

        public LocationFunctionsPost GetPost(Int64 ixLocationFunction) => _locationfunctionsRepository.GetPost(ixLocationFunction);
        public LocationFunctions Get(Int64 ixLocationFunction) => _locationfunctionsRepository.Get(ixLocationFunction);
        public IQueryable<LocationFunctions> Index() => _locationfunctionsRepository.Index();
        public IQueryable<LocationFunctions> IndexDb() => _locationfunctionsRepository.IndexDb();
        public bool VerifyLocationFunctionUnique(Int64 ixLocationFunction, string sLocationFunction) => _locationfunctionsRepository.VerifyLocationFunctionUnique(ixLocationFunction, sLocationFunction);
        public List<string> VerifyLocationFunctionDeleteOK(Int64 ixLocationFunction, string sLocationFunction) => _locationfunctionsRepository.VerifyLocationFunctionDeleteOK(ixLocationFunction, sLocationFunction);

        public Task<Int64> Create(LocationFunctionsPost locationfunctionsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._locationfunctionsRepository.RegisterCreate(locationfunctionsPost);
            try
            {
                this._locationfunctionsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._locationfunctionsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(locationfunctionsPost.ixLocationFunction);

        }
        public Task Edit(LocationFunctionsPost locationfunctionsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._locationfunctionsRepository.RegisterEdit(locationfunctionsPost);
            try
            {
                this._locationfunctionsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._locationfunctionsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(LocationFunctionsPost locationfunctionsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._locationfunctionsRepository.RegisterDelete(locationfunctionsPost);
            try
            {
                this._locationfunctionsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._locationfunctionsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

