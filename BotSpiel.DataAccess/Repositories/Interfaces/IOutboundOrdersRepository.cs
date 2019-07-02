using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IOutboundOrdersRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        OutboundOrdersPost GetPost(Int64 ixOutboundOrder);        
		OutboundOrders Get(Int64 ixOutboundOrder);
        IQueryable<OutboundOrders> Index();
       IQueryable<Statuses> selectStatuses();
        IQueryable<Companies> selectCompanies();
        IQueryable<Facilities> selectFacilities();
        IQueryable<BusinessPartners> selectBusinessPartners();
        IQueryable<OutboundOrderTypes> selectOutboundOrderTypes();
        IQueryable<CarrierServices> selectCarrierServices();
        IQueryable<OutboundShipments> selectOutboundShipments();
        IQueryable<PickBatches> selectPickBatches();
       List<KeyValuePair<Int64?, string>> selectOutboundShipmentsNullable();
        List<KeyValuePair<Int64?, string>> selectPickBatchesNullable();
        bool VerifyOutboundOrderUnique(Int64 ixOutboundOrder, string sOutboundOrder);
        List<string> VerifyOutboundOrderDeleteOK(Int64 ixOutboundOrder, string sOutboundOrder);
        void RegisterCreate(OutboundOrdersPost outboundordersPost);
        void RegisterEdit(OutboundOrdersPost outboundordersPost);
        void RegisterDelete(OutboundOrdersPost outboundordersPost);
        void Rollback();
        void Commit();
    }
}
  

