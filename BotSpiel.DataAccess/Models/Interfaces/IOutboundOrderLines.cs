using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IOutboundOrderLines
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixOutboundOrderLine { get; set; }
		Int64 ixOutboundOrderLineEdit { get; set; }
		String sOutboundOrderLine { get; set; }
		String sOrderLineReference { get; set; }
		Int64 ixMaterial { get; set; }
		String sBatchNumber { get; set; }
		String sSerialNumber { get; set; }
		Double nBaseUnitQuantityOrdered { get; set; }
		Double nBaseUnitQuantityShipped { get; set; }
		Int64 ixStatus { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		Materials Materials { get; set; }
		Statuses Statuses { get; set; }
    }
}
  

