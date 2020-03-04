using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class Payments : IPayments
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public Payments()
        {
		Invoices _Invoices = new Invoices();
		Invoices = _Invoices;

        }
		[Display(Name = "Payment ID")]
		public virtual Int64 ixPayment { get; set; }
		[Display(Name = "Payment ID")]
		public virtual Int64 ixPaymentEdit { get; set; }
		[Display(Name = "Payment")]
		public virtual String sPayment { get; set; }
		[Required]
		[Display(Name = "Invoice ID")]
		public virtual Int64 ixInvoice { get; set; }
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
		[ForeignKey("ixInvoice")]
		public virtual Invoices Invoices { get; set; }
    }
}
  

