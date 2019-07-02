using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IInventoryLocationsSlottingRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        InventoryLocationsSlottingPost GetPost(Int64 ixInventoryLocationSlotting);        
		InventoryLocationsSlotting Get(Int64 ixInventoryLocationSlotting);
        IQueryable<InventoryLocationsSlotting> Index();
       IQueryable<Materials> selectMaterials();
        IQueryable<InventoryLocations> selectInventoryLocations();
        bool VerifyInventoryLocationSlottingUnique(Int64 ixInventoryLocationSlotting, string sInventoryLocationSlotting);
        List<string> VerifyInventoryLocationSlottingDeleteOK(Int64 ixInventoryLocationSlotting, string sInventoryLocationSlotting);
        void RegisterCreate(InventoryLocationsSlottingPost inventorylocationsslottingPost);
        void RegisterEdit(InventoryLocationsSlottingPost inventorylocationsslottingPost);
        void RegisterDelete(InventoryLocationsSlottingPost inventorylocationsslottingPost);
        void Rollback();
        void Commit();
    }
}
  

