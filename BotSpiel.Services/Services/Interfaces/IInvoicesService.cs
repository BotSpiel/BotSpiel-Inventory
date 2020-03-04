using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IInvoicesService
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

        Task<Int64> Create(InvoicesPost invoicesPost);
        Task Edit(InvoicesPost invoicesPost);
        Task Delete(InvoicesPost invoicesPost);
    }
}
  

