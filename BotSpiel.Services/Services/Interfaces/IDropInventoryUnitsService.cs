using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IDropInventoryUnitsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        DropInventoryUnitsPost GetPost(Int64 ixDropInventoryUnit);        
		DropInventoryUnits Get(Int64 ixDropInventoryUnit);
        IQueryable<DropInventoryUnits> Index();
        IQueryable<DropInventoryUnits> IndexDb();
        List<string> VerifyDropInventoryUnitDeleteOK(Int64 ixDropInventoryUnit, string sDropInventoryUnit);

        Task<Int64> Create(DropInventoryUnitsPost dropinventoryunitsPost);
        Task Edit(DropInventoryUnitsPost dropinventoryunitsPost);
        Task Delete(DropInventoryUnitsPost dropinventoryunitsPost);
    }
}
  

