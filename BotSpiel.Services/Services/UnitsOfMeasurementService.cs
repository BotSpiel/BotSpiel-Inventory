using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class UnitsOfMeasurementService : IUnitsOfMeasurementService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IUnitsOfMeasurementRepository _unitsofmeasurementRepository;

        public UnitsOfMeasurementService(IUnitsOfMeasurementRepository unitsofmeasurementRepository)
        {
            _unitsofmeasurementRepository = unitsofmeasurementRepository;
        }

        public UnitsOfMeasurementPost GetPost(Int64 ixUnitOfMeasurement) => _unitsofmeasurementRepository.GetPost(ixUnitOfMeasurement);
        public UnitsOfMeasurement Get(Int64 ixUnitOfMeasurement) => _unitsofmeasurementRepository.Get(ixUnitOfMeasurement);
        public IQueryable<UnitsOfMeasurement> Index() => _unitsofmeasurementRepository.Index();
       public IQueryable<MeasurementSystems> selectMeasurementSystems() => _unitsofmeasurementRepository.selectMeasurementSystems();
        public IQueryable<MeasurementUnitsOf> selectMeasurementUnitsOf() => _unitsofmeasurementRepository.selectMeasurementUnitsOf();
        public bool VerifyUnitOfMeasurementUnique(Int64 ixUnitOfMeasurement, string sUnitOfMeasurement) => _unitsofmeasurementRepository.VerifyUnitOfMeasurementUnique(ixUnitOfMeasurement, sUnitOfMeasurement);
        public List<string> VerifyUnitOfMeasurementDeleteOK(Int64 ixUnitOfMeasurement, string sUnitOfMeasurement) => _unitsofmeasurementRepository.VerifyUnitOfMeasurementDeleteOK(ixUnitOfMeasurement, sUnitOfMeasurement);

        public Task<Int64> Create(UnitsOfMeasurementPost unitsofmeasurementPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._unitsofmeasurementRepository.RegisterCreate(unitsofmeasurementPost);
            try
            {
                this._unitsofmeasurementRepository.Commit();
            }
            catch(Exception ex)
            {
                this._unitsofmeasurementRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(unitsofmeasurementPost.ixUnitOfMeasurement);

        }
        public Task Edit(UnitsOfMeasurementPost unitsofmeasurementPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._unitsofmeasurementRepository.RegisterEdit(unitsofmeasurementPost);
            try
            {
                this._unitsofmeasurementRepository.Commit();
            }
            catch(Exception ex)
            {
                this._unitsofmeasurementRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(UnitsOfMeasurementPost unitsofmeasurementPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._unitsofmeasurementRepository.RegisterDelete(unitsofmeasurementPost);
            try
            {
                this._unitsofmeasurementRepository.Commit();
            }
            catch(Exception ex)
            {
                this._unitsofmeasurementRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

