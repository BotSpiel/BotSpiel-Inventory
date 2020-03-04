using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class TaxesRepository : ITaxesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly TaxesDB _context;
       private readonly InvoicePurchaseLineTaxAmountsDB _contextInvoicePurchaseLineTaxAmounts;
  
        public TaxesRepository(TaxesDB context, InvoicePurchaseLineTaxAmountsDB contextInvoicePurchaseLineTaxAmounts)
        {
            _context = context;
           _contextInvoicePurchaseLineTaxAmounts = contextInvoicePurchaseLineTaxAmounts;
  
        }

        public TaxesPost GetPost(Int64 ixTax) => _context.TaxesPost.AsNoTracking().Where(x => x.ixTax == ixTax).First();
         
		public Taxes Get(Int64 ixTax)
        {
            Taxes taxes = _context.Taxes.AsNoTracking().Where(x => x.ixTax == ixTax).First();
            taxes.Countries = _context.Countries.Find(taxes.ixCountry);
            taxes.CountrySubDivisions = _context.CountrySubDivisions.Find(taxes.ixCountrySubDivision);

            return taxes;
        }

        public IQueryable<Taxes> Index()
        {
            var taxes = _context.Taxes.Include(a => a.Countries).Include(a => a.CountrySubDivisions).AsNoTracking(); 
            return taxes;
        }

        public IQueryable<Taxes> IndexDb()
        {
            var taxes = _context.Taxes.Include(a => a.Countries).Include(a => a.CountrySubDivisions).AsNoTracking(); 
            return taxes;
        }
       public IQueryable<Countries> selectCountries()
        {
            List<Countries> countries = new List<Countries>();
            _context.Countries.Include(a => a.PlanetSubRegions).AsNoTracking()
                .ToList()
                .ForEach(x => countries.Add(x));
            return countries.AsQueryable();
        }
        public IQueryable<CountrySubDivisions> selectCountrySubDivisions()
        {
            List<CountrySubDivisions> countrysubdivisions = new List<CountrySubDivisions>();
            _context.CountrySubDivisions.Include(a => a.Countries).AsNoTracking()
                .ToList()
                .ForEach(x => countrysubdivisions.Add(x));
            return countrysubdivisions.AsQueryable();
        }
       public IQueryable<Countries> CountriesDb()
        {
            List<Countries> countries = new List<Countries>();
            _context.Countries.Include(a => a.PlanetSubRegions).AsNoTracking()
                .ToList()
                .ForEach(x => countries.Add(x));
            return countries.AsQueryable();
        }
        public IQueryable<CountrySubDivisions> CountrySubDivisionsDb()
        {
            List<CountrySubDivisions> countrysubdivisions = new List<CountrySubDivisions>();
            _context.CountrySubDivisions.Include(a => a.Countries).AsNoTracking()
                .ToList()
                .ForEach(x => countrysubdivisions.Add(x));
            return countrysubdivisions.AsQueryable();
        }
        public bool VerifyTaxUnique(Int64 ixTax, string sTax)
        {
            if (_context.Taxes.AsNoTracking().Where(x => x.sTax == sTax).Any() && ixTax == 0L) return false;
            else if (_context.Taxes.AsNoTracking().Where(x => x.sTax == sTax && x.ixTax != ixTax).Any() && ixTax != 0L) return false;
            else return true;
        }

        public List<string> VerifyTaxDeleteOK(Int64 ixTax, string sTax)
        {
            List<string> existInEntities = new List<string>();
           if (_contextInvoicePurchaseLineTaxAmounts.InvoicePurchaseLineTaxAmounts.AsNoTracking().Where(x => x.ixTax == ixTax).Any()) existInEntities.Add("InvoicePurchaseLineTaxAmounts");

            return existInEntities;
        }


        public void RegisterCreate(TaxesPost taxesPost)
		{
            _context.TaxesPost.Add(taxesPost); 
        }

        public void RegisterEdit(TaxesPost taxesPost)
        {
            _context.Entry(taxesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(TaxesPost taxesPost)
        {
            _context.TaxesPost.Remove(taxesPost);
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
  

