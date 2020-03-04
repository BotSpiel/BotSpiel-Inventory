using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class PutAwayHandlingUnits : IPutAwayHandlingUnits
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public PutAwayHandlingUnits()
        {
		HandlingUnits _HandlingUnits = new HandlingUnits();
		HandlingUnits = _HandlingUnits;
		InventoryLocations _InventoryLocations = new InventoryLocations();
		InventoryLocations = _InventoryLocations;

        }
		[Display(Name = "Put Away Handling Unit ID")]
		public virtual Int64 ixPutAwayHandlingUnit { get; set; }
		[Display(Name = "Put Away Handling Unit ID")]
		public virtual Int64 ixPutAwayHandlingUnitEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Put Away Handling Unit")]
		public virtual String sPutAwayHandlingUnit { get; set; }
		[StringLength(300)]
		[Required]
		[Display(Name = "Inventory Drop Location")]
		public virtual String sInventoryDropLocation { get; set; }
		[Required]
		[Display(Name = "Handling Unit ID")]
		public virtual Int64 ixHandlingUnit { get; set; }
		[Required]
		[Display(Name = "Inventory Location ID")]
		public virtual Int64 ixInventoryLocation { get; set; }
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
		[ForeignKey("ixHandlingUnit")]
		public virtual HandlingUnits HandlingUnits { get; set; }
		[ForeignKey("ixInventoryLocation")]
		public virtual InventoryLocations InventoryLocations { get; set; }
    }
}
  

