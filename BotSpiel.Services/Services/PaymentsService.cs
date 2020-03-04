using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class PaymentsService : IPaymentsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IPaymentsRepository _paymentsRepository;

        public PaymentsService(IPaymentsRepository paymentsRepository)
        {
            _paymentsRepository = paymentsRepository;
        }

        public PaymentsPost GetPost(Int64 ixPayment) => _paymentsRepository.GetPost(ixPayment);
        public Payments Get(Int64 ixPayment) => _paymentsRepository.Get(ixPayment);
        public IQueryable<Payments> Index() => _paymentsRepository.Index();
        public IQueryable<Payments> IndexDb() => _paymentsRepository.IndexDb();
       public IQueryable<Invoices> selectInvoices() => _paymentsRepository.selectInvoices();
       public IQueryable<Invoices> InvoicesDb() => _paymentsRepository.InvoicesDb();
        public bool VerifyPaymentUnique(Int64 ixPayment, string sPayment) => _paymentsRepository.VerifyPaymentUnique(ixPayment, sPayment);
        public List<string> VerifyPaymentDeleteOK(Int64 ixPayment, string sPayment) => _paymentsRepository.VerifyPaymentDeleteOK(ixPayment, sPayment);

        public Task<Int64> Create(PaymentsPost paymentsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._paymentsRepository.RegisterCreate(paymentsPost);
            try
            {
                this._paymentsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._paymentsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(paymentsPost.ixPayment);

        }
        public Task Edit(PaymentsPost paymentsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._paymentsRepository.RegisterEdit(paymentsPost);
            try
            {
                this._paymentsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._paymentsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(PaymentsPost paymentsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._paymentsRepository.RegisterDelete(paymentsPost);
            try
            {
                this._paymentsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._paymentsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

