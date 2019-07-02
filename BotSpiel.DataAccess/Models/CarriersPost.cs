using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class CarriersPost : ICarriersPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Carrier ID")]
		public virtual Int64 ixCarrier { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyCarrier", controller: "Carriers", AdditionalFields = nameof(ixCarrier))]
		[Display(Name = "Carrier")]
		public virtual String sCarrier { get; set; }
		[Required]
		[Display(Name = "Carrier Type ID")]
		public virtual Int64 ixCarrierType { get; set; }
		[StringLength(100)]
		[Display(Name = "Standard Carrier Alpha Code")]
		public virtual String sStandardCarrierAlphaCode { get; set; }
		[StringLength(100)]
		[Required]
		[Display(Name = "Carrier Consignment Number Prefix")]
		public virtual String sCarrierConsignmentNumberPrefix { get; set; }
		[Required]
		[Display(Name = "Carrier Consignment Number Start")]
		public virtual Int64 nCarrierConsignmentNumberStart { get; set; }
		[Display(Name = "Carrier Consignment Number Last Used")]
		public virtual Int64? nCarrierConsignmentNumberLastUsed { get; set; }
		[Display(Name = "Scheduled Pickup Time")]
		public virtual TimeSpan? dtScheduledPickupTime { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

