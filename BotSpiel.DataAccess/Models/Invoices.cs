using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class Invoices : IInvoices
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public Invoices()
        {
		Purchases _Purchases = new Purchases();
		Purchases = _Purchases;

        }
		[Display(Name = "Invoice ID")]
		public virtual Int64 ixInvoice { get; set; }
		[Display(Name = "Invoice ID")]
		public virtual Int64 ixInvoiceEdit { get; set; }
		[Display(Name = "Invoice")]
		public virtual String sInvoice { get; set; }
		[Required]
		[Display(Name = "Purchase ID")]
		public virtual Int64 ixPurchase { get; set; }
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
		[ForeignKey("ixPurchase")]
		public virtual Purchases Purchases { get; set; }
    }
}
  

