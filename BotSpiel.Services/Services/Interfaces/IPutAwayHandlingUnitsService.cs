using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IPutAwayHandlingUnitsService
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

        Task<Int64> Create(PutAwayHandlingUnitsPost putawayhandlingunitsPost);
        Task Edit(PutAwayHandlingUnitsPost putawayhandlingunitsPost);
        Task Delete(PutAwayHandlingUnitsPost putawayhandlingunitsPost);
    }
}
  

