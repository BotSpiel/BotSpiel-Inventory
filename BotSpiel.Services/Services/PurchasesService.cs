using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class PurchasesService : IPurchasesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IPurchasesRepository _purchasesRepository;

        public PurchasesService(IPurchasesRepository purchasesRepository)
        {
            _purchasesRepository = purchasesRepository;
        }

        public PurchasesPost GetPost(Int64 ixPurchase) => _purchasesRepository.GetPost(ixPurchase);
        public Purchases Get(Int64 ixPurchase) => _purchasesRepository.Get(ixPurchase);
        public IQueryable<Purchases> Index() => _purchasesRepository.Index();
        public IQueryable<Purchases> IndexDb() => _purchasesRepository.IndexDb();
       public IQueryable<People> selectPeople() => _purchasesRepository.selectPeople();
        public IQueryable<Companies> selectCompanies() => _purchasesRepository.selectCompanies();
       public IQueryable<People> PeopleDb() => _purchasesRepository.PeopleDb();
        public IQueryable<Companies> CompaniesDb() => _purchasesRepository.CompaniesDb();
       public List<KeyValuePair<Int64?, string>> selectCompaniesNullable() => _purchasesRepository.selectCompaniesNullable();
        public bool VerifyPurchaseUnique(Int64 ixPurchase, string sPurchase) => _purchasesRepository.VerifyPurchaseUnique(ixPurchase, sPurchase);
        public List<string> VerifyPurchaseDeleteOK(Int64 ixPurchase, string sPurchase) => _purchasesRepository.VerifyPurchaseDeleteOK(ixPurchase, sPurchase);

        public Task<Int64> Create(PurchasesPost purchasesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._purchasesRepository.RegisterCreate(purchasesPost);
            try
            {
                this._purchasesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._purchasesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(purchasesPost.ixPurchase);

        }
        public Task Edit(PurchasesPost purchasesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._purchasesRepository.RegisterEdit(purchasesPost);
            try
            {
                this._purchasesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._purchasesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(PurchasesPost purchasesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._purchasesRepository.RegisterDelete(purchasesPost);
            try
            {
                this._purchasesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._purchasesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

