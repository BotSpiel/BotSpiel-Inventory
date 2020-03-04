using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IOutboundOrderLinesService
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

        Task<Int64> Create(OutboundOrderLinesPost outboundorderlinesPost);
        Task Edit(OutboundOrderLinesPost outboundorderlinesPost);
        Task Delete(OutboundOrderLinesPost outboundorderlinesPost);
    }
}
  

