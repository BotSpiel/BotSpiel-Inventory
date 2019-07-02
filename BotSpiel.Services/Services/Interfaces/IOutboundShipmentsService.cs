using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IOutboundShipmentsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        OutboundShipmentsPost GetPost(Int64 ixOutboundShipment);        
		OutboundShipments Get(Int64 ixOutboundShipment);
        IQueryable<OutboundShipments> Index();
       IQueryable<Statuses> selectStatuses();
        IQueryable<Addresses> selectAddresses();
        IQueryable<Companies> selectCompanies();
        IQueryable<Facilities> selectFacilities();
        IQueryable<Carriers> selectCarriers();
        IQueryable<OutboundCarrierManifests> selectOutboundCarrierManifests();
       List<KeyValuePair<Int64?, string>> selectOutboundCarrierManifestsNullable();
        bool VerifyOutboundShipmentUnique(Int64 ixOutboundShipment, string sOutboundShipment);
        List<string> VerifyOutboundShipmentDeleteOK(Int64 ixOutboundShipment, string sOutboundShipment);

        Task<Int64> Create(OutboundShipmentsPost outboundshipmentsPost);
        Task Edit(OutboundShipmentsPost outboundshipmentsPost);
        Task Delete(OutboundShipmentsPost outboundshipmentsPost);
    }
}
  

