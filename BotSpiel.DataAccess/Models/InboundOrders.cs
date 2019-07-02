using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class InboundOrders : IInboundOrders
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public InboundOrders()
        {
		InboundOrderTypes _InboundOrderTypes = new InboundOrderTypes();
		InboundOrderTypes = _InboundOrderTypes;
		Facilities _Facilities = new Facilities();
		Facilities = _Facilities;
		Companies _Companies = new Companies();
		Companies = _Companies;
		BusinessPartners _BusinessPartners = new BusinessPartners();
		BusinessPartners = _BusinessPartners;
		Statuses _Statuses = new Statuses();
		Statuses = _Statuses;

        }
		[Display(Name = "Inbound Order ID")]
		public virtual Int64 ixInboundOrder { get; set; }
		[Display(Name = "Inbound Order ID")]
		public virtual Int64 ixInboundOrderEdit { get; set; }
		[Display(Name = "Inbound Order")]
		public virtual String sInboundOrder { get; set; }
		[StringLength(300)]
		[Required]
		[Display(Name = "Order Reference")]
		public virtual String sOrderReference { get; set; }
		[Required]
		[Display(Name = "Inbound Order Type ID")]
		public virtual Int64 ixInboundOrderType { get; set; }
		[Required]
		[Display(Name = "Facility ID")]
		public virtual Int64 ixFacility { get; set; }
		[Required]
		[Display(Name = "Company ID")]
		public virtual Int64 ixCompany { get; set; }
		[Required]
		[Display(Name = "Business Partner ID")]
		public virtual Int64 ixBusinessPartner { get; set; }
		[Display(Name = "Expected At")]
		public virtual DateTime? dtExpectedAt { get; set; }
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
		[ForeignKey("ixInboundOrderType")]
		public virtual InboundOrderTypes InboundOrderTypes { get; set; }
		[ForeignKey("ixFacility")]
		public virtual Facilities Facilities { get; set; }
		[ForeignKey("ixCompany")]
		public virtual Companies Companies { get; set; }
		[ForeignKey("ixBusinessPartner")]
		public virtual BusinessPartners BusinessPartners { get; set; }
		[ForeignKey("ixStatus")]
		public virtual Statuses Statuses { get; set; }
    }
}
  

