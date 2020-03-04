using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class InvoicePurchaseLineTaxAmountsRepository : IInvoicePurchaseLineTaxAmountsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly InvoicePurchaseLineTaxAmountsDB _context;
  
        public InvoicePurchaseLineTaxAmountsRepository(InvoicePurchaseLineTaxAmountsDB context)
        {
            _context = context;
  
        }

        public InvoicePurchaseLineTaxAmountsPost GetPost(Int64 ixInvoicePurchaseLineTaxAmount) => _context.InvoicePurchaseLineTaxAmountsPost.AsNoTracking().Where(x => x.ixInvoicePurchaseLineTaxAmount == ixInvoicePurchaseLineTaxAmount).First();
         
		public InvoicePurchaseLineTaxAmounts Get(Int64 ixInvoicePurchaseLineTaxAmount)
        {
            InvoicePurchaseLineTaxAmounts invoicepurchaselinetaxamounts = _context.InvoicePurchaseLineTaxAmounts.AsNoTracking().Where(x => x.ixInvoicePurchaseLineTaxAmount == ixInvoicePurchaseLineTaxAmount).First();
            invoicepurchaselinetaxamounts.Currencies = _context.Currencies.Find(invoicepurchaselinetaxamounts.ixCurrency);
            invoicepurchaselinetaxamounts.InvoicePurchaseLineAmounts = _context.InvoicePurchaseLineAmounts.Find(invoicepurchaselinetaxamounts.ixInvoicePurchaseLineAmount);
            invoicepurchaselinetaxamounts.Taxes = _context.Taxes.Find(invoicepurchaselinetaxamounts.ixTax);

            return invoicepurchaselinetaxamounts;
        }

        public IQueryable<InvoicePurchaseLineTaxAmounts> Index()
        {
            var invoicepurchaselinetaxamounts = _context.InvoicePurchaseLineTaxAmounts.Include(a => a.InvoicePurchaseLineAmounts).Include(a => a.Taxes).Include(a => a.Currencies).AsNoTracking(); 
            return invoicepurchaselinetaxamounts;
        }

        public IQueryable<InvoicePurchaseLineTaxAmounts> IndexDb()
        {
            var invoicepurchaselinetaxamounts = _context.InvoicePurchaseLineTaxAmounts.Include(a => a.InvoicePurchaseLineAmounts).Include(a => a.Taxes).Include(a => a.Currencies).AsNoTracking(); 
            return invoicepurchaselinetaxamounts;
        }
       public IQueryable<Currencies> selectCurrencies()
        {
            List<Currencies> currencies = new List<Currencies>();
            _context.Currencies.AsNoTracking()
                .ToList()
                .ForEach(x => currencies.Add(x));
            return currencies.AsQueryable();
        }
        public IQueryable<InvoicePurchaseLineAmounts> selectInvoicePurchaseLineAmounts()
        {
            List<InvoicePurchaseLineAmounts> invoicepurchaselineamounts = new List<InvoicePurchaseLineAmounts>();
            _context.InvoicePurchaseLineAmounts.Include(a => a.Currencies).Include(a => a.Invoices).Include(a => a.PurchaseLines).AsNoTracking()
                .ToList()
                .ForEach(x => invoicepurchaselineamounts.Add(x));
            return invoicepurchaselineamounts.AsQueryable();
        }
        public IQueryable<Taxes> selectTaxes()
        {
            List<Taxes> taxes = new List<Taxes>();
            _context.Taxes.Include(a => a.Countries).Include(a => a.CountrySubDivisions).AsNoTracking()
                .ToList()
                .ForEach(x => taxes.Add(x));
            return taxes.AsQueryable();
        }
       public IQueryable<Currencies> CurrenciesDb()
        {
            List<Currencies> currencies = new List<Currencies>();
            _context.Currencies.AsNoTracking()
                .ToList()
                .ForEach(x => currencies.Add(x));
            return currencies.AsQueryable();
        }
        public IQueryable<InvoicePurchaseLineAmounts> InvoicePurchaseLineAmountsDb()
        {
            List<InvoicePurchaseLineAmounts> invoicepurchaselineamounts = new List<InvoicePurchaseLineAmounts>();
            _context.InvoicePurchaseLineAmounts.Include(a => a.Currencies).Include(a => a.Invoices).Include(a => a.PurchaseLines).AsNoTracking()
                .ToList()
                .ForEach(x => invoicepurchaselineamounts.Add(x));
            return invoicepurchaselineamounts.AsQueryable();
        }
        public IQueryable<Taxes> TaxesDb()
        {
            List<Taxes> taxes = new List<Taxes>();
            _context.Taxes.Include(a => a.Countries).Include(a => a.CountrySubDivisions).AsNoTracking()
                .ToList()
                .ForEach(x => taxes.Add(x));
            return taxes.AsQueryable();
        }
        public bool VerifyInvoicePurchaseLineTaxAmountUnique(Int64 ixInvoicePurchaseLineTaxAmount, string sInvoicePurchaseLineTaxAmount)
        {
            if (_context.InvoicePurchaseLineTaxAmounts.AsNoTracking().Where(x => x.sInvoicePurchaseLineTaxAmount == sInvoicePurchaseLineTaxAmount).Any() && ixInvoicePurchaseLineTaxAmount == 0L) return false;
            else if (_context.InvoicePurchaseLineTaxAmounts.AsNoTracking().Where(x => x.sInvoicePurchaseLineTaxAmount == sInvoicePurchaseLineTaxAmount && x.ixInvoicePurchaseLineTaxAmount != ixInvoicePurchaseLineTaxAmount).Any() && ixInvoicePurchaseLineTaxAmount != 0L) return false;
            else return true;
        }

        public List<string> VerifyInvoicePurchaseLineTaxAmountDeleteOK(Int64 ixInvoicePurchaseLineTaxAmount, string sInvoicePurchaseLineTaxAmount)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(InvoicePurchaseLineTaxAmountsPost invoicepurchaselinetaxamountsPost)
		{
            _context.InvoicePurchaseLineTaxAmountsPost.Add(invoicepurchaselinetaxamountsPost); 
        }

        public void RegisterEdit(InvoicePurchaseLineTaxAmountsPost invoicepurchaselinetaxamountsPost)
        {
            _context.Entry(invoicepurchaselinetaxamountsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(InvoicePurchaseLineTaxAmountsPost invoicepurchaselinetaxamountsPost)
        {
            _context.InvoicePurchaseLineTaxAmountsPost.Remove(invoicepurchaselinetaxamountsPost);
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
  

