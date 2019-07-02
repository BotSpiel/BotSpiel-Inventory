using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class CompaniesService : ICompaniesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly ICompaniesRepository _companiesRepository;

        public CompaniesService(ICompaniesRepository companiesRepository)
        {
            _companiesRepository = companiesRepository;
        }

        public CompaniesPost GetPost(Int64 ixCompany) => _companiesRepository.GetPost(ixCompany);
        public Companies Get(Int64 ixCompany) => _companiesRepository.Get(ixCompany);
        public IQueryable<Companies> Index() => _companiesRepository.Index();
        public bool VerifyCompanyUnique(Int64 ixCompany, string sCompany) => _companiesRepository.VerifyCompanyUnique(ixCompany, sCompany);
        public List<string> VerifyCompanyDeleteOK(Int64 ixCompany, string sCompany) => _companiesRepository.VerifyCompanyDeleteOK(ixCompany, sCompany);

        public Task<Int64> Create(CompaniesPost companiesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._companiesRepository.RegisterCreate(companiesPost);
            try
            {
                this._companiesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._companiesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(companiesPost.ixCompany);

        }
        public Task Edit(CompaniesPost companiesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._companiesRepository.RegisterEdit(companiesPost);
            try
            {
                this._companiesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._companiesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(CompaniesPost companiesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._companiesRepository.RegisterDelete(companiesPost);
            try
            {
                this._companiesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._companiesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

