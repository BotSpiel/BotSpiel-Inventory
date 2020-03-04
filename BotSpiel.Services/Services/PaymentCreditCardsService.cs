using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class PaymentCreditCardsService : IPaymentCreditCardsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IPaymentCreditCardsRepository _paymentcreditcardsRepository;

        public PaymentCreditCardsService(IPaymentCreditCardsRepository paymentcreditcardsRepository)
        {
            _paymentcreditcardsRepository = paymentcreditcardsRepository;
        }

        public PaymentCreditCardsPost GetPost(Int64 ixPaymentCreditCard) => _paymentcreditcardsRepository.GetPost(ixPaymentCreditCard);
        public PaymentCreditCards Get(Int64 ixPaymentCreditCard) => _paymentcreditcardsRepository.Get(ixPaymentCreditCard);
        public IQueryable<PaymentCreditCards> Index() => _paymentcreditcardsRepository.Index();
        public IQueryable<PaymentCreditCards> IndexDb() => _paymentcreditcardsRepository.IndexDb();
        public bool VerifyPaymentCreditCardUnique(Int64 ixPaymentCreditCard, string sPaymentCreditCard) => _paymentcreditcardsRepository.VerifyPaymentCreditCardUnique(ixPaymentCreditCard, sPaymentCreditCard);
        public List<string> VerifyPaymentCreditCardDeleteOK(Int64 ixPaymentCreditCard, string sPaymentCreditCard) => _paymentcreditcardsRepository.VerifyPaymentCreditCardDeleteOK(ixPaymentCreditCard, sPaymentCreditCard);

        public Task<Int64> Create(PaymentCreditCardsPost paymentcreditcardsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._paymentcreditcardsRepository.RegisterCreate(paymentcreditcardsPost);
            try
            {
                this._paymentcreditcardsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._paymentcreditcardsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(paymentcreditcardsPost.ixPaymentCreditCard);

        }
        public Task Edit(PaymentCreditCardsPost paymentcreditcardsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._paymentcreditcardsRepository.RegisterEdit(paymentcreditcardsPost);
            try
            {
                this._paymentcreditcardsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._paymentcreditcardsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(PaymentCreditCardsPost paymentcreditcardsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._paymentcreditcardsRepository.RegisterDelete(paymentcreditcardsPost);
            try
            {
                this._paymentcreditcardsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._paymentcreditcardsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

