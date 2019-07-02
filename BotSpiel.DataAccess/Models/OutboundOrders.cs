using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class OutboundOrders : IOutboundOrders
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public OutboundOrders()
        {
		OutboundOrderTypes _OutboundOrderTypes = new OutboundOrderTypes();
		OutboundOrderTypes = _OutboundOrderTypes;
		Facilities _Facilities = new Facilities();
		Facilities = _Facilities;
		Companies _Companies = new Companies();
		Companies = _Companies;
		BusinessPartners _BusinessPartners = new BusinessPartners();
		BusinessPartners = _BusinessPartners;
		CarrierServices _CarrierServices = new CarrierServices();
		CarrierServices = _CarrierServices;
		Statuses _Statuses = new Statuses();
		Statuses = _Statuses;
		PickBatches _PickBatches = new PickBatches();
		PickBatches = _PickBatches;
		OutboundShipments _OutboundShipments = new OutboundShipments();
		OutboundShipments = _OutboundShipments;

        }
		[Display(Name = "Outbound Order ID")]
		public virtual Int64 ixOutboundOrder { get; set; }
		[Display(Name = "Outbound Order ID")]
		public virtual Int64 ixOutboundOrderEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Outbound Order")]
		public virtual String sOutboundOrder { get; set; }
		[StringLength(300)]
		[Required]
		[Display(Name = "Order Reference")]
		public virtual String sOrderReference { get; set; }
		[Required]
		[Display(Name = "Outbound Order Type ID")]
		public virtual Int64 ixOutboundOrderType { get; set; }
		[Required]
		[Display(Name = "Facility ID")]
		public virtual Int64 ixFacility { get; set; }
		[Required]
		[Display(Name = "Company ID")]
		public virtual Int64 ixCompany { get; set; }
		[Required]
		[Display(Name = "Business Partner ID")]
		public virtual Int64 ixBusinessPartner { get; set; }
		[Required]
		[Display(Name = "Deliver Earliest")]
		public virtual DateTime dtDeliverEarliest { get; set; }
		[Required]
		[Display(Name = "Deliver Latest")]
		public virtual DateTime dtDeliverLatest { get; set; }
		[Required]
		[Display(Name = "Carrier Service ID")]
		public virtual Int64 ixCarrierService { get; set; }
		[Required]
		[Display(Name = "Status ID")]
		public virtual Int64 ixStatus { get; set; }
		[Display(Name = "Pick Batch ID")]
		public virtual Int64? ixPickBatch { get; set; }
		[Display(Name = "Outbound Shipment ID")]
		public virtual Int64? ixOutboundShipment { get; set; }
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
		[ForeignKey("ixOutboundOrderType")]
		public virtual OutboundOrderTypes OutboundOrderTypes { get; set; }
		[ForeignKey("ixFacility")]
		public virtual Facilities Facilities { get; set; }
		[ForeignKey("ixCompany")]
		public virtual Companies Companies { get; set; }
		[ForeignKey("ixBusinessPartner")]
		public virtual BusinessPartners BusinessPartners { get; set; }
		[ForeignKey("ixCarrierService")]
		public virtual CarrierServices CarrierServices { get; set; }
		[ForeignKey("ixStatus")]
		public virtual Statuses Statuses { get; set; }
		[ForeignKey("ixPickBatch")]
		public virtual PickBatches PickBatches { get; set; }
		[ForeignKey("ixOutboundShipment")]
		public virtual OutboundShipments OutboundShipments { get; set; }
    }
}
  

