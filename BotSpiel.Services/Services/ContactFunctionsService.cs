using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class ContactFunctionsService : IContactFunctionsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IContactFunctionsRepository _contactfunctionsRepository;

        public ContactFunctionsService(IContactFunctionsRepository contactfunctionsRepository)
        {
            _contactfunctionsRepository = contactfunctionsRepository;
        }

        public ContactFunctionsPost GetPost(Int64 ixContactFunction) => _contactfunctionsRepository.GetPost(ixContactFunction);
        public ContactFunctions Get(Int64 ixContactFunction) => _contactfunctionsRepository.Get(ixContactFunction);
        public IQueryable<ContactFunctions> Index() => _contactfunctionsRepository.Index();
        public IQueryable<ContactFunctions> IndexDb() => _contactfunctionsRepository.IndexDb();
        public bool VerifyContactFunctionUnique(Int64 ixContactFunction, string sContactFunction) => _contactfunctionsRepository.VerifyContactFunctionUnique(ixContactFunction, sContactFunction);
        public List<string> VerifyContactFunctionDeleteOK(Int64 ixContactFunction, string sContactFunction) => _contactfunctionsRepository.VerifyContactFunctionDeleteOK(ixContactFunction, sContactFunction);

        public Task<Int64> Create(ContactFunctionsPost contactfunctionsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._contactfunctionsRepository.RegisterCreate(contactfunctionsPost);
            try
            {
                this._contactfunctionsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._contactfunctionsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(contactfunctionsPost.ixContactFunction);

        }
        public Task Edit(ContactFunctionsPost contactfunctionsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._contactfunctionsRepository.RegisterEdit(contactfunctionsPost);
            try
            {
                this._contactfunctionsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._contactfunctionsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(ContactFunctionsPost contactfunctionsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._contactfunctionsRepository.RegisterDelete(contactfunctionsPost);
            try
            {
                this._contactfunctionsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._contactfunctionsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

