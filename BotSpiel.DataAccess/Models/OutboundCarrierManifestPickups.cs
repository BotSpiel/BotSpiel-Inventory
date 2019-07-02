using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class OutboundCarrierManifestPickups : IOutboundCarrierManifestPickups
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public OutboundCarrierManifestPickups()
        {
		OutboundCarrierManifests _OutboundCarrierManifests = new OutboundCarrierManifests();
		OutboundCarrierManifests = _OutboundCarrierManifests;
		Statuses _Statuses = new Statuses();
		Statuses = _Statuses;

        }
		[Display(Name = "Outbound Carrier Manifest Pickup ID")]
		public virtual Int64 ixOutboundCarrierManifestPickup { get; set; }
		[Display(Name = "Outbound Carrier Manifest Pickup ID")]
		public virtual Int64 ixOutboundCarrierManifestPickupEdit { get; set; }
		[Display(Name = "Outbound Carrier Manifest Pickup")]
		public virtual String sOutboundCarrierManifestPickup { get; set; }
		[Required]
		[Display(Name = "Outbound Carrier Manifest ID")]
		public virtual Int64 ixOutboundCarrierManifest { get; set; }
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
		[ForeignKey("ixOutboundCarrierManifest")]
		public virtual OutboundCarrierManifests OutboundCarrierManifests { get; set; }
		[ForeignKey("ixStatus")]
		public virtual Statuses Statuses { get; set; }
    }
}
  

