using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IOutboundOrderTypesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        OutboundOrderTypesPost GetPost(Int64 ixOutboundOrderType);        
		OutboundOrderTypes Get(Int64 ixOutboundOrderType);
        IQueryable<OutboundOrderTypes> Index();
        IQueryable<OutboundOrderTypes> IndexDb();
        bool VerifyOutboundOrderTypeUnique(Int64 ixOutboundOrderType, string sOutboundOrderType);
        List<string> VerifyOutboundOrderTypeDeleteOK(Int64 ixOutboundOrderType, string sOutboundOrderType);
        void RegisterCreate(OutboundOrderTypesPost outboundordertypesPost);
        void RegisterEdit(OutboundOrderTypesPost outboundordertypesPost);
        void RegisterDelete(OutboundOrderTypesPost outboundordertypesPost);
        void Rollback();
        void Commit();
    }
}
  

