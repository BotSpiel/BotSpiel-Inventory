using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class PurchaseLinesService : IPurchaseLinesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IPurchaseLinesRepository _purchaselinesRepository;

        public PurchaseLinesService(IPurchaseLinesRepository purchaselinesRepository)
        {
            _purchaselinesRepository = purchaselinesRepository;
        }

        public PurchaseLinesPost GetPost(Int64 ixPurchaseLine) => _purchaselinesRepository.GetPost(ixPurchaseLine);
        public PurchaseLines Get(Int64 ixPurchaseLine) => _purchaselinesRepository.Get(ixPurchaseLine);
        public IQueryable<PurchaseLines> Index() => _purchaselinesRepository.Index();
        public IQueryable<PurchaseLines> IndexDb() => _purchaselinesRepository.IndexDb();
       public IQueryable<Materials> selectMaterials() => _purchaselinesRepository.selectMaterials();
        public IQueryable<Purchases> selectPurchases() => _purchaselinesRepository.selectPurchases();
       public IQueryable<Materials> MaterialsDb() => _purchaselinesRepository.MaterialsDb();
        public IQueryable<Purchases> PurchasesDb() => _purchaselinesRepository.PurchasesDb();
        public bool VerifyPurchaseLineUnique(Int64 ixPurchaseLine, string sPurchaseLine) => _purchaselinesRepository.VerifyPurchaseLineUnique(ixPurchaseLine, sPurchaseLine);
        public List<string> VerifyPurchaseLineDeleteOK(Int64 ixPurchaseLine, string sPurchaseLine) => _purchaselinesRepository.VerifyPurchaseLineDeleteOK(ixPurchaseLine, sPurchaseLine);

        public Task<Int64> Create(PurchaseLinesPost purchaselinesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._purchaselinesRepository.RegisterCreate(purchaselinesPost);
            try
            {
                this._purchaselinesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._purchaselinesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(purchaselinesPost.ixPurchaseLine);

        }
        public Task Edit(PurchaseLinesPost purchaselinesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._purchaselinesRepository.RegisterEdit(purchaselinesPost);
            try
            {
                this._purchaselinesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._purchaselinesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(PurchaseLinesPost purchaselinesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._purchaselinesRepository.RegisterDelete(purchaselinesPost);
            try
            {
                this._purchaselinesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._purchaselinesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

