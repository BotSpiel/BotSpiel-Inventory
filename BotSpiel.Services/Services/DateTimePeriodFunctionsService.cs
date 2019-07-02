using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class DateTimePeriodFunctionsService : IDateTimePeriodFunctionsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IDateTimePeriodFunctionsRepository _datetimeperiodfunctionsRepository;

        public DateTimePeriodFunctionsService(IDateTimePeriodFunctionsRepository datetimeperiodfunctionsRepository)
        {
            _datetimeperiodfunctionsRepository = datetimeperiodfunctionsRepository;
        }

        public DateTimePeriodFunctionsPost GetPost(Int64 ixDateTimePeriodFunction) => _datetimeperiodfunctionsRepository.GetPost(ixDateTimePeriodFunction);
        public DateTimePeriodFunctions Get(Int64 ixDateTimePeriodFunction) => _datetimeperiodfunctionsRepository.Get(ixDateTimePeriodFunction);
        public IQueryable<DateTimePeriodFunctions> Index() => _datetimeperiodfunctionsRepository.Index();
        public bool VerifyDateTimePeriodFunctionUnique(Int64 ixDateTimePeriodFunction, string sDateTimePeriodFunction) => _datetimeperiodfunctionsRepository.VerifyDateTimePeriodFunctionUnique(ixDateTimePeriodFunction, sDateTimePeriodFunction);
        public List<string> VerifyDateTimePeriodFunctionDeleteOK(Int64 ixDateTimePeriodFunction, string sDateTimePeriodFunction) => _datetimeperiodfunctionsRepository.VerifyDateTimePeriodFunctionDeleteOK(ixDateTimePeriodFunction, sDateTimePeriodFunction);

        public Task<Int64> Create(DateTimePeriodFunctionsPost datetimeperiodfunctionsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._datetimeperiodfunctionsRepository.RegisterCreate(datetimeperiodfunctionsPost);
            try
            {
                this._datetimeperiodfunctionsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._datetimeperiodfunctionsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(datetimeperiodfunctionsPost.ixDateTimePeriodFunction);

        }
        public Task Edit(DateTimePeriodFunctionsPost datetimeperiodfunctionsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._datetimeperiodfunctionsRepository.RegisterEdit(datetimeperiodfunctionsPost);
            try
            {
                this._datetimeperiodfunctionsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._datetimeperiodfunctionsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(DateTimePeriodFunctionsPost datetimeperiodfunctionsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._datetimeperiodfunctionsRepository.RegisterDelete(datetimeperiodfunctionsPost);
            try
            {
                this._datetimeperiodfunctionsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._datetimeperiodfunctionsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

