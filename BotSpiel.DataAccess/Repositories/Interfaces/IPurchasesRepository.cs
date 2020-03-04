using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IPurchasesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        PurchasesPost GetPost(Int64 ixPurchase);        
		Purchases Get(Int64 ixPurchase);
        IQueryable<Purchases> Index();
        IQueryable<Purchases> IndexDb();
       IQueryable<People> selectPeople();
        IQueryable<Companies> selectCompanies();
       IQueryable<People> PeopleDb();
        IQueryable<Companies> CompaniesDb();
       List<KeyValuePair<Int64?, string>> selectCompaniesNullable();
        bool VerifyPurchaseUnique(Int64 ixPurchase, string sPurchase);
        List<string> VerifyPurchaseDeleteOK(Int64 ixPurchase, string sPurchase);
        void RegisterCreate(PurchasesPost purchasesPost);
        void RegisterEdit(PurchasesPost purchasesPost);
        void RegisterDelete(PurchasesPost purchasesPost);
        void Rollback();
        void Commit();
    }
}
  

