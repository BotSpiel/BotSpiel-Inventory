using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IInboundOrderTypesService
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
        bool VerifyInboundOrderTypeUnique(Int64 ixInboundOrderType, string sInboundOrderType);
        List<string> VerifyInboundOrderTypeDeleteOK(Int64 ixInboundOrderType, string sInboundOrderType);

        Task<Int64> Create(InboundOrderTypesPost inboundordertypesPost);
        Task Edit(InboundOrderTypesPost inboundordertypesPost);
        Task Delete(InboundOrderTypesPost inboundordertypesPost);
    }
}
  

