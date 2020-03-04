using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IInboundOrderLinesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixInboundOrderLine { get; set; }
		String sInboundOrderLine { get; set; }
		Int64 ixInboundOrder { get; set; }
		String sOrderLineReference { get; set; }
		Int64 ixMaterial { get; set; }
		Int64? ixMaterialHandlingUnitConfiguration { get; set; }
		Int64? ixHandlingUnitType { get; set; }
		Double? nHandlingUnitQuantity { get; set; }
		Double nBaseUnitQuantityExpected { get; set; }
		Double nBaseUnitQuantityReceived { get; set; }
		String sSerialNumber { get; set; }
		String sBatchNumber { get; set; }
		DateTime? dtExpireAt { get; set; }
		Int64 ixStatus { get; set; }
		String UserName { get; set; }
    }
}
  

