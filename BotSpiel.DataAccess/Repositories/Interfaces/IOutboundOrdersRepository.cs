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
        IQueryable<OutboundOrders> IndexDb();
        //Custom Code Start | Added Code Block 
        IQueryable<OutboundOrdersPost> IndexDbPost();
        //Custom Code End
        IQueryable<Statuses> selectStatuses();
        IQueryable<Companies> selectCompanies();
        IQueryable<Facilities> selectFacilities();
        IQueryable<BusinessPartners> selectBusinessPartners();
        IQueryable<OutboundOrderTypes> selectOutboundOrderTypes();
        IQueryable<CarrierServices> selectCarrierServices();
        IQueryable<OutboundShipments> selectOutboundShipments();
        IQueryable<PickBatches> selectPickBatches();
       IQueryable<Statuses> StatusesDb();
        IQueryable<Companies> CompaniesDb();
        IQueryable<Facilities> FacilitiesDb();
        IQueryable<BusinessPartners> BusinessPartnersDb();
        IQueryable<OutboundOrderTypes> OutboundOrderTypesDb();
        IQueryable<CarrierServices> CarrierServicesDb();
        IQueryable<OutboundShipments> OutboundShipmentsDb();
        IQueryable<PickBatches> PickBatchesDb();
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
  

