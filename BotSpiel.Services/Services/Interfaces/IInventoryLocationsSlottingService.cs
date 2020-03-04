using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IInventoryLocationsSlottingService
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
        IQueryable<InventoryLocationsSlotting> IndexDb();
       IQueryable<Materials> selectMaterials();
        IQueryable<InventoryLocations> selectInventoryLocations();
       IQueryable<Materials> MaterialsDb();
        IQueryable<InventoryLocations> InventoryLocationsDb();
        bool VerifyInventoryLocationSlottingUnique(Int64 ixInventoryLocationSlotting, string sInventoryLocationSlotting);
        List<string> VerifyInventoryLocationSlottingDeleteOK(Int64 ixInventoryLocationSlotting, string sInventoryLocationSlotting);

        Task<Int64> Create(InventoryLocationsSlottingPost inventorylocationsslottingPost);
        Task Edit(InventoryLocationsSlottingPost inventorylocationsslottingPost);
        Task Delete(InventoryLocationsSlottingPost inventorylocationsslottingPost);
    }
}
  

