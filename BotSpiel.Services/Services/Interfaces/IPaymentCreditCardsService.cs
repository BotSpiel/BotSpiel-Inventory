using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IPaymentCreditCardsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        PaymentCreditCardsPost GetPost(Int64 ixPaymentCreditCard);        
		PaymentCreditCards Get(Int64 ixPaymentCreditCard);
        IQueryable<PaymentCreditCards> Index();
        IQueryable<PaymentCreditCards> IndexDb();
        bool VerifyPaymentCreditCardUnique(Int64 ixPaymentCreditCard, string sPaymentCreditCard);
        List<string> VerifyPaymentCreditCardDeleteOK(Int64 ixPaymentCreditCard, string sPaymentCreditCard);

        Task<Int64> Create(PaymentCreditCardsPost paymentcreditcardsPost);
        Task Edit(PaymentCreditCardsPost paymentcreditcardsPost);
        Task Delete(PaymentCreditCardsPost paymentcreditcardsPost);
    }
}
  

