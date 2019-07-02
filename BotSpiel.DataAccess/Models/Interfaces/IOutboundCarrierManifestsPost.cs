using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IOutboundCarrierManifestsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixOutboundCarrierManifest { get; set; }
		String sOutboundCarrierManifest { get; set; }
		Int64 ixCarrier { get; set; }
		Int64? ixPickupInventoryLocation { get; set; }
		DateTime? dtScheduledPickupAt { get; set; }
		Int64 ixStatus { get; set; }
		String UserName { get; set; }
    }
}
  

