using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class PeopleService : IPeopleService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IPeopleRepository _peopleRepository;

        public PeopleService(IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        public PeoplePost GetPost(Int64 ixPerson) => _peopleRepository.GetPost(ixPerson);
        public People Get(Int64 ixPerson) => _peopleRepository.Get(ixPerson);
        public IQueryable<People> Index() => _peopleRepository.Index();
        public IQueryable<People> IndexDb() => _peopleRepository.IndexDb();
       public IQueryable<Languages> selectLanguages() => _peopleRepository.selectLanguages();
       public IQueryable<Languages> LanguagesDb() => _peopleRepository.LanguagesDb();
        public bool VerifyPersonUnique(Int64 ixPerson, string sPerson) => _peopleRepository.VerifyPersonUnique(ixPerson, sPerson);
        public List<string> VerifyPersonDeleteOK(Int64 ixPerson, string sPerson) => _peopleRepository.VerifyPersonDeleteOK(ixPerson, sPerson);

        public Task<Int64> Create(PeoplePost peoplePost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._peopleRepository.RegisterCreate(peoplePost);
            try
            {
                this._peopleRepository.Commit();
            }
            catch(Exception ex)
            {
                this._peopleRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(peoplePost.ixPerson);

        }
        public Task Edit(PeoplePost peoplePost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._peopleRepository.RegisterEdit(peoplePost);
            try
            {
                this._peopleRepository.Commit();
            }
            catch(Exception ex)
            {
                this._peopleRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(PeoplePost peoplePost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._peopleRepository.RegisterDelete(peoplePost);
            try
            {
                this._peopleRepository.Commit();
            }
            catch(Exception ex)
            {
                this._peopleRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

