using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class MeasurementSystemsService : IMeasurementSystemsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IMeasurementSystemsRepository _measurementsystemsRepository;

        public MeasurementSystemsService(IMeasurementSystemsRepository measurementsystemsRepository)
        {
            _measurementsystemsRepository = measurementsystemsRepository;
        }

        public MeasurementSystemsPost GetPost(Int64 ixMeasurementSystem) => _measurementsystemsRepository.GetPost(ixMeasurementSystem);
        public MeasurementSystems Get(Int64 ixMeasurementSystem) => _measurementsystemsRepository.Get(ixMeasurementSystem);
        public IQueryable<MeasurementSystems> Index() => _measurementsystemsRepository.Index();
        public bool VerifyMeasurementSystemUnique(Int64 ixMeasurementSystem, string sMeasurementSystem) => _measurementsystemsRepository.VerifyMeasurementSystemUnique(ixMeasurementSystem, sMeasurementSystem);
        public List<string> VerifyMeasurementSystemDeleteOK(Int64 ixMeasurementSystem, string sMeasurementSystem) => _measurementsystemsRepository.VerifyMeasurementSystemDeleteOK(ixMeasurementSystem, sMeasurementSystem);

        public Task<Int64> Create(MeasurementSystemsPost measurementsystemsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._measurementsystemsRepository.RegisterCreate(measurementsystemsPost);
            try
            {
                this._measurementsystemsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._measurementsystemsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(measurementsystemsPost.ixMeasurementSystem);

        }
        public Task Edit(MeasurementSystemsPost measurementsystemsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._measurementsystemsRepository.RegisterEdit(measurementsystemsPost);
            try
            {
                this._measurementsystemsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._measurementsystemsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(MeasurementSystemsPost measurementsystemsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._measurementsystemsRepository.RegisterDelete(measurementsystemsPost);
            try
            {
                this._measurementsystemsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._measurementsystemsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

