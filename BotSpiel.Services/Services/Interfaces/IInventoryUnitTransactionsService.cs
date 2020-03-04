using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IInventoryUnitTransactionsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        InventoryUnitTransactionsPost GetPost(Int64 ixInventoryUnitTransaction);        
		InventoryUnitTransactions Get(Int64 ixInventoryUnitTransaction);
        IQueryable<InventoryUnitTransactions> Index();
        IQueryable<InventoryUnitTransactions> IndexDb();
       IQueryable<Statuses> selectStatuses();
        IQueryable<Companies> selectCompanies();
        IQueryable<Facilities> selectFacilities();
        IQueryable<Materials> selectMaterials();
        IQueryable<InventoryUnits> selectInventoryUnits();
        IQueryable<InventoryStates> selectInventoryStates();
        IQueryable<HandlingUnits> selectHandlingUnits();
        IQueryable<InventoryLocations> selectInventoryLocations();
        IQueryable<InventoryUnitTransactionContexts> selectInventoryUnitTransactionContexts();
       IQueryable<Statuses> StatusesDb();
        IQueryable<Companies> CompaniesDb();
        IQueryable<Facilities> FacilitiesDb();
        IQueryable<Materials> MaterialsDb();
        IQueryable<InventoryUnits> InventoryUnitsDb();
        IQueryable<InventoryStates> InventoryStatesDb();
        IQueryable<HandlingUnits> HandlingUnitsDb();
        IQueryable<InventoryLocations> InventoryLocationsDb();
        IQueryable<InventoryUnitTransactionContexts> InventoryUnitTransactionContextsDb();
       List<KeyValuePair<Int64?, string>> selectStatusesNullable();
        List<KeyValuePair<Int64?, string>> selectCompaniesNullable();
        List<KeyValuePair<Int64?, string>> selectFacilitiesNullable();
        List<KeyValuePair<Int64?, string>> selectMaterialsNullable();
        List<KeyValuePair<Int64?, string>> selectInventoryStatesNullable();
        List<KeyValuePair<Int64?, string>> selectHandlingUnitsNullable();
        List<KeyValuePair<Int64?, string>> selectInventoryLocationsNullable();
        bool VerifyInventoryUnitTransactionUnique(Int64 ixInventoryUnitTransaction, string sInventoryUnitTransaction);
        List<string> VerifyInventoryUnitTransactionDeleteOK(Int64 ixInventoryUnitTransaction, string sInventoryUnitTransaction);

        Task<Int64> Create(InventoryUnitTransactionsPost inventoryunittransactionsPost);
        Task Edit(InventoryUnitTransactionsPost inventoryunittransactionsPost);
        Task Delete(InventoryUnitTransactionsPost inventoryunittransactionsPost);
    }
}
  

