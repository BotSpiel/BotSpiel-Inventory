using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IPaymentCreditCardsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixPaymentCreditCard { get; set; }
		String sPaymentCreditCard { get; set; }
		String sCreditCardType { get; set; }
		String sFirstName { get; set; }
		String sLastName { get; set; }
		Int32 nExpireMonth { get; set; }
		Int32 nExpireYear { get; set; }
		String sCvvTwo { get; set; }
		String UserName { get; set; }
    }
}
  

