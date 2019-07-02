using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class DateTimePeriodFormatsService : IDateTimePeriodFormatsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IDateTimePeriodFormatsRepository _datetimeperiodformatsRepository;

        public DateTimePeriodFormatsService(IDateTimePeriodFormatsRepository datetimeperiodformatsRepository)
        {
            _datetimeperiodformatsRepository = datetimeperiodformatsRepository;
        }

        public DateTimePeriodFormatsPost GetPost(Int64 ixDateTimePeriodFormat) => _datetimeperiodformatsRepository.GetPost(ixDateTimePeriodFormat);
        public DateTimePeriodFormats Get(Int64 ixDateTimePeriodFormat) => _datetimeperiodformatsRepository.Get(ixDateTimePeriodFormat);
        public IQueryable<DateTimePeriodFormats> Index() => _datetimeperiodformatsRepository.Index();
        public bool VerifyDateTimePeriodFormatUnique(Int64 ixDateTimePeriodFormat, string sDateTimePeriodFormat) => _datetimeperiodformatsRepository.VerifyDateTimePeriodFormatUnique(ixDateTimePeriodFormat, sDateTimePeriodFormat);
        public List<string> VerifyDateTimePeriodFormatDeleteOK(Int64 ixDateTimePeriodFormat, string sDateTimePeriodFormat) => _datetimeperiodformatsRepository.VerifyDateTimePeriodFormatDeleteOK(ixDateTimePeriodFormat, sDateTimePeriodFormat);

        public Task<Int64> Create(DateTimePeriodFormatsPost datetimeperiodformatsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._datetimeperiodformatsRepository.RegisterCreate(datetimeperiodformatsPost);
            try
            {
                this._datetimeperiodformatsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._datetimeperiodformatsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(datetimeperiodformatsPost.ixDateTimePeriodFormat);

        }
        public Task Edit(DateTimePeriodFormatsPost datetimeperiodformatsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._datetimeperiodformatsRepository.RegisterEdit(datetimeperiodformatsPost);
            try
            {
                this._datetimeperiodformatsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._datetimeperiodformatsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(DateTimePeriodFormatsPost datetimeperiodformatsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._datetimeperiodformatsRepository.RegisterDelete(datetimeperiodformatsPost);
            try
            {
                this._datetimeperiodformatsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._datetimeperiodformatsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

