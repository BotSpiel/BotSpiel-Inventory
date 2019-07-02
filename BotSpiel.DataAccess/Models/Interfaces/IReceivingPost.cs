using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IReceivingPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixReceipt { get; set; }
		String sReceipt { get; set; }
		Int64 ixInventoryLocation { get; set; }
		Int64 ixInboundOrder { get; set; }
		Int64 ixHandlingUnit { get; set; }
		Int64 ixMaterial { get; set; }
		Int64? ixMaterialHandlingUnitConfiguration { get; set; }
		Int64? ixHandlingUnitType { get; set; }
		Double? nHandlingUnitQuantity { get; set; }
		String sBatchNumber { get; set; }
		String sSerialNumber { get; set; }
		Double nBaseUnitQuantityReceived { get; set; }
		Int64 ixStatus { get; set; }
		String UserName { get; set; }
    }
}
  

