using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IInvoicePurchaseLineTaxAmountsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        InvoicePurchaseLineTaxAmountsPost GetPost(Int64 ixInvoicePurchaseLineTaxAmount);        
		InvoicePurchaseLineTaxAmounts Get(Int64 ixInvoicePurchaseLineTaxAmount);
        IQueryable<InvoicePurchaseLineTaxAmounts> Index();
        IQueryable<InvoicePurchaseLineTaxAmounts> IndexDb();
       IQueryable<Currencies> selectCurrencies();
        IQueryable<Taxes> selectTaxes();
        IQueryable<InvoicePurchaseLineAmounts> selectInvoicePurchaseLineAmounts();
       IQueryable<Currencies> CurrenciesDb();
        IQueryable<Taxes> TaxesDb();
        IQueryable<InvoicePurchaseLineAmounts> InvoicePurchaseLineAmountsDb();
        bool VerifyInvoicePurchaseLineTaxAmountUnique(Int64 ixInvoicePurchaseLineTaxAmount, string sInvoicePurchaseLineTaxAmount);
        List<string> VerifyInvoicePurchaseLineTaxAmountDeleteOK(Int64 ixInvoicePurchaseLineTaxAmount, string sInvoicePurchaseLineTaxAmount);
        void RegisterCreate(InvoicePurchaseLineTaxAmountsPost invoicepurchaselinetaxamountsPost);
        void RegisterEdit(InvoicePurchaseLineTaxAmountsPost invoicepurchaselinetaxamountsPost);
        void RegisterDelete(InvoicePurchaseLineTaxAmountsPost invoicepurchaselinetaxamountsPost);
        void Rollback();
        void Commit();
    }
}
  

