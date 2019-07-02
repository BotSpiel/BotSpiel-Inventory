using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IReceivingRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        ReceivingPost GetPost(Int64 ixReceipt);        
		Receiving Get(Int64 ixReceipt);
        IQueryable<Receiving> Index();
       IQueryable<Statuses> selectStatuses();
        IQueryable<Materials> selectMaterials();
        IQueryable<HandlingUnits> selectHandlingUnits();
        IQueryable<HandlingUnitTypes> selectHandlingUnitTypes();
        IQueryable<InventoryLocations> selectInventoryLocations();
        IQueryable<MaterialHandlingUnitConfigurations> selectMaterialHandlingUnitConfigurations();
        IQueryable<InboundOrders> selectInboundOrders();
       List<KeyValuePair<Int64?, string>> selectHandlingUnitTypesNullable();
        List<KeyValuePair<Int64?, string>> selectMaterialHandlingUnitConfigurationsNullable();
        bool VerifyReceiptUnique(Int64 ixReceipt, string sReceipt);
        List<string> VerifyReceiptDeleteOK(Int64 ixReceipt, string sReceipt);
        void RegisterCreate(ReceivingPost receivingPost);
        void RegisterEdit(ReceivingPost receivingPost);
        void RegisterDelete(ReceivingPost receivingPost);
        void Rollback();
        void Commit();
    }
}
  

