using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class PurchaseLinesRepository : IPurchaseLinesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly PurchaseLinesDB _context;
       private readonly InvoicePurchaseLineAmountsDB _contextInvoicePurchaseLineAmounts;
  
        public PurchaseLinesRepository(PurchaseLinesDB context, InvoicePurchaseLineAmountsDB contextInvoicePurchaseLineAmounts)
        {
            _context = context;
           _contextInvoicePurchaseLineAmounts = contextInvoicePurchaseLineAmounts;
  
        }

        public PurchaseLinesPost GetPost(Int64 ixPurchaseLine) => _context.PurchaseLinesPost.AsNoTracking().Where(x => x.ixPurchaseLine == ixPurchaseLine).First();
         
		public PurchaseLines Get(Int64 ixPurchaseLine)
        {
            PurchaseLines purchaselines = _context.PurchaseLines.AsNoTracking().Where(x => x.ixPurchaseLine == ixPurchaseLine).First();
            purchaselines.Materials = _context.Materials.Find(purchaselines.ixMaterial);
            purchaselines.Purchases = _context.Purchases.Find(purchaselines.ixPurchase);

            return purchaselines;
        }

        public IQueryable<PurchaseLines> Index()
        {
            var purchaselines = _context.PurchaseLines.Include(a => a.Purchases).Include(a => a.Materials).AsNoTracking(); 
            return purchaselines;
        }

        public IQueryable<PurchaseLines> IndexDb()
        {
            var purchaselines = _context.PurchaseLines.Include(a => a.Purchases).Include(a => a.Materials).AsNoTracking(); 
            return purchaselines;
        }
       public IQueryable<Materials> selectMaterials()
        {
            List<Materials> materials = new List<Materials>();
            _context.Materials.Include(a => a.MaterialTypes).Include(a => a.UnitsOfMeasurementFKDiffBaseUnit).Include(a => a.UnitsOfMeasurementFKDiffDensityUnit).Include(a => a.UnitsOfMeasurementFKDiffHeightUnit).Include(a => a.UnitsOfMeasurementFKDiffLengthUnit).Include(a => a.UnitsOfMeasurementFKDiffShelflifeUnit).Include(a => a.UnitsOfMeasurementFKDiffWeightUnit).Include(a => a.UnitsOfMeasurementFKDiffWidthUnit).AsNoTracking()
                .ToList()
                .ForEach(x => materials.Add(x));
            return materials.AsQueryable();
        }
        public IQueryable<Purchases> selectPurchases()
        {
            List<Purchases> purchases = new List<Purchases>();
            _context.Purchases.Include(a => a.Companies).Include(a => a.People).AsNoTracking()
                .ToList()
                .ForEach(x => purchases.Add(x));
            return purchases.AsQueryable();
        }
       public IQueryable<Materials> MaterialsDb()
        {
            List<Materials> materials = new List<Materials>();
            _context.Materials.Include(a => a.MaterialTypes).Include(a => a.UnitsOfMeasurementFKDiffBaseUnit).Include(a => a.UnitsOfMeasurementFKDiffDensityUnit).Include(a => a.UnitsOfMeasurementFKDiffHeightUnit).Include(a => a.UnitsOfMeasurementFKDiffLengthUnit).Include(a => a.UnitsOfMeasurementFKDiffShelflifeUnit).Include(a => a.UnitsOfMeasurementFKDiffWeightUnit).Include(a => a.UnitsOfMeasurementFKDiffWidthUnit).AsNoTracking()
                .ToList()
                .ForEach(x => materials.Add(x));
            return materials.AsQueryable();
        }
        public IQueryable<Purchases> PurchasesDb()
        {
            List<Purchases> purchases = new List<Purchases>();
            _context.Purchases.Include(a => a.Companies).Include(a => a.People).AsNoTracking()
                .ToList()
                .ForEach(x => purchases.Add(x));
            return purchases.AsQueryable();
        }
        public bool VerifyPurchaseLineUnique(Int64 ixPurchaseLine, string sPurchaseLine)
        {
            if (_context.PurchaseLines.AsNoTracking().Where(x => x.sPurchaseLine == sPurchaseLine).Any() && ixPurchaseLine == 0L) return false;
            else if (_context.PurchaseLines.AsNoTracking().Where(x => x.sPurchaseLine == sPurchaseLine && x.ixPurchaseLine != ixPurchaseLine).Any() && ixPurchaseLine != 0L) return false;
            else return true;
        }

        public List<string> VerifyPurchaseLineDeleteOK(Int64 ixPurchaseLine, string sPurchaseLine)
        {
            List<string> existInEntities = new List<string>();
           if (_contextInvoicePurchaseLineAmounts.InvoicePurchaseLineAmounts.AsNoTracking().Where(x => x.ixPurchaseLine == ixPurchaseLine).Any()) existInEntities.Add("InvoicePurchaseLineAmounts");

            return existInEntities;
        }


        public void RegisterCreate(PurchaseLinesPost purchaselinesPost)
		{
            _context.PurchaseLinesPost.Add(purchaselinesPost); 
        }

        public void RegisterEdit(PurchaseLinesPost purchaselinesPost)
        {
            _context.Entry(purchaselinesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(PurchaseLinesPost purchaselinesPost)
        {
            _context.PurchaseLinesPost.Remove(purchaselinesPost);
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
  

