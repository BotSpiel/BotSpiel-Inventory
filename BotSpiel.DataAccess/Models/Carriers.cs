using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class Carriers : ICarriers
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public Carriers()
        {
		CarrierTypes _CarrierTypes = new CarrierTypes();
		CarrierTypes = _CarrierTypes;

        }
		[Display(Name = "Carrier ID")]
		public virtual Int64 ixCarrier { get; set; }
		[Display(Name = "Carrier ID")]
		public virtual Int64 ixCarrierEdit { get; set; }
		[Required]
		[StringLength(300)]
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
		[ForeignKey("ixCarrierType")]
		public virtual CarrierTypes CarrierTypes { get; set; }
    }
}
  

