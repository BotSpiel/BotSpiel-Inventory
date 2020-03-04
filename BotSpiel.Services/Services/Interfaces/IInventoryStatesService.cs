using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IInventoryStatesService
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

        Task<Int64> Create(InventoryStatesPost inventorystatesPost);
        Task Edit(InventoryStatesPost inventorystatesPost);
        Task Delete(InventoryStatesPost inventorystatesPost);
    }
}
  

