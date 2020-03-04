using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class PurchasesRepository : IPurchasesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly PurchasesDB _context;
       private readonly InvoicesDB _contextInvoices;
        private readonly PurchaseEmailsDB _contextPurchaseEmails;
        private readonly PurchaseLinesDB _contextPurchaseLines;
        private readonly PurchaseTextMessagesDB _contextPurchaseTextMessages;
  
        public PurchasesRepository(PurchasesDB context, InvoicesDB contextInvoices, PurchaseEmailsDB contextPurchaseEmails, PurchaseLinesDB contextPurchaseLines, PurchaseTextMessagesDB contextPurchaseTextMessages)
        {
            _context = context;
           _contextInvoices = contextInvoices;
            _contextPurchaseEmails = contextPurchaseEmails;
            _contextPurchaseLines = contextPurchaseLines;
            _contextPurchaseTextMessages = contextPurchaseTextMessages;
  
        }

        public PurchasesPost GetPost(Int64 ixPurchase) => _context.PurchasesPost.AsNoTracking().Where(x => x.ixPurchase == ixPurchase).First();
         
		public Purchases Get(Int64 ixPurchase)
        {
            Purchases purchases = _context.Purchases.AsNoTracking().Where(x => x.ixPurchase == ixPurchase).First();
            if (purchases.ixCompany != null)
        {
            purchases.Companies = _context.Companies.Find(purchases.ixCompany);
        }
            purchases.People = _context.People.Find(purchases.ixPerson);

            return purchases;
        }

        public IQueryable<Purchases> Index()
        {
            var purchases = _context.Purchases.Include(a => a.People).AsNoTracking(); 
            return purchases;
        }

        public IQueryable<Purchases> IndexDb()
        {
            var purchases = _context.Purchases.Include(a => a.People).AsNoTracking(); 
            return purchases;
        }
       public IQueryable<Companies> selectCompanies()
        {
            List<Companies> companies = new List<Companies>();
            _context.Companies.AsNoTracking()
                .ToList()
                .ForEach(x => companies.Add(x));
            return companies.AsQueryable();
        }
        public IQueryable<People> selectPeople()
        {
            List<People> people = new List<People>();
            _context.People.Include(a => a.Languages).AsNoTracking()
                .ToList()
                .ForEach(x => people.Add(x));
            return people.AsQueryable();
        }
       public IQueryable<Companies> CompaniesDb()
        {
            List<Companies> companies = new List<Companies>();
            _context.Companies.AsNoTracking()
                .ToList()
                .ForEach(x => companies.Add(x));
            return companies.AsQueryable();
        }
        public IQueryable<People> PeopleDb()
        {
            List<People> people = new List<People>();
            _context.People.Include(a => a.Languages).AsNoTracking()
                .ToList()
                .ForEach(x => people.Add(x));
            return people.AsQueryable();
        }
       public List<KeyValuePair<Int64?, string>> selectCompaniesNullable()
        {
            List<KeyValuePair<Int64?, string>> companiesNullable = new List<KeyValuePair<Int64?, string>>();
            companiesNullable.Add(new KeyValuePair<Int64?, string>(null, "None"));
            _context.Companies
                .OrderBy(k => k.sCompany)
                .ToList()
                .ForEach(k => companiesNullable.Add(new KeyValuePair<Int64?, string>(k.ixCompany, k.sCompany)));
            return companiesNullable;
        }
        public bool VerifyPurchaseUnique(Int64 ixPurchase, string sPurchase)
        {
            if (_context.Purchases.AsNoTracking().Where(x => x.sPurchase == sPurchase).Any() && ixPurchase == 0L) return false;
            else if (_context.Purchases.AsNoTracking().Where(x => x.sPurchase == sPurchase && x.ixPurchase != ixPurchase).Any() && ixPurchase != 0L) return false;
            else return true;
        }

        public List<string> VerifyPurchaseDeleteOK(Int64 ixPurchase, string sPurchase)
        {
            List<string> existInEntities = new List<string>();
           if (_contextInvoices.Invoices.AsNoTracking().Where(x => x.ixPurchase == ixPurchase).Any()) existInEntities.Add("Invoices");
            if (_contextPurchaseEmails.PurchaseEmails.AsNoTracking().Where(x => x.ixPurchase == ixPurchase).Any()) existInEntities.Add("PurchaseEmails");
            if (_contextPurchaseLines.PurchaseLines.AsNoTracking().Where(x => x.ixPurchase == ixPurchase).Any()) existInEntities.Add("PurchaseLines");
            if (_contextPurchaseTextMessages.PurchaseTextMessages.AsNoTracking().Where(x => x.ixPurchase == ixPurchase).Any()) existInEntities.Add("PurchaseTextMessages");

            return existInEntities;
        }


        public void RegisterCreate(PurchasesPost purchasesPost)
		{
            _context.PurchasesPost.Add(purchasesPost); 
        }

        public void RegisterEdit(PurchasesPost purchasesPost)
        {
            _context.Entry(purchasesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(PurchasesPost purchasesPost)
        {
            _context.PurchasesPost.Remove(purchasesPost);
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
  

