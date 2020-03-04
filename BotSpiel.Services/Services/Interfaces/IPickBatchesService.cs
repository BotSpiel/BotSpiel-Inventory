using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IPickBatchesService
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

        Task<Int64> Create(PickBatchesPost pickbatchesPost);
        Task Edit(PickBatchesPost pickbatchesPost);
        Task Delete(PickBatchesPost pickbatchesPost);
    }
}
  

