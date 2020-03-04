using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IHandlingUnitsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        HandlingUnitsPost GetPost(Int64 ixHandlingUnit);        
		HandlingUnits Get(Int64 ixHandlingUnit);
        IQueryable<HandlingUnits> Index();
        IQueryable<HandlingUnits> IndexDb();
       IQueryable<Statuses> selectStatuses();
        IQueryable<UnitsOfMeasurement> selectUnitsOfMeasurement();
        IQueryable<Materials> selectMaterials();
        IQueryable<HandlingUnits> selectHandlingUnits();
        IQueryable<HandlingUnitTypes> selectHandlingUnitTypes();
        IQueryable<MaterialHandlingUnitConfigurations> selectMaterialHandlingUnitConfigurations();
       IQueryable<Statuses> StatusesDb();
        IQueryable<UnitsOfMeasurement> UnitsOfMeasurementDb();
        IQueryable<Materials> MaterialsDb();
        IQueryable<HandlingUnits> HandlingUnitsDb();
        IQueryable<HandlingUnitTypes> HandlingUnitTypesDb();
        IQueryable<MaterialHandlingUnitConfigurations> MaterialHandlingUnitConfigurationsDb();
       List<KeyValuePair<Int64?, string>> selectStatusesNullable();
        List<KeyValuePair<Int64?, string>> selectUnitsOfMeasurementNullable();
        List<KeyValuePair<Int64?, string>> selectMaterialsNullable();
        List<KeyValuePair<Int64?, string>> selectHandlingUnitsNullable();
        List<KeyValuePair<Int64?, string>> selectMaterialHandlingUnitConfigurationsNullable();
        bool VerifyHandlingUnitUnique(Int64 ixHandlingUnit, string sHandlingUnit);
        List<string> VerifyHandlingUnitDeleteOK(Int64 ixHandlingUnit, string sHandlingUnit);
        void RegisterCreate(HandlingUnitsPost handlingunitsPost);
        void RegisterEdit(HandlingUnitsPost handlingunitsPost);
        void RegisterDelete(HandlingUnitsPost handlingunitsPost);
        void Rollback();
        void Commit();
    }
}
  

