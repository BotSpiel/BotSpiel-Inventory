using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IInvoicePurchaseLineTaxAmountsService
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

        Task<Int64> Create(InvoicePurchaseLineTaxAmountsPost invoicepurchaselinetaxamountsPost);
        Task Edit(InvoicePurchaseLineTaxAmountsPost invoicepurchaselinetaxamountsPost);
        Task Delete(InvoicePurchaseLineTaxAmountsPost invoicepurchaselinetaxamountsPost);
    }
}
  

