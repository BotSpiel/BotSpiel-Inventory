using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class PurchaseTextMessagesService : IPurchaseTextMessagesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IPurchaseTextMessagesRepository _purchasetextmessagesRepository;

        public PurchaseTextMessagesService(IPurchaseTextMessagesRepository purchasetextmessagesRepository)
        {
            _purchasetextmessagesRepository = purchasetextmessagesRepository;
        }

        public PurchaseTextMessagesPost GetPost(Int64 ixPurchaseTextMessage) => _purchasetextmessagesRepository.GetPost(ixPurchaseTextMessage);
        public PurchaseTextMessages Get(Int64 ixPurchaseTextMessage) => _purchasetextmessagesRepository.Get(ixPurchaseTextMessage);
        public IQueryable<PurchaseTextMessages> Index() => _purchasetextmessagesRepository.Index();
        public IQueryable<PurchaseTextMessages> IndexDb() => _purchasetextmessagesRepository.IndexDb();
       public IQueryable<Purchases> selectPurchases() => _purchasetextmessagesRepository.selectPurchases();
        public IQueryable<SendTextMessages> selectSendTextMessages() => _purchasetextmessagesRepository.selectSendTextMessages();
       public IQueryable<Purchases> PurchasesDb() => _purchasetextmessagesRepository.PurchasesDb();
        public IQueryable<SendTextMessages> SendTextMessagesDb() => _purchasetextmessagesRepository.SendTextMessagesDb();
        public bool VerifyPurchaseTextMessageUnique(Int64 ixPurchaseTextMessage, string sPurchaseTextMessage) => _purchasetextmessagesRepository.VerifyPurchaseTextMessageUnique(ixPurchaseTextMessage, sPurchaseTextMessage);
        public List<string> VerifyPurchaseTextMessageDeleteOK(Int64 ixPurchaseTextMessage, string sPurchaseTextMessage) => _purchasetextmessagesRepository.VerifyPurchaseTextMessageDeleteOK(ixPurchaseTextMessage, sPurchaseTextMessage);

        public Task<Int64> Create(PurchaseTextMessagesPost purchasetextmessagesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._purchasetextmessagesRepository.RegisterCreate(purchasetextmessagesPost);
            try
            {
                this._purchasetextmessagesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._purchasetextmessagesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(purchasetextmessagesPost.ixPurchaseTextMessage);

        }
        public Task Edit(PurchaseTextMessagesPost purchasetextmessagesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._purchasetextmessagesRepository.RegisterEdit(purchasetextmessagesPost);
            try
            {
                this._purchasetextmessagesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._purchasetextmessagesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(PurchaseTextMessagesPost purchasetextmessagesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._purchasetextmessagesRepository.RegisterDelete(purchasetextmessagesPost);
            try
            {
                this._purchasetextmessagesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._purchasetextmessagesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

