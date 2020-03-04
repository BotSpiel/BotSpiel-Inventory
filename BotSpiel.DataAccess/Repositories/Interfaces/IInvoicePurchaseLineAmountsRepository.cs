using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IInvoicePurchaseLineAmountsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        InvoicePurchaseLineAmountsPost GetPost(Int64 ixInvoicePurchaseLineAmount);        
		InvoicePurchaseLineAmounts Get(Int64 ixInvoicePurchaseLineAmount);
        IQueryable<InvoicePurchaseLineAmounts> Index();
        IQueryable<InvoicePurchaseLineAmounts> IndexDb();
       IQueryable<Currencies> selectCurrencies();
        IQueryable<Invoices> selectInvoices();
        IQueryable<PurchaseLines> selectPurchaseLines();
       IQueryable<Currencies> CurrenciesDb();
        IQueryable<Invoices> InvoicesDb();
        IQueryable<PurchaseLines> PurchaseLinesDb();
        bool VerifyInvoicePurchaseLineAmountUnique(Int64 ixInvoicePurchaseLineAmount, string sInvoicePurchaseLineAmount);
        List<string> VerifyInvoicePurchaseLineAmountDeleteOK(Int64 ixInvoicePurchaseLineAmount, string sInvoicePurchaseLineAmount);
        void RegisterCreate(InvoicePurchaseLineAmountsPost invoicepurchaselineamountsPost);
        void RegisterEdit(InvoicePurchaseLineAmountsPost invoicepurchaselineamountsPost);
        void RegisterDelete(InvoicePurchaseLineAmountsPost invoicepurchaselineamountsPost);
        void Rollback();
        void Commit();
    }
}
  

