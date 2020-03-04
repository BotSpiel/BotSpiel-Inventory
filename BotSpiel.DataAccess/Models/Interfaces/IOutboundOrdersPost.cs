using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IOutboundOrdersPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixOutboundOrder { get; set; }
		String sOutboundOrder { get; set; }
		String sOrderReference { get; set; }
		Int64 ixOutboundOrderType { get; set; }
		Int64 ixFacility { get; set; }
		Int64 ixCompany { get; set; }
		Int64 ixBusinessPartner { get; set; }
		DateTime? dtDeliverEarliest { get; set; }
		DateTime? dtDeliverLatest { get; set; }
		Int64 ixCarrierService { get; set; }
		Int64 ixStatus { get; set; }
		Int64? ixPickBatch { get; set; }
		Int64? ixOutboundShipment { get; set; }
		String UserName { get; set; }
    }
}
  

