using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IPurchaseEmailsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        PurchaseEmailsPost GetPost(Int64 ixPurchaseEmail);        
		PurchaseEmails Get(Int64 ixPurchaseEmail);
        IQueryable<PurchaseEmails> Index();
        IQueryable<PurchaseEmails> IndexDb();
       IQueryable<Purchases> selectPurchases();
        IQueryable<SendEmails> selectSendEmails();
       IQueryable<Purchases> PurchasesDb();
        IQueryable<SendEmails> SendEmailsDb();
        bool VerifyPurchaseEmailUnique(Int64 ixPurchaseEmail, string sPurchaseEmail);
        List<string> VerifyPurchaseEmailDeleteOK(Int64 ixPurchaseEmail, string sPurchaseEmail);
        void RegisterCreate(PurchaseEmailsPost purchaseemailsPost);
        void RegisterEdit(PurchaseEmailsPost purchaseemailsPost);
        void RegisterDelete(PurchaseEmailsPost purchaseemailsPost);
        void Rollback();
        void Commit();
    }
}
  

