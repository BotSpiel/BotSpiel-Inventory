using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IPaymentAddressesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        PaymentAddressesPost GetPost(Int64 ixPaymentAddress);        
		PaymentAddresses Get(Int64 ixPaymentAddress);
        IQueryable<PaymentAddresses> Index();
        IQueryable<PaymentAddresses> IndexDb();
        bool VerifyPaymentAddressUnique(Int64 ixPaymentAddress, string sPaymentAddress);
        List<string> VerifyPaymentAddressDeleteOK(Int64 ixPaymentAddress, string sPaymentAddress);
        void RegisterCreate(PaymentAddressesPost paymentaddressesPost);
        void RegisterEdit(PaymentAddressesPost paymentaddressesPost);
        void RegisterDelete(PaymentAddressesPost paymentaddressesPost);
        void Rollback();
        void Commit();
    }
}
  

