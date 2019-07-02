using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IInboundOrders
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixInboundOrder { get; set; }
		Int64 ixInboundOrderEdit { get; set; }
		String sInboundOrder { get; set; }
		String sOrderReference { get; set; }
		Int64 ixInboundOrderType { get; set; }
		Int64 ixFacility { get; set; }
		Int64 ixCompany { get; set; }
		Int64 ixBusinessPartner { get; set; }
		DateTime? dtExpectedAt { get; set; }
		Int64 ixStatus { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		InboundOrderTypes InboundOrderTypes { get; set; }
		Facilities Facilities { get; set; }
		Companies Companies { get; set; }
		BusinessPartners BusinessPartners { get; set; }
		Statuses Statuses { get; set; }
    }
}
  

