using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class LogicalOrientationsService : ILogicalOrientationsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly ILogicalOrientationsRepository _logicalorientationsRepository;

        public LogicalOrientationsService(ILogicalOrientationsRepository logicalorientationsRepository)
        {
            _logicalorientationsRepository = logicalorientationsRepository;
        }

        public LogicalOrientationsPost GetPost(Int64 ixLogicalOrientation) => _logicalorientationsRepository.GetPost(ixLogicalOrientation);
        public LogicalOrientations Get(Int64 ixLogicalOrientation) => _logicalorientationsRepository.Get(ixLogicalOrientation);
        public IQueryable<LogicalOrientations> Index() => _logicalorientationsRepository.Index();
        public bool VerifyLogicalOrientationUnique(Int64 ixLogicalOrientation, string sLogicalOrientation) => _logicalorientationsRepository.VerifyLogicalOrientationUnique(ixLogicalOrientation, sLogicalOrientation);
        public List<string> VerifyLogicalOrientationDeleteOK(Int64 ixLogicalOrientation, string sLogicalOrientation) => _logicalorientationsRepository.VerifyLogicalOrientationDeleteOK(ixLogicalOrientation, sLogicalOrientation);

        public Task<Int64> Create(LogicalOrientationsPost logicalorientationsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._logicalorientationsRepository.RegisterCreate(logicalorientationsPost);
            try
            {
                this._logicalorientationsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._logicalorientationsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(logicalorientationsPost.ixLogicalOrientation);

        }
        public Task Edit(LogicalOrientationsPost logicalorientationsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._logicalorientationsRepository.RegisterEdit(logicalorientationsPost);
            try
            {
                this._logicalorientationsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._logicalorientationsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(LogicalOrientationsPost logicalorientationsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._logicalorientationsRepository.RegisterDelete(logicalorientationsPost);
            try
            {
                this._logicalorientationsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._logicalorientationsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

