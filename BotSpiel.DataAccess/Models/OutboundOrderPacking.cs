using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class OutboundOrderPacking : IOutboundOrderPacking
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public OutboundOrderPacking()
        {
		OutboundOrderLines _OutboundOrderLines = new OutboundOrderLines();
		OutboundOrderLines = _OutboundOrderLines;
		HandlingUnits _HandlingUnits = new HandlingUnits();
		HandlingUnits = _HandlingUnits;
		Statuses _Statuses = new Statuses();
		Statuses = _Statuses;

        }
		[Display(Name = "Outbound Order Pack ID")]
		public virtual Int64 ixOutboundOrderPack { get; set; }
		[Display(Name = "Outbound Order Pack ID")]
		public virtual Int64 ixOutboundOrderPackEdit { get; set; }
		[Display(Name = "Outbound Order Pack")]
		public virtual String sOutboundOrderPack { get; set; }
		[Required]
		[Display(Name = "Outbound Order Line ID")]
		public virtual Int64 ixOutboundOrderLine { get; set; }
		[Required]
		[Display(Name = "Handling Unit ID")]
		public virtual Int64 ixHandlingUnit { get; set; }
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
		[ForeignKey("ixOutboundOrderLine")]
		public virtual OutboundOrderLines OutboundOrderLines { get; set; }
		[ForeignKey("ixHandlingUnit")]
		public virtual HandlingUnits HandlingUnits { get; set; }
		[ForeignKey("ixStatus")]
		public virtual Statuses Statuses { get; set; }
    }
}
  

