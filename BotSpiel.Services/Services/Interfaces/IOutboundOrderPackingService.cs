using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IOutboundOrderPackingService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        OutboundOrderPackingPost GetPost(Int64 ixOutboundOrderPack);        
		OutboundOrderPacking Get(Int64 ixOutboundOrderPack);
        IQueryable<OutboundOrderPacking> Index();
       IQueryable<Statuses> selectStatuses();
        IQueryable<HandlingUnits> selectHandlingUnits();
        IQueryable<OutboundOrderLines> selectOutboundOrderLines();
        bool VerifyOutboundOrderPackUnique(Int64 ixOutboundOrderPack, string sOutboundOrderPack);
        List<string> VerifyOutboundOrderPackDeleteOK(Int64 ixOutboundOrderPack, string sOutboundOrderPack);

        Task<Int64> Create(OutboundOrderPackingPost outboundorderpackingPost);
        Task Edit(OutboundOrderPackingPost outboundorderpackingPost);
        Task Delete(OutboundOrderPackingPost outboundorderpackingPost);
    }
}
  

