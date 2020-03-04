using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class PaymentAddressesService : IPaymentAddressesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IPaymentAddressesRepository _paymentaddressesRepository;

        public PaymentAddressesService(IPaymentAddressesRepository paymentaddressesRepository)
        {
            _paymentaddressesRepository = paymentaddressesRepository;
        }

        public PaymentAddressesPost GetPost(Int64 ixPaymentAddress) => _paymentaddressesRepository.GetPost(ixPaymentAddress);
        public PaymentAddresses Get(Int64 ixPaymentAddress) => _paymentaddressesRepository.Get(ixPaymentAddress);
        public IQueryable<PaymentAddresses> Index() => _paymentaddressesRepository.Index();
        public IQueryable<PaymentAddresses> IndexDb() => _paymentaddressesRepository.IndexDb();
        public bool VerifyPaymentAddressUnique(Int64 ixPaymentAddress, string sPaymentAddress) => _paymentaddressesRepository.VerifyPaymentAddressUnique(ixPaymentAddress, sPaymentAddress);
        public List<string> VerifyPaymentAddressDeleteOK(Int64 ixPaymentAddress, string sPaymentAddress) => _paymentaddressesRepository.VerifyPaymentAddressDeleteOK(ixPaymentAddress, sPaymentAddress);

        public Task<Int64> Create(PaymentAddressesPost paymentaddressesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._paymentaddressesRepository.RegisterCreate(paymentaddressesPost);
            try
            {
                this._paymentaddressesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._paymentaddressesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(paymentaddressesPost.ixPaymentAddress);

        }
        public Task Edit(PaymentAddressesPost paymentaddressesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._paymentaddressesRepository.RegisterEdit(paymentaddressesPost);
            try
            {
                this._paymentaddressesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._paymentaddressesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(PaymentAddressesPost paymentaddressesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._paymentaddressesRepository.RegisterDelete(paymentaddressesPost);
            try
            {
                this._paymentaddressesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._paymentaddressesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

