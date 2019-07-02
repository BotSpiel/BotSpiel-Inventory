using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class InventoryLocationsSlotting : IInventoryLocationsSlotting
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public InventoryLocationsSlotting()
        {
		InventoryLocations _InventoryLocations = new InventoryLocations();
		InventoryLocations = _InventoryLocations;
		Materials _Materials = new Materials();
		Materials = _Materials;

        }
		[Display(Name = "Inventory Location Slotting ID")]
		public virtual Int64 ixInventoryLocationSlotting { get; set; }
		[Display(Name = "Inventory Location Slotting ID")]
		public virtual Int64 ixInventoryLocationSlottingEdit { get; set; }
		[Display(Name = "Inventory Location Slotting")]
		public virtual String sInventoryLocationSlotting { get; set; }
		[Required]
		[Display(Name = "Inventory Location ID")]
		public virtual Int64 ixInventoryLocation { get; set; }
		[Required]
		[Display(Name = "Material ID")]
		public virtual Int64 ixMaterial { get; set; }
		[Required]
		[Display(Name = "Minimum Base Unit Quantity")]
		public virtual Double nMinimumBaseUnitQuantity { get; set; }
		[Required]
		[Display(Name = "Maximum Base Unit Quantity")]
		public virtual Double nMaximumBaseUnitQuantity { get; set; }
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
		[ForeignKey("ixInventoryLocation")]
		public virtual InventoryLocations InventoryLocations { get; set; }
		[ForeignKey("ixMaterial")]
		public virtual Materials Materials { get; set; }
    }
}
  

