using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IHandlingUnitsShippingRepository
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
        void RegisterCreate(HandlingUnitsShippingPost handlingunitsshippingPost);
        void RegisterEdit(HandlingUnitsShippingPost handlingunitsshippingPost);
        void RegisterDelete(HandlingUnitsShippingPost handlingunitsshippingPost);
        void Rollback();
        void Commit();
    }
}
  

