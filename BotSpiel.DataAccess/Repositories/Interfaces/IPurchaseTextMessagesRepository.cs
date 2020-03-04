using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IPurchaseTextMessagesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        PurchaseTextMessagesPost GetPost(Int64 ixPurchaseTextMessage);        
		PurchaseTextMessages Get(Int64 ixPurchaseTextMessage);
        IQueryable<PurchaseTextMessages> Index();
        IQueryable<PurchaseTextMessages> IndexDb();
       IQueryable<Purchases> selectPurchases();
        IQueryable<SendTextMessages> selectSendTextMessages();
       IQueryable<Purchases> PurchasesDb();
        IQueryable<SendTextMessages> SendTextMessagesDb();
        bool VerifyPurchaseTextMessageUnique(Int64 ixPurchaseTextMessage, string sPurchaseTextMessage);
        List<string> VerifyPurchaseTextMessageDeleteOK(Int64 ixPurchaseTextMessage, string sPurchaseTextMessage);
        void RegisterCreate(PurchaseTextMessagesPost purchasetextmessagesPost);
        void RegisterEdit(PurchaseTextMessagesPost purchasetextmessagesPost);
        void RegisterDelete(PurchaseTextMessagesPost purchasetextmessagesPost);
        void Rollback();
        void Commit();
    }
}
  

