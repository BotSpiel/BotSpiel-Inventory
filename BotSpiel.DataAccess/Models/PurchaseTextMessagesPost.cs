using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class PurchaseTextMessagesPost : IPurchaseTextMessagesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Purchase Text Message ID")]
		public virtual Int64 ixPurchaseTextMessage { get; set; }
		[Display(Name = "Purchase Text Message")]
		public virtual String sPurchaseTextMessage { get; set; }
		[Required]
		[Display(Name = "Purchase ID")]
		public virtual Int64 ixPurchase { get; set; }
		[Required]
		[Display(Name = "Send Text Message ID")]
		public virtual Int64 ixSendTextMessage { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

