using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IOutboundCarrierManifestPickups
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixOutboundCarrierManifestPickup { get; set; }
		Int64 ixOutboundCarrierManifestPickupEdit { get; set; }
		String sOutboundCarrierManifestPickup { get; set; }
		Int64 ixOutboundCarrierManifest { get; set; }
		Int64 ixStatus { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		OutboundCarrierManifests OutboundCarrierManifests { get; set; }
		Statuses Statuses { get; set; }
    }
}
  

