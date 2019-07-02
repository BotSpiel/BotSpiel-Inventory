using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IInventoryUnitTransactionsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixInventoryUnitTransaction { get; set; }
		String sInventoryUnitTransaction { get; set; }
		Int64 ixInventoryUnit { get; set; }
		Int64 ixInventoryUnitTransactionContext { get; set; }
		Int64? ixFacilityBefore { get; set; }
		Int64 ixFacilityAfter { get; set; }
		Int64? ixCompanyBefore { get; set; }
		Int64 ixCompanyAfter { get; set; }
		Int64? ixMaterialBefore { get; set; }
		Int64 ixMaterialAfter { get; set; }
		Int64? ixInventoryStateBefore { get; set; }
		Int64 ixInventoryStateAfter { get; set; }
		Int64? ixHandlingUnitBefore { get; set; }
		Int64? ixHandlingUnitAfter { get; set; }
		Int64? ixInventoryLocationBefore { get; set; }
		Int64 ixInventoryLocationAfter { get; set; }
		Double? nBaseUnitQuantityBefore { get; set; }
		Double nBaseUnitQuantityAfter { get; set; }
		String sSerialNumberBefore { get; set; }
		String sSerialNumberAfter { get; set; }
		String sBatchNumberBefore { get; set; }
		String sBatchNumberAfter { get; set; }
		DateTime? dtExpireAtBefore { get; set; }
		DateTime? dtExpireAtAfter { get; set; }
		Int64? ixStatusBefore { get; set; }
		Int64 ixStatusAfter { get; set; }
		String UserName { get; set; }
    }
}
  

