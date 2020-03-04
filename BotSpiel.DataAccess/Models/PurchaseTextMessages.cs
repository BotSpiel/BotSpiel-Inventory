using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class PurchaseTextMessages : IPurchaseTextMessages
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public PurchaseTextMessages()
        {
		Purchases _Purchases = new Purchases();
		Purchases = _Purchases;
		SendTextMessages _SendTextMessages = new SendTextMessages();
		SendTextMessages = _SendTextMessages;

        }
		[Display(Name = "Purchase Text Message ID")]
		public virtual Int64 ixPurchaseTextMessage { get; set; }
		[Display(Name = "Purchase Text Message ID")]
		public virtual Int64 ixPurchaseTextMessageEdit { get; set; }
		[Display(Name = "Purchase Text Message")]
		public virtual String sPurchaseTextMessage { get; set; }
		[Required]
		[Display(Name = "Purchase ID")]
		public virtual Int64 ixPurchase { get; set; }
		[Required]
		[Display(Name = "Send Text Message ID")]
		public virtual Int64 ixSendTextMessage { get; set; }
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
		[ForeignKey("ixSendTextMessage")]
		public virtual SendTextMessages SendTextMessages { get; set; }
    }
}
  

