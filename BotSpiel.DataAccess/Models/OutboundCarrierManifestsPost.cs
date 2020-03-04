using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class OutboundCarrierManifestsPost : IOutboundCarrierManifestsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

        */

        //Custom Code Start | Added Code Block
        public OutboundCarrierManifestsPost()
        {
            ixStatus = 5;
        }
        //Custom Code End


        [Display(Name = "Outbound Carrier Manifest ID")]
		public virtual Int64 ixOutboundCarrierManifest { get; set; }
		[Display(Name = "Outbound Carrier Manifest")]
		public virtual String sOutboundCarrierManifest { get; set; }
		[Required]
		[Display(Name = "Facility ID")]
		public virtual Int64 ixFacility { get; set; }
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
		public virtual String UserName { get; set; }
    }
}
  

