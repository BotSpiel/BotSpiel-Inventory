using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class InboundOrderTypes : IInboundOrderTypes
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Inbound Order Type ID")]
		public virtual Int64 ixInboundOrderType { get; set; }
		[Display(Name = "Inbound Order Type ID")]
		public virtual Int64 ixInboundOrderTypeEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Inbound Order Type")]
		public virtual String sInboundOrderType { get; set; }
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
    }
}
  

