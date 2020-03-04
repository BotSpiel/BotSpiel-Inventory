using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IOutboundOrderLinesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        OutboundOrderLinesPost GetPost(Int64 ixOutboundOrderLine);        
		OutboundOrderLines Get(Int64 ixOutboundOrderLine);
        IQueryable<OutboundOrderLines> Index();
        IQueryable<OutboundOrderLines> IndexDb();
       IQueryable<Statuses> selectStatuses();
        IQueryable<Materials> selectMaterials();
        IQueryable<OutboundOrders> selectOutboundOrders();
       IQueryable<Statuses> StatusesDb();
        IQueryable<Materials> MaterialsDb();
        IQueryable<OutboundOrders> OutboundOrdersDb();
        bool VerifyOutboundOrderLineUnique(Int64 ixOutboundOrderLine, string sOutboundOrderLine);
        List<string> VerifyOutboundOrderLineDeleteOK(Int64 ixOutboundOrderLine, string sOutboundOrderLine);
        void RegisterCreate(OutboundOrderLinesPost outboundorderlinesPost);
        void RegisterEdit(OutboundOrderLinesPost outboundorderlinesPost);
        void RegisterDelete(OutboundOrderLinesPost outboundorderlinesPost);
        void Rollback();
        void Commit();
    }
}
  

