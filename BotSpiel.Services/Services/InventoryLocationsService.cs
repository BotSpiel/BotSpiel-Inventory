using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class InventoryLocationsService : IInventoryLocationsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IInventoryLocationsRepository _inventorylocationsRepository;

        public InventoryLocationsService(IInventoryLocationsRepository inventorylocationsRepository)
        {
            _inventorylocationsRepository = inventorylocationsRepository;
        }

        public InventoryLocationsPost GetPost(Int64 ixInventoryLocation) => _inventorylocationsRepository.GetPost(ixInventoryLocation);
        public InventoryLocations Get(Int64 ixInventoryLocation) => _inventorylocationsRepository.Get(ixInventoryLocation);
        public IQueryable<InventoryLocations> Index() => _inventorylocationsRepository.Index();
        public IQueryable<InventoryLocations> IndexDb() => _inventorylocationsRepository.IndexDb();
        //Custom Code Start | Added Code Block 
        public IQueryable<InventoryLocationsPost> IndexDbPost() => _inventorylocationsRepository.IndexDbPost();
        //Custom Code End
        public IQueryable<LocationFunctions> selectLocationFunctions() => _inventorylocationsRepository.selectLocationFunctions();
        public IQueryable<UnitsOfMeasurement> selectUnitsOfMeasurement() => _inventorylocationsRepository.selectUnitsOfMeasurement();
        public IQueryable<Companies> selectCompanies() => _inventorylocationsRepository.selectCompanies();
        public IQueryable<Facilities> selectFacilities() => _inventorylocationsRepository.selectFacilities();
        public IQueryable<FacilityZones> selectFacilityZones() => _inventorylocationsRepository.selectFacilityZones();
        public IQueryable<FacilityWorkAreas> selectFacilityWorkAreas() => _inventorylocationsRepository.selectFacilityWorkAreas();
        public IQueryable<FacilityFloors> selectFacilityFloors() => _inventorylocationsRepository.selectFacilityFloors();
        public IQueryable<FacilityAisleFaces> selectFacilityAisleFaces() => _inventorylocationsRepository.selectFacilityAisleFaces();
        public IQueryable<InventoryLocationSizes> selectInventoryLocationSizes() => _inventorylocationsRepository.selectInventoryLocationSizes();
       public IQueryable<LocationFunctions> LocationFunctionsDb() => _inventorylocationsRepository.LocationFunctionsDb();
        public IQueryable<UnitsOfMeasurement> UnitsOfMeasurementDb() => _inventorylocationsRepository.UnitsOfMeasurementDb();
        public IQueryable<Companies> CompaniesDb() => _inventorylocationsRepository.CompaniesDb();
        public IQueryable<Facilities> FacilitiesDb() => _inventorylocationsRepository.FacilitiesDb();
        public IQueryable<FacilityZones> FacilityZonesDb() => _inventorylocationsRepository.FacilityZonesDb();
        public IQueryable<FacilityWorkAreas> FacilityWorkAreasDb() => _inventorylocationsRepository.FacilityWorkAreasDb();
        public IQueryable<FacilityFloors> FacilityFloorsDb() => _inventorylocationsRepository.FacilityFloorsDb();
        public IQueryable<FacilityAisleFaces> FacilityAisleFacesDb() => _inventorylocationsRepository.FacilityAisleFacesDb();
        public IQueryable<InventoryLocationSizes> InventoryLocationSizesDb() => _inventorylocationsRepository.InventoryLocationSizesDb();
       public List<KeyValuePair<Int64?, string>> selectUnitsOfMeasurementNullable() => _inventorylocationsRepository.selectUnitsOfMeasurementNullable();
        public List<KeyValuePair<Int64?, string>> selectCompaniesNullable() => _inventorylocationsRepository.selectCompaniesNullable();
        public List<KeyValuePair<Int64?, string>> selectInventoryLocationSizesNullable() => _inventorylocationsRepository.selectInventoryLocationSizesNullable();
        public bool VerifyInventoryLocationUnique(Int64 ixInventoryLocation, string sInventoryLocation) => _inventorylocationsRepository.VerifyInventoryLocationUnique(ixInventoryLocation, sInventoryLocation);
        public List<string> VerifyInventoryLocationDeleteOK(Int64 ixInventoryLocation, string sInventoryLocation) => _inventorylocationsRepository.VerifyInventoryLocationDeleteOK(ixInventoryLocation, sInventoryLocation);

        public Task<Int64> Create(InventoryLocationsPost inventorylocationsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._inventorylocationsRepository.RegisterCreate(inventorylocationsPost);
            try
            {
                this._inventorylocationsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inventorylocationsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(inventorylocationsPost.ixInventoryLocation);

        }
        public Task Edit(InventoryLocationsPost inventorylocationsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._inventorylocationsRepository.RegisterEdit(inventorylocationsPost);
            try
            {
                this._inventorylocationsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inventorylocationsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(InventoryLocationsPost inventorylocationsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._inventorylocationsRepository.RegisterDelete(inventorylocationsPost);
            try
            {
                this._inventorylocationsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._inventorylocationsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

