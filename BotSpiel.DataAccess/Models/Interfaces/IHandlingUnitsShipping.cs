using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IHandlingUnitsShipping
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixHandlingUnitShipping { get; set; }
		Int64 ixHandlingUnitShippingEdit { get; set; }
		String sHandlingUnitShipping { get; set; }
		Int64 ixHandlingUnit { get; set; }
		Int64 ixStatus { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		HandlingUnits HandlingUnits { get; set; }
		Statuses Statuses { get; set; }
    }
}
  

