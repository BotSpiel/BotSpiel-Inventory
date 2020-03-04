using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class PaymentCreditCardsPost : IPaymentCreditCardsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Payment Credit Card ID")]
		public virtual Int64 ixPaymentCreditCard { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyPaymentCreditCard", controller: "PaymentCreditCards", AdditionalFields = nameof(ixPaymentCreditCard))]
		[Display(Name = "Payment Credit Card")]
		public virtual String sPaymentCreditCard { get; set; }
		[StringLength(100)]
		[Required]
		[Display(Name = "Credit Card Type")]
		public virtual String sCreditCardType { get; set; }
		[StringLength(300)]
		[Required]
		[Display(Name = "First Name")]
		public virtual String sFirstName { get; set; }
		[StringLength(300)]
		[Required]
		[Display(Name = "Last Name")]
		public virtual String sLastName { get; set; }
		[Required]
		[Display(Name = "Expire Month")]
		public virtual Int32 nExpireMonth { get; set; }
		[Required]
		[Display(Name = "Expire Year")]
		public virtual Int32 nExpireYear { get; set; }
		[StringLength(100)]
		[Required]
		[Display(Name = "Cvv Two")]
		public virtual String sCvvTwo { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

