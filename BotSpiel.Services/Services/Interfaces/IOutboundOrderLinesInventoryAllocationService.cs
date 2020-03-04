using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IOutboundOrderLinesInventoryAllocationService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        OutboundOrderLinesInventoryAllocationPost GetPost(Int64 ixOutboundOrderLineInventoryAllocation);        
		OutboundOrderLinesInventoryAllocation Get(Int64 ixOutboundOrderLineInventoryAllocation);
        IQueryable<OutboundOrderLinesInventoryAllocation> Index();
        IQueryable<OutboundOrderLinesInventoryAllocation> IndexDb();
        //Custom Code Start | Added Code Block 
        IQueryable<OutboundOrderLinesInventoryAllocationPost> IndexDbPost();
        //Custom Code End
        IQueryable<Statuses> selectStatuses();
        IQueryable<OutboundOrderLines> selectOutboundOrderLines();
       IQueryable<Statuses> StatusesDb();
        IQueryable<OutboundOrderLines> OutboundOrderLinesDb();
        bool VerifyOutboundOrderLineInventoryAllocationUnique(Int64 ixOutboundOrderLineInventoryAllocation, string sOutboundOrderLineInventoryAllocation);
        List<string> VerifyOutboundOrderLineInventoryAllocationDeleteOK(Int64 ixOutboundOrderLineInventoryAllocation, string sOutboundOrderLineInventoryAllocation);

        Task<Int64> Create(OutboundOrderLinesInventoryAllocationPost outboundorderlinesinventoryallocationPost);
        Task Edit(OutboundOrderLinesInventoryAllocationPost outboundorderlinesinventoryallocationPost);
        Task Delete(OutboundOrderLinesInventoryAllocationPost outboundorderlinesinventoryallocationPost);
    }
}
  

