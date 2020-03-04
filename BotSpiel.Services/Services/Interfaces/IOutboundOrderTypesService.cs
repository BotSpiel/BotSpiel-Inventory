using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IOutboundOrderTypesService
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

        Task<Int64> Create(OutboundOrderTypesPost outboundordertypesPost);
        Task Edit(OutboundOrderTypesPost outboundordertypesPost);
        Task Delete(OutboundOrderTypesPost outboundordertypesPost);
    }
}
  

