using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class PurchaseEmails : IPurchaseEmails
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public PurchaseEmails()
        {
		Purchases _Purchases = new Purchases();
		Purchases = _Purchases;
		SendEmails _SendEmails = new SendEmails();
		SendEmails = _SendEmails;

        }
		[Display(Name = "Purchase Email ID")]
		public virtual Int64 ixPurchaseEmail { get; set; }
		[Display(Name = "Purchase Email ID")]
		public virtual Int64 ixPurchaseEmailEdit { get; set; }
		[Display(Name = "Purchase Email")]
		public virtual String sPurchaseEmail { get; set; }
		[Required]
		[Display(Name = "Purchase ID")]
		public virtual Int64 ixPurchase { get; set; }
		[Required]
		[Display(Name = "Send Email ID")]
		public virtual Int64 ixSendEmail { get; set; }
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
		[ForeignKey("ixSendEmail")]
		public virtual SendEmails SendEmails { get; set; }
    }
}
  

