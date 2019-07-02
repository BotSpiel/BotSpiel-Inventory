using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IInventoryUnitTransactionContextsRepository
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
        void RegisterCreate(InventoryUnitTransactionContextsPost inventoryunittransactioncontextsPost);
        void RegisterEdit(InventoryUnitTransactionContextsPost inventoryunittransactioncontextsPost);
        void RegisterDelete(InventoryUnitTransactionContextsPost inventoryunittransactioncontextsPost);
        void Rollback();
        void Commit();
    }
}
  

