using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class InvoicePurchaseLineAmountsService : IInvoicePurchaseLineAmountsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IInvoicePurchaseLineAmountsRepository _invoicepurchaselineamountsRepository;

        public InvoicePurchaseLineAmountsService(IInvoicePurchaseLineAmountsRepository invoicepurchaselineamountsRepository)
        {
            _invoicepurchaselineamountsRepository = invoicepurchaselineamountsRepository;
        }

        public InvoicePurchaseLineAmountsPost GetPost(Int64 ixInvoicePurchaseLineAmount) => _invoicepurchaselineamountsRepository.GetPost(ixInvoicePurchaseLineAmount);
        public InvoicePurchaseLineAmounts Get(Int64 ixInvoicePurchaseLineAmount) => _invoicepurchaselineamountsRepository.Get(ixInvoicePurchaseLineAmount);
        public IQueryable<InvoicePurchaseLineAmounts> Index() => _invoicepurchaselineamountsRepository.Index();
        public IQueryable<InvoicePurchaseLineAmounts> IndexDb() => _invoicepurchaselineamountsRepository.IndexDb();
       public IQueryable<Currencies> selectCurrencies() => _invoicepurchaselineamountsRepository.selectCurrencies();
        public IQueryable<Invoices> selectInvoices() => _invoicepurchaselineamountsRepository.selectInvoices();
        public IQueryable<PurchaseLines> selectPurchaseLines() => _invoicepurchaselineamountsRepository.selectPurchaseLines();
       public IQueryable<Currencies> CurrenciesDb() => _invoicepurchaselineamountsRepository.CurrenciesDb();
        public IQueryable<Invoices> InvoicesDb() => _invoicepurchaselineamountsRepository.InvoicesDb();
        public IQueryable<PurchaseLines> PurchaseLinesDb() => _invoicepurchaselineamountsRepository.PurchaseLinesDb();
        public bool VerifyInvoicePurchaseLineAmountUnique(Int64 ixInvoicePurchaseLineAmount, string sInvoicePurchaseLineAmount) => _invoicepurchaselineamountsRepository.VerifyInvoicePurchaseLineAmountUnique(ixInvoicePurchaseLineAmount, sInvoicePurchaseLineAmount);
        public List<string> VerifyInvoicePurchaseLineAmountDeleteOK(Int64 ixInvoicePurchaseLineAmount, string sInvoicePurchaseLineAmount) => _invoicepurchaselineamountsRepository.VerifyInvoicePurchaseLineAmountDeleteOK(ixInvoicePurchaseLineAmount, sInvoicePurchaseLineAmount);

        public Task<Int64> Create(InvoicePurchaseLineAmountsPost invoicepurchaselineamountsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._invoicepurchaselineamountsRepository.RegisterCreate(invoicepurchaselineamountsPost);
            try
            {
                this._invoicepurchaselineamountsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._invoicepurchaselineamountsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(invoicepurchaselineamountsPost.ixInvoicePurchaseLineAmount);

        }
        public Task Edit(InvoicePurchaseLineAmountsPost invoicepurchaselineamountsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._invoicepurchaselineamountsRepository.RegisterEdit(invoicepurchaselineamountsPost);
            try
            {
                this._invoicepurchaselineamountsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._invoicepurchaselineamountsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(InvoicePurchaseLineAmountsPost invoicepurchaselineamountsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._invoicepurchaselineamountsRepository.RegisterDelete(invoicepurchaselineamountsPost);
            try
            {
                this._invoicepurchaselineamountsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._invoicepurchaselineamountsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

