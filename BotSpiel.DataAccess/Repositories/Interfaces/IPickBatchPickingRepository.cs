using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IPickBatchPickingRepository
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
        void RegisterCreate(PickBatchPickingPost pickbatchpickingPost);
        void RegisterEdit(PickBatchPickingPost pickbatchpickingPost);
        void RegisterDelete(PickBatchPickingPost pickbatchpickingPost);
        void Rollback();
        void Commit();
    }
}
  

