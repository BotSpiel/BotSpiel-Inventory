using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class InventoryUnitTransactionsRepository : IInventoryUnitTransactionsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly InventoryUnitTransactionsDB _context;
  
        public InventoryUnitTransactionsRepository(InventoryUnitTransactionsDB context)
        {
            _context = context;
  
        }

        public InventoryUnitTransactionsPost GetPost(Int64 ixInventoryUnitTransaction) => _context.InventoryUnitTransactionsPost.AsNoTracking().Where(x => x.ixInventoryUnitTransaction == ixInventoryUnitTransaction).First();
         
		public InventoryUnitTransactions Get(Int64 ixInventoryUnitTransaction)
        {
            InventoryUnitTransactions inventoryunittransactions = _context.InventoryUnitTransactions.AsNoTracking().Where(x => x.ixInventoryUnitTransaction == ixInventoryUnitTransaction).First();
            inventoryunittransactions.CompaniesFKDiffCompanyAfter = _context.Companies.Find(inventoryunittransactions.ixCompanyAfter);
            if (inventoryunittransactions.ixCompanyBefore != null)
        {
            inventoryunittransactions.CompaniesFKDiffCompanyBefore = _context.Companies.Find(inventoryunittransactions.ixCompanyBefore);
        }
            inventoryunittransactions.FacilitiesFKDiffFacilityAfter = _context.Facilities.Find(inventoryunittransactions.ixFacilityAfter);
            if (inventoryunittransactions.ixFacilityBefore != null)
        {
            inventoryunittransactions.FacilitiesFKDiffFacilityBefore = _context.Facilities.Find(inventoryunittransactions.ixFacilityBefore);
        }
            if (inventoryunittransactions.ixHandlingUnitAfter != null)
        {
            inventoryunittransactions.HandlingUnitsFKDiffHandlingUnitAfter = _context.HandlingUnits.Find(inventoryunittransactions.ixHandlingUnitAfter);
        }
            if (inventoryunittransactions.ixHandlingUnitBefore != null)
        {
            inventoryunittransactions.HandlingUnitsFKDiffHandlingUnitBefore = _context.HandlingUnits.Find(inventoryunittransactions.ixHandlingUnitBefore);
        }
            inventoryunittransactions.InventoryLocationsFKDiffInventoryLocationAfter = _context.InventoryLocations.Find(inventoryunittransactions.ixInventoryLocationAfter);
            if (inventoryunittransactions.ixInventoryLocationBefore != null)
        {
            inventoryunittransactions.InventoryLocationsFKDiffInventoryLocationBefore = _context.InventoryLocations.Find(inventoryunittransactions.ixInventoryLocationBefore);
        }
            inventoryunittransactions.InventoryStatesFKDiffInventoryStateAfter = _context.InventoryStates.Find(inventoryunittransactions.ixInventoryStateAfter);
            if (inventoryunittransactions.ixInventoryStateBefore != null)
        {
            inventoryunittransactions.InventoryStatesFKDiffInventoryStateBefore = _context.InventoryStates.Find(inventoryunittransactions.ixInventoryStateBefore);
        }
            inventoryunittransactions.InventoryUnits = _context.InventoryUnits.Find(inventoryunittransactions.ixInventoryUnit);
            inventoryunittransactions.InventoryUnitTransactionContexts = _context.InventoryUnitTransactionContexts.Find(inventoryunittransactions.ixInventoryUnitTransactionContext);
            inventoryunittransactions.MaterialsFKDiffMaterialAfter = _context.Materials.Find(inventoryunittransactions.ixMaterialAfter);
            if (inventoryunittransactions.ixMaterialBefore != null)
        {
            inventoryunittransactions.MaterialsFKDiffMaterialBefore = _context.Materials.Find(inventoryunittransactions.ixMaterialBefore);
        }
            inventoryunittransactions.StatusesFKDiffStatusAfter = _context.Statuses.Find(inventoryunittransactions.ixStatusAfter);
            if (inventoryunittransactions.ixStatusBefore != null)
        {
            inventoryunittransactions.StatusesFKDiffStatusBefore = _context.Statuses.Find(inventoryunittransactions.ixStatusBefore);
        }

            return inventoryunittransactions;
        }

        public IQueryable<InventoryUnitTransactions> Index()
        {
            var inventoryunittransactions = _context.InventoryUnitTransactions.Include(a => a.InventoryUnits).Include(a => a.FacilitiesFKDiffFacilityAfter).Include(a => a.CompaniesFKDiffCompanyAfter).Include(a => a.MaterialsFKDiffMaterialAfter).Include(a => a.InventoryUnitTransactionContexts).Include(a => a.InventoryStatesFKDiffInventoryStateAfter).Include(a => a.InventoryLocationsFKDiffInventoryLocationAfter).Include(a => a.StatusesFKDiffStatusAfter).AsNoTracking(); 
            return inventoryunittransactions;
        }
       public IQueryable<Companies> selectCompanies()
        {
            List<Companies> companies = new List<Companies>();
            _context.Companies.AsNoTracking()
                .ToList()
                .ForEach(x => companies.Add(x));
            return companies.AsQueryable();
        }
        public IQueryable<Facilities> selectFacilities()
        {
            List<Facilities> facilities = new List<Facilities>();
            _context.Facilities.Include(a => a.Addresses).AsNoTracking()
                .ToList()
                .ForEach(x => facilities.Add(x));
            return facilities.AsQueryable();
        }
        public IQueryable<HandlingUnits> selectHandlingUnits()
        {
            List<HandlingUnits> handlingunits = new List<HandlingUnits>();
            _context.HandlingUnits.Include(a => a.HandlingUnitsFKDiffParentHandlingUnit).Include(a => a.HandlingUnitTypes).Include(a => a.MaterialHandlingUnitConfigurations).Include(a => a.MaterialsFKDiffPackingMaterial).Include(a => a.Statuses).Include(a => a.UnitsOfMeasurementFKDiffHeightUnit).Include(a => a.UnitsOfMeasurementFKDiffLengthUnit).Include(a => a.UnitsOfMeasurementFKDiffWeightUnit).Include(a => a.UnitsOfMeasurementFKDiffWidthUnit).AsNoTracking()
                .ToList()
                .ForEach(x => handlingunits.Add(x));
            return handlingunits.AsQueryable();
        }
        public IQueryable<InventoryLocations> selectInventoryLocations()
        {
            List<InventoryLocations> inventorylocations = new List<InventoryLocations>();
            _context.InventoryLocations.Include(a => a.Companies).Include(a => a.FacilityAisleFaces).Include(a => a.FacilityFloors).Include(a => a.FacilityWorkAreas).Include(a => a.FacilityZones).Include(a => a.InventoryLocationSizes).Include(a => a.LocationFunctions).Include(a => a.UnitsOfMeasurementFKDiffXOffsetUnit).Include(a => a.UnitsOfMeasurementFKDiffYOffsetUnit).Include(a => a.UnitsOfMeasurementFKDiffZOffsetUnit).AsNoTracking()
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
        public IQueryable<InventoryUnits> selectInventoryUnits()
        {
            List<InventoryUnits> inventoryunits = new List<InventoryUnits>();
            _context.InventoryUnits.Include(a => a.Companies).Include(a => a.Facilities).Include(a => a.HandlingUnits).Include(a => a.InventoryLocations).Include(a => a.InventoryStates).Include(a => a.Materials).Include(a => a.Statuses).AsNoTracking()
                .ToList()
                .ForEach(x => inventoryunits.Add(x));
            return inventoryunits.AsQueryable();
        }
        public IQueryable<InventoryUnitTransactionContexts> selectInventoryUnitTransactionContexts()
        {
            List<InventoryUnitTransactionContexts> inventoryunittransactioncontexts = new List<InventoryUnitTransactionContexts>();
            _context.InventoryUnitTransactionContexts.AsNoTracking()
                .ToList()
                .ForEach(x => inventoryunittransactioncontexts.Add(x));
            return inventoryunittransactioncontexts.AsQueryable();
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
       public List<KeyValuePair<Int64?, string>> selectCompaniesNullable()
        {
            List<KeyValuePair<Int64?, string>> companiesNullable = new List<KeyValuePair<Int64?, string>>();
            companiesNullable.Add(new KeyValuePair<Int64?, string>(null, "None"));
            _context.Companies
                .OrderBy(k => k.sCompany)
                .ToList()
                .ForEach(k => companiesNullable.Add(new KeyValuePair<Int64?, string>(k.ixCompany, k.sCompany)));
            return companiesNullable;
        }
        public List<KeyValuePair<Int64?, string>> selectFacilitiesNullable()
        {
            List<KeyValuePair<Int64?, string>> facilitiesNullable = new List<KeyValuePair<Int64?, string>>();
            facilitiesNullable.Add(new KeyValuePair<Int64?, string>(null, "None"));
            _context.Facilities
                .OrderBy(k => k.sFacility)
                .ToList()
                .ForEach(k => facilitiesNullable.Add(new KeyValuePair<Int64?, string>(k.ixFacility, k.sFacility)));
            return facilitiesNullable;
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
        public List<KeyValuePair<Int64?, string>> selectInventoryStatesNullable()
        {
            List<KeyValuePair<Int64?, string>> inventorystatesNullable = new List<KeyValuePair<Int64?, string>>();
            inventorystatesNullable.Add(new KeyValuePair<Int64?, string>(null, "None"));
            _context.InventoryStates
                .OrderBy(k => k.sInventoryState)
                .ToList()
                .ForEach(k => inventorystatesNullable.Add(new KeyValuePair<Int64?, string>(k.ixInventoryState, k.sInventoryState)));
            return inventorystatesNullable;
        }
        public List<KeyValuePair<Int64?, string>> selectMaterialsNullable()
        {
            List<KeyValuePair<Int64?, string>> materialsNullable = new List<KeyValuePair<Int64?, string>>();
            materialsNullable.Add(new KeyValuePair<Int64?, string>(null, "None"));
            _context.Materials
                .OrderBy(k => k.sMaterial)
                .ToList()
                .ForEach(k => materialsNullable.Add(new KeyValuePair<Int64?, string>(k.ixMaterial, k.sMaterial)));
            return materialsNullable;
        }
        public List<KeyValuePair<Int64?, string>> selectStatusesNullable()
        {
            List<KeyValuePair<Int64?, string>> statusesNullable = new List<KeyValuePair<Int64?, string>>();
            statusesNullable.Add(new KeyValuePair<Int64?, string>(null, "None"));
            _context.Statuses
                .OrderBy(k => k.sStatus)
                .ToList()
                .ForEach(k => statusesNullable.Add(new KeyValuePair<Int64?, string>(k.ixStatus, k.sStatus)));
            return statusesNullable;
        }
        public bool VerifyInventoryUnitTransactionUnique(Int64 ixInventoryUnitTransaction, string sInventoryUnitTransaction)
        {
            if (_context.InventoryUnitTransactions.AsNoTracking().Where(x => x.sInventoryUnitTransaction == sInventoryUnitTransaction).Any() && ixInventoryUnitTransaction == 0L) return false;
            else if (_context.InventoryUnitTransactions.AsNoTracking().Where(x => x.sInventoryUnitTransaction == sInventoryUnitTransaction && x.ixInventoryUnitTransaction != ixInventoryUnitTransaction).Any() && ixInventoryUnitTransaction != 0L) return false;
            else return true;
        }

        public List<string> VerifyInventoryUnitTransactionDeleteOK(Int64 ixInventoryUnitTransaction, string sInventoryUnitTransaction)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(InventoryUnitTransactionsPost inventoryunittransactionsPost)
		{
            _context.InventoryUnitTransactionsPost.Add(inventoryunittransactionsPost); 
        }

        public void RegisterEdit(InventoryUnitTransactionsPost inventoryunittransactionsPost)
        {
            _context.Entry(inventoryunittransactionsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(InventoryUnitTransactionsPost inventoryunittransactionsPost)
        {
            _context.InventoryUnitTransactionsPost.Remove(inventoryunittransactionsPost);
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
  

