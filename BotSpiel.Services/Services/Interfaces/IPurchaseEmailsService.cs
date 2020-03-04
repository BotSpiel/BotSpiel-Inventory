using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IPurchaseEmailsService
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

        Task<Int64> Create(PurchaseEmailsPost purchaseemailsPost);
        Task Edit(PurchaseEmailsPost purchaseemailsPost);
        Task Delete(PurchaseEmailsPost purchaseemailsPost);
    }
}
  

