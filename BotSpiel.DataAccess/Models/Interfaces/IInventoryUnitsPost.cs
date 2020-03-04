using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IInventoryUnitsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixInventoryUnit { get; set; }
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
		Double nBaseUnitQuantityQueued { get; set; }
		Int64 ixStatus { get; set; }
		String UserName { get; set; }
    }
}
  

