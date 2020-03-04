using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class ComplementsService : IComplementsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IComplementsRepository _complementsRepository;

        public ComplementsService(IComplementsRepository complementsRepository)
        {
            _complementsRepository = complementsRepository;
        }

        public ComplementsPost GetPost(Int64 ixComplement) => _complementsRepository.GetPost(ixComplement);
        public Complements Get(Int64 ixComplement) => _complementsRepository.Get(ixComplement);
        public IQueryable<Complements> Index() => _complementsRepository.Index();
        public IQueryable<Complements> IndexDb() => _complementsRepository.IndexDb();
       public IQueryable<Languages> selectLanguages() => _complementsRepository.selectLanguages();
        public IQueryable<LanguageStyles> selectLanguageStyles() => _complementsRepository.selectLanguageStyles();
        public IQueryable<ResponseTypes> selectResponseTypes() => _complementsRepository.selectResponseTypes();
       public IQueryable<Languages> LanguagesDb() => _complementsRepository.LanguagesDb();
        public IQueryable<LanguageStyles> LanguageStylesDb() => _complementsRepository.LanguageStylesDb();
        public IQueryable<ResponseTypes> ResponseTypesDb() => _complementsRepository.ResponseTypesDb();
        public List<string> VerifyComplementDeleteOK(Int64 ixComplement, string sComplement) => _complementsRepository.VerifyComplementDeleteOK(ixComplement, sComplement);

        public Task<Int64> Create(ComplementsPost complementsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._complementsRepository.RegisterCreate(complementsPost);
            try
            {
                this._complementsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._complementsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(complementsPost.ixComplement);

        }
        public Task Edit(ComplementsPost complementsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._complementsRepository.RegisterEdit(complementsPost);
            try
            {
                this._complementsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._complementsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(ComplementsPost complementsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._complementsRepository.RegisterDelete(complementsPost);
            try
            {
                this._complementsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._complementsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

