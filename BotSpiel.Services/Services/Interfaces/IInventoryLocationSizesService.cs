using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IInventoryLocationSizesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        InventoryLocationSizesPost GetPost(Int64 ixInventoryLocationSize);        
		InventoryLocationSizes Get(Int64 ixInventoryLocationSize);
        IQueryable<InventoryLocationSizes> Index();
       IQueryable<UnitsOfMeasurement> selectUnitsOfMeasurement();
        bool VerifyInventoryLocationSizeUnique(Int64 ixInventoryLocationSize, string sInventoryLocationSize);
        List<string> VerifyInventoryLocationSizeDeleteOK(Int64 ixInventoryLocationSize, string sInventoryLocationSize);

        Task<Int64> Create(InventoryLocationSizesPost inventorylocationsizesPost);
        Task Edit(InventoryLocationSizesPost inventorylocationsizesPost);
        Task Delete(InventoryLocationSizesPost inventorylocationsizesPost);
    }
}
  

