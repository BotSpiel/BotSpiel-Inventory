using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class InboundOrderTypesPost : IInboundOrderTypesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Inbound Order Type ID")]
		public virtual Int64 ixInboundOrderType { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyInboundOrderType", controller: "InboundOrderTypes", AdditionalFields = nameof(ixInboundOrderType))]
		[Display(Name = "Inbound Order Type")]
		public virtual String sInboundOrderType { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

