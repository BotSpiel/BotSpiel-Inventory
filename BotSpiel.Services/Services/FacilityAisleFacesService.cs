using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class FacilityAisleFacesService : IFacilityAisleFacesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IFacilityAisleFacesRepository _facilityaislefacesRepository;

        public FacilityAisleFacesService(IFacilityAisleFacesRepository facilityaislefacesRepository)
        {
            _facilityaislefacesRepository = facilityaislefacesRepository;
        }

        public FacilityAisleFacesPost GetPost(Int64 ixFacilityAisleFace) => _facilityaislefacesRepository.GetPost(ixFacilityAisleFace);
        public FacilityAisleFaces Get(Int64 ixFacilityAisleFace) => _facilityaislefacesRepository.Get(ixFacilityAisleFace);
        public IQueryable<FacilityAisleFaces> Index() => _facilityaislefacesRepository.Index();
        public IQueryable<FacilityAisleFaces> IndexDb() => _facilityaislefacesRepository.IndexDb();
       public IQueryable<LocationFunctions> selectLocationFunctions() => _facilityaislefacesRepository.selectLocationFunctions();
        public IQueryable<UnitsOfMeasurement> selectUnitsOfMeasurement() => _facilityaislefacesRepository.selectUnitsOfMeasurement();
        public IQueryable<Facilities> selectFacilities() => _facilityaislefacesRepository.selectFacilities();
        public IQueryable<FacilityZones> selectFacilityZones() => _facilityaislefacesRepository.selectFacilityZones();
        public IQueryable<FacilityFloors> selectFacilityFloors() => _facilityaislefacesRepository.selectFacilityFloors();
        public IQueryable<FacilityAisleFaces> selectFacilityAisleFaces() => _facilityaislefacesRepository.selectFacilityAisleFaces();
        public IQueryable<BaySequenceTypes> selectBaySequenceTypes() => _facilityaislefacesRepository.selectBaySequenceTypes();
        public IQueryable<LogicalOrientations> selectLogicalOrientations() => _facilityaislefacesRepository.selectLogicalOrientations();
        public IQueryable<AisleFaceStorageTypes> selectAisleFaceStorageTypes() => _facilityaislefacesRepository.selectAisleFaceStorageTypes();
        public IQueryable<InventoryLocationSizes> selectInventoryLocationSizes() => _facilityaislefacesRepository.selectInventoryLocationSizes();
       public IQueryable<LocationFunctions> LocationFunctionsDb() => _facilityaislefacesRepository.LocationFunctionsDb();
        public IQueryable<UnitsOfMeasurement> UnitsOfMeasurementDb() => _facilityaislefacesRepository.UnitsOfMeasurementDb();
        public IQueryable<Facilities> FacilitiesDb() => _facilityaislefacesRepository.FacilitiesDb();
        public IQueryable<FacilityZones> FacilityZonesDb() => _facilityaislefacesRepository.FacilityZonesDb();
        public IQueryable<FacilityFloors> FacilityFloorsDb() => _facilityaislefacesRepository.FacilityFloorsDb();
        public IQueryable<FacilityAisleFaces> FacilityAisleFacesDb() => _facilityaislefacesRepository.FacilityAisleFacesDb();
        public IQueryable<BaySequenceTypes> BaySequenceTypesDb() => _facilityaislefacesRepository.BaySequenceTypesDb();
        public IQueryable<LogicalOrientations> LogicalOrientationsDb() => _facilityaislefacesRepository.LogicalOrientationsDb();
        public IQueryable<AisleFaceStorageTypes> AisleFaceStorageTypesDb() => _facilityaislefacesRepository.AisleFaceStorageTypesDb();
        public IQueryable<InventoryLocationSizes> InventoryLocationSizesDb() => _facilityaislefacesRepository.InventoryLocationSizesDb();
       public List<KeyValuePair<Int64?, string>> selectLocationFunctionsNullable() => _facilityaislefacesRepository.selectLocationFunctionsNullable();
        public List<KeyValuePair<Int64?, string>> selectUnitsOfMeasurementNullable() => _facilityaislefacesRepository.selectUnitsOfMeasurementNullable();
        public List<KeyValuePair<Int64?, string>> selectFacilityZonesNullable() => _facilityaislefacesRepository.selectFacilityZonesNullable();
        public List<KeyValuePair<Int64?, string>> selectFacilityAisleFacesNullable() => _facilityaislefacesRepository.selectFacilityAisleFacesNullable();
        public bool VerifyFacilityAisleFaceUnique(Int64 ixFacilityAisleFace, string sFacilityAisleFace) => _facilityaislefacesRepository.VerifyFacilityAisleFaceUnique(ixFacilityAisleFace, sFacilityAisleFace);
        public List<string> VerifyFacilityAisleFaceDeleteOK(Int64 ixFacilityAisleFace, string sFacilityAisleFace) => _facilityaislefacesRepository.VerifyFacilityAisleFaceDeleteOK(ixFacilityAisleFace, sFacilityAisleFace);

        public Task<Int64> Create(FacilityAisleFacesPost facilityaislefacesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._facilityaislefacesRepository.RegisterCreate(facilityaislefacesPost);
            try
            {
                this._facilityaislefacesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._facilityaislefacesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(facilityaislefacesPost.ixFacilityAisleFace);

        }
        public Task Edit(FacilityAisleFacesPost facilityaislefacesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._facilityaislefacesRepository.RegisterEdit(facilityaislefacesPost);
            try
            {
                this._facilityaislefacesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._facilityaislefacesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(FacilityAisleFacesPost facilityaislefacesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._facilityaislefacesRepository.RegisterDelete(facilityaislefacesPost);
            try
            {
                this._facilityaislefacesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._facilityaislefacesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

