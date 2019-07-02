using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IInboundOrdersService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        InboundOrdersPost GetPost(Int64 ixInboundOrder);        
		InboundOrders Get(Int64 ixInboundOrder);
        IQueryable<InboundOrders> Index();
       IQueryable<Statuses> selectStatuses();
        IQueryable<Companies> selectCompanies();
        IQueryable<Facilities> selectFacilities();
        IQueryable<BusinessPartners> selectBusinessPartners();
        IQueryable<InboundOrderTypes> selectInboundOrderTypes();
        bool VerifyInboundOrderUnique(Int64 ixInboundOrder, string sInboundOrder);
        List<string> VerifyInboundOrderDeleteOK(Int64 ixInboundOrder, string sInboundOrder);

        Task<Int64> Create(InboundOrdersPost inboundordersPost);
        Task Edit(InboundOrdersPost inboundordersPost);
        Task Delete(InboundOrdersPost inboundordersPost);
    }
}
  

