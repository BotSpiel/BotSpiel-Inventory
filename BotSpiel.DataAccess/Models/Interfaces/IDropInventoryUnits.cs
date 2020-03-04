using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IDropInventoryUnits
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixDropInventoryUnit { get; set; }
		Int64 ixDropInventoryUnitEdit { get; set; }
		String sDropInventoryUnit { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
    }
}
  

