using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class InvoicesService : IInvoicesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IInvoicesRepository _invoicesRepository;

        public InvoicesService(IInvoicesRepository invoicesRepository)
        {
            _invoicesRepository = invoicesRepository;
        }

        public InvoicesPost GetPost(Int64 ixInvoice) => _invoicesRepository.GetPost(ixInvoice);
        public Invoices Get(Int64 ixInvoice) => _invoicesRepository.Get(ixInvoice);
        public IQueryable<Invoices> Index() => _invoicesRepository.Index();
        public IQueryable<Invoices> IndexDb() => _invoicesRepository.IndexDb();
       public IQueryable<Purchases> selectPurchases() => _invoicesRepository.selectPurchases();
       public IQueryable<Purchases> PurchasesDb() => _invoicesRepository.PurchasesDb();
        public bool VerifyInvoiceUnique(Int64 ixInvoice, string sInvoice) => _invoicesRepository.VerifyInvoiceUnique(ixInvoice, sInvoice);
        public List<string> VerifyInvoiceDeleteOK(Int64 ixInvoice, string sInvoice) => _invoicesRepository.VerifyInvoiceDeleteOK(ixInvoice, sInvoice);

        public Task<Int64> Create(InvoicesPost invoicesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._invoicesRepository.RegisterCreate(invoicesPost);
            try
            {
                this._invoicesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._invoicesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(invoicesPost.ixInvoice);

        }
        public Task Edit(InvoicesPost invoicesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._invoicesRepository.RegisterEdit(invoicesPost);
            try
            {
                this._invoicesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._invoicesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(InvoicesPost invoicesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._invoicesRepository.RegisterDelete(invoicesPost);
            try
            {
                this._invoicesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._invoicesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

