using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IInventoryModuleGridsService
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

        Task<Int64> Create(InventoryModuleGridsPost inventorymodulegridsPost);
        Task Edit(InventoryModuleGridsPost inventorymodulegridsPost);
        Task Delete(InventoryModuleGridsPost inventorymodulegridsPost);
    }
}
  

