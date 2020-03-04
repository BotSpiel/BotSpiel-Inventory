using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class GreetingsService : IGreetingsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IGreetingsRepository _greetingsRepository;

        public GreetingsService(IGreetingsRepository greetingsRepository)
        {
            _greetingsRepository = greetingsRepository;
        }

        public GreetingsPost GetPost(Int64 ixGreeting) => _greetingsRepository.GetPost(ixGreeting);
        public Greetings Get(Int64 ixGreeting) => _greetingsRepository.Get(ixGreeting);
        public IQueryable<Greetings> Index() => _greetingsRepository.Index();
        public IQueryable<Greetings> IndexDb() => _greetingsRepository.IndexDb();
       public IQueryable<Languages> selectLanguages() => _greetingsRepository.selectLanguages();
        public IQueryable<LanguageStyles> selectLanguageStyles() => _greetingsRepository.selectLanguageStyles();
        public IQueryable<ResponseTypes> selectResponseTypes() => _greetingsRepository.selectResponseTypes();
       public IQueryable<Languages> LanguagesDb() => _greetingsRepository.LanguagesDb();
        public IQueryable<LanguageStyles> LanguageStylesDb() => _greetingsRepository.LanguageStylesDb();
        public IQueryable<ResponseTypes> ResponseTypesDb() => _greetingsRepository.ResponseTypesDb();
        public List<string> VerifyGreetingDeleteOK(Int64 ixGreeting, string sGreeting) => _greetingsRepository.VerifyGreetingDeleteOK(ixGreeting, sGreeting);

        public Task<Int64> Create(GreetingsPost greetingsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._greetingsRepository.RegisterCreate(greetingsPost);
            try
            {
                this._greetingsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._greetingsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(greetingsPost.ixGreeting);

        }
        public Task Edit(GreetingsPost greetingsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._greetingsRepository.RegisterEdit(greetingsPost);
            try
            {
                this._greetingsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._greetingsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(GreetingsPost greetingsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._greetingsRepository.RegisterDelete(greetingsPost);
            try
            {
                this._greetingsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._greetingsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

