using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class InvoicesRepository : IInvoicesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly InvoicesDB _context;
       private readonly InvoicePurchaseLineAmountsDB _contextInvoicePurchaseLineAmounts;
        private readonly PaymentsDB _contextPayments;
  
        public InvoicesRepository(InvoicesDB context, InvoicePurchaseLineAmountsDB contextInvoicePurchaseLineAmounts, PaymentsDB contextPayments)
        {
            _context = context;
           _contextInvoicePurchaseLineAmounts = contextInvoicePurchaseLineAmounts;
            _contextPayments = contextPayments;
  
        }

        public InvoicesPost GetPost(Int64 ixInvoice) => _context.InvoicesPost.AsNoTracking().Where(x => x.ixInvoice == ixInvoice).First();
         
		public Invoices Get(Int64 ixInvoice)
        {
            Invoices invoices = _context.Invoices.AsNoTracking().Where(x => x.ixInvoice == ixInvoice).First();
            invoices.Purchases = _context.Purchases.Find(invoices.ixPurchase);

            return invoices;
        }

        public IQueryable<Invoices> Index()
        {
            var invoices = _context.Invoices.Include(a => a.Purchases).AsNoTracking(); 
            return invoices;
        }

        public IQueryable<Invoices> IndexDb()
        {
            var invoices = _context.Invoices.Include(a => a.Purchases).AsNoTracking(); 
            return invoices;
        }
       public IQueryable<Purchases> selectPurchases()
        {
            List<Purchases> purchases = new List<Purchases>();
            _context.Purchases.Include(a => a.Companies).Include(a => a.People).AsNoTracking()
                .ToList()
                .ForEach(x => purchases.Add(x));
            return purchases.AsQueryable();
        }
       public IQueryable<Purchases> PurchasesDb()
        {
            List<Purchases> purchases = new List<Purchases>();
            _context.Purchases.Include(a => a.Companies).Include(a => a.People).AsNoTracking()
                .ToList()
                .ForEach(x => purchases.Add(x));
            return purchases.AsQueryable();
        }
        public bool VerifyInvoiceUnique(Int64 ixInvoice, string sInvoice)
        {
            if (_context.Invoices.AsNoTracking().Where(x => x.sInvoice == sInvoice).Any() && ixInvoice == 0L) return false;
            else if (_context.Invoices.AsNoTracking().Where(x => x.sInvoice == sInvoice && x.ixInvoice != ixInvoice).Any() && ixInvoice != 0L) return false;
            else return true;
        }

        public List<string> VerifyInvoiceDeleteOK(Int64 ixInvoice, string sInvoice)
        {
            List<string> existInEntities = new List<string>();
           if (_contextInvoicePurchaseLineAmounts.InvoicePurchaseLineAmounts.AsNoTracking().Where(x => x.ixInvoice == ixInvoice).Any()) existInEntities.Add("InvoicePurchaseLineAmounts");
            if (_contextPayments.Payments.AsNoTracking().Where(x => x.ixInvoice == ixInvoice).Any()) existInEntities.Add("Payments");

            return existInEntities;
        }


        public void RegisterCreate(InvoicesPost invoicesPost)
		{
            _context.InvoicesPost.Add(invoicesPost); 
        }

        public void RegisterEdit(InvoicesPost invoicesPost)
        {
            _context.Entry(invoicesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(InvoicesPost invoicesPost)
        {
            _context.InvoicesPost.Remove(invoicesPost);
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
  

