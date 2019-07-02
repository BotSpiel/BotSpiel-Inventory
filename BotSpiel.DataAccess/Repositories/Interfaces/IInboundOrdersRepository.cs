using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IInboundOrdersRepository
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
        void RegisterCreate(InboundOrdersPost inboundordersPost);
        void RegisterEdit(InboundOrdersPost inboundordersPost);
        void RegisterDelete(InboundOrdersPost inboundordersPost);
        void Rollback();
        void Commit();
    }
}
  

