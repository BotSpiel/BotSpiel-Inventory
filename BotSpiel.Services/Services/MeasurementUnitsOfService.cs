using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class MeasurementUnitsOfService : IMeasurementUnitsOfService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IMeasurementUnitsOfRepository _measurementunitsofRepository;

        public MeasurementUnitsOfService(IMeasurementUnitsOfRepository measurementunitsofRepository)
        {
            _measurementunitsofRepository = measurementunitsofRepository;
        }

        public MeasurementUnitsOfPost GetPost(Int64 ixMeasurementUnitOf) => _measurementunitsofRepository.GetPost(ixMeasurementUnitOf);
        public MeasurementUnitsOf Get(Int64 ixMeasurementUnitOf) => _measurementunitsofRepository.Get(ixMeasurementUnitOf);
        public IQueryable<MeasurementUnitsOf> Index() => _measurementunitsofRepository.Index();
        public bool VerifyMeasurementUnitOfUnique(Int64 ixMeasurementUnitOf, string sMeasurementUnitOf) => _measurementunitsofRepository.VerifyMeasurementUnitOfUnique(ixMeasurementUnitOf, sMeasurementUnitOf);
        public List<string> VerifyMeasurementUnitOfDeleteOK(Int64 ixMeasurementUnitOf, string sMeasurementUnitOf) => _measurementunitsofRepository.VerifyMeasurementUnitOfDeleteOK(ixMeasurementUnitOf, sMeasurementUnitOf);

        public Task<Int64> Create(MeasurementUnitsOfPost measurementunitsofPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._measurementunitsofRepository.RegisterCreate(measurementunitsofPost);
            try
            {
                this._measurementunitsofRepository.Commit();
            }
            catch(Exception ex)
            {
                this._measurementunitsofRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(measurementunitsofPost.ixMeasurementUnitOf);

        }
        public Task Edit(MeasurementUnitsOfPost measurementunitsofPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._measurementunitsofRepository.RegisterEdit(measurementunitsofPost);
            try
            {
                this._measurementunitsofRepository.Commit();
            }
            catch(Exception ex)
            {
                this._measurementunitsofRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(MeasurementUnitsOfPost measurementunitsofPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._measurementunitsofRepository.RegisterDelete(measurementunitsofPost);
            try
            {
                this._measurementunitsofRepository.Commit();
            }
            catch(Exception ex)
            {
                this._measurementunitsofRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

