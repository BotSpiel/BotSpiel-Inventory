using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class CurrenciesRepository : ICurrenciesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly CurrenciesDB _context;
       private readonly InvoicePurchaseLineAmountsDB _contextInvoicePurchaseLineAmounts;
        private readonly InvoicePurchaseLineTaxAmountsDB _contextInvoicePurchaseLineTaxAmounts;
  
        public CurrenciesRepository(CurrenciesDB context, InvoicePurchaseLineAmountsDB contextInvoicePurchaseLineAmounts, InvoicePurchaseLineTaxAmountsDB contextInvoicePurchaseLineTaxAmounts)
        {
            _context = context;
           _contextInvoicePurchaseLineAmounts = contextInvoicePurchaseLineAmounts;
            _contextInvoicePurchaseLineTaxAmounts = contextInvoicePurchaseLineTaxAmounts;
  
        }

        public CurrenciesPost GetPost(Int64 ixCurrency) => _context.CurrenciesPost.AsNoTracking().Where(x => x.ixCurrency == ixCurrency).First();
         
		public Currencies Get(Int64 ixCurrency)
        {
            Currencies currencies = _context.Currencies.AsNoTracking().Where(x => x.ixCurrency == ixCurrency).First();
            return currencies;
        }

        public IQueryable<Currencies> Index()
        {
            var currencies = _context.Currencies.AsNoTracking(); 
            return currencies;
        }

        public IQueryable<Currencies> IndexDb()
        {
            var currencies = _context.Currencies.AsNoTracking(); 
            return currencies;
        }
        public bool VerifyCurrencyUnique(Int64 ixCurrency, string sCurrency)
        {
            if (_context.Currencies.AsNoTracking().Where(x => x.sCurrency == sCurrency).Any() && ixCurrency == 0L) return false;
            else if (_context.Currencies.AsNoTracking().Where(x => x.sCurrency == sCurrency && x.ixCurrency != ixCurrency).Any() && ixCurrency != 0L) return false;
            else return true;
        }

        public List<string> VerifyCurrencyDeleteOK(Int64 ixCurrency, string sCurrency)
        {
            List<string> existInEntities = new List<string>();
           if (_contextInvoicePurchaseLineAmounts.InvoicePurchaseLineAmounts.AsNoTracking().Where(x => x.ixCurrency == ixCurrency).Any()) existInEntities.Add("InvoicePurchaseLineAmounts");
            if (_contextInvoicePurchaseLineTaxAmounts.InvoicePurchaseLineTaxAmounts.AsNoTracking().Where(x => x.ixCurrency == ixCurrency).Any()) existInEntities.Add("InvoicePurchaseLineTaxAmounts");

            return existInEntities;
        }


        public void RegisterCreate(CurrenciesPost currenciesPost)
		{
            _context.CurrenciesPost.Add(currenciesPost); 
        }

        public void RegisterEdit(CurrenciesPost currenciesPost)
        {
            _context.Entry(currenciesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(CurrenciesPost currenciesPost)
        {
            _context.CurrenciesPost.Remove(currenciesPost);
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
  

