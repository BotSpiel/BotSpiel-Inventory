using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IInventoryLocationsSlotting
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixInventoryLocationSlotting { get; set; }
		Int64 ixInventoryLocationSlottingEdit { get; set; }
		String sInventoryLocationSlotting { get; set; }
		Int64 ixInventoryLocation { get; set; }
		Int64 ixMaterial { get; set; }
		Double nMinimumBaseUnitQuantity { get; set; }
		Double nMaximumBaseUnitQuantity { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		InventoryLocations InventoryLocations { get; set; }
		Materials Materials { get; set; }
    }
}
  

