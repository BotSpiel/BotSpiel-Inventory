using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class InventoryUnitTransactions : IInventoryUnitTransactions
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public InventoryUnitTransactions()
        {
		InventoryUnits _InventoryUnits = new InventoryUnits();
		InventoryUnits = _InventoryUnits;
		InventoryUnitTransactionContexts _InventoryUnitTransactionContexts = new InventoryUnitTransactionContexts();
		InventoryUnitTransactionContexts = _InventoryUnitTransactionContexts;
		Facilities _FacilitiesFKDiffFacilityBefore = new Facilities();
		FacilitiesFKDiffFacilityBefore = _FacilitiesFKDiffFacilityBefore;
		Facilities _FacilitiesFKDiffFacilityAfter = new Facilities();
		FacilitiesFKDiffFacilityAfter = _FacilitiesFKDiffFacilityAfter;
		Companies _CompaniesFKDiffCompanyBefore = new Companies();
		CompaniesFKDiffCompanyBefore = _CompaniesFKDiffCompanyBefore;
		Companies _CompaniesFKDiffCompanyAfter = new Companies();
		CompaniesFKDiffCompanyAfter = _CompaniesFKDiffCompanyAfter;
		Materials _MaterialsFKDiffMaterialBefore = new Materials();
		MaterialsFKDiffMaterialBefore = _MaterialsFKDiffMaterialBefore;
		Materials _MaterialsFKDiffMaterialAfter = new Materials();
		MaterialsFKDiffMaterialAfter = _MaterialsFKDiffMaterialAfter;
		InventoryStates _InventoryStatesFKDiffInventoryStateBefore = new InventoryStates();
		InventoryStatesFKDiffInventoryStateBefore = _InventoryStatesFKDiffInventoryStateBefore;
		InventoryStates _InventoryStatesFKDiffInventoryStateAfter = new InventoryStates();
		InventoryStatesFKDiffInventoryStateAfter = _InventoryStatesFKDiffInventoryStateAfter;
		HandlingUnits _HandlingUnitsFKDiffHandlingUnitBefore = new HandlingUnits();
		HandlingUnitsFKDiffHandlingUnitBefore = _HandlingUnitsFKDiffHandlingUnitBefore;
		HandlingUnits _HandlingUnitsFKDiffHandlingUnitAfter = new HandlingUnits();
		HandlingUnitsFKDiffHandlingUnitAfter = _HandlingUnitsFKDiffHandlingUnitAfter;
		InventoryLocations _InventoryLocationsFKDiffInventoryLocationBefore = new InventoryLocations();
		InventoryLocationsFKDiffInventoryLocationBefore = _InventoryLocationsFKDiffInventoryLocationBefore;
		InventoryLocations _InventoryLocationsFKDiffInventoryLocationAfter = new InventoryLocations();
		InventoryLocationsFKDiffInventoryLocationAfter = _InventoryLocationsFKDiffInventoryLocationAfter;
		Statuses _StatusesFKDiffStatusBefore = new Statuses();
		StatusesFKDiffStatusBefore = _StatusesFKDiffStatusBefore;
		Statuses _StatusesFKDiffStatusAfter = new Statuses();
		StatusesFKDiffStatusAfter = _StatusesFKDiffStatusAfter;

        }
		[Display(Name = "Inventory Unit Transaction ID")]
		public virtual Int64 ixInventoryUnitTransaction { get; set; }
		[Display(Name = "Inventory Unit Transaction ID")]
		public virtual Int64 ixInventoryUnitTransactionEdit { get; set; }
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
		[Required]
		[Display(Name = "Created At")]
		public virtual DateTime dtCreatedAt { get; set; }
		[Required]
		[Display(Name = "Changed At")]
		public virtual DateTime dtChangedAt { get; set; }
		[StringLength(256)]
		[Required]
		[Display(Name = "Created By")]
		public virtual String sCreatedBy { get; set; }
		[StringLength(256)]
		[Required]
		[Display(Name = "Changed By")]
		public virtual String sChangedBy { get; set; }
		[ForeignKey("ixInventoryUnit")]
		public virtual InventoryUnits InventoryUnits { get; set; }
		[ForeignKey("ixInventoryUnitTransactionContext")]
		public virtual InventoryUnitTransactionContexts InventoryUnitTransactionContexts { get; set; }
		[ForeignKey("ixFacilityBefore")]
		public virtual Facilities FacilitiesFKDiffFacilityBefore { get; set; }
		[ForeignKey("ixFacilityAfter")]
		public virtual Facilities FacilitiesFKDiffFacilityAfter { get; set; }
		[ForeignKey("ixCompanyBefore")]
		public virtual Companies CompaniesFKDiffCompanyBefore { get; set; }
		[ForeignKey("ixCompanyAfter")]
		public virtual Companies CompaniesFKDiffCompanyAfter { get; set; }
		[ForeignKey("ixMaterialBefore")]
		public virtual Materials MaterialsFKDiffMaterialBefore { get; set; }
		[ForeignKey("ixMaterialAfter")]
		public virtual Materials MaterialsFKDiffMaterialAfter { get; set; }
		[ForeignKey("ixInventoryStateBefore")]
		public virtual InventoryStates InventoryStatesFKDiffInventoryStateBefore { get; set; }
		[ForeignKey("ixInventoryStateAfter")]
		public virtual InventoryStates InventoryStatesFKDiffInventoryStateAfter { get; set; }
		[ForeignKey("ixHandlingUnitBefore")]
		public virtual HandlingUnits HandlingUnitsFKDiffHandlingUnitBefore { get; set; }
		[ForeignKey("ixHandlingUnitAfter")]
		public virtual HandlingUnits HandlingUnitsFKDiffHandlingUnitAfter { get; set; }
		[ForeignKey("ixInventoryLocationBefore")]
		public virtual InventoryLocations InventoryLocationsFKDiffInventoryLocationBefore { get; set; }
		[ForeignKey("ixInventoryLocationAfter")]
		public virtual InventoryLocations InventoryLocationsFKDiffInventoryLocationAfter { get; set; }
		[ForeignKey("ixStatusBefore")]
		public virtual Statuses StatusesFKDiffStatusBefore { get; set; }
		[ForeignKey("ixStatusAfter")]
		public virtual Statuses StatusesFKDiffStatusAfter { get; set; }
    }
}
  

