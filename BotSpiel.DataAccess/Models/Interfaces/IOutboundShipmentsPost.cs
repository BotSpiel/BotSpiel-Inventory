using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IOutboundShipmentsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixOutboundShipment { get; set; }
		String sOutboundShipment { get; set; }
		Int64 ixFacility { get; set; }
		Int64 ixCompany { get; set; }
		Int64 ixCarrier { get; set; }
		String sCarrierConsignmentNumber { get; set; }
		Int64 ixStatus { get; set; }
		Int64 ixAddress { get; set; }
		Int64? ixOutboundCarrierManifest { get; set; }
		String UserName { get; set; }
    }
}
  

