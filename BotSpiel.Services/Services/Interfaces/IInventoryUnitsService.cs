using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IInventoryUnitsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        InventoryUnitsPost GetPost(Int64 ixInventoryUnit);        
		InventoryUnits Get(Int64 ixInventoryUnit);
        IQueryable<InventoryUnits> Index();
       IQueryable<Statuses> selectStatuses();
        IQueryable<Companies> selectCompanies();
        IQueryable<Facilities> selectFacilities();
        IQueryable<Materials> selectMaterials();
        IQueryable<InventoryStates> selectInventoryStates();
        IQueryable<HandlingUnits> selectHandlingUnits();
        IQueryable<InventoryLocations> selectInventoryLocations();
       List<KeyValuePair<Int64?, string>> selectHandlingUnitsNullable();
        bool VerifyInventoryUnitUnique(Int64 ixInventoryUnit, string sInventoryUnit);
        List<string> VerifyInventoryUnitDeleteOK(Int64 ixInventoryUnit, string sInventoryUnit);
        //Custom Code Start | Replaced Code Block
        //Replaced Code Block Start
        //Task<Int64> Create(InventoryUnitsPost inventoryunitsPost);
        //Task Edit(InventoryUnitsPost inventoryunitsPost);
        //Replaced Code Block End
        Task<Int64> Create(InventoryUnitsPost inventoryunitsPost, Int64 ixInventoryUnitTransactionContext);
        Task Edit(InventoryUnitsPost inventoryunitsPost, Int64 ixInventoryUnitTransactionContext);
        //Custom Code End
        Task Delete(InventoryUnitsPost inventoryunitsPost);
    }
}
  
