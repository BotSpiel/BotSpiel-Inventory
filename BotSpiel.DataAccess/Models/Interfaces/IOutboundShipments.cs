using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IOutboundShipments
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixOutboundShipment { get; set; }
		Int64 ixOutboundShipmentEdit { get; set; }
		String sOutboundShipment { get; set; }
		Int64 ixFacility { get; set; }
		Int64 ixCompany { get; set; }
		Int64 ixCarrier { get; set; }
		String sCarrierConsignmentNumber { get; set; }
		Int64 ixStatus { get; set; }
		Int64 ixAddress { get; set; }
		Int64? ixOutboundCarrierManifest { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		Facilities Facilities { get; set; }
		Companies Companies { get; set; }
		Carriers Carriers { get; set; }
		Statuses Statuses { get; set; }
		Addresses Addresses { get; set; }
		OutboundCarrierManifests OutboundCarrierManifests { get; set; }
    }
}
  

