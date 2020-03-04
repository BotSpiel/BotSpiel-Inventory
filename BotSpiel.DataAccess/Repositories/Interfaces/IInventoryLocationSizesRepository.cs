using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IInventoryLocationSizesRepository
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
        IQueryable<InventoryLocationSizes> IndexDb();
       IQueryable<UnitsOfMeasurement> selectUnitsOfMeasurement();
       IQueryable<UnitsOfMeasurement> UnitsOfMeasurementDb();
        bool VerifyInventoryLocationSizeUnique(Int64 ixInventoryLocationSize, string sInventoryLocationSize);
        List<string> VerifyInventoryLocationSizeDeleteOK(Int64 ixInventoryLocationSize, string sInventoryLocationSize);
        void RegisterCreate(InventoryLocationSizesPost inventorylocationsizesPost);
        void RegisterEdit(InventoryLocationSizesPost inventorylocationsizesPost);
        void RegisterDelete(InventoryLocationSizesPost inventorylocationsizesPost);
        void Rollback();
        void Commit();
    }
}
  

