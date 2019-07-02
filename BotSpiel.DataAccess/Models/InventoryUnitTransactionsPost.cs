using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class InventoryUnitTransactionsPost : IInventoryUnitTransactionsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Inventory Unit Transaction ID")]
		public virtual Int64 ixInventoryUnitTransaction { get; set; }
		[Display(Name = "Inventory Unit Transaction")]
		public virtual String sInventoryUnitTransaction { get; set; }
		[Required]
		[Display(Name = "Inventory Unit ID")]
		public virtual Int64 ixInventoryUnit { get; set; }
		[Required]
		[Display(Name = "Inventory Unit Transaction Context ID")]
		public virtual Int64 ixInventoryUnitTransactionContext { get; set; }
		[Display(Name = "Facility Before ID")]
		public virtual Int64? ixFacilityBefore { get; set; }
		[Required]
		[Display(Name = "Facility After ID")]
		public virtual Int64 ixFacilityAfter { get; set; }
		[Display(Name = "Company Before ID")]
		public virtual Int64? ixCompanyBefore { get; set; }
		[Required]
		[Display(Name = "Company After ID")]
		public virtual Int64 ixCompanyAfter { get; set; }
		[Display(Name = "Material Before ID")]
		public virtual Int64? ixMaterialBefore { get; set; }
		[Required]
		[Display(Name = "Material After ID")]
		public virtual Int64 ixMaterialAfter { get; set; }
		[Display(Name = "Inventory State Before ID")]
		public virtual Int64? ixInventoryStateBefore { get; set; }
		[Required]
		[Display(Name = "Inventory State After ID")]
		public virtual Int64 ixInventoryStateAfter { get; set; }
		[Display(Name = "Handling Unit Before ID")]
		public virtual Int64? ixHandlingUnitBefore { get; set; }
		[Display(Name = "Handling Unit After ID")]
		public virtual Int64? ixHandlingUnitAfter { get; set; }
		[Display(Name = "Inventory Location Before ID")]
		public virtual Int64? ixInventoryLocationBefore { get; set; }
		[Required]
		[Display(Name = "Inventory Location After ID")]
		public virtual Int64 ixInventoryLocationAfter { get; set; }
		[Display(Name = "Base Unit Quantity Before")]
		public virtual Double? nBaseUnitQuantityBefore { get; set; }
		[Required]
		[Display(Name = "Base Unit Quantity After")]
		public virtual Double nBaseUnitQuantityAfter { get; set; }
		[StringLength(100)]
		[Display(Name = "Serial Number Before")]
		public virtual String sSerialNumberBefore { get; set; }
		[StringLength(100)]
		[Display(Name = "Serial Number After")]
		public virtual String sSerialNumberAfter { get; set; }
		[StringLength(100)]
		[Display(Name = "Batch Number Before")]
		public virtual String sBatchNumberBefore { get; set; }
		[StringLength(100)]
		[Display(Name = "Batch Number After")]
		public virtual String sBatchNumberAfter { get; set; }
		[Display(Name = "Expire At Before")]
		public virtual DateTime? dtExpireAtBefore { get; set; }
		[Display(Name = "Expire At After")]
		public virtual DateTime? dtExpireAtAfter { get; set; }
		[Display(Name = "Status Before ID")]
		public virtual Int64? ixStatusBefore { get; set; }
		[Required]
		[Display(Name = "Status After ID")]
		public virtual Int64 ixStatusAfter { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

