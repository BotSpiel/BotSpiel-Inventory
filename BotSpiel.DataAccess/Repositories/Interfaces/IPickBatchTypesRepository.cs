using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IPickBatchTypesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        PickBatchTypesPost GetPost(Int64 ixPickBatchType);        
		PickBatchTypes Get(Int64 ixPickBatchType);
        IQueryable<PickBatchTypes> Index();
        IQueryable<PickBatchTypes> IndexDb();
        bool VerifyPickBatchTypeUnique(Int64 ixPickBatchType, string sPickBatchType);
        List<string> VerifyPickBatchTypeDeleteOK(Int64 ixPickBatchType, string sPickBatchType);
        void RegisterCreate(PickBatchTypesPost pickbatchtypesPost);
        void RegisterEdit(PickBatchTypesPost pickbatchtypesPost);
        void RegisterDelete(PickBatchTypesPost pickbatchtypesPost);
        void Rollback();
        void Commit();
    }
}
  

