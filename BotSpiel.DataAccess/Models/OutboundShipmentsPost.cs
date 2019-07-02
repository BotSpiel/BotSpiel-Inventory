using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class OutboundShipmentsPost : IOutboundShipmentsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Outbound Shipment ID")]
		public virtual Int64 ixOutboundShipment { get; set; }
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
		public virtual String UserName { get; set; }
    }
}
  

