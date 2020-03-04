using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IOutboundOrderLinePacking
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixOutboundOrderLinePack { get; set; }
		Int64 ixOutboundOrderLinePackEdit { get; set; }
		String sOutboundOrderLinePack { get; set; }
		Int64 ixOutboundOrderLine { get; set; }
		Int64 ixHandlingUnit { get; set; }
		Double nBaseUnitQuantityPacked { get; set; }
		Int64 ixStatus { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		OutboundOrderLines OutboundOrderLines { get; set; }
		HandlingUnits HandlingUnits { get; set; }
		Statuses Statuses { get; set; }
    }
}
  

