using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IOutboundOrderPackingRepository
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
        void RegisterCreate(OutboundOrderPackingPost outboundorderpackingPost);
        void RegisterEdit(OutboundOrderPackingPost outboundorderpackingPost);
        void RegisterDelete(OutboundOrderPackingPost outboundorderpackingPost);
        void Rollback();
        void Commit();
    }
}
  

