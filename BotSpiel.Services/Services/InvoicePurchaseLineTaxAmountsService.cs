using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class InvoicePurchaseLineTaxAmountsService : IInvoicePurchaseLineTaxAmountsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IInvoicePurchaseLineTaxAmountsRepository _invoicepurchaselinetaxamountsRepository;

        public InvoicePurchaseLineTaxAmountsService(IInvoicePurchaseLineTaxAmountsRepository invoicepurchaselinetaxamountsRepository)
        {
            _invoicepurchaselinetaxamountsRepository = invoicepurchaselinetaxamountsRepository;
        }

        public InvoicePurchaseLineTaxAmountsPost GetPost(Int64 ixInvoicePurchaseLineTaxAmount) => _invoicepurchaselinetaxamountsRepository.GetPost(ixInvoicePurchaseLineTaxAmount);
        public InvoicePurchaseLineTaxAmounts Get(Int64 ixInvoicePurchaseLineTaxAmount) => _invoicepurchaselinetaxamountsRepository.Get(ixInvoicePurchaseLineTaxAmount);
        public IQueryable<InvoicePurchaseLineTaxAmounts> Index() => _invoicepurchaselinetaxamountsRepository.Index();
        public IQueryable<InvoicePurchaseLineTaxAmounts> IndexDb() => _invoicepurchaselinetaxamountsRepository.IndexDb();
       public IQueryable<Currencies> selectCurrencies() => _invoicepurchaselinetaxamountsRepository.selectCurrencies();
        public IQueryable<Taxes> selectTaxes() => _invoicepurchaselinetaxamountsRepository.selectTaxes();
        public IQueryable<InvoicePurchaseLineAmounts> selectInvoicePurchaseLineAmounts() => _invoicepurchaselinetaxamountsRepository.selectInvoicePurchaseLineAmounts();
       public IQueryable<Currencies> CurrenciesDb() => _invoicepurchaselinetaxamountsRepository.CurrenciesDb();
        public IQueryable<Taxes> TaxesDb() => _invoicepurchaselinetaxamountsRepository.TaxesDb();
        public IQueryable<InvoicePurchaseLineAmounts> InvoicePurchaseLineAmountsDb() => _invoicepurchaselinetaxamountsRepository.InvoicePurchaseLineAmountsDb();
        public bool VerifyInvoicePurchaseLineTaxAmountUnique(Int64 ixInvoicePurchaseLineTaxAmount, string sInvoicePurchaseLineTaxAmount) => _invoicepurchaselinetaxamountsRepository.VerifyInvoicePurchaseLineTaxAmountUnique(ixInvoicePurchaseLineTaxAmount, sInvoicePurchaseLineTaxAmount);
        public List<string> VerifyInvoicePurchaseLineTaxAmountDeleteOK(Int64 ixInvoicePurchaseLineTaxAmount, string sInvoicePurchaseLineTaxAmount) => _invoicepurchaselinetaxamountsRepository.VerifyInvoicePurchaseLineTaxAmountDeleteOK(ixInvoicePurchaseLineTaxAmount, sInvoicePurchaseLineTaxAmount);

        public Task<Int64> Create(InvoicePurchaseLineTaxAmountsPost invoicepurchaselinetaxamountsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._invoicepurchaselinetaxamountsRepository.RegisterCreate(invoicepurchaselinetaxamountsPost);
            try
            {
                this._invoicepurchaselinetaxamountsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._invoicepurchaselinetaxamountsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(invoicepurchaselinetaxamountsPost.ixInvoicePurchaseLineTaxAmount);

        }
        public Task Edit(InvoicePurchaseLineTaxAmountsPost invoicepurchaselinetaxamountsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._invoicepurchaselinetaxamountsRepository.RegisterEdit(invoicepurchaselinetaxamountsPost);
            try
            {
                this._invoicepurchaselinetaxamountsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._invoicepurchaselinetaxamountsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(InvoicePurchaseLineTaxAmountsPost invoicepurchaselinetaxamountsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._invoicepurchaselinetaxamountsRepository.RegisterDelete(invoicepurchaselinetaxamountsPost);
            try
            {
                this._invoicepurchaselinetaxamountsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._invoicepurchaselinetaxamountsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

