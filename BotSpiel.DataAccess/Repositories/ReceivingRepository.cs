using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class ReceivingRepository : IReceivingRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly ReceivingDB _context;
  
        public ReceivingRepository(ReceivingDB context)
        {
            _context = context;
  
        }

        public ReceivingPost GetPost(Int64 ixReceipt) => _context.ReceivingPost.AsNoTracking().Where(x => x.ixReceipt == ixReceipt).First();
         
		public Receiving Get(Int64 ixReceipt)
        {
            Receiving receiving = _context.Receiving.AsNoTracking().Where(x => x.ixReceipt == ixReceipt).First();
            receiving.HandlingUnits = _context.HandlingUnits.Find(receiving.ixHandlingUnit);
            if (receiving.ixHandlingUnitType != null)
        {
            receiving.HandlingUnitTypes = _context.HandlingUnitTypes.Find(receiving.ixHandlingUnitType);
        }
            receiving.InboundOrders = _context.InboundOrders.Find(receiving.ixInboundOrder);
            receiving.InventoryLocations = _context.InventoryLocations.Find(receiving.ixInventoryLocation);
            receiving.InventoryStates = _context.InventoryStates.Find(receiving.ixInventoryState);
            if (receiving.ixMaterialHandlingUnitConfiguration != null)
        {
            receiving.MaterialHandlingUnitConfigurations = _context.MaterialHandlingUnitConfigurations.Find(receiving.ixMaterialHandlingUnitConfiguration);
        }
            receiving.Materials = _context.Materials.Find(receiving.ixMaterial);
            receiving.Statuses = _context.Statuses.Find(receiving.ixStatus);

            return receiving;
        }

        public IQueryable<Receiving> Index()
        {
            var receiving = _context.Receiving.Include(a => a.InboundOrders).Include(a => a.InventoryLocations).Include(a => a.HandlingUnits).Include(a => a.Materials).Include(a => a.Statuses).Include(a => a.InventoryStates).AsNoTracking(); 
            return receiving;
        }

        public IQueryable<Receiving> IndexDb()
        {
            var receiving = _context.Receiving.Include(a => a.InboundOrders).Include(a => a.InventoryLocations).Include(a => a.HandlingUnits).Include(a => a.Materials).Include(a => a.Statuses).Include(a => a.InventoryStates).AsNoTracking(); 
            return receiving;
        }
       public IQueryable<HandlingUnits> selectHandlingUnits()
        {
            List<HandlingUnits> handlingunits = new List<HandlingUnits>();
            _context.HandlingUnits.Include(a => a.HandlingUnitsFKDiffParentHandlingUnit).Include(a => a.HandlingUnitTypes).Include(a => a.MaterialHandlingUnitConfigurations).Include(a => a.MaterialsFKDiffPackingMaterial).Include(a => a.Statuses).Include(a => a.UnitsOfMeasurementFKDiffHeightUnit).Include(a => a.UnitsOfMeasurementFKDiffLengthUnit).Include(a => a.UnitsOfMeasurementFKDiffWeightUnit).Include(a => a.UnitsOfMeasurementFKDiffWidthUnit).AsNoTracking()
                .ToList()
                .ForEach(x => handlingunits.Add(x));
            return handlingunits.AsQueryable();
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
        //Custom Code Start | Added Code Block 
        public List<KeyValuePair<Int64?, string>> selectInboundOrdersFirst()
        {
            List<KeyValuePair<Int64?, string>> inboundorders = new List<KeyValuePair<Int64?, string>>();
            inboundorders.Add(new KeyValuePair<Int64?, string>(null, "Select an Inbound Order"));
            _context.InboundOrders.Where(x => x.Statuses.sStatus != "Complete")
                .OrderBy(k => k.sInboundOrder)
                .ToList()
                .ForEach(k => inboundorders.Add(new KeyValuePair<Int64?, string>(k.ixInboundOrder, k.sInboundOrder)));
            return inboundorders;
        }
        //Custom Code End


        public IQueryable<InventoryLocations> selectInventoryLocations()
        {
            List<InventoryLocations> inventorylocations = new List<InventoryLocations>();
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //_context.InventoryLocations.Include(a => a.Companies).Include(a => a.FacilityAisleFaces).Include(a => a.FacilityFloors).Include(a => a.FacilityWorkAreas).Include(a => a.FacilityZones).Include(a => a.InventoryLocationSizes).Include(a => a.LocationFunctions).Include(a => a.UnitsOfMeasurementFKDiffXOffsetUnit).Include(a => a.UnitsOfMeasurementFKDiffYOffsetUnit).Include(a => a.UnitsOfMeasurementFKDiffZOffsetUnit).AsNoTracking()
            //Replaced Code Block End
            //Custom Code End
            _context.InventoryLocations.Include(a => a.Companies).Include(a => a.FacilityAisleFaces).Include(a => a.FacilityFloors).Include(a => a.FacilityWorkAreas).Include(a => a.FacilityZones).Include(a => a.InventoryLocationSizes).Include(a => a.LocationFunctions).Include(a => a.UnitsOfMeasurementFKDiffXOffsetUnit).Include(a => a.UnitsOfMeasurementFKDiffYOffsetUnit).Include(a => a.UnitsOfMeasurementFKDiffZOffsetUnit).Where(a => a.LocationFunctions.sLocationFunctionCode == "RC").AsNoTracking()
                .ToList()
                .ForEach(x => inventorylocations.Add(x));
            return inventorylocations.AsQueryable();
        }
        public IQueryable<InventoryStates> selectInventoryStates()
        {
            List<InventoryStates> inventorystates = new List<InventoryStates>();
            _context.InventoryStates.AsNoTracking()
                .ToList()
                .ForEach(x => inventorystates.Add(x));
            return inventorystates.AsQueryable();
        }
        public IQueryable<MaterialHandlingUnitConfigurations> selectMaterialHandlingUnitConfigurations()
        {
            List<MaterialHandlingUnitConfigurations> materialhandlingunitconfigurations = new List<MaterialHandlingUnitConfigurations>();
            //_context.MaterialHandlingUnitConfigurations.Include(a => a.HandlingUnitTypes).Include(a => a.Materials).Include(a => a.UnitsOfMeasurementFKDiffHeightUnit).Include(a => a.UnitsOfMeasurementFKDiffLengthUnit).Include(a => a.UnitsOfMeasurementFKDiffWidthUnit).AsNoTracking()
            //    .ToList()
            //    .ForEach(x => materialhandlingunitconfigurations.Add(x));
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
       public IQueryable<HandlingUnits> HandlingUnitsDb()
        {
            List<HandlingUnits> handlingunits = new List<HandlingUnits>();
            _context.HandlingUnits.Include(a => a.HandlingUnitsFKDiffParentHandlingUnit).Include(a => a.HandlingUnitTypes).Include(a => a.MaterialHandlingUnitConfigurations).Include(a => a.MaterialsFKDiffPackingMaterial).Include(a => a.Statuses).Include(a => a.UnitsOfMeasurementFKDiffHeightUnit).Include(a => a.UnitsOfMeasurementFKDiffLengthUnit).Include(a => a.UnitsOfMeasurementFKDiffWeightUnit).Include(a => a.UnitsOfMeasurementFKDiffWidthUnit).AsNoTracking()
                .ToList()
                .ForEach(x => handlingunits.Add(x));
            return handlingunits.AsQueryable();
        }
        public IQueryable<HandlingUnitTypes> HandlingUnitTypesDb()
        {
            List<HandlingUnitTypes> handlingunittypes = new List<HandlingUnitTypes>();
            _context.HandlingUnitTypes.AsNoTracking()
                .ToList()
                .ForEach(x => handlingunittypes.Add(x));
            return handlingunittypes.AsQueryable();
        }
        public IQueryable<InboundOrders> InboundOrdersDb()
        {
            List<InboundOrders> inboundorders = new List<InboundOrders>();
            _context.InboundOrders.Include(a => a.BusinessPartners).Include(a => a.Companies).Include(a => a.Facilities).Include(a => a.InboundOrderTypes).Include(a => a.Statuses).AsNoTracking()
                .ToList()
                .ForEach(x => inboundorders.Add(x));
            return inboundorders.AsQueryable();
        }
        public IQueryable<InventoryLocations> InventoryLocationsDb()
        {
            List<InventoryLocations> inventorylocations = new List<InventoryLocations>();
            _context.InventoryLocations.Include(a => a.Companies).Include(a => a.FacilityAisleFaces).Include(a => a.FacilityFloors).Include(a => a.FacilityWorkAreas).Include(a => a.FacilityZones).Include(a => a.InventoryLocationSizes).Include(a => a.LocationFunctions).Include(a => a.UnitsOfMeasurementFKDiffXOffsetUnit).Include(a => a.UnitsOfMeasurementFKDiffYOffsetUnit).Include(a => a.UnitsOfMeasurementFKDiffZOffsetUnit).AsNoTracking()
                .ToList()
                .ForEach(x => inventorylocations.Add(x));
            return inventorylocations.AsQueryable();
        }
        public IQueryable<InventoryStates> InventoryStatesDb()
        {
            List<InventoryStates> inventorystates = new List<InventoryStates>();
            _context.InventoryStates.AsNoTracking()
                .ToList()
                .ForEach(x => inventorystates.Add(x));
            return inventorystates.AsQueryable();
        }
        public IQueryable<MaterialHandlingUnitConfigurations> MaterialHandlingUnitConfigurationsDb()
        {
            List<MaterialHandlingUnitConfigurations> materialhandlingunitconfigurations = new List<MaterialHandlingUnitConfigurations>();
            _context.MaterialHandlingUnitConfigurations.Include(a => a.HandlingUnitTypes).Include(a => a.Materials).Include(a => a.UnitsOfMeasurementFKDiffHeightUnit).Include(a => a.UnitsOfMeasurementFKDiffLengthUnit).Include(a => a.UnitsOfMeasurementFKDiffWidthUnit).AsNoTracking()
                .ToList()
                .ForEach(x => materialhandlingunitconfigurations.Add(x));
            return materialhandlingunitconfigurations.AsQueryable();
        }
        public IQueryable<Materials> MaterialsDb()
        {
            List<Materials> materials = new List<Materials>();
            _context.Materials.Include(a => a.MaterialTypes).Include(a => a.UnitsOfMeasurementFKDiffBaseUnit).Include(a => a.UnitsOfMeasurementFKDiffDensityUnit).Include(a => a.UnitsOfMeasurementFKDiffHeightUnit).Include(a => a.UnitsOfMeasurementFKDiffLengthUnit).Include(a => a.UnitsOfMeasurementFKDiffShelflifeUnit).Include(a => a.UnitsOfMeasurementFKDiffWeightUnit).Include(a => a.UnitsOfMeasurementFKDiffWidthUnit).AsNoTracking()
                .ToList()
                .ForEach(x => materials.Add(x));
            return materials.AsQueryable();
        }
        public IQueryable<Statuses> StatusesDb()
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
        public bool VerifyReceiptUnique(Int64 ixReceipt, string sReceipt)
        {
            if (_context.Receiving.AsNoTracking().Where(x => x.sReceipt == sReceipt).Any() && ixReceipt == 0L) return false;
            else if (_context.Receiving.AsNoTracking().Where(x => x.sReceipt == sReceipt && x.ixReceipt != ixReceipt).Any() && ixReceipt != 0L) return false;
            else return true;
        }

        public List<string> VerifyReceiptDeleteOK(Int64 ixReceipt, string sReceipt)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(ReceivingPost receivingPost)
		{
            _context.ReceivingPost.Add(receivingPost); 
        }

        public void RegisterEdit(ReceivingPost receivingPost)
        {
            _context.Entry(receivingPost).State = EntityState.Modified;
        }

        public void RegisterDelete(ReceivingPost receivingPost)
        {
            _context.ReceivingPost.Remove(receivingPost);
        }

        public void Rollback()
        {
            _context.Rollback();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        //Custom Code Start | Added Code Block 
        public List<KeyValuePair<Int64?, string>> selectEmptyMaterialsDropdown()
        {
            List<KeyValuePair<Int64?, string>> emptyDropdown = new List<KeyValuePair<Int64?, string>>();
            emptyDropdown.Add(new KeyValuePair<Int64?, string>(null, "Select Inbound Order First"));
            return emptyDropdown;
        }
        //Custom Code End


    }
}
  

