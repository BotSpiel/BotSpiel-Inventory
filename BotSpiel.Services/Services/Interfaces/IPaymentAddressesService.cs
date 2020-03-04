using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IPaymentAddressesService
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

        Task<Int64> Create(PaymentAddressesPost paymentaddressesPost);
        Task Edit(PaymentAddressesPost paymentaddressesPost);
        Task Delete(PaymentAddressesPost paymentaddressesPost);
    }
}
  

