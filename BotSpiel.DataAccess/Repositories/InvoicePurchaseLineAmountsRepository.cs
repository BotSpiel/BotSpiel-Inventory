using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class InvoicePurchaseLineAmountsRepository : IInvoicePurchaseLineAmountsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly InvoicePurchaseLineAmountsDB _context;
       private readonly InvoicePurchaseLineTaxAmountsDB _contextInvoicePurchaseLineTaxAmounts;
  
        public InvoicePurchaseLineAmountsRepository(InvoicePurchaseLineAmountsDB context, InvoicePurchaseLineTaxAmountsDB contextInvoicePurchaseLineTaxAmounts)
        {
            _context = context;
           _contextInvoicePurchaseLineTaxAmounts = contextInvoicePurchaseLineTaxAmounts;
  
        }

        public InvoicePurchaseLineAmountsPost GetPost(Int64 ixInvoicePurchaseLineAmount) => _context.InvoicePurchaseLineAmountsPost.AsNoTracking().Where(x => x.ixInvoicePurchaseLineAmount == ixInvoicePurchaseLineAmount).First();
         
		public InvoicePurchaseLineAmounts Get(Int64 ixInvoicePurchaseLineAmount)
        {
            InvoicePurchaseLineAmounts invoicepurchaselineamounts = _context.InvoicePurchaseLineAmounts.AsNoTracking().Where(x => x.ixInvoicePurchaseLineAmount == ixInvoicePurchaseLineAmount).First();
            invoicepurchaselineamounts.Currencies = _context.Currencies.Find(invoicepurchaselineamounts.ixCurrency);
            invoicepurchaselineamounts.Invoices = _context.Invoices.Find(invoicepurchaselineamounts.ixInvoice);
            invoicepurchaselineamounts.PurchaseLines = _context.PurchaseLines.Find(invoicepurchaselineamounts.ixPurchaseLine);

            return invoicepurchaselineamounts;
        }

        public IQueryable<InvoicePurchaseLineAmounts> Index()
        {
            var invoicepurchaselineamounts = _context.InvoicePurchaseLineAmounts.Include(a => a.Invoices).Include(a => a.PurchaseLines).Include(a => a.Currencies).AsNoTracking(); 
            return invoicepurchaselineamounts;
        }

        public IQueryable<InvoicePurchaseLineAmounts> IndexDb()
        {
            var invoicepurchaselineamounts = _context.InvoicePurchaseLineAmounts.Include(a => a.Invoices).Include(a => a.PurchaseLines).Include(a => a.Currencies).AsNoTracking(); 
            return invoicepurchaselineamounts;
        }
       public IQueryable<Currencies> selectCurrencies()
        {
            List<Currencies> currencies = new List<Currencies>();
            _context.Currencies.AsNoTracking()
                .ToList()
                .ForEach(x => currencies.Add(x));
            return currencies.AsQueryable();
        }
        public IQueryable<Invoices> selectInvoices()
        {
            List<Invoices> invoices = new List<Invoices>();
            _context.Invoices.Include(a => a.Purchases).AsNoTracking()
                .ToList()
                .ForEach(x => invoices.Add(x));
            return invoices.AsQueryable();
        }
        public IQueryable<PurchaseLines> selectPurchaseLines()
        {
            List<PurchaseLines> purchaselines = new List<PurchaseLines>();
            _context.PurchaseLines.Include(a => a.Materials).Include(a => a.Purchases).AsNoTracking()
                .ToList()
                .ForEach(x => purchaselines.Add(x));
            return purchaselines.AsQueryable();
        }
       public IQueryable<Currencies> CurrenciesDb()
        {
            List<Currencies> currencies = new List<Currencies>();
            _context.Currencies.AsNoTracking()
                .ToList()
                .ForEach(x => currencies.Add(x));
            return currencies.AsQueryable();
        }
        public IQueryable<Invoices> InvoicesDb()
        {
            List<Invoices> invoices = new List<Invoices>();
            _context.Invoices.Include(a => a.Purchases).AsNoTracking()
                .ToList()
                .ForEach(x => invoices.Add(x));
            return invoices.AsQueryable();
        }
        public IQueryable<PurchaseLines> PurchaseLinesDb()
        {
            List<PurchaseLines> purchaselines = new List<PurchaseLines>();
            _context.PurchaseLines.Include(a => a.Materials).Include(a => a.Purchases).AsNoTracking()
                .ToList()
                .ForEach(x => purchaselines.Add(x));
            return purchaselines.AsQueryable();
        }
        public bool VerifyInvoicePurchaseLineAmountUnique(Int64 ixInvoicePurchaseLineAmount, string sInvoicePurchaseLineAmount)
        {
            if (_context.InvoicePurchaseLineAmounts.AsNoTracking().Where(x => x.sInvoicePurchaseLineAmount == sInvoicePurchaseLineAmount).Any() && ixInvoicePurchaseLineAmount == 0L) return false;
            else if (_context.InvoicePurchaseLineAmounts.AsNoTracking().Where(x => x.sInvoicePurchaseLineAmount == sInvoicePurchaseLineAmount && x.ixInvoicePurchaseLineAmount != ixInvoicePurchaseLineAmount).Any() && ixInvoicePurchaseLineAmount != 0L) return false;
            else return true;
        }

        public List<string> VerifyInvoicePurchaseLineAmountDeleteOK(Int64 ixInvoicePurchaseLineAmount, string sInvoicePurchaseLineAmount)
        {
            List<string> existInEntities = new List<string>();
           if (_contextInvoicePurchaseLineTaxAmounts.InvoicePurchaseLineTaxAmounts.AsNoTracking().Where(x => x.ixInvoicePurchaseLineAmount == ixInvoicePurchaseLineAmount).Any()) existInEntities.Add("InvoicePurchaseLineTaxAmounts");

            return existInEntities;
        }


        public void RegisterCreate(InvoicePurchaseLineAmountsPost invoicepurchaselineamountsPost)
		{
            _context.InvoicePurchaseLineAmountsPost.Add(invoicepurchaselineamountsPost); 
        }

        public void RegisterEdit(InvoicePurchaseLineAmountsPost invoicepurchaselineamountsPost)
        {
            _context.Entry(invoicepurchaselineamountsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(InvoicePurchaseLineAmountsPost invoicepurchaselineamountsPost)
        {
            _context.InvoicePurchaseLineAmountsPost.Remove(invoicepurchaselineamountsPost);
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
  

