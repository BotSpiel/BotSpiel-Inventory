using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IOutboundOrderPacking
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixOutboundOrderPack { get; set; }
		Int64 ixOutboundOrderPackEdit { get; set; }
		String sOutboundOrderPack { get; set; }
		Int64 ixOutboundOrderLine { get; set; }
		Int64 ixHandlingUnit { get; set; }
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
  

