using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IFacilityAisleFacesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        FacilityAisleFacesPost GetPost(Int64 ixFacilityAisleFace);        
		FacilityAisleFaces Get(Int64 ixFacilityAisleFace);
        IQueryable<FacilityAisleFaces> Index();
        IQueryable<FacilityAisleFaces> IndexDb();
       IQueryable<LocationFunctions> selectLocationFunctions();
        IQueryable<UnitsOfMeasurement> selectUnitsOfMeasurement();
        IQueryable<Facilities> selectFacilities();
        IQueryable<FacilityZones> selectFacilityZones();
        IQueryable<FacilityFloors> selectFacilityFloors();
        IQueryable<FacilityAisleFaces> selectFacilityAisleFaces();
        IQueryable<BaySequenceTypes> selectBaySequenceTypes();
        IQueryable<LogicalOrientations> selectLogicalOrientations();
        IQueryable<AisleFaceStorageTypes> selectAisleFaceStorageTypes();
        IQueryable<InventoryLocationSizes> selectInventoryLocationSizes();
       IQueryable<LocationFunctions> LocationFunctionsDb();
        IQueryable<UnitsOfMeasurement> UnitsOfMeasurementDb();
        IQueryable<Facilities> FacilitiesDb();
        IQueryable<FacilityZones> FacilityZonesDb();
        IQueryable<FacilityFloors> FacilityFloorsDb();
        IQueryable<FacilityAisleFaces> FacilityAisleFacesDb();
        IQueryable<BaySequenceTypes> BaySequenceTypesDb();
        IQueryable<LogicalOrientations> LogicalOrientationsDb();
        IQueryable<AisleFaceStorageTypes> AisleFaceStorageTypesDb();
        IQueryable<InventoryLocationSizes> InventoryLocationSizesDb();
       List<KeyValuePair<Int64?, string>> selectLocationFunctionsNullable();
        List<KeyValuePair<Int64?, string>> selectUnitsOfMeasurementNullable();
        List<KeyValuePair<Int64?, string>> selectFacilityZonesNullable();
        List<KeyValuePair<Int64?, string>> selectFacilityAisleFacesNullable();
        bool VerifyFacilityAisleFaceUnique(Int64 ixFacilityAisleFace, string sFacilityAisleFace);
        List<string> VerifyFacilityAisleFaceDeleteOK(Int64 ixFacilityAisleFace, string sFacilityAisleFace);

        Task<Int64> Create(FacilityAisleFacesPost facilityaislefacesPost);
        Task Edit(FacilityAisleFacesPost facilityaislefacesPost);
        Task Delete(FacilityAisleFacesPost facilityaislefacesPost);
    }
}
  

