using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IInvoicesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        InvoicesPost GetPost(Int64 ixInvoice);        
		Invoices Get(Int64 ixInvoice);
        IQueryable<Invoices> Index();
        IQueryable<Invoices> IndexDb();
       IQueryable<Purchases> selectPurchases();
       IQueryable<Purchases> PurchasesDb();
        bool VerifyInvoiceUnique(Int64 ixInvoice, string sInvoice);
        List<string> VerifyInvoiceDeleteOK(Int64 ixInvoice, string sInvoice);
        void RegisterCreate(InvoicesPost invoicesPost);
        void RegisterEdit(InvoicesPost invoicesPost);
        void RegisterDelete(InvoicesPost invoicesPost);
        void Rollback();
        void Commit();
    }
}
  

