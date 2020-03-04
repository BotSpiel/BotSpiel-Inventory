using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IPickBatchPickingService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        PickBatchPickingPost GetPost(Int64 ixPickBatchPick);        
		PickBatchPicking Get(Int64 ixPickBatchPick);
        IQueryable<PickBatchPicking> Index();
        IQueryable<PickBatchPicking> IndexDb();
       IQueryable<InventoryUnits> selectInventoryUnits();
        IQueryable<HandlingUnits> selectHandlingUnits();
        IQueryable<PickBatches> selectPickBatches();
       IQueryable<InventoryUnits> InventoryUnitsDb();
        IQueryable<HandlingUnits> HandlingUnitsDb();
        IQueryable<PickBatches> PickBatchesDb();
        List<string> VerifyPickBatchPickDeleteOK(Int64 ixPickBatchPick, string sPickBatchPick);

        Task<Int64> Create(PickBatchPickingPost pickbatchpickingPost);
        Task Edit(PickBatchPickingPost pickbatchpickingPost);
        Task Delete(PickBatchPickingPost pickbatchpickingPost);
    }
}
  

