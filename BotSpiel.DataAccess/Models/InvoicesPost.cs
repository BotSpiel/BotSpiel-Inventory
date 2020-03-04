using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class InvoicesPost : IInvoicesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Invoice ID")]
		public virtual Int64 ixInvoice { get; set; }
		[Display(Name = "Invoice")]
		public virtual String sInvoice { get; set; }
		[Required]
		[Display(Name = "Purchase ID")]
		public virtual Int64 ixPurchase { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

