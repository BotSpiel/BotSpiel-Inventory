using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IOutboundOrderLinesInventoryAllocationRepository
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
        void RegisterCreate(OutboundOrderLinesInventoryAllocationPost outboundorderlinesinventoryallocationPost);
        void RegisterEdit(OutboundOrderLinesInventoryAllocationPost outboundorderlinesinventoryallocationPost);
        void RegisterDelete(OutboundOrderLinesInventoryAllocationPost outboundorderlinesinventoryallocationPost);
        void Rollback();
        void Commit();
    }
}
  

