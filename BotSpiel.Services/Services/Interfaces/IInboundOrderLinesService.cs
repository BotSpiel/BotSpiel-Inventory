using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IInboundOrderLinesService
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

        Task<Int64> Create(InboundOrderLinesPost inboundorderlinesPost);
        Task Edit(InboundOrderLinesPost inboundorderlinesPost);
        Task Delete(InboundOrderLinesPost inboundorderlinesPost);
    }
}
  

