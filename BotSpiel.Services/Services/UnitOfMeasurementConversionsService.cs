using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class UnitOfMeasurementConversionsService : IUnitOfMeasurementConversionsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IUnitOfMeasurementConversionsRepository _unitofmeasurementconversionsRepository;

        public UnitOfMeasurementConversionsService(IUnitOfMeasurementConversionsRepository unitofmeasurementconversionsRepository)
        {
            _unitofmeasurementconversionsRepository = unitofmeasurementconversionsRepository;
        }

        public UnitOfMeasurementConversionsPost GetPost(Int64 ixUnitOfMeasurementConversion) => _unitofmeasurementconversionsRepository.GetPost(ixUnitOfMeasurementConversion);
        public UnitOfMeasurementConversions Get(Int64 ixUnitOfMeasurementConversion) => _unitofmeasurementconversionsRepository.Get(ixUnitOfMeasurementConversion);
        public IQueryable<UnitOfMeasurementConversions> Index() => _unitofmeasurementconversionsRepository.Index();
        public IQueryable<UnitOfMeasurementConversions> IndexDb() => _unitofmeasurementconversionsRepository.IndexDb();
       public IQueryable<UnitsOfMeasurement> selectUnitsOfMeasurement() => _unitofmeasurementconversionsRepository.selectUnitsOfMeasurement();
       public IQueryable<UnitsOfMeasurement> UnitsOfMeasurementDb() => _unitofmeasurementconversionsRepository.UnitsOfMeasurementDb();
        public bool VerifyUnitOfMeasurementConversionUnique(Int64 ixUnitOfMeasurementConversion, string sUnitOfMeasurementConversion) => _unitofmeasurementconversionsRepository.VerifyUnitOfMeasurementConversionUnique(ixUnitOfMeasurementConversion, sUnitOfMeasurementConversion);
        public List<string> VerifyUnitOfMeasurementConversionDeleteOK(Int64 ixUnitOfMeasurementConversion, string sUnitOfMeasurementConversion) => _unitofmeasurementconversionsRepository.VerifyUnitOfMeasurementConversionDeleteOK(ixUnitOfMeasurementConversion, sUnitOfMeasurementConversion);

        public Task<Int64> Create(UnitOfMeasurementConversionsPost unitofmeasurementconversionsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._unitofmeasurementconversionsRepository.RegisterCreate(unitofmeasurementconversionsPost);
            try
            {
                this._unitofmeasurementconversionsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._unitofmeasurementconversionsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(unitofmeasurementconversionsPost.ixUnitOfMeasurementConversion);

        }
        public Task Edit(UnitOfMeasurementConversionsPost unitofmeasurementconversionsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._unitofmeasurementconversionsRepository.RegisterEdit(unitofmeasurementconversionsPost);
            try
            {
                this._unitofmeasurementconversionsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._unitofmeasurementconversionsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(UnitOfMeasurementConversionsPost unitofmeasurementconversionsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._unitofmeasurementconversionsRepository.RegisterDelete(unitofmeasurementconversionsPost);
            try
            {
                this._unitofmeasurementconversionsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._unitofmeasurementconversionsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

