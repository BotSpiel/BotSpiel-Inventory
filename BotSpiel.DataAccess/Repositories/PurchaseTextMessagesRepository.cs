using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class PurchaseTextMessagesRepository : IPurchaseTextMessagesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly PurchaseTextMessagesDB _context;
  
        public PurchaseTextMessagesRepository(PurchaseTextMessagesDB context)
        {
            _context = context;
  
        }

        public PurchaseTextMessagesPost GetPost(Int64 ixPurchaseTextMessage) => _context.PurchaseTextMessagesPost.AsNoTracking().Where(x => x.ixPurchaseTextMessage == ixPurchaseTextMessage).First();
         
		public PurchaseTextMessages Get(Int64 ixPurchaseTextMessage)
        {
            PurchaseTextMessages purchasetextmessages = _context.PurchaseTextMessages.AsNoTracking().Where(x => x.ixPurchaseTextMessage == ixPurchaseTextMessage).First();
            purchasetextmessages.Purchases = _context.Purchases.Find(purchasetextmessages.ixPurchase);
            purchasetextmessages.SendTextMessages = _context.SendTextMessages.Find(purchasetextmessages.ixSendTextMessage);

            return purchasetextmessages;
        }

        public IQueryable<PurchaseTextMessages> Index()
        {
            var purchasetextmessages = _context.PurchaseTextMessages.Include(a => a.Purchases).Include(a => a.SendTextMessages).AsNoTracking(); 
            return purchasetextmessages;
        }

        public IQueryable<PurchaseTextMessages> IndexDb()
        {
            var purchasetextmessages = _context.PurchaseTextMessages.Include(a => a.Purchases).Include(a => a.SendTextMessages).AsNoTracking(); 
            return purchasetextmessages;
        }
       public IQueryable<Purchases> selectPurchases()
        {
            List<Purchases> purchases = new List<Purchases>();
            _context.Purchases.Include(a => a.Companies).Include(a => a.People).AsNoTracking()
                .ToList()
                .ForEach(x => purchases.Add(x));
            return purchases.AsQueryable();
        }
        public IQueryable<SendTextMessages> selectSendTextMessages()
        {
            List<SendTextMessages> sendtextmessages = new List<SendTextMessages>();
            _context.SendTextMessages.Include(a => a.People).AsNoTracking()
                .ToList()
                .ForEach(x => sendtextmessages.Add(x));
            return sendtextmessages.AsQueryable();
        }
       public IQueryable<Purchases> PurchasesDb()
        {
            List<Purchases> purchases = new List<Purchases>();
            _context.Purchases.Include(a => a.Companies).Include(a => a.People).AsNoTracking()
                .ToList()
                .ForEach(x => purchases.Add(x));
            return purchases.AsQueryable();
        }
        public IQueryable<SendTextMessages> SendTextMessagesDb()
        {
            List<SendTextMessages> sendtextmessages = new List<SendTextMessages>();
            _context.SendTextMessages.Include(a => a.People).AsNoTracking()
                .ToList()
                .ForEach(x => sendtextmessages.Add(x));
            return sendtextmessages.AsQueryable();
        }
        public bool VerifyPurchaseTextMessageUnique(Int64 ixPurchaseTextMessage, string sPurchaseTextMessage)
        {
            if (_context.PurchaseTextMessages.AsNoTracking().Where(x => x.sPurchaseTextMessage == sPurchaseTextMessage).Any() && ixPurchaseTextMessage == 0L) return false;
            else if (_context.PurchaseTextMessages.AsNoTracking().Where(x => x.sPurchaseTextMessage == sPurchaseTextMessage && x.ixPurchaseTextMessage != ixPurchaseTextMessage).Any() && ixPurchaseTextMessage != 0L) return false;
            else return true;
        }

        public List<string> VerifyPurchaseTextMessageDeleteOK(Int64 ixPurchaseTextMessage, string sPurchaseTextMessage)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(PurchaseTextMessagesPost purchasetextmessagesPost)
		{
            _context.PurchaseTextMessagesPost.Add(purchasetextmessagesPost); 
        }

        public void RegisterEdit(PurchaseTextMessagesPost purchasetextmessagesPost)
        {
            _context.Entry(purchasetextmessagesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(PurchaseTextMessagesPost purchasetextmessagesPost)
        {
            _context.PurchaseTextMessagesPost.Remove(purchasetextmessagesPost);
        }

        public void Rollback()
        {
            _context.Rollback();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

    }
}
  

