using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class PurchaseEmailsRepository : IPurchaseEmailsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly PurchaseEmailsDB _context;
  
        public PurchaseEmailsRepository(PurchaseEmailsDB context)
        {
            _context = context;
  
        }

        public PurchaseEmailsPost GetPost(Int64 ixPurchaseEmail) => _context.PurchaseEmailsPost.AsNoTracking().Where(x => x.ixPurchaseEmail == ixPurchaseEmail).First();
         
		public PurchaseEmails Get(Int64 ixPurchaseEmail)
        {
            PurchaseEmails purchaseemails = _context.PurchaseEmails.AsNoTracking().Where(x => x.ixPurchaseEmail == ixPurchaseEmail).First();
            purchaseemails.Purchases = _context.Purchases.Find(purchaseemails.ixPurchase);
            purchaseemails.SendEmails = _context.SendEmails.Find(purchaseemails.ixSendEmail);

            return purchaseemails;
        }

        public IQueryable<PurchaseEmails> Index()
        {
            var purchaseemails = _context.PurchaseEmails.Include(a => a.Purchases).Include(a => a.SendEmails).AsNoTracking(); 
            return purchaseemails;
        }

        public IQueryable<PurchaseEmails> IndexDb()
        {
            var purchaseemails = _context.PurchaseEmails.Include(a => a.Purchases).Include(a => a.SendEmails).AsNoTracking(); 
            return purchaseemails;
        }
       public IQueryable<Purchases> selectPurchases()
        {
            List<Purchases> purchases = new List<Purchases>();
            _context.Purchases.Include(a => a.Companies).Include(a => a.People).AsNoTracking()
                .ToList()
                .ForEach(x => purchases.Add(x));
            return purchases.AsQueryable();
        }
        public IQueryable<SendEmails> selectSendEmails()
        {
            List<SendEmails> sendemails = new List<SendEmails>();
            _context.SendEmails.Include(a => a.People).AsNoTracking()
                .ToList()
                .ForEach(x => sendemails.Add(x));
            return sendemails.AsQueryable();
        }
       public IQueryable<Purchases> PurchasesDb()
        {
            List<Purchases> purchases = new List<Purchases>();
            _context.Purchases.Include(a => a.Companies).Include(a => a.People).AsNoTracking()
                .ToList()
                .ForEach(x => purchases.Add(x));
            return purchases.AsQueryable();
        }
        public IQueryable<SendEmails> SendEmailsDb()
        {
            List<SendEmails> sendemails = new List<SendEmails>();
            _context.SendEmails.Include(a => a.People).AsNoTracking()
                .ToList()
                .ForEach(x => sendemails.Add(x));
            return sendemails.AsQueryable();
        }
        public bool VerifyPurchaseEmailUnique(Int64 ixPurchaseEmail, string sPurchaseEmail)
        {
            if (_context.PurchaseEmails.AsNoTracking().Where(x => x.sPurchaseEmail == sPurchaseEmail).Any() && ixPurchaseEmail == 0L) return false;
            else if (_context.PurchaseEmails.AsNoTracking().Where(x => x.sPurchaseEmail == sPurchaseEmail && x.ixPurchaseEmail != ixPurchaseEmail).Any() && ixPurchaseEmail != 0L) return false;
            else return true;
        }

        public List<string> VerifyPurchaseEmailDeleteOK(Int64 ixPurchaseEmail, string sPurchaseEmail)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(PurchaseEmailsPost purchaseemailsPost)
		{
            _context.PurchaseEmailsPost.Add(purchaseemailsPost); 
        }

        public void RegisterEdit(PurchaseEmailsPost purchaseemailsPost)
        {
            _context.Entry(purchaseemailsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(PurchaseEmailsPost purchaseemailsPost)
        {
            _context.PurchaseEmailsPost.Remove(purchaseemailsPost);
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
  

