using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IReceiving
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixReceipt { get; set; }
		Int64 ixReceiptEdit { get; set; }
		String sReceipt { get; set; }
		Int64 ixInventoryLocation { get; set; }
		Int64 ixInboundOrder { get; set; }
		Int64 ixHandlingUnit { get; set; }
		Int64 ixMaterial { get; set; }
		Int64? ixMaterialHandlingUnitConfiguration { get; set; }
		Int64? ixHandlingUnitType { get; set; }
		Double? nHandlingUnitQuantity { get; set; }
		String sSerialNumber { get; set; }
		String sBatchNumber { get; set; }
		DateTime? dtExpireAt { get; set; }
		Double nBaseUnitQuantityReceived { get; set; }
		Int64 ixInventoryState { get; set; }
		Int64 ixStatus { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		InventoryLocations InventoryLocations { get; set; }
		InboundOrders InboundOrders { get; set; }
		HandlingUnits HandlingUnits { get; set; }
		Materials Materials { get; set; }
		MaterialHandlingUnitConfigurations MaterialHandlingUnitConfigurations { get; set; }
		HandlingUnitTypes HandlingUnitTypes { get; set; }
		InventoryStates InventoryStates { get; set; }
		Statuses Statuses { get; set; }
    }
}
  

