using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class OutboundCarrierManifests : IOutboundCarrierManifests
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public OutboundCarrierManifests()
        {
		Carriers _Carriers = new Carriers();
		Carriers = _Carriers;
		InventoryLocations _InventoryLocationsFKDiffPickupInventoryLocation = new InventoryLocations();
		InventoryLocationsFKDiffPickupInventoryLocation = _InventoryLocationsFKDiffPickupInventoryLocation;
		Statuses _Statuses = new Statuses();
		Statuses = _Statuses;

        }
		[Display(Name = "Outbound Carrier Manifest ID")]
		public virtual Int64 ixOutboundCarrierManifest { get; set; }
		[Display(Name = "Outbound Carrier Manifest ID")]
		public virtual Int64 ixOutboundCarrierManifestEdit { get; set; }
		[Display(Name = "Outbound Carrier Manifest")]
		public virtual String sOutboundCarrierManifest { get; set; }
		[Required]
		[Display(Name = "Carrier ID")]
		public virtual Int64 ixCarrier { get; set; }
		[Display(Name = "Pickup Inventory Location ID")]
		public virtual Int64? ixPickupInventoryLocation { get; set; }
		[Display(Name = "Scheduled Pickup At")]
		public virtual DateTime? dtScheduledPickupAt { get; set; }
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
		[ForeignKey("ixCarrier")]
		public virtual Carriers Carriers { get; set; }
		[ForeignKey("ixPickupInventoryLocation")]
		public virtual InventoryLocations InventoryLocationsFKDiffPickupInventoryLocation { get; set; }
		[ForeignKey("ixStatus")]
		public virtual Statuses Statuses { get; set; }
    }
}
  

