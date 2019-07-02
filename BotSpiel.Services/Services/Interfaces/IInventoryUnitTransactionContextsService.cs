using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IInventoryUnitTransactionContextsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        InventoryUnitTransactionContextsPost GetPost(Int64 ixInventoryUnitTransactionContext);        
		InventoryUnitTransactionContexts Get(Int64 ixInventoryUnitTransactionContext);
        IQueryable<InventoryUnitTransactionContexts> Index();
        bool VerifyInventoryUnitTransactionContextUnique(Int64 ixInventoryUnitTransactionContext, string sInventoryUnitTransactionContext);
        List<string> VerifyInventoryUnitTransactionContextDeleteOK(Int64 ixInventoryUnitTransactionContext, string sInventoryUnitTransactionContext);

        Task<Int64> Create(InventoryUnitTransactionContextsPost inventoryunittransactioncontextsPost);
        Task Edit(InventoryUnitTransactionContextsPost inventoryunittransactioncontextsPost);
        Task Delete(InventoryUnitTransactionContextsPost inventoryunittransactioncontextsPost);
    }
}
  

