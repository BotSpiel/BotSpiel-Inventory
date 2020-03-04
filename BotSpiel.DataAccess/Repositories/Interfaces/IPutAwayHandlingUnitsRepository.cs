using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IPutAwayHandlingUnitsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        PutAwayHandlingUnitsPost GetPost(Int64 ixPutAwayHandlingUnit);        
		PutAwayHandlingUnits Get(Int64 ixPutAwayHandlingUnit);
        IQueryable<PutAwayHandlingUnits> Index();
        IQueryable<PutAwayHandlingUnits> IndexDb();
       IQueryable<HandlingUnits> selectHandlingUnits();
        IQueryable<InventoryLocations> selectInventoryLocations();
       IQueryable<HandlingUnits> HandlingUnitsDb();
        IQueryable<InventoryLocations> InventoryLocationsDb();
        List<string> VerifyPutAwayHandlingUnitDeleteOK(Int64 ixPutAwayHandlingUnit, string sPutAwayHandlingUnit);
        void RegisterCreate(PutAwayHandlingUnitsPost putawayhandlingunitsPost);
        void RegisterEdit(PutAwayHandlingUnitsPost putawayhandlingunitsPost);
        void RegisterDelete(PutAwayHandlingUnitsPost putawayhandlingunitsPost);
        void Rollback();
        void Commit();
    }
}
  

