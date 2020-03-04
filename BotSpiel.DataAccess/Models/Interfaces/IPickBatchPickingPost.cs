using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IPickBatchPickingPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixPickBatchPick { get; set; }
		String sPickBatchPick { get; set; }
		Int64 ixPickBatch { get; set; }
		Int64 ixInventoryUnit { get; set; }
		Double nBaseUnitQuantityPicked { get; set; }
		String sPackToHandlingUnit { get; set; }
		Int64 ixHandlingUnit { get; set; }
		String UserName { get; set; }
    }
}
  

