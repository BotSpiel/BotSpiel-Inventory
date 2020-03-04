using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IPurchasesService
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

        Task<Int64> Create(PurchasesPost purchasesPost);
        Task Edit(PurchasesPost purchasesPost);
        Task Delete(PurchasesPost purchasesPost);
    }
}
  

