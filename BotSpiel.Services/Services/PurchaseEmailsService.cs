using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class PurchaseEmailsService : IPurchaseEmailsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IPurchaseEmailsRepository _purchaseemailsRepository;

        public PurchaseEmailsService(IPurchaseEmailsRepository purchaseemailsRepository)
        {
            _purchaseemailsRepository = purchaseemailsRepository;
        }

        public PurchaseEmailsPost GetPost(Int64 ixPurchaseEmail) => _purchaseemailsRepository.GetPost(ixPurchaseEmail);
        public PurchaseEmails Get(Int64 ixPurchaseEmail) => _purchaseemailsRepository.Get(ixPurchaseEmail);
        public IQueryable<PurchaseEmails> Index() => _purchaseemailsRepository.Index();
        public IQueryable<PurchaseEmails> IndexDb() => _purchaseemailsRepository.IndexDb();
       public IQueryable<Purchases> selectPurchases() => _purchaseemailsRepository.selectPurchases();
        public IQueryable<SendEmails> selectSendEmails() => _purchaseemailsRepository.selectSendEmails();
       public IQueryable<Purchases> PurchasesDb() => _purchaseemailsRepository.PurchasesDb();
        public IQueryable<SendEmails> SendEmailsDb() => _purchaseemailsRepository.SendEmailsDb();
        public bool VerifyPurchaseEmailUnique(Int64 ixPurchaseEmail, string sPurchaseEmail) => _purchaseemailsRepository.VerifyPurchaseEmailUnique(ixPurchaseEmail, sPurchaseEmail);
        public List<string> VerifyPurchaseEmailDeleteOK(Int64 ixPurchaseEmail, string sPurchaseEmail) => _purchaseemailsRepository.VerifyPurchaseEmailDeleteOK(ixPurchaseEmail, sPurchaseEmail);

        public Task<Int64> Create(PurchaseEmailsPost purchaseemailsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._purchaseemailsRepository.RegisterCreate(purchaseemailsPost);
            try
            {
                this._purchaseemailsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._purchaseemailsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(purchaseemailsPost.ixPurchaseEmail);

        }
        public Task Edit(PurchaseEmailsPost purchaseemailsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._purchaseemailsRepository.RegisterEdit(purchaseemailsPost);
            try
            {
                this._purchaseemailsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._purchaseemailsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(PurchaseEmailsPost purchaseemailsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._purchaseemailsRepository.RegisterDelete(purchaseemailsPost);
            try
            {
                this._purchaseemailsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._purchaseemailsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

