using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IInventoryUnits
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixInventoryUnit { get; set; }
		Int64 ixInventoryUnitEdit { get; set; }
		String sInventoryUnit { get; set; }
		Int64 ixFacility { get; set; }
		Int64 ixCompany { get; set; }
		Int64 ixMaterial { get; set; }
		Int64 ixInventoryState { get; set; }
		Int64? ixHandlingUnit { get; set; }
		Int64 ixInventoryLocation { get; set; }
		Double nBaseUnitQuantity { get; set; }
		String sSerialNumber { get; set; }
		String sBatchNumber { get; set; }
		DateTime? dtExpireAt { get; set; }
		Int64 ixStatus { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		Facilities Facilities { get; set; }
		Companies Companies { get; set; }
		Materials Materials { get; set; }
		InventoryStates InventoryStates { get; set; }
		HandlingUnits HandlingUnits { get; set; }
		InventoryLocations InventoryLocations { get; set; }
		Statuses Statuses { get; set; }
    }
}
  

