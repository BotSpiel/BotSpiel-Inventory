using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IPurchaseLinesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        PurchaseLinesPost GetPost(Int64 ixPurchaseLine);        
		PurchaseLines Get(Int64 ixPurchaseLine);
        IQueryable<PurchaseLines> Index();
        IQueryable<PurchaseLines> IndexDb();
       IQueryable<Materials> selectMaterials();
        IQueryable<Purchases> selectPurchases();
       IQueryable<Materials> MaterialsDb();
        IQueryable<Purchases> PurchasesDb();
        bool VerifyPurchaseLineUnique(Int64 ixPurchaseLine, string sPurchaseLine);
        List<string> VerifyPurchaseLineDeleteOK(Int64 ixPurchaseLine, string sPurchaseLine);

        Task<Int64> Create(PurchaseLinesPost purchaselinesPost);
        Task Edit(PurchaseLinesPost purchaselinesPost);
        Task Delete(PurchaseLinesPost purchaselinesPost);
    }
}
  

