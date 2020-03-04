using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class PurchaseEmailsPost : IPurchaseEmailsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Purchase Email ID")]
		public virtual Int64 ixPurchaseEmail { get; set; }
		[Display(Name = "Purchase Email")]
		public virtual String sPurchaseEmail { get; set; }
		[Required]
		[Display(Name = "Purchase ID")]
		public virtual Int64 ixPurchase { get; set; }
		[Required]
		[Display(Name = "Send Email ID")]
		public virtual Int64 ixSendEmail { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

