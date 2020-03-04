using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class StatusesRepository : IStatusesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly StatusesDB _context;
       private readonly DocumentsDB _contextDocuments;
        private readonly HandlingUnitsDB _contextHandlingUnits;
        private readonly HandlingUnitsShippingDB _contextHandlingUnitsShipping;
        private readonly InboundOrderLinesDB _contextInboundOrderLines;
        private readonly InboundOrdersDB _contextInboundOrders;
        private readonly InventoryUnitsDB _contextInventoryUnits;
        private readonly InventoryUnitTransactionsDB _contextInventoryUnitTransactions;
        private readonly MoveQueuesDB _contextMoveQueues;
        private readonly OutboundCarrierManifestPickupsDB _contextOutboundCarrierManifestPickups;
        private readonly OutboundCarrierManifestsDB _contextOutboundCarrierManifests;
        private readonly OutboundOrderLinePackingDB _contextOutboundOrderLinePacking;
        private readonly OutboundOrderLinesDB _contextOutboundOrderLines;
        private readonly OutboundOrderLinesInventoryAllocationDB _contextOutboundOrderLinesInventoryAllocation;
        private readonly OutboundOrdersDB _contextOutboundOrders;
        private readonly OutboundShipmentsDB _contextOutboundShipments;
        private readonly PickBatchesDB _contextPickBatches;
        private readonly PickBatchPickingDB _contextPickBatchPicking;
        private readonly ReceivingDB _contextReceiving;
  
        public StatusesRepository(StatusesDB context, DocumentsDB contextDocuments, HandlingUnitsDB contextHandlingUnits, HandlingUnitsShippingDB contextHandlingUnitsShipping, InboundOrderLinesDB contextInboundOrderLines, InboundOrdersDB contextInboundOrders, InventoryUnitsDB contextInventoryUnits, InventoryUnitTransactionsDB contextInventoryUnitTransactions, MoveQueuesDB contextMoveQueues, OutboundCarrierManifestPickupsDB contextOutboundCarrierManifestPickups, OutboundCarrierManifestsDB contextOutboundCarrierManifests, OutboundOrderLinePackingDB contextOutboundOrderLinePacking, OutboundOrderLinesDB contextOutboundOrderLines, OutboundOrderLinesInventoryAllocationDB contextOutboundOrderLinesInventoryAllocation, OutboundOrdersDB contextOutboundOrders, OutboundShipmentsDB contextOutboundShipments, PickBatchesDB contextPickBatches, PickBatchPickingDB contextPickBatchPicking, ReceivingDB contextReceiving)
        {
            _context = context;
           _contextDocuments = contextDocuments;
            _contextHandlingUnits = contextHandlingUnits;
            _contextHandlingUnitsShipping = contextHandlingUnitsShipping;
            _contextInboundOrderLines = contextInboundOrderLines;
            _contextInboundOrders = contextInboundOrders;
            _contextInventoryUnits = contextInventoryUnits;
            _contextInventoryUnitTransactions = contextInventoryUnitTransactions;
            _contextMoveQueues = contextMoveQueues;
            _contextOutboundCarrierManifestPickups = contextOutboundCarrierManifestPickups;
            _contextOutboundCarrierManifests = contextOutboundCarrierManifests;
            _contextOutboundOrderLinePacking = contextOutboundOrderLinePacking;
            _contextOutboundOrderLines = contextOutboundOrderLines;
            _contextOutboundOrderLinesInventoryAllocation = contextOutboundOrderLinesInventoryAllocation;
            _contextOutboundOrders = contextOutboundOrders;
            _contextOutboundShipments = contextOutboundShipments;
            _contextPickBatches = contextPickBatches;
            _contextPickBatchPicking = contextPickBatchPicking;
            _contextReceiving = contextReceiving;
  
        }

        public StatusesPost GetPost(Int64 ixStatus) => _context.StatusesPost.AsNoTracking().Where(x => x.ixStatus == ixStatus).First();
         
		public Statuses Get(Int64 ixStatus)
        {
            Statuses statuses = _context.Statuses.AsNoTracking().Where(x => x.ixStatus == ixStatus).First();
            return statuses;
        }

        public IQueryable<Statuses> Index()
        {
            var statuses = _context.Statuses.AsNoTracking(); 
            return statuses;
        }

        public IQueryable<Statuses> IndexDb()
        {
            var statuses = _context.Statuses.AsNoTracking(); 
            return statuses;
        }
        public bool VerifyStatusUnique(Int64 ixStatus, string sStatus)
        {
            if (_context.Statuses.AsNoTracking().Where(x => x.sStatus == sStatus).Any() && ixStatus == 0L) return false;
            else if (_context.Statuses.AsNoTracking().Where(x => x.sStatus == sStatus && x.ixStatus != ixStatus).Any() && ixStatus != 0L) return false;
            else return true;
        }

        public List<string> VerifyStatusDeleteOK(Int64 ixStatus, string sStatus)
        {
            List<string> existInEntities = new List<string>();
           if (_contextDocuments.Documents.AsNoTracking().Where(x => x.ixStatus == ixStatus).Any()) existInEntities.Add("Documents");
            if (_contextHandlingUnits.HandlingUnits.AsNoTracking().Where(x => x.ixStatus == ixStatus).Any()) existInEntities.Add("HandlingUnits");
            if (_contextHandlingUnitsShipping.HandlingUnitsShipping.AsNoTracking().Where(x => x.ixStatus == ixStatus).Any()) existInEntities.Add("HandlingUnitsShipping");
            if (_contextInboundOrderLines.InboundOrderLines.AsNoTracking().Where(x => x.ixStatus == ixStatus).Any()) existInEntities.Add("InboundOrderLines");
            if (_contextInboundOrders.InboundOrders.AsNoTracking().Where(x => x.ixStatus == ixStatus).Any()) existInEntities.Add("InboundOrders");
            if (_contextInventoryUnits.InventoryUnits.AsNoTracking().Where(x => x.ixStatus == ixStatus).Any()) existInEntities.Add("InventoryUnits");
            if (_contextInventoryUnitTransactions.InventoryUnitTransactions.AsNoTracking().Where(x => x.ixStatusAfter == ixStatus).Any()) existInEntities.Add("InventoryUnitTransactions");
            if (_contextInventoryUnitTransactions.InventoryUnitTransactions.AsNoTracking().Where(x => x.ixStatusBefore == ixStatus).Any()) existInEntities.Add("InventoryUnitTransactions");
            if (_contextMoveQueues.MoveQueues.AsNoTracking().Where(x => x.ixStatus == ixStatus).Any()) existInEntities.Add("MoveQueues");
            if (_contextOutboundCarrierManifestPickups.OutboundCarrierManifestPickups.AsNoTracking().Where(x => x.ixStatus == ixStatus).Any()) existInEntities.Add("OutboundCarrierManifestPickups");
            if (_contextOutboundCarrierManifests.OutboundCarrierManifests.AsNoTracking().Where(x => x.ixStatus == ixStatus).Any()) existInEntities.Add("OutboundCarrierManifests");
            if (_contextOutboundOrderLinePacking.OutboundOrderLinePacking.AsNoTracking().Where(x => x.ixStatus == ixStatus).Any()) existInEntities.Add("OutboundOrderLinePacking");
            if (_contextOutboundOrderLines.OutboundOrderLines.AsNoTracking().Where(x => x.ixStatus == ixStatus).Any()) existInEntities.Add("OutboundOrderLines");
            if (_contextOutboundOrderLinesInventoryAllocation.OutboundOrderLinesInventoryAllocation.AsNoTracking().Where(x => x.ixStatus == ixStatus).Any()) existInEntities.Add("OutboundOrderLinesInventoryAllocation");
            if (_contextOutboundOrders.OutboundOrders.AsNoTracking().Where(x => x.ixStatus == ixStatus).Any()) existInEntities.Add("OutboundOrders");
            if (_contextOutboundShipments.OutboundShipments.AsNoTracking().Where(x => x.ixStatus == ixStatus).Any()) existInEntities.Add("OutboundShipments");
            if (_contextPickBatches.PickBatches.AsNoTracking().Where(x => x.ixStatus == ixStatus).Any()) existInEntities.Add("PickBatches");
            if (_contextReceiving.Receiving.AsNoTracking().Where(x => x.ixStatus == ixStatus).Any()) existInEntities.Add("Receiving");

            return existInEntities;
        }


        public void RegisterCreate(StatusesPost statusesPost)
		{
            _context.StatusesPost.Add(statusesPost); 
        }

        public void RegisterEdit(StatusesPost statusesPost)
        {
            _context.Entry(statusesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(StatusesPost statusesPost)
        {
            _context.StatusesPost.Remove(statusesPost);
        }

        public void Rollback()
        {
            _context.Rollback();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

    }
}
  

