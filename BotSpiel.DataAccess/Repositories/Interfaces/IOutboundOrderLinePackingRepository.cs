using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IOutboundOrderLinePackingRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        OutboundOrderLinePackingPost GetPost(Int64 ixOutboundOrderLinePack);        
		OutboundOrderLinePacking Get(Int64 ixOutboundOrderLinePack);
        IQueryable<OutboundOrderLinePacking> Index();
        IQueryable<OutboundOrderLinePacking> IndexDb();
       IQueryable<Statuses> selectStatuses();
        IQueryable<HandlingUnits> selectHandlingUnits();
        IQueryable<OutboundOrderLines> selectOutboundOrderLines();
       IQueryable<Statuses> StatusesDb();
        IQueryable<HandlingUnits> HandlingUnitsDb();
        IQueryable<OutboundOrderLines> OutboundOrderLinesDb();
        bool VerifyOutboundOrderLinePackUnique(Int64 ixOutboundOrderLinePack, string sOutboundOrderLinePack);
        List<string> VerifyOutboundOrderLinePackDeleteOK(Int64 ixOutboundOrderLinePack, string sOutboundOrderLinePack);
        void RegisterCreate(OutboundOrderLinePackingPost outboundorderlinepackingPost);
        void RegisterEdit(OutboundOrderLinePackingPost outboundorderlinepackingPost);
        void RegisterDelete(OutboundOrderLinePackingPost outboundorderlinepackingPost);
        void Rollback();
        void Commit();
    }
}
  

