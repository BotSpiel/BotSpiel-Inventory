using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IPaymentsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        PaymentsPost GetPost(Int64 ixPayment);        
		Payments Get(Int64 ixPayment);
        IQueryable<Payments> Index();
        IQueryable<Payments> IndexDb();
       IQueryable<Invoices> selectInvoices();
       IQueryable<Invoices> InvoicesDb();
        bool VerifyPaymentUnique(Int64 ixPayment, string sPayment);
        List<string> VerifyPaymentDeleteOK(Int64 ixPayment, string sPayment);

        Task<Int64> Create(PaymentsPost paymentsPost);
        Task Edit(PaymentsPost paymentsPost);
        Task Delete(PaymentsPost paymentsPost);
    }
}
  

