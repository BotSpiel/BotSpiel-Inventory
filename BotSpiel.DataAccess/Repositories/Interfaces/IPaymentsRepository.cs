using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IPaymentsRepository
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
        void RegisterCreate(PaymentsPost paymentsPost);
        void RegisterEdit(PaymentsPost paymentsPost);
        void RegisterDelete(PaymentsPost paymentsPost);
        void Rollback();
        void Commit();
    }
}
  

