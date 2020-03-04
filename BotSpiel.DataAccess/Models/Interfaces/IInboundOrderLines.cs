using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IInboundOrderLines
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixInboundOrderLine { get; set; }
		Int64 ixInboundOrderLineEdit { get; set; }
		String sInboundOrderLine { get; set; }
		Int64 ixInboundOrder { get; set; }
		String sOrderLineReference { get; set; }
		Int64 ixMaterial { get; set; }
		Int64? ixMaterialHandlingUnitConfiguration { get; set; }
		Int64? ixHandlingUnitType { get; set; }
		Double? nHandlingUnitQuantity { get; set; }
		Double nBaseUnitQuantityExpected { get; set; }
		Double nBaseUnitQuantityReceived { get; set; }
		String sSerialNumber { get; set; }
		String sBatchNumber { get; set; }
		DateTime? dtExpireAt { get; set; }
		Int64 ixStatus { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		InboundOrders InboundOrders { get; set; }
		Materials Materials { get; set; }
		MaterialHandlingUnitConfigurations MaterialHandlingUnitConfigurations { get; set; }
		HandlingUnitTypes HandlingUnitTypes { get; set; }
		Statuses Statuses { get; set; }
    }
}
  

