using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IPickBatchTypesService
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
        bool VerifyPickBatchTypeUnique(Int64 ixPickBatchType, string sPickBatchType);
        List<string> VerifyPickBatchTypeDeleteOK(Int64 ixPickBatchType, string sPickBatchType);

        Task<Int64> Create(PickBatchTypesPost pickbatchtypesPost);
        Task Edit(PickBatchTypesPost pickbatchtypesPost);
        Task Delete(PickBatchTypesPost pickbatchtypesPost);
    }
}
  

