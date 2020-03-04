using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IInboundOrderLinesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        InboundOrderLinesPost GetPost(Int64 ixInboundOrderLine);        
		InboundOrderLines Get(Int64 ixInboundOrderLine);
        IQueryable<InboundOrderLines> Index();
        IQueryable<InboundOrderLines> IndexDb();
       IQueryable<Statuses> selectStatuses();
        IQueryable<Materials> selectMaterials();
        IQueryable<HandlingUnitTypes> selectHandlingUnitTypes();
        IQueryable<MaterialHandlingUnitConfigurations> selectMaterialHandlingUnitConfigurations();
        IQueryable<InboundOrders> selectInboundOrders();
       IQueryable<Statuses> StatusesDb();
        IQueryable<Materials> MaterialsDb();
        IQueryable<HandlingUnitTypes> HandlingUnitTypesDb();
        IQueryable<MaterialHandlingUnitConfigurations> MaterialHandlingUnitConfigurationsDb();
        IQueryable<InboundOrders> InboundOrdersDb();
       List<KeyValuePair<Int64?, string>> selectHandlingUnitTypesNullable();
        List<KeyValuePair<Int64?, string>> selectMaterialHandlingUnitConfigurationsNullable();
        bool VerifyInboundOrderLineUnique(Int64 ixInboundOrderLine, string sInboundOrderLine);
        List<string> VerifyInboundOrderLineDeleteOK(Int64 ixInboundOrderLine, string sInboundOrderLine);
        void RegisterCreate(InboundOrderLinesPost inboundorderlinesPost);
        void RegisterEdit(InboundOrderLinesPost inboundorderlinesPost);
        void RegisterDelete(InboundOrderLinesPost inboundorderlinesPost);
        void Rollback();
        void Commit();
    }
}
  

