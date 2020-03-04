using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IPaymentCreditCardsRepository
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
        void RegisterCreate(PaymentCreditCardsPost paymentcreditcardsPost);
        void RegisterEdit(PaymentCreditCardsPost paymentcreditcardsPost);
        void RegisterDelete(PaymentCreditCardsPost paymentcreditcardsPost);
        void Rollback();
        void Commit();
    }
}
  

