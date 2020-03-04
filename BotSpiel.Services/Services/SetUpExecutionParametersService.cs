using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class SetUpExecutionParametersService : ISetUpExecutionParametersService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly ISetUpExecutionParametersRepository _setupexecutionparametersRepository;

        public SetUpExecutionParametersService(ISetUpExecutionParametersRepository setupexecutionparametersRepository)
        {
            _setupexecutionparametersRepository = setupexecutionparametersRepository;
        }

        public SetUpExecutionParametersPost GetPost(Int64 ixSetUpExecutionParameter) => _setupexecutionparametersRepository.GetPost(ixSetUpExecutionParameter);
        public SetUpExecutionParameters Get(Int64 ixSetUpExecutionParameter) => _setupexecutionparametersRepository.Get(ixSetUpExecutionParameter);
        public IQueryable<SetUpExecutionParameters> Index() => _setupexecutionparametersRepository.Index();
        public IQueryable<SetUpExecutionParameters> IndexDb() => _setupexecutionparametersRepository.IndexDb();
       public IQueryable<Companies> selectCompanies() => _setupexecutionparametersRepository.selectCompanies();
        public IQueryable<Facilities> selectFacilities() => _setupexecutionparametersRepository.selectFacilities();
        public IQueryable<FacilityWorkAreas> selectFacilityWorkAreas() => _setupexecutionparametersRepository.selectFacilityWorkAreas();
       public IQueryable<Companies> CompaniesDb() => _setupexecutionparametersRepository.CompaniesDb();
        public IQueryable<Facilities> FacilitiesDb() => _setupexecutionparametersRepository.FacilitiesDb();
        public IQueryable<FacilityWorkAreas> FacilityWorkAreasDb() => _setupexecutionparametersRepository.FacilityWorkAreasDb();
        public bool VerifySetUpExecutionParameterUnique(Int64 ixSetUpExecutionParameter, string sSetUpExecutionParameter) => _setupexecutionparametersRepository.VerifySetUpExecutionParameterUnique(ixSetUpExecutionParameter, sSetUpExecutionParameter);
        public List<string> VerifySetUpExecutionParameterDeleteOK(Int64 ixSetUpExecutionParameter, string sSetUpExecutionParameter) => _setupexecutionparametersRepository.VerifySetUpExecutionParameterDeleteOK(ixSetUpExecutionParameter, sSetUpExecutionParameter);

        public Task<Int64> Create(SetUpExecutionParametersPost setupexecutionparametersPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._setupexecutionparametersRepository.RegisterCreate(setupexecutionparametersPost);
            try
            {
                this._setupexecutionparametersRepository.Commit();
            }
            catch(Exception ex)
            {
                this._setupexecutionparametersRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(setupexecutionparametersPost.ixSetUpExecutionParameter);

        }
        public Task Edit(SetUpExecutionParametersPost setupexecutionparametersPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._setupexecutionparametersRepository.RegisterEdit(setupexecutionparametersPost);
            try
            {
                this._setupexecutionparametersRepository.Commit();
            }
            catch(Exception ex)
            {
                this._setupexecutionparametersRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(SetUpExecutionParametersPost setupexecutionparametersPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._setupexecutionparametersRepository.RegisterDelete(setupexecutionparametersPost);
            try
            {
                this._setupexecutionparametersRepository.Commit();
            }
            catch(Exception ex)
            {
                this._setupexecutionparametersRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

