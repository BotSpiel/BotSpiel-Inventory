using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class FarewellsService : IFarewellsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IFarewellsRepository _farewellsRepository;

        public FarewellsService(IFarewellsRepository farewellsRepository)
        {
            _farewellsRepository = farewellsRepository;
        }

        public FarewellsPost GetPost(Int64 ixFarewell) => _farewellsRepository.GetPost(ixFarewell);
        public Farewells Get(Int64 ixFarewell) => _farewellsRepository.Get(ixFarewell);
        public IQueryable<Farewells> Index() => _farewellsRepository.Index();
        public IQueryable<Farewells> IndexDb() => _farewellsRepository.IndexDb();
       public IQueryable<Languages> selectLanguages() => _farewellsRepository.selectLanguages();
        public IQueryable<LanguageStyles> selectLanguageStyles() => _farewellsRepository.selectLanguageStyles();
        public IQueryable<ResponseTypes> selectResponseTypes() => _farewellsRepository.selectResponseTypes();
       public IQueryable<Languages> LanguagesDb() => _farewellsRepository.LanguagesDb();
        public IQueryable<LanguageStyles> LanguageStylesDb() => _farewellsRepository.LanguageStylesDb();
        public IQueryable<ResponseTypes> ResponseTypesDb() => _farewellsRepository.ResponseTypesDb();
        public List<string> VerifyFarewellDeleteOK(Int64 ixFarewell, string sFarewell) => _farewellsRepository.VerifyFarewellDeleteOK(ixFarewell, sFarewell);

        public Task<Int64> Create(FarewellsPost farewellsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._farewellsRepository.RegisterCreate(farewellsPost);
            try
            {
                this._farewellsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._farewellsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(farewellsPost.ixFarewell);

        }
        public Task Edit(FarewellsPost farewellsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._farewellsRepository.RegisterEdit(farewellsPost);
            try
            {
                this._farewellsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._farewellsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(FarewellsPost farewellsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._farewellsRepository.RegisterDelete(farewellsPost);
            try
            {
                this._farewellsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._farewellsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

