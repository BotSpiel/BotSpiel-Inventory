using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IHandlingUnitsShippingService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        HandlingUnitsShippingPost GetPost(Int64 ixHandlingUnitShipping);        
		HandlingUnitsShipping Get(Int64 ixHandlingUnitShipping);
        IQueryable<HandlingUnitsShipping> Index();
        IQueryable<HandlingUnitsShipping> IndexDb();
       IQueryable<Statuses> selectStatuses();
        IQueryable<HandlingUnits> selectHandlingUnits();
       IQueryable<Statuses> StatusesDb();
        IQueryable<HandlingUnits> HandlingUnitsDb();
        bool VerifyHandlingUnitShippingUnique(Int64 ixHandlingUnitShipping, string sHandlingUnitShipping);
        List<string> VerifyHandlingUnitShippingDeleteOK(Int64 ixHandlingUnitShipping, string sHandlingUnitShipping);

        Task<Int64> Create(HandlingUnitsShippingPost handlingunitsshippingPost);
        Task Edit(HandlingUnitsShippingPost handlingunitsshippingPost);
        Task Delete(HandlingUnitsShippingPost handlingunitsshippingPost);
    }
}
  

