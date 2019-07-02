using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IInventoryUnitsRepository
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
        void RegisterCreate(InventoryUnitsPost inventoryunitsPost);
        void RegisterEdit(InventoryUnitsPost inventoryunitsPost);
        void RegisterDelete(InventoryUnitsPost inventoryunitsPost);
        void Rollback();
        void Commit();
    }
}
  

