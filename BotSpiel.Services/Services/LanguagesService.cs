using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class LanguagesService : ILanguagesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly ILanguagesRepository _languagesRepository;

        public LanguagesService(ILanguagesRepository languagesRepository)
        {
            _languagesRepository = languagesRepository;
        }

        public LanguagesPost GetPost(Int64 ixLanguage) => _languagesRepository.GetPost(ixLanguage);
        public Languages Get(Int64 ixLanguage) => _languagesRepository.Get(ixLanguage);
        public IQueryable<Languages> Index() => _languagesRepository.Index();
        public bool VerifyLanguageUnique(Int64 ixLanguage, string sLanguage) => _languagesRepository.VerifyLanguageUnique(ixLanguage, sLanguage);
        public List<string> VerifyLanguageDeleteOK(Int64 ixLanguage, string sLanguage) => _languagesRepository.VerifyLanguageDeleteOK(ixLanguage, sLanguage);

        public Task<Int64> Create(LanguagesPost languagesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._languagesRepository.RegisterCreate(languagesPost);
            try
            {
                this._languagesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._languagesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(languagesPost.ixLanguage);

        }
        public Task Edit(LanguagesPost languagesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._languagesRepository.RegisterEdit(languagesPost);
            try
            {
                this._languagesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._languagesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(LanguagesPost languagesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._languagesRepository.RegisterDelete(languagesPost);
            try
            {
                this._languagesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._languagesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

