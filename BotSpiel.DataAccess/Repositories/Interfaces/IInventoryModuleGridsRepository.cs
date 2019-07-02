using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IInventoryModuleGridsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        InventoryModuleGridsPost GetPost(Int64 ixInventoryModuleGrid);        
		InventoryModuleGrids Get(Int64 ixInventoryModuleGrid);
        IQueryable<InventoryModuleGrids> Index();
		IQueryable<InventoryModuleGridsconfig> Indexconfig();
		IQueryable<InventoryModuleGridsmd> Indexmd();
		IQueryable<InventoryModuleGridstx> Indextx();
		IQueryable<InventoryModuleGridsanalytics> Indexanalytics();
        List<string> VerifyInventoryModuleGridDeleteOK(Int64 ixInventoryModuleGrid, string sInventoryModuleGrid);
        void RegisterCreate(InventoryModuleGridsPost inventorymodulegridsPost);
        void RegisterEdit(InventoryModuleGridsPost inventorymodulegridsPost);
        void RegisterDelete(InventoryModuleGridsPost inventorymodulegridsPost);
        void Rollback();
        void Commit();
    }
}
  

