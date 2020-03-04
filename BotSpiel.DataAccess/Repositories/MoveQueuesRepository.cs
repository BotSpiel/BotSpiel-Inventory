using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class MoveQueuesRepository : IMoveQueuesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly MoveQueuesDB _context;
  
        public MoveQueuesRepository(MoveQueuesDB context)
        {
            _context = context;
  
        }

        public MoveQueuesPost GetPost(Int64 ixMoveQueue) => _context.MoveQueuesPost.AsNoTracking().Where(x => x.ixMoveQueue == ixMoveQueue).First();
         
		public MoveQueues Get(Int64 ixMoveQueue)
        {
            MoveQueues movequeues = _context.MoveQueues.AsNoTracking().Where(x => x.ixMoveQueue == ixMoveQueue).First();
            if (movequeues.ixSourceHandlingUnit != null)
        {
            movequeues.HandlingUnitsFKDiffSourceHandlingUnit = _context.HandlingUnits.Find(movequeues.ixSourceHandlingUnit);
        }
            if (movequeues.ixTargetHandlingUnit != null)
        {
            movequeues.HandlingUnitsFKDiffTargetHandlingUnit = _context.HandlingUnits.Find(movequeues.ixTargetHandlingUnit);
        }
            if (movequeues.ixInboundOrderLine != null)
        {
            movequeues.InboundOrderLines = _context.InboundOrderLines.Find(movequeues.ixInboundOrderLine);
        }
            if (movequeues.ixSourceInventoryLocation != null)
        {
            movequeues.InventoryLocationsFKDiffSourceInventoryLocation = _context.InventoryLocations.Find(movequeues.ixSourceInventoryLocation);
        }
            if (movequeues.ixTargetInventoryLocation != null)
        {
            movequeues.InventoryLocationsFKDiffTargetInventoryLocation = _context.InventoryLocations.Find(movequeues.ixTargetInventoryLocation);
        }
            if (movequeues.ixSourceInventoryUnit != null)
        {
            movequeues.InventoryUnitsFKDiffSourceInventoryUnit = _context.InventoryUnits.Find(movequeues.ixSourceInventoryUnit);
        }
            if (movequeues.ixTargetInventoryUnit != null)
        {
            movequeues.InventoryUnitsFKDiffTargetInventoryUnit = _context.InventoryUnits.Find(movequeues.ixTargetInventoryUnit);
        }
            movequeues.MoveQueueContexts = _context.MoveQueueContexts.Find(movequeues.ixMoveQueueContext);
            movequeues.MoveQueueTypes = _context.MoveQueueTypes.Find(movequeues.ixMoveQueueType);
            if (movequeues.ixOutboundOrderLine != null)
        {
            movequeues.OutboundOrderLines = _context.OutboundOrderLines.Find(movequeues.ixOutboundOrderLine);
        }
            if (movequeues.ixPickBatch != null)
        {
            movequeues.PickBatches = _context.PickBatches.Find(movequeues.ixPickBatch);
        }
            movequeues.Statuses = _context.Statuses.Find(movequeues.ixStatus);

            return movequeues;
        }

        public IQueryable<MoveQueues> Index()
        {
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //var movequeues = _context.MoveQueues.Include(a => a.MoveQueueTypes).Include(a => a.MoveQueueContexts).Include(a => a.Statuses).AsNoTracking();
            //Replaced Code Block End
            var movequeues = _context.MoveQueues.OrderByDescending(a => a.ixMoveQueue).Include(a => a.MoveQueueTypes).Include(a => a.MoveQueueContexts).Include(a => a.Statuses).AsNoTracking();
            //Custom Code End
            return movequeues;
        }

        public IQueryable<MoveQueues> IndexDb()
        {
            var movequeues = _context.MoveQueues.Include(a => a.MoveQueueTypes).Include(a => a.MoveQueueContexts).Include(a => a.Statuses).AsNoTracking(); 
            return movequeues;
        }
       public IQueryable<HandlingUnits> selectHandlingUnits()
        {
            List<HandlingUnits> handlingunits = new List<HandlingUnits>();
            _context.HandlingUnits.Include(a => a.HandlingUnitsFKDiffParentHandlingUnit).Include(a => a.HandlingUnitTypes).Include(a => a.MaterialHandlingUnitConfigurations).Include(a => a.MaterialsFKDiffPackingMaterial).Include(a => a.Statuses).Include(a => a.UnitsOfMeasurementFKDiffHeightUnit).Include(a => a.UnitsOfMeasurementFKDiffLengthUnit).Include(a => a.UnitsOfMeasurementFKDiffWeightUnit).Include(a => a.UnitsOfMeasurementFKDiffWidthUnit).AsNoTracking()
                .ToList()
                .ForEach(x => handlingunits.Add(x));
            return handlingunits.AsQueryable();
        }
        public IQueryable<InboundOrderLines> selectInboundOrderLines()
        {
            List<InboundOrderLines> inboundorderlines = new List<InboundOrderLines>();
            _context.InboundOrderLines.Include(a => a.HandlingUnitTypes).Include(a => a.InboundOrders).Include(a => a.MaterialHandlingUnitConfigurations).Include(a => a.Materials).Include(a => a.Statuses).AsNoTracking()
                .ToList()
                .ForEach(x => inboundorderlines.Add(x));
            return inboundorderlines.AsQueryable();
        }
        public IQueryable<InventoryLocations> selectInventoryLocations()
        {
            List<InventoryLocations> inventorylocations = new List<InventoryLocations>();
            _context.InventoryLocations.Include(a => a.Companies).Include(a => a.FacilityAisleFaces).Include(a => a.FacilityFloors).Include(a => a.FacilityWorkAreas).Include(a => a.FacilityZones).Include(a => a.InventoryLocationSizes).Include(a => a.LocationFunctions).Include(a => a.UnitsOfMeasurementFKDiffXOffsetUnit).Include(a => a.UnitsOfMeasurementFKDiffYOffsetUnit).Include(a => a.UnitsOfMeasurementFKDiffZOffsetUnit).AsNoTracking()
                .ToList()
                .ForEach(x => inventorylocations.Add(x));
            return inventorylocations.AsQueryable();
        }
        public IQueryable<InventoryUnits> selectInventoryUnits()
        {
            List<InventoryUnits> inventoryunits = new List<InventoryUnits>();
            _context.InventoryUnits.Include(a => a.Companies).Include(a => a.Facilities).Include(a => a.HandlingUnits).Include(a => a.InventoryLocations).Include(a => a.InventoryStates).Include(a => a.Materials).Include(a => a.Statuses).AsNoTracking()
                .ToList()
                .ForEach(x => inventoryunits.Add(x));
            return inventoryunits.AsQueryable();
        }
        public IQueryable<MoveQueueContexts> selectMoveQueueContexts()
        {
            List<MoveQueueContexts> movequeuecontexts = new List<MoveQueueContexts>();
            _context.MoveQueueContexts.AsNoTracking()
                .ToList()
                .ForEach(x => movequeuecontexts.Add(x));
            return movequeuecontexts.AsQueryable();
        }
        public IQueryable<MoveQueueTypes> selectMoveQueueTypes()
        {
            List<MoveQueueTypes> movequeuetypes = new List<MoveQueueTypes>();
            _context.MoveQueueTypes.AsNoTracking()
                .ToList()
                .ForEach(x => movequeuetypes.Add(x));
            return movequeuetypes.AsQueryable();
        }
        public IQueryable<OutboundOrderLines> selectOutboundOrderLines()
        {
            List<OutboundOrderLines> outboundorderlines = new List<OutboundOrderLines>();
            _context.OutboundOrderLines.Include(a => a.Materials).Include(a => a.OutboundOrders).Include(a => a.Statuses).AsNoTracking()
                .ToList()
                .ForEach(x => outboundorderlines.Add(x));
            return outboundorderlines.AsQueryable();
        }
        public IQueryable<PickBatches> selectPickBatches()
        {
            List<PickBatches> pickbatches = new List<PickBatches>();
            _context.PickBatches.Include(a => a.PickBatchTypes).Include(a => a.Statuses).AsNoTracking()
                .ToList()
                .ForEach(x => pickbatches.Add(x));
            return pickbatches.AsQueryable();
        }
        public IQueryable<Statuses> selectStatuses()
        {
            List<Statuses> statuses = new List<Statuses>();
            _context.Statuses.AsNoTracking()
                .ToList()
                .ForEach(x => statuses.Add(x));
            return statuses.AsQueryable();
        }
       public IQueryable<HandlingUnits> HandlingUnitsDb()
        {
            List<HandlingUnits> handlingunits = new List<HandlingUnits>();
            _context.HandlingUnits.Include(a => a.HandlingUnitsFKDiffParentHandlingUnit).Include(a => a.HandlingUnitTypes).Include(a => a.MaterialHandlingUnitConfigurations).Include(a => a.MaterialsFKDiffPackingMaterial).Include(a => a.Statuses).Include(a => a.UnitsOfMeasurementFKDiffHeightUnit).Include(a => a.UnitsOfMeasurementFKDiffLengthUnit).Include(a => a.UnitsOfMeasurementFKDiffWeightUnit).Include(a => a.UnitsOfMeasurementFKDiffWidthUnit).AsNoTracking()
                .ToList()
                .ForEach(x => handlingunits.Add(x));
            return handlingunits.AsQueryable();
        }
        public IQueryable<InboundOrderLines> InboundOrderLinesDb()
        {
            List<InboundOrderLines> inboundorderlines = new List<InboundOrderLines>();
            _context.InboundOrderLines.Include(a => a.HandlingUnitTypes).Include(a => a.InboundOrders).Include(a => a.MaterialHandlingUnitConfigurations).Include(a => a.Materials).Include(a => a.Statuses).AsNoTracking()
                .ToList()
                .ForEach(x => inboundorderlines.Add(x));
            return inboundorderlines.AsQueryable();
        }
        public IQueryable<InventoryLocations> InventoryLocationsDb()
        {
            List<InventoryLocations> inventorylocations = new List<InventoryLocations>();
            _context.InventoryLocations.Include(a => a.Companies).Include(a => a.FacilityAisleFaces).Include(a => a.FacilityFloors).Include(a => a.FacilityWorkAreas).Include(a => a.FacilityZones).Include(a => a.InventoryLocationSizes).Include(a => a.LocationFunctions).Include(a => a.UnitsOfMeasurementFKDiffXOffsetUnit).Include(a => a.UnitsOfMeasurementFKDiffYOffsetUnit).Include(a => a.UnitsOfMeasurementFKDiffZOffsetUnit).AsNoTracking()
                .ToList()
                .ForEach(x => inventorylocations.Add(x));
            return inventorylocations.AsQueryable();
        }
        public IQueryable<InventoryUnits> InventoryUnitsDb()
        {
            List<InventoryUnits> inventoryunits = new List<InventoryUnits>();
            _context.InventoryUnits.Include(a => a.Companies).Include(a => a.Facilities).Include(a => a.HandlingUnits).Include(a => a.InventoryLocations).Include(a => a.InventoryStates).Include(a => a.Materials).Include(a => a.Statuses).AsNoTracking()
                .ToList()
                .ForEach(x => inventoryunits.Add(x));
            return inventoryunits.AsQueryable();
        }
        public IQueryable<MoveQueueContexts> MoveQueueContextsDb()
        {
            List<MoveQueueContexts> movequeuecontexts = new List<MoveQueueContexts>();
            _context.MoveQueueContexts.AsNoTracking()
                .ToList()
                .ForEach(x => movequeuecontexts.Add(x));
            return movequeuecontexts.AsQueryable();
        }
        public IQueryable<MoveQueueTypes> MoveQueueTypesDb()
        {
            List<MoveQueueTypes> movequeuetypes = new List<MoveQueueTypes>();
            _context.MoveQueueTypes.AsNoTracking()
                .ToList()
                .ForEach(x => movequeuetypes.Add(x));
            return movequeuetypes.AsQueryable();
        }
        public IQueryable<OutboundOrderLines> OutboundOrderLinesDb()
        {
            List<OutboundOrderLines> outboundorderlines = new List<OutboundOrderLines>();
            _context.OutboundOrderLines.Include(a => a.Materials).Include(a => a.OutboundOrders).Include(a => a.Statuses).AsNoTracking()
                .ToList()
                .ForEach(x => outboundorderlines.Add(x));
            return outboundorderlines.AsQueryable();
        }
        public IQueryable<PickBatches> PickBatchesDb()
        {
            List<PickBatches> pickbatches = new List<PickBatches>();
            _context.PickBatches.Include(a => a.PickBatchTypes).Include(a => a.Statuses).AsNoTracking()
                .ToList()
                .ForEach(x => pickbatches.Add(x));
            return pickbatches.AsQueryable();
        }
        public IQueryable<Statuses> StatusesDb()
        {
            List<Statuses> statuses = new List<Statuses>();
            _context.Statuses.AsNoTracking()
                .ToList()
                .ForEach(x => statuses.Add(x));
            return statuses.AsQueryable();
        }
       public List<KeyValuePair<Int64?, string>> selectHandlingUnitsNullable()
        {
            List<KeyValuePair<Int64?, string>> handlingunitsNullable = new List<KeyValuePair<Int64?, string>>();
            handlingunitsNullable.Add(new KeyValuePair<Int64?, string>(null, "None"));
            _context.HandlingUnits
                .OrderBy(k => k.sHandlingUnit)
                .ToList()
                .ForEach(k => handlingunitsNullable.Add(new KeyValuePair<Int64?, string>(k.ixHandlingUnit, k.sHandlingUnit)));
            return handlingunitsNullable;
        }
        public List<KeyValuePair<Int64?, string>> selectInboundOrderLinesNullable()
        {
            List<KeyValuePair<Int64?, string>> inboundorderlinesNullable = new List<KeyValuePair<Int64?, string>>();
            inboundorderlinesNullable.Add(new KeyValuePair<Int64?, string>(null, "None"));
            _context.InboundOrderLines
                .OrderBy(k => k.sInboundOrderLine)
                .ToList()
                .ForEach(k => inboundorderlinesNullable.Add(new KeyValuePair<Int64?, string>(k.ixInboundOrderLine, k.sInboundOrderLine)));
            return inboundorderlinesNullable;
        }
        public List<KeyValuePair<Int64?, string>> selectInventoryLocationsNullable()
        {
            List<KeyValuePair<Int64?, string>> inventorylocationsNullable = new List<KeyValuePair<Int64?, string>>();
            inventorylocationsNullable.Add(new KeyValuePair<Int64?, string>(null, "None"));
            _context.InventoryLocations
                .OrderBy(k => k.sInventoryLocation)
                .ToList()
                .ForEach(k => inventorylocationsNullable.Add(new KeyValuePair<Int64?, string>(k.ixInventoryLocation, k.sInventoryLocation)));
            return inventorylocationsNullable;
        }
        public List<KeyValuePair<Int64?, string>> selectInventoryUnitsNullable()
        {
            List<KeyValuePair<Int64?, string>> inventoryunitsNullable = new List<KeyValuePair<Int64?, string>>();
            inventoryunitsNullable.Add(new KeyValuePair<Int64?, string>(null, "None"));
            _context.InventoryUnits
                .OrderBy(k => k.sInventoryUnit)
                .ToList()
                .ForEach(k => inventoryunitsNullable.Add(new KeyValuePair<Int64?, string>(k.ixInventoryUnit, k.sInventoryUnit)));
            return inventoryunitsNullable;
        }
        public List<KeyValuePair<Int64?, string>> selectOutboundOrderLinesNullable()
        {
            List<KeyValuePair<Int64?, string>> outboundorderlinesNullable = new List<KeyValuePair<Int64?, string>>();
            outboundorderlinesNullable.Add(new KeyValuePair<Int64?, string>(null, "None"));
            _context.OutboundOrderLines
                .OrderBy(k => k.sOutboundOrderLine)
                .ToList()
                .ForEach(k => outboundorderlinesNullable.Add(new KeyValuePair<Int64?, string>(k.ixOutboundOrderLine, k.sOutboundOrderLine)));
            return outboundorderlinesNullable;
        }
        public List<KeyValuePair<Int64?, string>> selectPickBatchesNullable()
        {
            List<KeyValuePair<Int64?, string>> pickbatchesNullable = new List<KeyValuePair<Int64?, string>>();
            pickbatchesNullable.Add(new KeyValuePair<Int64?, string>(null, "None"));
            _context.PickBatches
                .OrderBy(k => k.sPickBatch)
                .ToList()
                .ForEach(k => pickbatchesNullable.Add(new KeyValuePair<Int64?, string>(k.ixPickBatch, k.sPickBatch)));
            return pickbatchesNullable;
        }
        public bool VerifyMoveQueueUnique(Int64 ixMoveQueue, string sMoveQueue)
        {
            if (_context.MoveQueues.AsNoTracking().Where(x => x.sMoveQueue == sMoveQueue).Any() && ixMoveQueue == 0L) return false;
            else if (_context.MoveQueues.AsNoTracking().Where(x => x.sMoveQueue == sMoveQueue && x.ixMoveQueue != ixMoveQueue).Any() && ixMoveQueue != 0L) return false;
            else return true;
        }

        public List<string> VerifyMoveQueueDeleteOK(Int64 ixMoveQueue, string sMoveQueue)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(MoveQueuesPost movequeuesPost)
		{
            _context.MoveQueuesPost.Add(movequeuesPost); 
        }

        public void RegisterEdit(MoveQueuesPost movequeuesPost)
        {
            _context.Entry(movequeuesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(MoveQueuesPost movequeuesPost)
        {
            _context.MoveQueuesPost.Remove(movequeuesPost);
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
  

