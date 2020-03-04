using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class OutboundOrderLinesRepository : IOutboundOrderLinesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly OutboundOrderLinesDB _context;
       private readonly MoveQueuesDB _contextMoveQueues;
        private readonly OutboundOrderLinePackingDB _contextOutboundOrderLinePacking;
        private readonly OutboundOrderLinesInventoryAllocationDB _contextOutboundOrderLinesInventoryAllocation;
  
        public OutboundOrderLinesRepository(OutboundOrderLinesDB context, MoveQueuesDB contextMoveQueues, OutboundOrderLinePackingDB contextOutboundOrderLinePacking, OutboundOrderLinesInventoryAllocationDB contextOutboundOrderLinesInventoryAllocation)
        {
            _context = context;
           _contextMoveQueues = contextMoveQueues;
            _contextOutboundOrderLinePacking = contextOutboundOrderLinePacking;
            _contextOutboundOrderLinesInventoryAllocation = contextOutboundOrderLinesInventoryAllocation;
  
        }

        public OutboundOrderLinesPost GetPost(Int64 ixOutboundOrderLine) => _context.OutboundOrderLinesPost.AsNoTracking().Where(x => x.ixOutboundOrderLine == ixOutboundOrderLine).First();
         
		public OutboundOrderLines Get(Int64 ixOutboundOrderLine)
        {
            OutboundOrderLines outboundorderlines = _context.OutboundOrderLines.AsNoTracking().Where(x => x.ixOutboundOrderLine == ixOutboundOrderLine).First();
            outboundorderlines.Materials = _context.Materials.Find(outboundorderlines.ixMaterial);
            outboundorderlines.OutboundOrders = _context.OutboundOrders.Find(outboundorderlines.ixOutboundOrder);
            outboundorderlines.Statuses = _context.Statuses.Find(outboundorderlines.ixStatus);

            return outboundorderlines;
        }

        public IQueryable<OutboundOrderLines> Index()
        {
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //var outboundorderlines = _context.OutboundOrderLines.Include(a => a.Materials).Include(a => a.Statuses).Include(a => a.OutboundOrders).AsNoTracking();
            //Replaced Code Block End
            var outboundorderlines = _context.OutboundOrderLines.OrderByDescending(a => a.ixOutboundOrderLine).Include(a => a.Materials).Include(a => a.Statuses).Include(a => a.OutboundOrders).AsNoTracking();
            //Custom Code End
            return outboundorderlines;
        }

        public IQueryable<OutboundOrderLines> IndexDb()
        {
            var outboundorderlines = _context.OutboundOrderLines.Include(a => a.Materials).Include(a => a.Statuses).Include(a => a.OutboundOrders).AsNoTracking(); 
            return outboundorderlines;
        }
       public IQueryable<Materials> selectMaterials()
        {
            List<Materials> materials = new List<Materials>();
            _context.Materials.Include(a => a.MaterialTypes).Include(a => a.UnitsOfMeasurementFKDiffBaseUnit).Include(a => a.UnitsOfMeasurementFKDiffDensityUnit).Include(a => a.UnitsOfMeasurementFKDiffHeightUnit).Include(a => a.UnitsOfMeasurementFKDiffLengthUnit).Include(a => a.UnitsOfMeasurementFKDiffShelflifeUnit).Include(a => a.UnitsOfMeasurementFKDiffWeightUnit).Include(a => a.UnitsOfMeasurementFKDiffWidthUnit).AsNoTracking()
                .ToList()
                .ForEach(x => materials.Add(x));
            return materials.AsQueryable();
        }
        public IQueryable<OutboundOrders> selectOutboundOrders()
        {
            List<OutboundOrders> outboundorders = new List<OutboundOrders>();
            _context.OutboundOrders.Include(a => a.BusinessPartners).Include(a => a.CarrierServices).Include(a => a.Companies).Include(a => a.Facilities).Include(a => a.OutboundOrderTypes).Include(a => a.OutboundShipments).Include(a => a.PickBatches).Include(a => a.Statuses).AsNoTracking()
                .ToList()
                .ForEach(x => outboundorders.Add(x));
            return outboundorders.AsQueryable();
        }
        public IQueryable<Statuses> selectStatuses()
        {
            List<Statuses> statuses = new List<Statuses>();
            _context.Statuses.AsNoTracking()
                .ToList()
                .ForEach(x => statuses.Add(x));
            return statuses.AsQueryable();
        }
       public IQueryable<Materials> MaterialsDb()
        {
            List<Materials> materials = new List<Materials>();
            _context.Materials.Include(a => a.MaterialTypes).Include(a => a.UnitsOfMeasurementFKDiffBaseUnit).Include(a => a.UnitsOfMeasurementFKDiffDensityUnit).Include(a => a.UnitsOfMeasurementFKDiffHeightUnit).Include(a => a.UnitsOfMeasurementFKDiffLengthUnit).Include(a => a.UnitsOfMeasurementFKDiffShelflifeUnit).Include(a => a.UnitsOfMeasurementFKDiffWeightUnit).Include(a => a.UnitsOfMeasurementFKDiffWidthUnit).AsNoTracking()
                .ToList()
                .ForEach(x => materials.Add(x));
            return materials.AsQueryable();
        }
        public IQueryable<OutboundOrders> OutboundOrdersDb()
        {
            List<OutboundOrders> outboundorders = new List<OutboundOrders>();
            _context.OutboundOrders.Include(a => a.BusinessPartners).Include(a => a.CarrierServices).Include(a => a.Companies).Include(a => a.Facilities).Include(a => a.OutboundOrderTypes).Include(a => a.OutboundShipments).Include(a => a.PickBatches).Include(a => a.Statuses).AsNoTracking()
                .ToList()
                .ForEach(x => outboundorders.Add(x));
            return outboundorders.AsQueryable();
        }
        public IQueryable<Statuses> StatusesDb()
        {
            List<Statuses> statuses = new List<Statuses>();
            _context.Statuses.AsNoTracking()
                .ToList()
                .ForEach(x => statuses.Add(x));
            return statuses.AsQueryable();
        }
        public bool VerifyOutboundOrderLineUnique(Int64 ixOutboundOrderLine, string sOutboundOrderLine)
        {
            if (_context.OutboundOrderLines.AsNoTracking().Where(x => x.sOutboundOrderLine == sOutboundOrderLine).Any() && ixOutboundOrderLine == 0L) return false;
            else if (_context.OutboundOrderLines.AsNoTracking().Where(x => x.sOutboundOrderLine == sOutboundOrderLine && x.ixOutboundOrderLine != ixOutboundOrderLine).Any() && ixOutboundOrderLine != 0L) return false;
            else return true;
        }

        public List<string> VerifyOutboundOrderLineDeleteOK(Int64 ixOutboundOrderLine, string sOutboundOrderLine)
        {
            List<string> existInEntities = new List<string>();
           if (_contextMoveQueues.MoveQueues.AsNoTracking().Where(x => x.ixOutboundOrderLine == ixOutboundOrderLine).Any()) existInEntities.Add("MoveQueues");
            if (_contextOutboundOrderLinePacking.OutboundOrderLinePacking.AsNoTracking().Where(x => x.ixOutboundOrderLine == ixOutboundOrderLine).Any()) existInEntities.Add("OutboundOrderLinePacking");
            if (_contextOutboundOrderLinesInventoryAllocation.OutboundOrderLinesInventoryAllocation.AsNoTracking().Where(x => x.ixOutboundOrderLine == ixOutboundOrderLine).Any()) existInEntities.Add("OutboundOrderLinesInventoryAllocation");

            return existInEntities;
        }


        public void RegisterCreate(OutboundOrderLinesPost outboundorderlinesPost)
		{
            _context.OutboundOrderLinesPost.Add(outboundorderlinesPost); 
        }

        public void RegisterEdit(OutboundOrderLinesPost outboundorderlinesPost)
        {
            _context.Entry(outboundorderlinesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(OutboundOrderLinesPost outboundorderlinesPost)
        {
            _context.OutboundOrderLinesPost.Remove(outboundorderlinesPost);
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
  

