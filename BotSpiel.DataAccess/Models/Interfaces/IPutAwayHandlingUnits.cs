using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IPutAwayHandlingUnits
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixPutAwayHandlingUnit { get; set; }
		Int64 ixPutAwayHandlingUnitEdit { get; set; }
		String sPutAwayHandlingUnit { get; set; }
		String sInventoryDropLocation { get; set; }
		Int64 ixHandlingUnit { get; set; }
		Int64 ixInventoryLocation { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		HandlingUnits HandlingUnits { get; set; }
		InventoryLocations InventoryLocations { get; set; }
    }
}
  

