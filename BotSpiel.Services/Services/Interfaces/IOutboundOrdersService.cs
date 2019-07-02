using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IOutboundOrdersService
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

        Task<Int64> Create(OutboundOrdersPost outboundordersPost);
        Task Edit(OutboundOrdersPost outboundordersPost);
        Task Delete(OutboundOrdersPost outboundordersPost);
    }
}
  

