using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IPurchaseLinesRepository
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
        void RegisterCreate(PurchaseLinesPost purchaselinesPost);
        void RegisterEdit(PurchaseLinesPost purchaselinesPost);
        void RegisterDelete(PurchaseLinesPost purchaselinesPost);
        void Rollback();
        void Commit();
    }
}
  

