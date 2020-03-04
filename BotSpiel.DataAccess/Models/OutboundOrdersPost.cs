using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class OutboundOrdersPost : IOutboundOrdersPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        //Custom Code Start | Added Code Block
        public OutboundOrdersPost()
        {
            ixStatus = 5;
        }
        //Custom Code End
         
		[Display(Name = "Outbound Order ID")]
		public virtual Int64 ixOutboundOrder { get; set; }
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
		[Display(Name = "Deliver Earliest")]
		public virtual DateTime? dtDeliverEarliest { get; set; }
		[Display(Name = "Deliver Latest")]
		public virtual DateTime? dtDeliverLatest { get; set; }
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
		public virtual String UserName { get; set; }
    }
}
  

