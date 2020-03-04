using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class LanguageStylesService : ILanguageStylesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly ILanguageStylesRepository _languagestylesRepository;

        public LanguageStylesService(ILanguageStylesRepository languagestylesRepository)
        {
            _languagestylesRepository = languagestylesRepository;
        }

        public LanguageStylesPost GetPost(Int64 ixLanguageStyle) => _languagestylesRepository.GetPost(ixLanguageStyle);
        public LanguageStyles Get(Int64 ixLanguageStyle) => _languagestylesRepository.Get(ixLanguageStyle);
        public IQueryable<LanguageStyles> Index() => _languagestylesRepository.Index();
        public IQueryable<LanguageStyles> IndexDb() => _languagestylesRepository.IndexDb();
        public List<string> VerifyLanguageStyleDeleteOK(Int64 ixLanguageStyle, string sLanguageStyle) => _languagestylesRepository.VerifyLanguageStyleDeleteOK(ixLanguageStyle, sLanguageStyle);

        public Task<Int64> Create(LanguageStylesPost languagestylesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._languagestylesRepository.RegisterCreate(languagestylesPost);
            try
            {
                this._languagestylesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._languagestylesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(languagestylesPost.ixLanguageStyle);

        }
        public Task Edit(LanguageStylesPost languagestylesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._languagestylesRepository.RegisterEdit(languagestylesPost);
            try
            {
                this._languagestylesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._languagestylesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(LanguageStylesPost languagestylesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._languagestylesRepository.RegisterDelete(languagestylesPost);
            try
            {
                this._languagestylesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._languagestylesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

