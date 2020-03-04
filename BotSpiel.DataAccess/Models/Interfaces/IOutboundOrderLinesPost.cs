using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IOutboundOrderLinesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixOutboundOrderLine { get; set; }
		String sOutboundOrderLine { get; set; }
		Int64 ixOutboundOrder { get; set; }
		String sOrderLineReference { get; set; }
		Int64 ixMaterial { get; set; }
		String sBatchNumber { get; set; }
		String sSerialNumber { get; set; }
		Double nBaseUnitQuantityOrdered { get; set; }
		Double nBaseUnitQuantityShipped { get; set; }
		Int64 ixStatus { get; set; }
		String UserName { get; set; }
    }
}
  

