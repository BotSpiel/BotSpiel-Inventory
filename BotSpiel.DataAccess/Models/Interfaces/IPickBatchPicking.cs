using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IPickBatchPicking
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixPickBatchPick { get; set; }
		Int64 ixPickBatchPickEdit { get; set; }
		String sPickBatchPick { get; set; }
		Int64 ixPickBatch { get; set; }
		Int64 ixInventoryUnit { get; set; }
		Double nBaseUnitQuantityPicked { get; set; }
		String sPackToHandlingUnit { get; set; }
		Int64 ixHandlingUnit { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		PickBatches PickBatches { get; set; }
		InventoryUnits InventoryUnits { get; set; }
		HandlingUnits HandlingUnits { get; set; }
    }
}
  

