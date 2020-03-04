using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IMaterialHandlingUnitConfigurationsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        MaterialHandlingUnitConfigurationsPost GetPost(Int64 ixMaterialHandlingUnitConfiguration);        
		MaterialHandlingUnitConfigurations Get(Int64 ixMaterialHandlingUnitConfiguration);
        IQueryable<MaterialHandlingUnitConfigurations> Index();
        IQueryable<MaterialHandlingUnitConfigurations> IndexDb();
       IQueryable<UnitsOfMeasurement> selectUnitsOfMeasurement();
        IQueryable<Materials> selectMaterials();
        IQueryable<HandlingUnitTypes> selectHandlingUnitTypes();
       IQueryable<UnitsOfMeasurement> UnitsOfMeasurementDb();
        IQueryable<Materials> MaterialsDb();
        IQueryable<HandlingUnitTypes> HandlingUnitTypesDb();
       List<KeyValuePair<Int64?, string>> selectUnitsOfMeasurementNullable();
        bool VerifyMaterialHandlingUnitConfigurationUnique(Int64 ixMaterialHandlingUnitConfiguration, string sMaterialHandlingUnitConfiguration);
        List<string> VerifyMaterialHandlingUnitConfigurationDeleteOK(Int64 ixMaterialHandlingUnitConfiguration, string sMaterialHandlingUnitConfiguration);
        void RegisterCreate(MaterialHandlingUnitConfigurationsPost materialhandlingunitconfigurationsPost);
        void RegisterEdit(MaterialHandlingUnitConfigurationsPost materialhandlingunitconfigurationsPost);
        void RegisterDelete(MaterialHandlingUnitConfigurationsPost materialhandlingunitconfigurationsPost);
        void Rollback();
        void Commit();
    }
}
  

