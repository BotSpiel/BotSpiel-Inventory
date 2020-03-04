using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IInboundOrderTypesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        InboundOrderTypesPost GetPost(Int64 ixInboundOrderType);        
		InboundOrderTypes Get(Int64 ixInboundOrderType);
        IQueryable<InboundOrderTypes> Index();
        IQueryable<InboundOrderTypes> IndexDb();
        bool VerifyInboundOrderTypeUnique(Int64 ixInboundOrderType, string sInboundOrderType);
        List<string> VerifyInboundOrderTypeDeleteOK(Int64 ixInboundOrderType, string sInboundOrderType);
        void RegisterCreate(InboundOrderTypesPost inboundordertypesPost);
        void RegisterEdit(InboundOrderTypesPost inboundordertypesPost);
        void RegisterDelete(InboundOrderTypesPost inboundordertypesPost);
        void Rollback();
        void Commit();
    }
}
  

