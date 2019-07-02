using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class InventoryUnits : IInventoryUnits
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public InventoryUnits()
        {
		Facilities _Facilities = new Facilities();
		Facilities = _Facilities;
		Companies _Companies = new Companies();
		Companies = _Companies;
		Materials _Materials = new Materials();
		Materials = _Materials;
		InventoryStates _InventoryStates = new InventoryStates();
		InventoryStates = _InventoryStates;
		HandlingUnits _HandlingUnits = new HandlingUnits();
		HandlingUnits = _HandlingUnits;
		InventoryLocations _InventoryLocations = new InventoryLocations();
		InventoryLocations = _InventoryLocations;
		Statuses _Statuses = new Statuses();
		Statuses = _Statuses;

        }
		[Display(Name = "Inventory Unit ID")]
		public virtual Int64 ixInventoryUnit { get; set; }
		[Display(Name = "Inventory Unit ID")]
		public virtual Int64 ixInventoryUnitEdit { get; set; }
		[Display(Name = "Inventory Unit")]
		public virtual String sInventoryUnit { get; set; }
		[Required]
		[Display(Name = "Facility ID")]
		public virtual Int64 ixFacility { get; set; }
		[Required]
		[Display(Name = "Company ID")]
		public virtual Int64 ixCompany { get; set; }
		[Required]
		[Display(Name = "Material ID")]
		public virtual Int64 ixMaterial { get; set; }
		[Required]
		[Display(Name = "Inventory State ID")]
		public virtual Int64 ixInventoryState { get; set; }
		[Display(Name = "Handling Unit ID")]
		public virtual Int64? ixHandlingUnit { get; set; }
		[Required]
		[Display(Name = "Inventory Location ID")]
		public virtual Int64 ixInventoryLocation { get; set; }
		[Required]
		[Display(Name = "Base Unit Quantity")]
		public virtual Double nBaseUnitQuantity { get; set; }
		[StringLength(100)]
		[Display(Name = "Serial Number")]
		public virtual String sSerialNumber { get; set; }
		[StringLength(100)]
		[Display(Name = "Batch Number")]
		public virtual String sBatchNumber { get; set; }
		[Display(Name = "Expire At")]
		public virtual DateTime? dtExpireAt { get; set; }
		[Required]
		[Display(Name = "Status ID")]
		public virtual Int64 ixStatus { get; set; }
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
		[ForeignKey("ixFacility")]
		public virtual Facilities Facilities { get; set; }
		[ForeignKey("ixCompany")]
		public virtual Companies Companies { get; set; }
		[ForeignKey("ixMaterial")]
		public virtual Materials Materials { get; set; }
		[ForeignKey("ixInventoryState")]
		public virtual InventoryStates InventoryStates { get; set; }
		[ForeignKey("ixHandlingUnit")]
		public virtual HandlingUnits HandlingUnits { get; set; }
		[ForeignKey("ixInventoryLocation")]
		public virtual InventoryLocations InventoryLocations { get; set; }
		[ForeignKey("ixStatus")]
		public virtual Statuses Statuses { get; set; }
    }
}
  

