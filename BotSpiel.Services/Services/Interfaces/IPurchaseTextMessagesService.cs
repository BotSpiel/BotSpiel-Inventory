using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IPurchaseTextMessagesService
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

        Task<Int64> Create(PurchaseTextMessagesPost purchasetextmessagesPost);
        Task Edit(PurchaseTextMessagesPost purchasetextmessagesPost);
        Task Delete(PurchaseTextMessagesPost purchasetextmessagesPost);
    }
}
  

