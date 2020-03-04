using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IInventoryStatesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        InventoryStatesPost GetPost(Int64 ixInventoryState);        
		InventoryStates Get(Int64 ixInventoryState);
        IQueryable<InventoryStates> Index();
        IQueryable<InventoryStates> IndexDb();
        bool VerifyInventoryStateUnique(Int64 ixInventoryState, string sInventoryState);
        List<string> VerifyInventoryStateDeleteOK(Int64 ixInventoryState, string sInventoryState);
        void RegisterCreate(InventoryStatesPost inventorystatesPost);
        void RegisterEdit(InventoryStatesPost inventorystatesPost);
        void RegisterDelete(InventoryStatesPost inventorystatesPost);
        void Rollback();
        void Commit();
    }
}
  

