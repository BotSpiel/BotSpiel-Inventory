using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class AccusationsService : IAccusationsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IAccusationsRepository _accusationsRepository;

        public AccusationsService(IAccusationsRepository accusationsRepository)
        {
            _accusationsRepository = accusationsRepository;
        }

        public AccusationsPost GetPost(Int64 ixAccusation) => _accusationsRepository.GetPost(ixAccusation);
        public Accusations Get(Int64 ixAccusation) => _accusationsRepository.Get(ixAccusation);
        public IQueryable<Accusations> Index() => _accusationsRepository.Index();
        public IQueryable<Accusations> IndexDb() => _accusationsRepository.IndexDb();
       public IQueryable<Languages> selectLanguages() => _accusationsRepository.selectLanguages();
        public IQueryable<LanguageStyles> selectLanguageStyles() => _accusationsRepository.selectLanguageStyles();
        public IQueryable<ResponseTypes> selectResponseTypes() => _accusationsRepository.selectResponseTypes();
       public IQueryable<Languages> LanguagesDb() => _accusationsRepository.LanguagesDb();
        public IQueryable<LanguageStyles> LanguageStylesDb() => _accusationsRepository.LanguageStylesDb();
        public IQueryable<ResponseTypes> ResponseTypesDb() => _accusationsRepository.ResponseTypesDb();
        public List<string> VerifyAccusationDeleteOK(Int64 ixAccusation, string sAccusation) => _accusationsRepository.VerifyAccusationDeleteOK(ixAccusation, sAccusation);

        public Task<Int64> Create(AccusationsPost accusationsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._accusationsRepository.RegisterCreate(accusationsPost);
            try
            {
                this._accusationsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._accusationsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(accusationsPost.ixAccusation);

        }
        public Task Edit(AccusationsPost accusationsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._accusationsRepository.RegisterEdit(accusationsPost);
            try
            {
                this._accusationsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._accusationsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(AccusationsPost accusationsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._accusationsRepository.RegisterDelete(accusationsPost);
            try
            {
                this._accusationsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._accusationsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

