using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IInventoryLocationsSlottingPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixInventoryLocationSlotting { get; set; }
		String sInventoryLocationSlotting { get; set; }
		Int64 ixInventoryLocation { get; set; }
		Int64 ixMaterial { get; set; }
		Double nMinimumBaseUnitQuantity { get; set; }
		Double nMaximumBaseUnitQuantity { get; set; }
		String UserName { get; set; }
    }
}
  

