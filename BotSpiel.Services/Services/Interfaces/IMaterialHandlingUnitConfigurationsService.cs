using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IMaterialHandlingUnitConfigurationsService
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

        Task<Int64> Create(MaterialHandlingUnitConfigurationsPost materialhandlingunitconfigurationsPost);
        Task Edit(MaterialHandlingUnitConfigurationsPost materialhandlingunitconfigurationsPost);
        Task Delete(MaterialHandlingUnitConfigurationsPost materialhandlingunitconfigurationsPost);
    }
}
  

