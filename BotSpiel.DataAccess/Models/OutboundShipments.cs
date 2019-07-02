using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class OutboundShipments : IOutboundShipments
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public OutboundShipments()
        {
		Facilities _Facilities = new Facilities();
		Facilities = _Facilities;
		Companies _Companies = new Companies();
		Companies = _Companies;
		Carriers _Carriers = new Carriers();
		Carriers = _Carriers;
		Statuses _Statuses = new Statuses();
		Statuses = _Statuses;
		Addresses _Addresses = new Addresses();
		Addresses = _Addresses;
		OutboundCarrierManifests _OutboundCarrierManifests = new OutboundCarrierManifests();
		OutboundCarrierManifests = _OutboundCarrierManifests;

        }
		[Display(Name = "Outbound Shipment ID")]
		public virtual Int64 ixOutboundShipment { get; set; }
		[Display(Name = "Outbound Shipment ID")]
		public virtual Int64 ixOutboundShipmentEdit { get; set; }
		[Display(Name = "Outbound Shipment")]
		public virtual String sOutboundShipment { get; set; }
		[Required]
		[Display(Name = "Facility ID")]
		public virtual Int64 ixFacility { get; set; }
		[Required]
		[Display(Name = "Company ID")]
		public virtual Int64 ixCompany { get; set; }
		[Required]
		[Display(Name = "Carrier ID")]
		public virtual Int64 ixCarrier { get; set; }
		[StringLength(300)]
		[Required]
		[Display(Name = "Carrier Consignment Number")]
		public virtual String sCarrierConsignmentNumber { get; set; }
		[Required]
		[Display(Name = "Status ID")]
		public virtual Int64 ixStatus { get; set; }
		[Required]
		[Display(Name = "Address ID")]
		public virtual Int64 ixAddress { get; set; }
		[Display(Name = "Outbound Carrier Manifest ID")]
		public virtual Int64? ixOutboundCarrierManifest { get; set; }
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
		[ForeignKey("ixCarrier")]
		public virtual Carriers Carriers { get; set; }
		[ForeignKey("ixStatus")]
		public virtual Statuses Statuses { get; set; }
		[ForeignKey("ixAddress")]
		public virtual Addresses Addresses { get; set; }
		[ForeignKey("ixOutboundCarrierManifest")]
		public virtual OutboundCarrierManifests OutboundCarrierManifests { get; set; }
    }
}
  

