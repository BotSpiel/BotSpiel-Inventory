using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class InboundOrderLinesRepository : IInboundOrderLinesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly InboundOrderLinesDB _context;
       private readonly MoveQueuesDB _contextMoveQueues;
  
        public InboundOrderLinesRepository(InboundOrderLinesDB context, MoveQueuesDB contextMoveQueues)
        {
            _context = context;
           _contextMoveQueues = contextMoveQueues;
  
        }

        public InboundOrderLinesPost GetPost(Int64 ixInboundOrderLine) => _context.InboundOrderLinesPost.AsNoTracking().Where(x => x.ixInboundOrderLine == ixInboundOrderLine).First();
         
		public InboundOrderLines Get(Int64 ixInboundOrderLine)
        {
            InboundOrderLines inboundorderlines = _context.InboundOrderLines.AsNoTracking().Where(x => x.ixInboundOrderLine == ixInboundOrderLine).First();
            if (inboundorderlines.ixHandlingUnitType != null)
        {
            inboundorderlines.HandlingUnitTypes = _context.HandlingUnitTypes.Find(inboundorderlines.ixHandlingUnitType);
        }
            inboundorderlines.InboundOrders = _context.InboundOrders.Find(inboundorderlines.ixInboundOrder);
            if (inboundorderlines.ixMaterialHandlingUnitConfiguration != null)
        {
            inboundorderlines.MaterialHandlingUnitConfigurations = _context.MaterialHandlingUnitConfigurations.Find(inboundorderlines.ixMaterialHandlingUnitConfiguration);
        }
            inboundorderlines.Materials = _context.Materials.Find(inboundorderlines.ixMaterial);
            inboundorderlines.Statuses = _context.Statuses.Find(inboundorderlines.ixStatus);

            return inboundorderlines;
        }

        public IQueryable<InboundOrderLines> Index()
        {
            var inboundorderlines = _context.InboundOrderLines.Include(a => a.InboundOrders).Include(a => a.Materials).Include(a => a.Statuses).AsNoTracking(); 
            return inboundorderlines;
        }
       public IQueryable<HandlingUnitTypes> selectHandlingUnitTypes()
        {
            List<HandlingUnitTypes> handlingunittypes = new List<HandlingUnitTypes>();
            _context.HandlingUnitTypes.AsNoTracking()
                .ToList()
                .ForEach(x => handlingunittypes.Add(x));
            return handlingunittypes.AsQueryable();
        }
        public IQueryable<InboundOrders> selectInboundOrders()
        {
            List<InboundOrders> inboundorders = new List<InboundOrders>();
            _context.InboundOrders.Include(a => a.BusinessPartners).Include(a => a.Companies).Include(a => a.Facilities).Include(a => a.InboundOrderTypes).Include(a => a.Statuses).AsNoTracking()
                .ToList()
                .ForEach(x => inboundorders.Add(x));
            return inboundorders.AsQueryable();
        }
        public IQueryable<MaterialHandlingUnitConfigurations> selectMaterialHandlingUnitConfigurations()
        {
            List<MaterialHandlingUnitConfigurations> materialhandlingunitconfigurations = new List<MaterialHandlingUnitConfigurations>();
            _context.MaterialHandlingUnitConfigurations.Include(a => a.HandlingUnitTypes).Include(a => a.Materials).Include(a => a.UnitsOfMeasurementFKDiffHeightUnit).Include(a => a.UnitsOfMeasurementFKDiffLengthUnit).Include(a => a.UnitsOfMeasurementFKDiffWidthUnit).AsNoTracking()
                .ToList()
                .ForEach(x => materialhandlingunitconfigurations.Add(x));
            return materialhandlingunitconfigurations.AsQueryable();
        }
        public IQueryable<Materials> selectMaterials()
        {
            List<Materials> materials = new List<Materials>();
            _context.Materials.Include(a => a.MaterialTypes).Include(a => a.UnitsOfMeasurementFKDiffBaseUnit).Include(a => a.UnitsOfMeasurementFKDiffDensityUnit).Include(a => a.UnitsOfMeasurementFKDiffHeightUnit).Include(a => a.UnitsOfMeasurementFKDiffLengthUnit).Include(a => a.UnitsOfMeasurementFKDiffShelflifeUnit).Include(a => a.UnitsOfMeasurementFKDiffWeightUnit).Include(a => a.UnitsOfMeasurementFKDiffWidthUnit).AsNoTracking()
                .ToList()
                .ForEach(x => materials.Add(x));
            return materials.AsQueryable();
        }
        public IQueryable<Statuses> selectStatuses()
        {
            List<Statuses> statuses = new List<Statuses>();
            _context.Statuses.AsNoTracking()
                .ToList()
                .ForEach(x => statuses.Add(x));
            return statuses.AsQueryable();
        }
       public List<KeyValuePair<Int64?, string>> selectHandlingUnitTypesNullable()
        {
            List<KeyValuePair<Int64?, string>> handlingunittypesNullable = new List<KeyValuePair<Int64?, string>>();
            handlingunittypesNullable.Add(new KeyValuePair<Int64?, string>(null, "None"));
            _context.HandlingUnitTypes
                .OrderBy(k => k.sHandlingUnitType)
                .ToList()
                .ForEach(k => handlingunittypesNullable.Add(new KeyValuePair<Int64?, string>(k.ixHandlingUnitType, k.sHandlingUnitType)));
            return handlingunittypesNullable;
        }
        public List<KeyValuePair<Int64?, string>> selectMaterialHandlingUnitConfigurationsNullable()
        {
            List<KeyValuePair<Int64?, string>> materialhandlingunitconfigurationsNullable = new List<KeyValuePair<Int64?, string>>();
            materialhandlingunitconfigurationsNullable.Add(new KeyValuePair<Int64?, string>(null, "None"));
            _context.MaterialHandlingUnitConfigurations
                .OrderBy(k => k.sMaterialHandlingUnitConfiguration)
                .ToList()
                .ForEach(k => materialhandlingunitconfigurationsNullable.Add(new KeyValuePair<Int64?, string>(k.ixMaterialHandlingUnitConfiguration, k.sMaterialHandlingUnitConfiguration)));
            return materialhandlingunitconfigurationsNullable;
        }
        public bool VerifyInboundOrderLineUnique(Int64 ixInboundOrderLine, string sInboundOrderLine)
        {
            if (_context.InboundOrderLines.AsNoTracking().Where(x => x.sInboundOrderLine == sInboundOrderLine).Any() && ixInboundOrderLine == 0L) return false;
            else if (_context.InboundOrderLines.AsNoTracking().Where(x => x.sInboundOrderLine == sInboundOrderLine && x.ixInboundOrderLine != ixInboundOrderLine).Any() && ixInboundOrderLine != 0L) return false;
            else return true;
        }

        public List<string> VerifyInboundOrderLineDeleteOK(Int64 ixInboundOrderLine, string sInboundOrderLine)
        {
            List<string> existInEntities = new List<string>();
           if (_contextMoveQueues.MoveQueues.AsNoTracking().Where(x => x.ixInboundOrderLine == ixInboundOrderLine).Any()) existInEntities.Add("MoveQueues");

            return existInEntities;
        }


        public void RegisterCreate(InboundOrderLinesPost inboundorderlinesPost)
		{
            _context.InboundOrderLinesPost.Add(inboundorderlinesPost); 
        }

        public void RegisterEdit(InboundOrderLinesPost inboundorderlinesPost)
        {
            _context.Entry(inboundorderlinesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(InboundOrderLinesPost inboundorderlinesPost)
        {
            _context.InboundOrderLinesPost.Remove(inboundorderlinesPost);
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
  

