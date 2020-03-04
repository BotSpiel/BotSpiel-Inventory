using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IPickBatchesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        PickBatchesPost GetPost(Int64 ixPickBatch);        
		PickBatches Get(Int64 ixPickBatch);
        IQueryable<PickBatches> Index();
        IQueryable<PickBatches> IndexDb();

        //Custom Code Start | Added Code Block 
        IQueryable<PickBatchesPost> IndexDbPost();
        //Custom Code End
        IQueryable<Statuses> selectStatuses();
        IQueryable<PickBatchTypes> selectPickBatchTypes();
       IQueryable<Statuses> StatusesDb();
        IQueryable<PickBatchTypes> PickBatchTypesDb();
        bool VerifyPickBatchUnique(Int64 ixPickBatch, string sPickBatch);
        List<string> VerifyPickBatchDeleteOK(Int64 ixPickBatch, string sPickBatch);
        void RegisterCreate(PickBatchesPost pickbatchesPost);
        void RegisterEdit(PickBatchesPost pickbatchesPost);
        void RegisterDelete(PickBatchesPost pickbatchesPost);
        void Rollback();
        void Commit();
    }
}
  

