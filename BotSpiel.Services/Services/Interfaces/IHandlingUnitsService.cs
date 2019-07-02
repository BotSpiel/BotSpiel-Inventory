using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IHandlingUnitsService
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
       IQueryable<Statuses> selectStatuses();
        IQueryable<UnitsOfMeasurement> selectUnitsOfMeasurement();
        IQueryable<Materials> selectMaterials();
        IQueryable<HandlingUnits> selectHandlingUnits();
        IQueryable<HandlingUnitTypes> selectHandlingUnitTypes();
        IQueryable<MaterialHandlingUnitConfigurations> selectMaterialHandlingUnitConfigurations();
       List<KeyValuePair<Int64?, string>> selectStatusesNullable();
        List<KeyValuePair<Int64?, string>> selectUnitsOfMeasurementNullable();
        List<KeyValuePair<Int64?, string>> selectMaterialsNullable();
        List<KeyValuePair<Int64?, string>> selectHandlingUnitsNullable();
        List<KeyValuePair<Int64?, string>> selectMaterialHandlingUnitConfigurationsNullable();
        bool VerifyHandlingUnitUnique(Int64 ixHandlingUnit, string sHandlingUnit);
        List<string> VerifyHandlingUnitDeleteOK(Int64 ixHandlingUnit, string sHandlingUnit);

        Task<Int64> Create(HandlingUnitsPost handlingunitsPost);
        Task Edit(HandlingUnitsPost handlingunitsPost);
        Task Delete(HandlingUnitsPost handlingunitsPost);
    }
}
  

