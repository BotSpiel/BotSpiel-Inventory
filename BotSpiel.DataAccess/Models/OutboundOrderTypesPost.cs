using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class OutboundOrderTypesPost : IOutboundOrderTypesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Outbound Order Type ID")]
		public virtual Int64 ixOutboundOrderType { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyOutboundOrderType", controller: "OutboundOrderTypes", AdditionalFields = nameof(ixOutboundOrderType))]
		[Display(Name = "Outbound Order Type")]
		public virtual String sOutboundOrderType { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

