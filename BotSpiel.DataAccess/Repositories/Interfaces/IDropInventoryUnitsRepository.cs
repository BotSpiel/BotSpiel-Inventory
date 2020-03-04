using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IDropInventoryUnitsRepository
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
        void RegisterCreate(DropInventoryUnitsPost dropinventoryunitsPost);
        void RegisterEdit(DropInventoryUnitsPost dropinventoryunitsPost);
        void RegisterDelete(DropInventoryUnitsPost dropinventoryunitsPost);
        void Rollback();
        void Commit();
    }
}
  

