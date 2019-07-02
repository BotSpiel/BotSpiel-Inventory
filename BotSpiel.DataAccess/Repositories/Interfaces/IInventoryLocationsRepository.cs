using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IInventoryLocationsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        InventoryLocationsPost GetPost(Int64 ixInventoryLocation);        
		InventoryLocations Get(Int64 ixInventoryLocation);
        IQueryable<InventoryLocations> Index();
       IQueryable<LocationFunctions> selectLocationFunctions();
        IQueryable<UnitsOfMeasurement> selectUnitsOfMeasurement();
        IQueryable<Companies> selectCompanies();
        IQueryable<FacilityZones> selectFacilityZones();
        IQueryable<FacilityWorkAreas> selectFacilityWorkAreas();
        IQueryable<FacilityFloors> selectFacilityFloors();
        IQueryable<FacilityAisleFaces> selectFacilityAisleFaces();
        IQueryable<InventoryLocationSizes> selectInventoryLocationSizes();
       List<KeyValuePair<Int64?, string>> selectUnitsOfMeasurementNullable();
        List<KeyValuePair<Int64?, string>> selectCompaniesNullable();
        List<KeyValuePair<Int64?, string>> selectInventoryLocationSizesNullable();
        bool VerifyInventoryLocationUnique(Int64 ixInventoryLocation, string sInventoryLocation);
        List<string> VerifyInventoryLocationDeleteOK(Int64 ixInventoryLocation, string sInventoryLocation);
        void RegisterCreate(InventoryLocationsPost inventorylocationsPost);
        void RegisterEdit(InventoryLocationsPost inventorylocationsPost);
        void RegisterDelete(InventoryLocationsPost inventorylocationsPost);
        void Rollback();
        void Commit();
    }
}
  

