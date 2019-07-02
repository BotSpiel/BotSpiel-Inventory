using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IOutboundOrders
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixOutboundOrder { get; set; }
		Int64 ixOutboundOrderEdit { get; set; }
		String sOutboundOrder { get; set; }
		String sOrderReference { get; set; }
		Int64 ixOutboundOrderType { get; set; }
		Int64 ixFacility { get; set; }
		Int64 ixCompany { get; set; }
		Int64 ixBusinessPartner { get; set; }
		DateTime dtDeliverEarliest { get; set; }
		DateTime dtDeliverLatest { get; set; }
		Int64 ixCarrierService { get; set; }
		Int64 ixStatus { get; set; }
		Int64? ixPickBatch { get; set; }
		Int64? ixOutboundShipment { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		OutboundOrderTypes OutboundOrderTypes { get; set; }
		Facilities Facilities { get; set; }
		Companies Companies { get; set; }
		BusinessPartners BusinessPartners { get; set; }
		CarrierServices CarrierServices { get; set; }
		Statuses Statuses { get; set; }
		PickBatches PickBatches { get; set; }
		OutboundShipments OutboundShipments { get; set; }
    }
}
  

