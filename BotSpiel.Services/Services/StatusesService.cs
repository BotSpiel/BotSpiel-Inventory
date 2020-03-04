using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class StatusesService : IStatusesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IStatusesRepository _statusesRepository;

        public StatusesService(IStatusesRepository statusesRepository)
        {
            _statusesRepository = statusesRepository;
        }

        public StatusesPost GetPost(Int64 ixStatus) => _statusesRepository.GetPost(ixStatus);
        public Statuses Get(Int64 ixStatus) => _statusesRepository.Get(ixStatus);
        public IQueryable<Statuses> Index() => _statusesRepository.Index();
        public IQueryable<Statuses> IndexDb() => _statusesRepository.IndexDb();
        public bool VerifyStatusUnique(Int64 ixStatus, string sStatus) => _statusesRepository.VerifyStatusUnique(ixStatus, sStatus);
        public List<string> VerifyStatusDeleteOK(Int64 ixStatus, string sStatus) => _statusesRepository.VerifyStatusDeleteOK(ixStatus, sStatus);

        //Custom Code Start | Added Code Block 

        public string getStatus(Int64 ixStatus)
        {
            if (IndexDb().Where(x => x.ixStatus == ixStatus).Select(x => x.sStatus).Any())
            {
                return IndexDb().Where(x => x.ixStatus == ixStatus).Select(x => x.sStatus).FirstOrDefault();
            }
            else
            {
                return "";
            }
        }

        //Custom Code End

        public Task<Int64> Create(StatusesPost statusesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._statusesRepository.RegisterCreate(statusesPost);
            try
            {
                this._statusesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._statusesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(statusesPost.ixStatus);

        }
        public Task Edit(StatusesPost statusesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._statusesRepository.RegisterEdit(statusesPost);
            try
            {
                this._statusesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._statusesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(StatusesPost statusesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._statusesRepository.RegisterDelete(statusesPost);
            try
            {
                this._statusesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._statusesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

