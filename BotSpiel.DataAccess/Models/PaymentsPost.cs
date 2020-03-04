using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class PaymentsPost : IPaymentsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Payment ID")]
		public virtual Int64 ixPayment { get; set; }
		[Display(Name = "Payment")]
		public virtual String sPayment { get; set; }
		[Required]
		[Display(Name = "Invoice ID")]
		public virtual Int64 ixInvoice { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

