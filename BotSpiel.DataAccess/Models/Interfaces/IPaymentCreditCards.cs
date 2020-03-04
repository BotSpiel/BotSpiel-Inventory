using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IPaymentCreditCards
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixPaymentCreditCard { get; set; }
		Int64 ixPaymentCreditCardEdit { get; set; }
		String sPaymentCreditCard { get; set; }
		String sCreditCardType { get; set; }
		String sFirstName { get; set; }
		String sLastName { get; set; }
		Int32 nExpireMonth { get; set; }
		Int32 nExpireYear { get; set; }
		String sCvvTwo { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
    }
}
  

