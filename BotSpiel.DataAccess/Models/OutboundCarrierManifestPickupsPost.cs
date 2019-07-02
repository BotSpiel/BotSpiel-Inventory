using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class OutboundCarrierManifestPickupsPost : IOutboundCarrierManifestPickupsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Outbound Carrier Manifest Pickup ID")]
		public virtual Int64 ixOutboundCarrierManifestPickup { get; set; }
		[Display(Name = "Outbound Carrier Manifest Pickup")]
		public virtual String sOutboundCarrierManifestPickup { get; set; }
		[Required]
		[Display(Name = "Outbound Carrier Manifest ID")]
		public virtual Int64 ixOutboundCarrierManifest { get; set; }
		[Required]
		[Display(Name = "Status ID")]
		public virtual Int64 ixStatus { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

