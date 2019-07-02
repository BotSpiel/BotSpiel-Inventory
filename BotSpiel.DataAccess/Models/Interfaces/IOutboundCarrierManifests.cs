using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IOutboundCarrierManifests
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixOutboundCarrierManifest { get; set; }
		Int64 ixOutboundCarrierManifestEdit { get; set; }
		String sOutboundCarrierManifest { get; set; }
		Int64 ixCarrier { get; set; }
		Int64? ixPickupInventoryLocation { get; set; }
		DateTime? dtScheduledPickupAt { get; set; }
		Int64 ixStatus { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		Carriers Carriers { get; set; }
		InventoryLocations InventoryLocationsFKDiffPickupInventoryLocation { get; set; }
		Statuses Statuses { get; set; }
    }
}
  

